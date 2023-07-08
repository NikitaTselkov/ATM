using ATM.Models;
using Core;
using Prism.Commands;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.ViewModels.Pages
{
    public class TopUpBalancePageViewModel : ViewModelBase
    {
        private readonly IRegionManager _regionManager;

        #region Commands

        private DelegateCommand _navigateBackCommand;
        public DelegateCommand NavigateBackCommand =>
            _navigateBackCommand ?? (_navigateBackCommand = new DelegateCommand(ExecuteNavigateBackCommand));

        private DelegateCommand _topUpBalanceCommand;
        public DelegateCommand TopUpBalanceCommand =>
            _topUpBalanceCommand ?? (_topUpBalanceCommand = new DelegateCommand(ExecuteTopUpBalanceCommand));

        #endregion

        public decimal Balance => UserAuthorization.GetBalance();

        private long _topUpAmount;
        public long TopUpAmount
        {
            get { return _topUpAmount; }
            set { SetProperty(ref _topUpAmount, value); }
        }

        public TopUpBalancePageViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        private void ExecuteNavigateBackCommand()
        {
            _regionManager.RequestNavigate(RegionNames.MainPage, PageNames.MainPage);
        }

        private void ExecuteTopUpBalanceCommand()
        {
            ATMStateModel.ConvertSumToBanknotes(TopUpAmount); //AddBanknoteForUsers(TopUpAmount);
        }
    }
}
