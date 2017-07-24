﻿using System;
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

        public virtual IMvxCommand SignInCommand
        {
            get
            {
                _signInCommand = _signInCommand ?? new MvxCommand(AttemptSignIn, CanSignIn);
                return _signInCommand;
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


        public SignInViewModel(IAuthService authService, IDialogService dialogService)
        {
            _authService = authService;
            _dialogService = dialogService;
            IsBusy = false;

            //TODO:Remove default login
            Username = "admin";
            Password = "maun2806";
        }

        private void AttemptSignIn()
        {
            IsBusy = true;
            
            try
            {
                _user = _authService.SignIn(Username, Password);
                ShowViewModel<AppDashboardViewModel>(new { username= User.UserName });
            }
            catch (Exception e)
            {
                _dialogService.Alert($"{e.Message}", "Sign In Failed", "OK");
            }
            
           IsBusy = false;
        }

        private bool CanSignIn()
        {
            return !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password);
        }
    }
}