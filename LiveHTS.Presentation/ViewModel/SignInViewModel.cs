using System;
using Cheesebaron.MvxPlugins.Settings.Interfaces;
using LiveHTS.Core.Interfaces.Services.Access;
using LiveHTS.Core.Interfaces.Services.Config;
using LiveHTS.Core.Model.Config;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Core.Model.Survey;
using LiveHTS.Core.Service.Config;
using LiveHTS.Presentation.Interfaces;
using LiveHTS.Presentation.Interfaces.ViewModel;
using MvvmCross.Core.ViewModels;
using Newtonsoft.Json;

namespace LiveHTS.Presentation.ViewModel
{
    public class SignInViewModel:MvxViewModel, ISignInViewModel
    {
        private readonly IAuthService _authService;
        private readonly IDialogService _dialogService;
        private readonly ISettings _settings;
        private readonly IDeviceSetupService _deviceSetupService;

        private string _username;
        private string _password;
        private IMvxCommand _signInCommand;
        private bool _isBusy;


        public User User { get; private set; }

        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                RaisePropertyChanged(() => Username);
                SignInCommand.RaiseCanExecuteChanged();
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                RaisePropertyChanged(() =>Password);
                SignInCommand.RaiseCanExecuteChanged();
            }
        }

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                RaisePropertyChanged(() => IsBusy);
            }
        }

        public bool AutoSignIn { get; set; }

        public virtual IMvxCommand SignInCommand
        {
            get
            {
                _signInCommand = _signInCommand ?? new MvxCommand(AttemptSignIn, CanSignIn);
                return _signInCommand;
            }
        }
        
        public SignInViewModel(IAuthService authService, IDialogService dialogService, ISettings settings, IDeviceSetupService deviceSetupService)
        {
            _authService = authService;
            _dialogService = dialogService;
            _settings = settings;
            _deviceSetupService = deviceSetupService;
            IsBusy = false;
            AutoSignIn = true;
            if (AutoSignIn)
            {
                Username = "admin";
                Password = "maun2806";
            }
        }

        public void LoadDeviceInfo(string serial, string name, string manufacturer)
        {
            var practice = _authService.GetDefaultPractice();
            _settings.AddOrUpdateValue("livehts.practiceid", practice.Id.ToString());
            _settings.AddOrUpdateValue("livehts.practicename", practice.Name);

            _deviceSetupService.CheckRegister(new Device(serial, serial, name,practice.Id));
            var device = _authService.GetDefaultDevice();
            _settings.AddOrUpdateValue("livehts.deviceid", device.Id.ToString());

            var provider = _authService.GetDefaultProvider();
            _settings.AddOrUpdateValue("livehts.providerid", provider.Id.ToString());
            _settings.AddOrUpdateValue("livehts.providername", provider.Person.FullName);
        }

        public void UpdateSession()
        {
            throw new NotImplementedException();
        }

        private bool CanSignIn()
        {
            return !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password);
        }
        private void AttemptSignIn()
        {
            IsBusy = true;
            
            try
            {
                User = _authService.SignIn(Username, Password);

                _settings.AddOrUpdateValue("livehts.userid", User.Id.ToString());
                _settings.AddOrUpdateValue("livehts.username", User.UserName);

                ShowViewModel<AppDashboardViewModel>(new { username= User.UserName });
            }
            catch (Exception e)
            {
                _dialogService.Alert($"{e.Message}", "Sign In Failed", "OK");
            }
            
           IsBusy = false;
        }
        public void Quit()
        {
            _dialogService.ConfirmExit();
        }
    }
}