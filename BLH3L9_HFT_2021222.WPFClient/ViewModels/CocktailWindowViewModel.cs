using BLH3L9_HFT_2021222.Models;
using BLH3L9_HFT_2021222.WpfClient;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BLH3L9_HFT_2021222.WPFClient.ViewModels
{
    public class CocktailWindowViewModel : ObservableRecipient
    {
        public RestCollection<Cocktail> Cocktails { get; set; }

        private Cocktail selectedCocktail;

        public Cocktail SelectedCocktail
        {
            get { return selectedCocktail; }
            set
            {
                if (value != null)
                {
                    SetProperty(ref selectedCocktail, value);
                    //selectedCocktail = new Cocktail()
                    //{
                    //    CocktailId = value.CocktailId,
                    //    CocktailName = value.CocktailName
                    //};
                    OnPropertyChanged();
                    (DeleteCocktailCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }
        public ICommand CreateCocktailCommand { get; set; }
        public ICommand DeleteCocktailCommand { get; set; }
        public ICommand UpdateCocktailCommand { get; set; }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }
        public CocktailWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Cocktails = new RestCollection<Cocktail>("http://localhost:61344/", "cocktail", "hub");
                CreateCocktailCommand = new RelayCommand(() =>
                {
                    Cocktails.Add(new Cocktail()
                    {
                        CocktailName = selectedCocktail.CocktailName,
                        Price = selectedCocktail.Price
                    });
                });

                UpdateCocktailCommand = new RelayCommand(() =>
                {
                    Cocktails.Update(selectedCocktail);
                });

                DeleteCocktailCommand = new RelayCommand(() =>
                {
                    Cocktails.Delete(selectedCocktail.CocktailId);
                },
                () =>
                {
                    return selectedCocktail != null;
                });
                SelectedCocktail = new Cocktail();
            }
        }
    }
}
