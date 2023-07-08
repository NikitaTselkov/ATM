﻿using ATM.Models;
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

        public long TotalMoney => ATMStateModel.GetAllMoney();

        public List<CassettesInfo> Cassettes => ATMStateModel.CountAndDenominationOfBanknotes;

        #region Commands

        private DelegateCommand _navigateBackCommand;
        public DelegateCommand NavigateBackCommand =>
            _navigateBackCommand ?? (_navigateBackCommand = new DelegateCommand(ExecuteNavigateBackCommand));

        #endregion

        public CurrentStatePageViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        private void ExecuteNavigateBackCommand()
        {
            _regionManager.RequestNavigate(RegionNames.MainPage, PageNames.MainPage);
        }
    }
}
