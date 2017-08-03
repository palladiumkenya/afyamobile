using LiveHTS.Core.Interfaces.Services;
using LiveHTS.Presentation.Interfaces;
using LiveHTS.Presentation.Interfaces.ViewModel;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.ViewModel
{
    public class AppDashboardViewModel:MvxViewModel,IAppDashboardViewModel
    {
        private readonly IDialogService _dialogService;
        private readonly IAppDashboardService _dashboardService;
        private string _profile;
        private IMvxCommand _registryCommand;
        private bool _isBusy;
        private IMvxCommand _registerNewClientCommand;

        public IMvxCommand RegistryCommand
        {
            get
            {
                _registryCommand = _registryCommand ?? new MvxCommand(ShowRegistry);
                return _registryCommand;
            }
        }

        public IMvxCommand RegisterNewClientCommand
        {
            get
            {
                _registerNewClientCommand = _registerNewClientCommand ?? new MvxCommand(RegisterNew);
                return _registerNewClientCommand;
            }
        }

      

        public string Profile
        {
            get { return _profile; }
            set
            {
                _profile = value;
                RaisePropertyChanged(() => Profile);
                RaisePropertyChanged(() => Greeting);
            }
        }

        public bool IsBusy
        {
            get { return _isBusy; }
            set { _isBusy = value; RaisePropertyChanged(() => IsBusy);}
        }

        public string Greeting => string.IsNullOrWhiteSpace(_profile) ? string.Empty : $"Karibu {_profile}";


        public AppDashboardViewModel(IAppDashboardService dashboardService, IDialogService dialogService)
        {
            _dashboardService = dashboardService;
            _dialogService = dialogService;
        }

        public void Init(string username)
        {
            Profile = username;
        }

        private void ShowRegistry()
        {
            ShowViewModel<RegistryViewModel>();
        }
        private void RegisterNew()
        {
            ShowViewModel<ClientRegistrationViewModel>();
        }

        public void Quit()
        {
         _dialogService.ConfirmExit();
        }
    }
}