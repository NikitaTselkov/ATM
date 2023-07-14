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
        private readonly IRegionManager _regionManager;
        
        private long _totalMoney;
        public long TotalMoney
        {
            get { return _totalMoney; }
            set { SetProperty(ref _totalMoney, value); }
        }

        private List<CassettesInfo> _cassettes;
        public List<CassettesInfo> Cassettes
        {
            get { return _cassettes; }
            set { SetProperty(ref _cassettes, value); }
        }

        #region Commands

        private DelegateCommand _navigateBackCommand;
        public DelegateCommand NavigateBackCommand =>
            _navigateBackCommand ?? (_navigateBackCommand = new DelegateCommand(ExecuteNavigateBackCommand));

        private DelegateCommand _loadedCommand;
        public DelegateCommand LoadedCommand =>
            _loadedCommand ?? (_loadedCommand = new DelegateCommand(ExecuteLoadedCommand));

        #endregion

        public CurrentStatePageViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        private void ExecuteNavigateBackCommand()
        {
            _regionManager.RequestNavigate(RegionNames.MainPage, PageNames.MainPage);
        }
        
        private void ExecuteLoadedCommand()
        {
            TotalMoney = ATMStateModel.GetAllMoney();
            Cassettes = ATMStateModel.GetCountAndDenominationOfBanknotes();
        }
    }
}
