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
            set
            {
                _client = value; RaisePropertyChanged(() => Client);

                PartnerCollection = ConvertToWrapperClass(Client.Relationships, this);
            }
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

    

        public Client SeletctedRelationShip
        {
            get { return _seletctedRelationShip; }
            set { _seletctedRelationShip = value;RaisePropertyChanged(() => SeletctedRelationShip); }
        }

        private void AddRelationShip()
        {
            ShowViewModel<ClientRelationshipsViewModel>(new {id = Client.Id});
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

        public async void RemoveRelationship(PartnerItem item)
        {
            try
            {
                var result = await _dialogService.ConfirmAction("Are you sure ?", "Remove Relationship");
                if ( result)
                {
                    _dashboardService.RemoveRelationShip(item.Id);
                }
            }
            catch (Exception e)
            {
                MvxTrace.Error(e.Message);
                _dialogService.Alert(e.Message,"Remove Relationship");
            }
        }

        private List<PartnerItemWrap> _partnerCollection = new List<PartnerItemWrap>();
        public List<PartnerItemWrap> PartnerCollection
        {
            get { return _partnerCollection; }
            set
            {
                _partnerCollection = value;
                RaisePropertyChanged(() => PartnerCollection);
            }
        }

        private static List<PartnerItemWrap> ConvertToWrapperClass(IEnumerable<ClientRelationship> clientRelationships, ClientDashboardViewModel clientDashboardViewModel)
        {
            List<PartnerItemWrap> list = new List<PartnerItemWrap>();
            foreach (var r in clientRelationships)
            {
                list.Add(new PartnerItemWrap(new PartnerItem(r), clientDashboardViewModel));
            }

            return list;
        }
    }
}