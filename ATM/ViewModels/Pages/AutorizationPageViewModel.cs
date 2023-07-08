using ATM.Models;
using Core;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace ATM.ViewModels.Pages
{
    public class AutorizationPageViewModel : ViewModelBase
    {
        private readonly IRegionManager _regionManager;

        #region Commands

        private DelegateCommand _navigateBackCommand;
        public DelegateCommand NavigateBackCommand =>
            _navigateBackCommand ?? (_navigateBackCommand = new DelegateCommand(ExecuteNavigateBackCommand));

        private DelegateCommand<SendPasswordEventArgs> _checkUsersCardAutorization;
        public DelegateCommand<SendPasswordEventArgs> CheckUsersCardAutorization =>
            _checkUsersCardAutorization ?? (_checkUsersCardAutorization = new DelegateCommand<SendPasswordEventArgs>(ExecuteCheckUsersCardAutorization));

        #endregion


        public AutorizationPageViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        private void ExecuteNavigateBackCommand()
        {
            _regionManager.RequestNavigate(RegionNames.MainPage, PageNames.MainPage);
        }

        private void ExecuteCheckUsersCardAutorization(SendPasswordEventArgs args)
        {
            //TODO: Проверка в BD пароля

            UserAuthorization.AutorizationOfUsersCard(1234567891012131, 1243);

            _regionManager.RequestNavigate(RegionNames.MainPage, _nextLink);
        }
    }
}
