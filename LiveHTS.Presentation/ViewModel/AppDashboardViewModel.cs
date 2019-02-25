using Acr.UserDialogs;
using Cheesebaron.MvxPlugins.Settings.Interfaces;
using LiveHTS.Core.Interfaces.Services;
using LiveHTS.Core.Interfaces.Services.Config;
using LiveHTS.Presentation.Interfaces;
using LiveHTS.Presentation.Interfaces.ViewModel;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.ViewModel
{
    public class AppDashboardViewModel:MvxViewModel,IAppDashboardViewModel
    {
        private readonly IDeviceSetupService _deviceSetupService;
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
        private  IMvxCommand _pushDataCommand;
        private string _practiceName;
        private IMvxCommand _summaryCommand;
        private IMvxCommand _smartCardCommand;

        public IMvxCommand RegistryCommand
        {
            get
            {
                _registryCommand = _registryCommand ?? new MvxCommand(ShowRegistry,()=>!IsBusy);
                return _registryCommand;
            }
        }

        public IMvxCommand RegisterNewClientCommand
        {
            get
            {
                _registerNewClientCommand = _registerNewClientCommand ?? new MvxCommand(RegisterNew, () => !IsBusy);
                return _registerNewClientCommand;
            }
        }

        public IMvxCommand QuitCommand
        {
            get
            {
                _quitCommand = _quitCommand ?? new MvxCommand(Quit, () => !IsBusy);
                return _quitCommand;
            }
        }

        public IMvxCommand DeviceCommand
        {
            get
            {
                _deviceCommand = _deviceCommand ?? new MvxCommand(ShowDevice, () => !IsBusy);
                return _deviceCommand;
            }
        }

        public IMvxCommand PracticeCommand
        {
            get
            {
                _practiceCommand = _practiceCommand ?? new MvxCommand(ShowPractice, () => !IsBusy);
                return _practiceCommand;
            }
        }

        public IMvxCommand PullDataCommand
        {
            get
            {
                _pullCommand = _pullCommand ?? new MvxCommand(PullData, () => !IsBusy);
                return _pullCommand;
            }
        }

        public IMvxCommand PushDataCommand
        {
            get
            {
                _pushDataCommand = _pushDataCommand ?? new MvxCommand(PushData, () => !IsBusy);
                return _pushDataCommand;
            }
        }

        public IMvxCommand SummaryCommand
        {
            get
            {
                _summaryCommand = _summaryCommand ?? new MvxCommand(Summary, () => !IsBusy);
                return _summaryCommand;
            }
        }

        public IMvxCommand SmartCardCommand
        {
            get
            {
                _smartCardCommand = _smartCardCommand ?? new MvxCommand(SmartCard, () => !IsBusy);
                return _smartCardCommand;
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

        public string PracticeName
        {
            get { return _practiceName; }
            set { _practiceName = value; RaisePropertyChanged(() => PracticeName);}
        }

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value; RaisePropertyChanged(() => IsBusy);
                //ManageStatus();
            }
        }

        public string Greeting => string.IsNullOrWhiteSpace(_profile) ? string.Empty : $"Karibu {_profile}";


        public AppDashboardViewModel(IAppDashboardService dashboardService, IDialogService dialogService, IUserDialogs userDialogs, ISettings settings, IDeviceSetupService deviceSetupService)
        {
            _dashboardService = dashboardService;
            _dialogService = dialogService;
            _settings = settings;
            _deviceSetupService = deviceSetupService;
        }

        public void Init(string username)
        {
            Profile = username;
        }

        public override void ViewAppeared()
        {
            IsBusy = false;
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
            var practicename = _settings.GetValue("livehts.practicename", "");
            if (!string.IsNullOrWhiteSpace(practicename))
            {
                PracticeName = practicename;
            }
        }

        private void ShowRegistry()
        {
            IsBusy = true;
            ShowViewModel<RegistryViewModel>();
        }
        private void RegisterNew()
        {
            IsBusy = true;

            if (_deviceSetupService.HasPulledData())
            {
                ClearCache(_settings);
                IsBusy = false;
                ShowViewModel<ClientRegistrationViewModel>(new { mode = "new" });
            }
            else
            {
                IsBusy = false;
                _dialogService.Alert("Please Pull Data before proceeding !");
            }

            //ShowViewModel<ClientRegistrationViewModel>();
        }
        private void ShowDevice()
        {
            IsBusy = true;
            ShowViewModel<DeviceViewModel>();
        }

        private void ShowPractice()
        {
            IsBusy = true;
            ShowViewModel<PracticeViewModel>();
        }
        private void PullData()
        {
            IsBusy = true;
            ShowViewModel<PullDataViewModel>();
        }
        private void PushData()
        {
            IsBusy = true;
            ShowViewModel<PushDataViewModel>();
        }
        private void Summary()
        {
            IsBusy = true;
            ShowViewModel<UserSummaryViewModel>();
        }
        private void SmartCard()
        {
            IsBusy = true;
            if (_deviceSetupService.HasPulledData())
            {
                IsBusy = false;
                ShowViewModel<SmartCardViewModel>();
            }
            else
            {
                IsBusy = false;
                _dialogService.Alert("Please Pull Data before proceeding !");
            }
        }

        public void Quit()
        {
            IsBusy = true;
            _dialogService.ConfirmExit();
        }

        private void ClearCache(ISettings settings)
        {

            if (settings.Contains(nameof(ClientDemographicViewModel)))
                settings.DeleteValue(nameof(ClientDemographicViewModel));

            if (settings.Contains(nameof(ClientContactViewModel)))
                settings.DeleteValue(nameof(ClientContactViewModel));

            if (settings.Contains(nameof(ClientProfileViewModel)))
                settings.DeleteValue(nameof(ClientProfileViewModel));

            if (settings.Contains(nameof(ClientEnrollmentViewModel)))
                settings.DeleteValue(nameof(ClientEnrollmentViewModel));
        }

        private void ManageStatus()
        {
            if (IsBusy)
            {
                Common.StatusInfo.Show(_dialogService);
            }
            else
            {
                Common.StatusInfo.Close(_dialogService);
            }
        }
    }
}