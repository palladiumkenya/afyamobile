using System.Windows.Input;
using LiveHTS.Core.Interfaces.Services.Access;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Presentation.Interfaces;
using LiveHTS.Presentation.Interfaces.ViewModel;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.ViewModel
{
    public class SignInViewModel:MvxViewModel, ISignInViewModel
    {
        private readonly IAuthService _authService;
        private readonly IDialogService _dialogService;

        private User _user;
        private string _username;
        private string _password;
        private IMvxCommand _signInCommand;
        private bool _isBusy;

        public User User
        {
            get { return _user; }
        }

        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                RaisePropertyChanged(() => Username);
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                RaisePropertyChanged(() =>Password);
            }
        }

        public IMvxCommand SignInCommand
        {
            get
            {
                _signInCommand = _signInCommand ?? new MvxCommand(SignIn, CanSignIn);
                return _signInCommand;
            }
        }

        public bool IsBusy
        {
            get { return _isBusy; }
        }


        public SignInViewModel(IAuthService authService, IDialogService dialogService)
        {
            _authService = authService;
            _dialogService = dialogService;
        }

        private void SignIn()
        {
            _user = _authService.SignIn(Username, Password);
            if (null!=_user)
            {
                ShowViewModel<AppDashboardViewModel>(new { user = User });
            }
            else
            {
               _dialogService.Alert("We were unable to log you in!", "Login Failed", "OK");
            }
        }

        private bool CanSignIn()
        {
            return !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password);
        }
    }
}