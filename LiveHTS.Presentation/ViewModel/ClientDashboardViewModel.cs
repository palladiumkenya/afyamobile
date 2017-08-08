using System;
using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core.Interfaces.Services.Clients;
using LiveHTS.Core.Interfaces.Services.Interview;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Core.Model.Survey;
using LiveHTS.Presentation.Interfaces;
using LiveHTS.Presentation.Interfaces.ViewModel;
using LiveHTS.Presentation.ViewModel.Template;
using LiveHTS.Presentation.ViewModel.Wrapper;
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
        private Client _seletctedRelationShip;
        private List<RelationshipTemplateWrap> _relationships = new List<RelationshipTemplateWrap>();
        private Module _module;
        private List<FormTemplateWrap> _forms;

        private IMvxCommand _manageRegistrationCommand;
        private IMvxCommand _addRelationShipCommand;
        

        private bool _isBusy;
       
        public Client Client
        {
            get { return _client; }
            set
            {
                _client = value; RaisePropertyChanged(() => Client);
                Relationships = ConvertToWrapperClass(Client.Relationships, this);
            }
        }        
        public Client SeletctedRelationShip
        {
            get { return _seletctedRelationShip; }
            set { _seletctedRelationShip = value; RaisePropertyChanged(() => SeletctedRelationShip); }
        }
        public List<RelationshipTemplateWrap> Relationships
        {
            get { return _relationships; }
            set
            {
                _relationships = value;
                RaisePropertyChanged(() => Relationships);
            }
        }
        public Module Module
        {
            get { return _module; }
            set
            {
                _module = value; RaisePropertyChanged(() => Module);
                var forms = Module.Forms.ToList();
                foreach (var form in forms)
                {
                    form.ClientEncounters = _interviewService.LoadEncounters(Client.Id, form.Id).ToList();
                }
                Forms = ConvertToWrapperClass(forms, this);
            }
        }
        public List<FormTemplateWrap> Forms
        {
            get { return _forms; }
            set { _forms = value;RaisePropertyChanged(() => Forms); }
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

      

        public bool IsBusy
        {
            get { return _isBusy; }
            set { _isBusy = value; RaisePropertyChanged(() => IsBusy); }
        }

        public ClientDashboardViewModel(IDashboardService dashboardService, IDialogService dialogService, IInterviewService interviewService)
        {
            _dashboardService = dashboardService;
            _dialogService = dialogService;
            _interviewService = interviewService;
        }

        private void ManageRegistration()
        {
            ShowViewModel<ClientRegistrationViewModel>(new { id = Client.Id });
        }
        private void AddRelationShip()
        {
            ShowViewModel<ClientRelationshipsViewModel>(new {id = Client.Id});
        }
        public void Init(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return;
            Client = _dashboardService.LoadClient(new Guid(id));
            Module = _dashboardService.LoadModule();
        }
        public void ShowRegistry()
        {
            ShowViewModel<RegistryViewModel>();
        } 
        public async void RemoveRelationship(RelationshipTemplate template)
        {
            try
            {
                var result = await _dialogService.ConfirmAction("Are you sure ?", "Remove Relationship");
                if ( result)
                {
                    _dashboardService.RemoveRelationShip(template.Id);
                }
            }
            catch (Exception e)
            {
                MvxTrace.Error(e.Message);
                _dialogService.Alert(e.Message,"Remove Relationship");
            }
        }
        public void StartEncounter(FormTemplate formTemplate)
        {
            throw new NotImplementedException();
        }
        private static List<RelationshipTemplateWrap> ConvertToWrapperClass(IEnumerable<ClientRelationship> clientRelationships, ClientDashboardViewModel clientDashboardViewModel)
        {
            List<RelationshipTemplateWrap> list = new List<RelationshipTemplateWrap>();
            foreach (var r in clientRelationships)
            {
                list.Add(new RelationshipTemplateWrap(new RelationshipTemplate(r), clientDashboardViewModel));
            }

            return list;
        }

        private static List<FormTemplateWrap> ConvertToWrapperClass(List<Form> forms, ClientDashboardViewModel clientDashboardViewModel)
        {
            List<FormTemplateWrap> list = new List<FormTemplateWrap>();
            foreach (var r in forms)
            {
                list.Add(new FormTemplateWrap(clientDashboardViewModel,new FormTemplate(r)));
            }

            return list;
        }

        private bool CanStartEncounter()
        {
            throw new NotImplementedException();
        }
      

        public void ResumeEncounter(EncounterTemplate encounterTemplate)
        {
            throw new NotImplementedException();
        }

        public void ReviewEncounter(EncounterTemplate encounterTemplate)
        {
            throw new NotImplementedException();
        }

        public void DiscardEncounter(EncounterTemplate encounterTemplate)
        {
            throw new NotImplementedException();
        }
    }
}