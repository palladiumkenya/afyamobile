using System;
using LiveHTS.Core.Interfaces.Services.Clients;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Presentation.Interfaces;
using LiveHTS.Presentation.Interfaces.ViewModel;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform.Platform;

namespace LiveHTS.Presentation.ViewModel
{
    public class ClientDashboardViewModel:MvxViewModel,IClientDashboardViewModel
    {
        private readonly IDialogService _dialogService;
        private readonly IDashboardService _dashboardService;
        private Client _client;
        private bool _isBusy;
        private  IMvxCommand _manageRegistrationCommand;
        private  IMvxCommand _addRelationShipCommand;
        private  IMvxCommand _removeRelationShipCommand;
        private Client _seletctedRelationShip;

        public Client Client
        {
            get { return _client; }
            set { _client = value;RaisePropertyChanged(() => Client); }
        }

        public IMvxCommand ManageRegistrationCommand
        {
            get
            {
                _manageRegistrationCommand = _manageRegistrationCommand ?? new MvxCommand(ManageRegistration);
                return _manageRegistrationCommand;
            }
        }

        public IMvxCommand AddRelationShipCommand
        {
            get
            {
                _addRelationShipCommand = _addRelationShipCommand ?? new MvxCommand(AddRelationShip);
                return _addRelationShipCommand;
            }
        }

        public IMvxCommand RemoveRelationShipCommand
        {
            get
            {
                _removeRelationShipCommand = _removeRelationShipCommand ?? new MvxCommand(RemoveRelationShip);
                return _removeRelationShipCommand;
            }
        }

        public Client SeletctedRelationShip
        {
            get { return _seletctedRelationShip; }
            set { _seletctedRelationShip = value;RaisePropertyChanged(() => SeletctedRelationShip); }
        }

        private void AddRelationShip()
        {
            ShowViewModel<ClientRelationshipsViewModel>(new {id = Client.Id});
        }
        private void RemoveRelationShip()
        {
            try
            {
                _dialogService.ConfirmAction("Delete", (sender, args) => _dashboardService.RemoveRelationShip(Client.Id));
            }
            catch (Exception e)
            {
                MvxTrace.Error(e.Message);
            }
        }
        private void ManageRegistration()
        {
            ShowViewModel<ClientRegistrationViewModel>(new {id = Client.Id});
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

        public ClientDashboardViewModel(IDashboardService dashboardService, IDialogService dialogService)
        {
            _dashboardService = dashboardService;
            _dialogService = dialogService;
        }

        public void Init(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return;

            Client= _dashboardService.LoadClient(new Guid(id));
        }

    }
}