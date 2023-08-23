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
    public class MainWindowViewModel
    {
       public ICommand GoToAlcoholsCommand { get; set; }
       public ICommand GoToBrandsCommand { get; set; }
       public ICommand GoToCocktailsCommand { get; set; }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public MainWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                GoToAlcoholsCommand = new RelayCommand(() =>
                {
                    AlcoholWindow aw = new AlcoholWindow();
                    aw.Show();
                });

                GoToBrandsCommand = new RelayCommand(() =>
                {
                    BrandWindow bw = new BrandWindow();
                    bw.Show();
                });

                GoToCocktailsCommand = new RelayCommand(() =>
                {
                    CocktailWindow cw = new CocktailWindow();
                    cw.Show();
                });
            }           
        }
    }
}
