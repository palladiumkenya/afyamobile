using Acr.UserDialogs;
using Cheesebaron.MvxPlugins.Settings.Interfaces;
using LiveHTS.Core.Interfaces.Services;
using LiveHTS.Presentation.Interfaces;
using LiveHTS.Presentation.Interfaces.ViewModel;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.ViewModel
{
    public class AppDashboardViewModel:MvxViewModel,IAppDashboardViewModel
    {
        private readonly ISettings _settings;
        private readonly IDialogService _dialogService;
        private readonly IAppDashboardService _dashboardService;
        private string _profile;
        private IMvxCommand _registryCommand;
        private bool _isBusy;
        private IMvxCommand _registerNewClientCommand;
        private IMvxCommand _quitCommand;
        private IMvxCommand _deviceCommand;
        private IMvxCommand _practiceCommand;
        private IMvxCommand _pullCommand;

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

        public IMvxCommand QuitCommand
        {
            get
            {
                _quitCommand = _quitCommand ?? new MvxCommand(Quit);
                return _quitCommand;
            }
        }

        public IMvxCommand DeviceCommand
        {
            get
            {
                _deviceCommand = _deviceCommand ?? new MvxCommand(ShowDevice);
                return _deviceCommand;
            }
        }

        public IMvxCommand PracticeCommand
        {
            get
            {
                _practiceCommand = _practiceCommand ?? new MvxCommand(ShowPractice);
                return _practiceCommand;
            }
        }

        public IMvxCommand PullDataCommand
        {
            get
            {
                _pullCommand = _pullCommand ?? new MvxCommand(PullData);
                return _pullCommand;
            }
        }

      
        public string Profile
        {
            get { return $"Signed in as {_profile}"; }
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


        public AppDashboardViewModel(IAppDashboardService dashboardService, IDialogService dialogService, IUserDialogs userDialogs, ISettings settings)
        {
            _dashboardService = dashboardService;
            _dialogService = dialogService;
            _settings = settings;
        }

        public void Init(string username)
        {
            Profile = username;
        }

        public override void ViewAppeared()
        {
            /*
             _settings.AddOrUpdateValue("livehts.userid", _user.Id.ToString());
                _settings.AddOrUpdateValue("livehts.username", _user.UserName);
                
                _settings.AddOrUpdateValue("livehts.providerid", provider.Id.ToString());
                _settings.AddOrUpdateValue("livehts.providername", provider.Person.FullName);


        
             */
            var profile = _settings.GetValue("livehts.username", "");
            if (!string.IsNullOrWhiteSpace(profile))
            {
                Profile = profile;
            }
        }

        private void ShowRegistry()
        {
            ShowViewModel<RegistryViewModel>();
        }
        private void RegisterNew()
        {
            ShowViewModel<ClientRegistrationViewModel>();
        }
        private void ShowDevice()
        {
            ShowViewModel<DeviceViewModel>();
        }

        private void ShowPractice()
        {
            ShowViewModel<PracticeViewModel>();
        }
        private void PullData()
        {
            ShowViewModel<PullDataViewModel>();
        }


        public void Quit()
        {
          _dialogService.ConfirmExit();
        }
    }
}