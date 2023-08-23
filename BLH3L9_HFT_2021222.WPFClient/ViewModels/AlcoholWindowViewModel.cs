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
    public class AlcoholWindowViewModel : ObservableRecipient
    {
        public RestCollection<Alcohol> Alcohols { get; set; }

        private Alcohol selectedAlcohol;

        public Alcohol SelectedAlcohol
        {
            get { return selectedAlcohol; }
            set
            {
                if (value != null)
                {
                    SetProperty(ref selectedAlcohol, value);
                    //selectedAlcohol = new Alcohol()
                    //{
                    //    AlcoholId = value.AlcoholId,
                    //    AlcoholName = value.AlcoholName
                    //};
                    OnPropertyChanged();
                    (DeleteAlcoholCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }

        public ICommand CreateAlcoholCommand { get; set; }
        public ICommand DeleteAlcoholCommand { get; set; }
        public ICommand UpdateAlcoholCommand { get; set; }



        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }
        public AlcoholWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Alcohols = new RestCollection<Alcohol>("http://localhost:61344/", "alcohol", "hub");
                CreateAlcoholCommand = new RelayCommand(() =>
                {
                    Alcohols.Add(new Alcohol()
                    {
                        AlcoholName = selectedAlcohol.AlcoholName,
                        Grain = selectedAlcohol.Grain
                    });
                });

                UpdateAlcoholCommand = new RelayCommand(() =>
                {
                    Alcohols.Update(selectedAlcohol);
                });

                DeleteAlcoholCommand = new RelayCommand(() =>
                {
                    Alcohols.Delete(selectedAlcohol.AlcoholId);
                },
                () =>
                {
                    return selectedAlcohol != null;
                });
                SelectedAlcohol = new Alcohol();
            }
        }
    }
}
