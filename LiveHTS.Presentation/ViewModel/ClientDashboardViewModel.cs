using System;
using LiveHTS.Core.Interfaces.Services.Clients;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Presentation.Interfaces.ViewModel;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.ViewModel
{
    public class ClientDashboardViewModel:MvxViewModel,IClientDashboardViewModel
    {
        private readonly IDashboardService _dashboardService;
        private Client _client;
        private bool _isBusy;

        public Client Client
        {
            get { return _client; }
            set { _client = value;RaisePropertyChanged(() => Client); }
        }

        public bool IsBusy
        {
            get { return _isBusy; }
            set { _isBusy = value; RaisePropertyChanged(() => IsBusy); }
        }

        public void ShowRegistry()
        {
            ShowViewModel<RegistryViewModel>();
        }

        public ClientDashboardViewModel(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public void Init(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return;

            Client= _dashboardService.LoadClient(new Guid(id));
        }

    }
}