using LiveHTS.Core.Interfaces.Services;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Presentation.Interfaces;
using LiveHTS.Presentation.Interfaces.ViewModel;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.ViewModel
{
    public class AppDashboardViewModel:MvxViewModel,IAppDashboardViewModel
    {
        private readonly IAppDashboardService _dashboardService;
        private string _profile;
        private IMvxCommand _registryCommand;
        private bool _isBusy;

        public IMvxCommand RegistryCommand
        {
            get
            {
                _registryCommand = _registryCommand ?? new MvxCommand(ShowrRegistryView);
                return _registryCommand;
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


        public AppDashboardViewModel(IAppDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public void Init(string username)
        {
            Profile = username;
        }

        private void ShowrRegistryView()
        {
            ShowViewModel<RegistryViewModel>();
        }
    }
}