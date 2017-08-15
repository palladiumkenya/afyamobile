using System;
using System.Collections.Generic;
using System.Linq;
using Cheesebaron.MvxPlugins.Settings.Interfaces;
using LiveHTS.Core.Interfaces.Services.Clients;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Survey;
using LiveHTS.Presentation.DTO;
using LiveHTS.Presentation.Events;
using LiveHTS.Presentation.Interfaces;
using LiveHTS.Presentation.Interfaces.ViewModel;
using LiveHTS.Presentation.ViewModel.Template;
using LiveHTS.Presentation.ViewModel.Wrapper;
using MvvmCross.Core.ViewModels;
using Newtonsoft.Json;

namespace LiveHTS.Presentation.ViewModel
{
    public class ClientEncounterViewModel : MvxViewModel, IClientEncounterViewModel
    {
        private readonly IDialogService _dialogService;
        private readonly IEncounterService _encounterService;

        private readonly ISettings _settings;
        private ClientEncounterDTO _clientEncounterInfo;
        private Form _form;
        private Encounter _encounter;
        private ClientDTO _clientDTO;
        private List<QuestionTemplateWrap> _questions;
        private IMvxCommand _saveChangesCommand;

        public Guid UserId
        {
            get
            {
                return _settings.GetValue("livehts.userid",Guid.Empty);
            }
        }

        public string UserName
        {
            get
            {
                return _settings.GetValue("livehts.username", "admin");
            }
        }

        public Guid ProviderId
        {
            get
            {
                return _settings.GetValue("livehts.providerid", Guid.Empty);
            }
        }

        public string ProviderName
        {
            get
            {
                return _settings.GetValue("livehts.providername", "");
            }
        }

        public ClientDTO ClientDTO
        {
            get { return _clientDTO; }
            set { _clientDTO = value; ; RaisePropertyChanged(() => ClientDTO); }
        }

        public ClientEncounterDTO ClientEncounterDTO
        {
            get { return _clientEncounterInfo; }
            set { _clientEncounterInfo = value; RaisePropertyChanged(() => ClientEncounterDTO); }
        }

        public Form Form
        {
            get { return _form; }
            set
            {
                _form = value; RaisePropertyChanged(() => Form);
                Questions = ConvertToQuestionWrapperClass(_form.Questions, this);
            }
        }

        public List<QuestionTemplateWrap> Questions
        {
            get { return _questions; }
            set { _questions = value; RaisePropertyChanged( () => Questions); }
        }

        public Encounter Encounter
        {
            get { return _encounter; }
            set { _encounter = value; RaisePropertyChanged(() => Encounter); }
        }

        public event EventHandler<ConceptChangedEvent> ConceptChanged;

        public IMvxCommand SaveChangesCommand
        {
            get
            {
                _saveChangesCommand = _saveChangesCommand ?? new MvxCommand(SaveChanges, CanSaveChanges);
                return _saveChangesCommand;
            }
        }

        public ClientEncounterViewModel(ISettings settings, IDialogService dialogService, IEncounterService encounterService)
        {
            _settings = settings;
            _dialogService = dialogService;
            _encounterService = encounterService;
        }

        public void Init(string formId,string mode, string encounterId)
        {
            if (null == Form)
            {
                Form = _encounterService.LoadForm(new Guid(formId));
                if (null != Form)
                {
                    var formJson = JsonConvert.SerializeObject(Form);
                    _settings.AddOrUpdateValue("client.form", formJson);
                }
            }

            var clientJson = _settings.GetValue("client.dto", "");
            var clientEncounterJson = _settings.GetValue("client.encounter", "");

            if (!string.IsNullOrWhiteSpace(clientJson))
            {
                ClientDTO = JsonConvert.DeserializeObject<ClientDTO>(clientJson);
            }

            if (!string.IsNullOrWhiteSpace(clientEncounterJson))
            {
                ClientEncounterDTO = JsonConvert.DeserializeObject<ClientEncounterDTO>(clientEncounterJson);
            }            

            if (mode == "new")
            {
                //  New Encounter

                Encounter = _encounterService.StartEncounter(ClientEncounterDTO.FormId,
                    ClientEncounterDTO.EncounterTypeId, ClientEncounterDTO.ClientId, ProviderId, UserId);
            }
            else
            {
                //  Load Encounter

                Encounter = _encounterService.LoadEncounter(ClientEncounterDTO.FormId,
                    ClientEncounterDTO.EncounterTypeId, ClientEncounterDTO.ClientId,true);
            }
        }

        public override void ViewAppeared()
        {
            var clientJson = _settings.GetValue("client.dto", "");
            var clientEncounterJson = _settings.GetValue("client.encounter", "");
            var formJson = _settings.GetValue("client.form", "");

            if (!string.IsNullOrWhiteSpace(clientJson))
            {
                ClientDTO = JsonConvert.DeserializeObject<ClientDTO>(clientJson);
            }

            if (!string.IsNullOrWhiteSpace(clientEncounterJson))
            {
                ClientEncounterDTO = JsonConvert.DeserializeObject<ClientEncounterDTO>(clientEncounterJson);
            }

            if (!string.IsNullOrWhiteSpace(formJson))
            {
                Form = JsonConvert.DeserializeObject<Form>(formJson);
            }
        }

        private static List<QuestionTemplateWrap> ConvertToQuestionWrapperClass(List<Question> questions, ClientEncounterViewModel clientDashboardViewModel)
        {
            List<QuestionTemplateWrap> list = new List<QuestionTemplateWrap>();
            foreach (var r in questions)
            {
                list.Add(new QuestionTemplateWrap(clientDashboardViewModel,new QuestionTemplate(r)));
            }
            return list;
        }

        private bool CanSaveChanges()
        {
            return true;
        }

        private void SaveChanges()
        {
            //readResponses

            

            var response= Questions.Last().QuestionTemplate.Response;

            throw new NotImplementedException();
        }
    }
}