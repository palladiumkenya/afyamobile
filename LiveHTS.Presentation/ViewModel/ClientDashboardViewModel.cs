using System;
using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core.Interfaces.Services.Clients;
using LiveHTS.Core.Interfaces.Services.Interview;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Core.Model.Survey;
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
        private readonly IInterviewService _interviewService;
        private Client _client;
        private bool _isBusy;
        private  IMvxCommand _manageRegistrationCommand;
        private  IMvxCommand _addRelationShipCommand;
        private  IMvxCommand _removeRelationShipCommand;
        private Client _seletctedRelationShip;
        private Module _module;
        private IEnumerable<Form> _forms;
        private IMvxCommand _startEncounterCommand;

        public Module Module
        {
            get { return _module; }
            set { _module = value; RaisePropertyChanged(() => Module); }
        }

        public IEnumerable<Form> Forms
        {
            get { return _forms; }
            set { _forms = value;RaisePropertyChanged(() => Forms); }
        }

        public Client Client
        {
            get { return _client; }
            set { _client = value; RaisePropertyChanged(() => Client); }
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

        public IMvxCommand StartEncounterCommand
        {
            get
            {
                _startEncounterCommand = _startEncounterCommand ?? new MvxCommand(StartEncounter, CanStartEncounter);
                return _startEncounterCommand;
            }
        }

        private bool CanStartEncounter()
        {
            throw new NotImplementedException();
        }

        private void StartEncounter()
        {
            throw new NotImplementedException();
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

        public ClientDashboardViewModel(IDashboardService dashboardService, IDialogService dialogService, IInterviewService interviewService)
        {
            _dashboardService = dashboardService;
            _dialogService = dialogService;
            _interviewService = interviewService;
        }

        public void Init(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return;
            Client= _dashboardService.LoadClient(new Guid(id));
            Module = _dashboardService.LoadModule();
            Forms = Module.Forms.ToList();
            foreach (var form in Forms)
            {
                form.ClientEncounters = _interviewService.LoadEncounters(Client.Id, form.Id).ToList();
            }
        }
    }
}