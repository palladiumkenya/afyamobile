using System;
using System.Collections.Generic;
using System.Linq;
using Cheesebaron.MvxPlugins.Settings.Interfaces;
using LiveHTS.Core.Interfaces.Services.Clients;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Lookup;
using LiveHTS.Core.Model.Survey;
using LiveHTS.Presentation.DTO;
using LiveHTS.Presentation.Events;
using LiveHTS.Presentation.Interfaces;
using LiveHTS.Presentation.Interfaces.ViewModel;
using LiveHTS.Presentation.ViewModel.Template;
using LiveHTS.Presentation.ViewModel.Wrapper;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform.Platform;
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
            set { _encounter = value; }
        }

        public Manifest Manifest
        {
            get { return _manifest; }
            set { _manifest = value; }
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
            Manifest.UpdateEncounter(Encounter);
            var manifestJson = JsonConvert.SerializeObject(Manifest);
            _settings.AddOrUpdateValue("client.manifest", manifestJson);

            Refresh();
        }

        public void Refresh()
        {
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

        public void AllowNext(QuestionTemplate questionTemplate)
        {
            AllowNextMultiple(questionTemplate);
            return;
            bool validate = false;

            //validate
            try
            {
                _obsService.ValidateResponse(Encounter.Id, questionTemplate.Id, questionTemplate.GetResponse());
                validate = true;
                questionTemplate.ErrorSummary = string.Empty;
            }
            catch (Exception e)
            {
                questionTemplate.ErrorSummary = e.Message;
            }

            if (validate)
            {
                //update encounter

                var liveResponse = new Response(Encounter.Id);

                var question = _manifest.GetQuestion(questionTemplate.Id);
                liveResponse.SetQuestion(question);
                liveResponse.SetObs(Encounter.Id, questionTemplate.Id, question.Concept.ConceptTypeId,
                    questionTemplate.GetResponse());

                Encounter.AddOrUpdate(liveResponse.Obs);

                //update manifest

                Manifest.UpdateEncounter(Encounter);

                //save

                var encounterJson = JsonConvert.SerializeObject(Encounter);
                _settings.AddOrUpdateValue("client.encounter", encounterJson);

                var manifestJson = JsonConvert.SerializeObject(Manifest);
                _settings.AddOrUpdateValue("client.manifest", manifestJson);



                if (null != Manifest)
                {
                    var liveSkipQs = new List<QuestionTemplateWrap>();

                    var nextQ = _obsService.GetLiveQuestion(Manifest, questionTemplate.Id);

                    if (null == nextQ)
                        return;

                    var skipQs = nextQ.SkippedQuestionIds;

                    var liveQ = Questions.FirstOrDefault(x => x.QuestionTemplate.Id == nextQ.Id);

                    if (skipQs.Count > 0)
                        liveSkipQs = Questions.Where(x => skipQs.Contains(x.QuestionTemplate.Id)).ToList();

                    if (null != liveQ)
                    {
                        foreach (var skipQ in liveSkipQs)
                        {
                            skipQ.QuestionTemplate.Allow = false;
                        }

                        if (!liveQ.QuestionTemplate.Allow)
                            liveQ.QuestionTemplate.Allow = true;

                        //set nextQ value

                        var response = Manifest.GetResponse(liveQ.QuestionTemplate.Id);

                        var responseValue = null == response ? null : response.GetValue().Value;
                        liveQ.QuestionTemplate.SetResponse(responseValue);


                    }
                }
            }
        }

        public void AllowNextMultiple(QuestionTemplate questionTemplate)
        {
            bool validate = false;

            //validate
            try
            {
                _obsService.ValidateResponse(Encounter.Id, questionTemplate.Id, questionTemplate.GetResponse());
                validate = true;
                questionTemplate.ErrorSummary = string.Empty;
            }
            catch (Exception e)
            {
                questionTemplate.ErrorSummary = e.Message;
            }

            if (validate)
            {
                //update encounter

                var liveResponse = new Response(Encounter.Id);

                var question = _manifest.GetQuestion(questionTemplate.Id);
                liveResponse.SetQuestion(question);
                liveResponse.SetObs(Encounter.Id, questionTemplate.Id, question.Concept.ConceptTypeId,
                    questionTemplate.GetResponse());

                Encounter.AddOrUpdate(liveResponse.Obs);

                //update manifest

                Manifest.UpdateEncounter(Encounter);

                //save

                var encounterJson = JsonConvert.SerializeObject(Encounter);
                _settings.AddOrUpdateValue("client.encounter", encounterJson);

                var manifestJson = JsonConvert.SerializeObject(Manifest);
                _settings.AddOrUpdateValue("client.manifest", manifestJson);



                if (null != Manifest)
                {
                    var liveSkipQs = new List<QuestionTemplateWrap>();

                    var nextQs = _obsService.GetLiveQuestions(Manifest, questionTemplate.Id);

                    if (null == nextQs||nextQs.Count==0)
                        return;
                                
                    foreach (var nextQ in nextQs)
                    {
                        var skipQs = nextQ.SkippedQuestionIds;

                        var liveQ = Questions.FirstOrDefault(x => x.QuestionTemplate.Id == nextQ.Id);

                        if (skipQs.Count > 0)
                            liveSkipQs = Questions.Where(x => skipQs.Contains(x.QuestionTemplate.Id)).ToList();

                        if (null != liveQ)
                        {
                            foreach (var skipQ in liveSkipQs)
                            {
                                skipQ.QuestionTemplate.Allow = false;
                            }

                            if (!liveQ.QuestionTemplate.Allow)
                                liveQ.QuestionTemplate.Allow = true;

                            //set nextQ value

                            var response = Manifest.GetResponse(liveQ.QuestionTemplate.Id);

                            var responseValue = null == response ? null : response.GetValue().Value;
                            if (null != responseValue)
                                liveQ.QuestionTemplate.SetResponse(responseValue);
                        }
                    }
                    
                }
            }
        }

        public override void ViewAppeared()
        {
            var clientJson = _settings.GetValue("client.dto", "");
            var clientEncounterDTOJson = _settings.GetValue("client.encounter.dto", "");
            var formJson = _settings.GetValue("client.form", "");
            var clientEncounterJson = _settings.GetValue("client.encounter", "");
            var clientManifestJson = _settings.GetValue("client.manifest", "");

            if (null == ClientDTO && !string.IsNullOrWhiteSpace(clientJson))
            {
                ClientDTO = JsonConvert.DeserializeObject<ClientDTO>(clientJson);
            }

            if (null == ClientEncounterDTO && !string.IsNullOrWhiteSpace(clientEncounterDTOJson))
            {
                ClientEncounterDTO = JsonConvert.DeserializeObject<ClientEncounterDTO>(clientEncounterDTOJson);
            }

            if (null == Form && !string.IsNullOrWhiteSpace(formJson))
            {
                Form = JsonConvert.DeserializeObject<Form>(formJson);
            }

            if (!string.IsNullOrWhiteSpace(clientEncounterJson))
            {
                Encounter = JsonConvert.DeserializeObject<Encounter>(clientEncounterJson);
            }

            if (!string.IsNullOrWhiteSpace(clientManifestJson))
            {
                Manifest = JsonConvert.DeserializeObject<Manifest>(clientManifestJson);
            }

            Manifest.UpdateEncounter(Encounter);
            Refresh();
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