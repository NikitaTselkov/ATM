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
        private List<CassettesInfo> _banknotes;

        #region Commands

        private DelegateCommand _navigateBackCommand;
        public DelegateCommand NavigateBackCommand =>
            _navigateBackCommand ?? (_navigateBackCommand = new DelegateCommand(ExecuteNavigateBackCommand));

        private DelegateCommand _topUpBalanceCommand;
        public DelegateCommand TopUpBalanceCommand =>
            _topUpBalanceCommand ?? (_topUpBalanceCommand = new DelegateCommand(ExecuteTopUpBalanceCommand));

        #endregion

        private decimal _balance = UserAuthorization.GetBalance();
        public decimal Balance
        {
            get { return _balance; }
            set { SetProperty(ref _balance, value); }
        }

        private long _topUpAmount;
        public long TopUpAmount
        {
            get { return _topUpAmount; }
            set { SetProperty(ref _topUpAmount, value); }
        }

        public TopUpBalancePageViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            _banknotes = ATMStateModel.GetBanknotesFromUser();
            TopUpAmount = _banknotes.Sum(s => s.Denomination * s.CountOfBanknotes);
        }

        private void ExecuteNavigateBackCommand()
        {
            _regionManager.RequestNavigate(RegionNames.MainPage, PageNames.MainPage);
        }

        private void ExecuteTopUpBalanceCommand()
        {
            foreach (var banknote in _banknotes)
            {
                for (int i = 0; i < banknote.CountOfBanknotes; i++)
                {
                    ATMStateModel.AddBanknoteForUsers(new Banknote(banknote.Denomination));
                }
            }

            Balance = UserAuthorization.GetBalance();
        }
    }
}
