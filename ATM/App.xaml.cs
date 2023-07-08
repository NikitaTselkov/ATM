using ATM.ViewModels.Pages;
using ATM.Views;
using ATM.Views.Pages;
using Core;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using System.Windows;

namespace ATM
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void ConfigureViewModelLocator()
        {
            ViewModelLocationProvider.Register<MainPage, MainPageViewModel>();

            base.ConfigureViewModelLocator();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MainWindow, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();

            containerRegistry.RegisterSingleton<IApplicationCommands, ApplicationCommands>();
        }
    }
}
