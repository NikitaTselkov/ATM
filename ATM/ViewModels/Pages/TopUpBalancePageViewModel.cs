using ATM.DataBase;
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

        private DelegateCommand _loadedCommand;
        public DelegateCommand LoadedCommand =>
            _loadedCommand ?? (_loadedCommand = new DelegateCommand(ExecuteLoadedCommand));

        private DelegateCommand _topUpATMCommand;
        public DelegateCommand TopUpATMCommand =>
            _topUpATMCommand ?? (_topUpATMCommand = new DelegateCommand(ExecuteTopUpATMCommand));

        #endregion

        private long _balance = UserAuthorization.GetBalance();
        public long Balance
        {
            get { return _balance; }
            set { SetProperty(ref _balance, value); }
        }

        private long _aTMBalance;
        public long ATMBalance
        {
            get { return _aTMBalance; }
            set { SetProperty(ref _aTMBalance, value); }
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

        private void ExecuteLoadedCommand()
        {
            Balance = UserAuthorization.GetBalance();
            ATMBalance = ATMStateModel.GetAllMoney();
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

        private void ExecuteTopUpATMCommand()
        {
            foreach (var banknote in _banknotes)
            {
                for (int i = 0; i < banknote.CountOfBanknotes; i++)
                {
                    ATMStateModel.AddBanknote(new Banknote(banknote.Denomination));
                }

                DataBaseControl.EditCassettes(banknote);
            }

            ATMBalance = ATMStateModel.GetAllMoney();
        }
    }
}
