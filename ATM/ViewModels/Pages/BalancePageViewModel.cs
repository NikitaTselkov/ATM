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
    public class BalancePageViewModel : ViewModelBase
    {
        private readonly IRegionManager _regionManager;

        #region Commands

        private DelegateCommand _navigateBackCommand;
        public DelegateCommand NavigateBackCommand =>
            _navigateBackCommand ?? (_navigateBackCommand = new DelegateCommand(ExecuteNavigateBackCommand));

        #endregion

        public double Balance => UserAuthorization.GetBalance();

        public BalancePageViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        private void ExecuteNavigateBackCommand()
        {
            _regionManager.RequestNavigate(RegionNames.MainPage, PageNames.MainPage);
        }
    }
}
