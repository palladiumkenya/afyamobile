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
        private readonly IObsService _obsService;

        private readonly ISettings _settings;
        private ClientEncounterDTO _clientEncounterInfo;
        private Form _form;
        private Encounter _encounter;
        private ClientDTO _clientDTO;
        private List<QuestionTemplateWrap> _questions;
        private IMvxCommand _saveChangesCommand;
        private Manifest _manifest;

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

        public Manifest Manifest
        {
            get { return _manifest; }
            set { _manifest = value; RaisePropertyChanged(() => Manifest); }
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

        public ClientEncounterViewModel(ISettings settings, IDialogService dialogService, IEncounterService encounterService, IObsService obsService)
        {
            _settings = settings;
            _dialogService = dialogService;
            _encounterService = encounterService;
            _obsService = obsService;
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
            var clientEncounterJson = _settings.GetValue("client.encounter.dto", "");

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

                _settings.AddOrUpdateValue("client.form.mode", "new");

                Encounter = _encounterService.StartEncounter(ClientEncounterDTO.FormId,
                    ClientEncounterDTO.EncounterTypeId, ClientEncounterDTO.ClientId, ProviderId, UserId);


            }
            else
            {
                //  Load Encounter

                _settings.AddOrUpdateValue("client.form.mode", "open");

                Encounter = _encounterService.LoadEncounter(ClientEncounterDTO.FormId,
                    ClientEncounterDTO.EncounterTypeId, ClientEncounterDTO.ClientId,true);
            }


            var e = JsonConvert.SerializeObject(Encounter);
            _settings.AddOrUpdateValue("client.encounter", e);

            _obsService.Initialize(Encounter);

            Manifest = _obsService.Manifest;
            var manifestJson = JsonConvert.SerializeObject(Manifest);

            _settings.AddOrUpdateValue("manifest", manifestJson);
        }

        public void ProcessQuestions(string mode)
        {
            var manifestJson = _settings.GetValue("manifest", "");

            if (!string.IsNullOrWhiteSpace(manifestJson))
            {
                Manifest = JsonConvert.DeserializeObject<Manifest>(manifestJson);
            }

            if (null != Manifest)
            {
                var current = _obsService.GetLiveQuestion(Manifest);

                var liveQ = Questions.FirstOrDefault(x => x.QuestionTemplate.Id == current.Id);

                if (null != liveQ)
                {
                    liveQ.QuestionTemplate.Allow = true;
                }
            }

        }

        public override void ViewAppeared()
        {
            var clientJson = _settings.GetValue("client.dto", "");
            var clientEncounterJson = _settings.GetValue("client.encounter.dto", "");
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

            var mode = _settings.GetValue("client.form.mode", "new");

            ProcessQuestions(mode);
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
            return false;
        }

        private void SaveChanges()
        {
            //readResponses

            

            var response= Questions.Last().QuestionTemplate.ResponseText;

            //throw new NotImplementedException();
        }
    }
}