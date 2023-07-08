using ATM.Models;
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
        private readonly IRegionManager _regionManager;

        #region Commands

        private DelegateCommand<string> _navigateCommand;
        public DelegateCommand<string> NavigateCommand =>
            _navigateCommand ?? (_navigateCommand = new DelegateCommand<string>(ExecuteNavigateCommand));

        #endregion

        public MainPageViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        private void ExecuteNavigateCommand(string navigationPath)
        {
            if (string.IsNullOrEmpty(navigationPath))
                throw new ArgumentNullException();

            var param = new NavigationParameters();

            if (UserAuthorization.IsUsersCardAuthorized())
            {
                _regionManager.RequestNavigate(RegionNames.MainPage, navigationPath);
            }
            else
            {
                param.Add("nextLink", navigationPath);
                _regionManager.RequestNavigate(RegionNames.MainPage, PageNames.AutorizationPage, param);
            }
        }
    }
}
