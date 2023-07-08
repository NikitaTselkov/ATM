using ATM.Models;
using Core;
using Prism.Commands;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.ViewModels.Pages
{
    public class CurrentStatePageViewModel : ViewModelBase
    {
        public class BanknoteStateBindingHelper
        {
            public int Denomination { get; set; }
            public int CountOfBanknotes { get; set; }
        }

        private readonly IRegionManager _regionManager;

        public long TotalMoney => ATMStateModel.GetAllMoney();

        private List<BanknoteStateBindingHelper> _cassettes;
        public List<BanknoteStateBindingHelper> Cassettes
        {
            get { return _cassettes; }
            set { SetProperty(ref _cassettes, value); }
        }

        #region Commands

        private DelegateCommand _navigateBackCommand;
        public DelegateCommand NavigateBackCommand =>
            _navigateBackCommand ?? (_navigateBackCommand = new DelegateCommand(ExecuteNavigateBackCommand));

        #endregion

        public CurrentStatePageViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;

            SetCountAndDenominationOfBanknotes();
        }

        private void SetCountAndDenominationOfBanknotes()
        {
            Cassettes = new List<BanknoteStateBindingHelper>();

            foreach (var cassette in ATMStateModel.Cassettes)
            {
                Cassettes.Add(new BanknoteStateBindingHelper { Denomination = cassette.Banknotes.Peek().Denomination, CountOfBanknotes = cassette.CountOfBanknotes });
            }
        }

        private void ExecuteNavigateBackCommand()
        {
            _regionManager.RequestNavigate(RegionNames.MainPage, PageNames.MainPage);
        }
    }
}
