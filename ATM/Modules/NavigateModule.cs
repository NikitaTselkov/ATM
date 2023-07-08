using ATM.ViewModels.Pages;
using ATM.Views.Pages;
using Core;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Modules
{
    public class NavigateModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var region = containerProvider.Resolve<IRegionManager>();

            region.RegisterViewWithRegion(RegionNames.MainPage, typeof(MainPage));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MainPage>();
            containerRegistry.RegisterForNavigation<BalancePage>();
            containerRegistry.RegisterForNavigation<CurrentStatePage>();
            containerRegistry.RegisterForNavigation<TopUpBalancePage>();
            containerRegistry.RegisterForNavigation<AutorizationPage>();
        }
    }
}
