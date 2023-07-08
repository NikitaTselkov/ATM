using ATM.Views.Pages;
using Core;
using Prism.Regions;
using System.Windows;

namespace ATM.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(IRegionManager regionManager)
        {
            InitializeComponent();

            regionManager.RegisterViewWithRegion(RegionNames.MainPage, typeof(MainPage));
        }
    }
}
