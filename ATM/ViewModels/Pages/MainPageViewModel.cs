using Core;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.ViewModels.Pages
{
    public class MainPageViewModel : ViewModelBase
    {
        private DelegateCommand<string> _navigateToBalancePageCommand;
        private readonly IRegionManager _regionManager;

        public DelegateCommand<string> NavigateToBalancePageCommand =>
            _navigateToBalancePageCommand ?? (_navigateToBalancePageCommand = new DelegateCommand<string>(ExecuteNavigateToBalancePageCommand));


        public MainPageViewModel(IRegionManager regionManager, IApplicationCommands applicationCommands)
        {
            _regionManager = regionManager;
            applicationCommands.NavigateCommand.RegisterCommand(NavigateToBalancePageCommand);
        }


        private void ExecuteNavigateToBalancePageCommand(string navigationPath)
        {
            if (string.IsNullOrEmpty(navigationPath))
                throw new ArgumentNullException();

            _regionManager.RequestNavigate(RegionNames.MainPage, new Uri(navigationPath, UriKind.Relative));
        }
    }
}
