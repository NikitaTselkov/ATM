using ATM.Models;
using Core;
using Prism.Commands;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ATM.ViewModels.Pages
{
    public class WithdrawMoneyPageViewModel : ViewModelBase
    {
        private readonly IRegionManager _regionManager;

        #region Commands

        private DelegateCommand _navigateBackCommand;
        public DelegateCommand NavigateBackCommand =>
            _navigateBackCommand ?? (_navigateBackCommand = new DelegateCommand(ExecuteNavigateBackCommand));

        private DelegateCommand _withdrawMoneyCommand;
        public DelegateCommand WithdrawMoneyCommand =>
            _withdrawMoneyCommand ?? (_withdrawMoneyCommand = new DelegateCommand(ExecuteWithdrawMoneyCommand));

        private DelegateCommand _loadedCommand;
        public DelegateCommand LoadedCommand =>
            _loadedCommand ?? (_loadedCommand = new DelegateCommand(ExecuteLoadedCommand));

        #endregion

        private long _balance = UserAuthorization.GetBalance();
        public long Balance
        {
            get { return _balance; }
            set { SetProperty(ref _balance, value); }
        }

        private long _withdrawalAmount;
        public long WithdrawalAmount
        {
            get { return _withdrawalAmount; }
            set { SetProperty(ref _withdrawalAmount, value); }
        }

        private List<DenominationsBindingHelper> _denominations;
        public List<DenominationsBindingHelper> Denominations
        {
            get { return _denominations; }
            set { SetProperty(ref _denominations, value); }
        }

        public WithdrawMoneyPageViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;

            InitDenominations();
        }

        private void ExecuteNavigateBackCommand()
        {
            _regionManager.RequestNavigate(RegionNames.MainPage, PageNames.MainPage);
        }

        private void ExecuteLoadedCommand()
        {
            Balance = UserAuthorization.GetBalance();
        }

        private void ExecuteWithdrawMoneyCommand()
        {
            try
            {
                if (WithdrawalAmount > Balance)
                    throw new ArgumentOutOfRangeException();

                var selectedDenomination = Denominations.FirstOrDefault(s => s.IsChecked)?.Denomination;

                if (selectedDenomination is null)
                    selectedDenomination = 0;

                if (selectedDenomination.Value > WithdrawalAmount)
                    return;

                if (selectedDenomination == 0)
                {
                    var banknotes = ATMStateModel.ConvertSumToBanknotes(WithdrawalAmount);
                    banknotes.ForEach(f => ATMStateModel.RemoveBanknoteByDenomination(f.Denomination));
                }
                else if (ATMStateModel.IsDenominationsExist(selectedDenomination.Value))
                {
                    var withdrawalAmount = WithdrawalAmount;
                    while (withdrawalAmount >= 50)
                    {
                        ATMStateModel.RemoveBanknoteByDenomination(selectedDenomination.Value);
                        withdrawalAmount -= selectedDenomination.Value;
                    }
                }
                else
                    throw new Exception($"There are no banknotes with denominations of {selectedDenomination.Value}");

                Balance = UserAuthorization.GetBalance();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void InitDenominations()
        {
            Denominations = new List<DenominationsBindingHelper>();

            Denominations.Add(new DenominationsBindingHelper(50, false));
            Denominations.Add(new DenominationsBindingHelper(100, false));
            Denominations.Add(new DenominationsBindingHelper(500, false));
            Denominations.Add(new DenominationsBindingHelper(1000, false));
            Denominations.Add(new DenominationsBindingHelper(2000, false));
            Denominations.Add(new DenominationsBindingHelper(5000, false));
        }

        public class DenominationsBindingHelper
        {
            public int Denomination { get; set; }
            public bool IsChecked { get; set; }

            public DenominationsBindingHelper(int denomination, bool isChecked)
            {
                Denomination = denomination;
                IsChecked = isChecked;
            }
        }
    }
}
