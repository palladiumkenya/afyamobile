using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        
        private ClientDTO _clientDTO;
        private ObservableCollection<QuestionTemplateWrap> _questions=new ObservableCollection<QuestionTemplateWrap>();
        private IMvxCommand _saveChangesCommand;
        
        private string _formError;
        private Encounter _encounter;
        private Manifest _manifest;
        private bool _isLoading;

        public bool IsLoading
        {
            get { return _isLoading; }
            set { _isLoading = value; RaisePropertyChanged(() => IsLoading); }
        }

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

        public string FormError
        {
            get { return _formError; }
            set { _formError = value; RaisePropertyChanged(() => FormError); }
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

        public ObservableCollection<QuestionTemplateWrap> Questions
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

            //Load Form + Question Metadata

            if (null == Form)
            {
                Form = _encounterService.LoadForm(new Guid(formId));
                if (null != Form)
                {
                    var formJson = JsonConvert.SerializeObject(Form);
                    _settings.AddOrUpdateValue("client.form", formJson);
                }
            }

            
            //Load Client and Encounter Type

            var clientJson = _settings.GetValue("client.dto", "");
            if (!string.IsNullOrWhiteSpace(clientJson))
            {
                ClientDTO = JsonConvert.DeserializeObject<ClientDTO>(clientJson);
            }

            var clientEncounterJson = _settings.GetValue("client.encounter.dto", "");
            if (!string.IsNullOrWhiteSpace(clientEncounterJson))
            {
                ClientEncounterDTO = JsonConvert.DeserializeObject<ClientEncounterDTO>(clientEncounterJson);
            }


            //Load or Create Encounter 

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

            if (null == Encounter)
            {
                throw new ArgumentException("Encounter has not been Initialized");
            }

            //Store Encounter 

            var encounterJson = JsonConvert.SerializeObject(Encounter);
            _settings.AddOrUpdateValue("client.encounter", encounterJson);

            
            //Initialize and store Manifest

            _obsService.Initialize(Encounter);
            Manifest = _obsService.Manifest;
            Manifest.UpdateEncounter(Encounter);
            var manifestJson = JsonConvert.SerializeObject(Manifest);
            _settings.AddOrUpdateValue("client.manifest", manifestJson);

            //Load View
            
            LoadView();
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
            LoadView();

        }

        public void LoadView()
        {
            if (null != Manifest)
            {                
                if (Manifest.HasResponses())
                {

                    // Load saved responses

                    var responses = Manifest
                        .ResponseStore
                        .OrderBy(x => x.Question.Rank)
                        .ToList();

                    foreach (var r in responses)
                    {
                        var q = Questions.FirstOrDefault(x => x.QuestionTemplate.Id == r.QuestionId);
                        if (null != q)
                        {

                            // determine if to Allow Response

                            if (!q.QuestionTemplate.Allow)
                                q.QuestionTemplate.Allow = true;

                            
                            // determine if to set Response
                            var existingResponse = q.QuestionTemplate.GetResponse();

                            if (null == existingResponse || string.IsNullOrWhiteSpace(existingResponse.ToString()))
                            {
                                q.QuestionTemplate.SetResponse(r.GetValue().Value);
                            }
                            
                            
                        }
                    }
                }
                else
                {
                    // Load active Questsion

                    var activeQuestion = _obsService.GetLiveQuestion(Manifest);

                    var liveQuestion = Questions.FirstOrDefault(x => x.QuestionTemplate.Id == activeQuestion.Id);

                    if (null != liveQuestion)
                    {
                        if (!liveQuestion.QuestionTemplate.Allow)
                            liveQuestion.QuestionTemplate.Allow = true;
                    }
                }
            }
        }
        public bool ValidateResponse(QuestionTemplate questionTemplate)
        {
            bool validate = false;

            try
            {
                _obsService.ValidateResponse(Encounter.Id, questionTemplate.Id, questionTemplate.GetResponse());
                validate = true;
                questionTemplate.ErrorSummary = string.Empty;
            }
            catch (NullReferenceException ex)
            {

            }
            catch (Exception e)
            {
                questionTemplate.ErrorSummary = e.Message;
            }

            return validate;
        }
        public void AllowNextQuestion(QuestionTemplate questionTemplate)
        {
            AllowAllInLine(questionTemplate);
            SaveChangesCommand.RaiseCanExecuteChanged();
        }
        private void AllowAllInLine(QuestionTemplate questionTemplate)
        {

            // validate Response

            bool isResponseValid = ValidateResponse(questionTemplate);


            if (isResponseValid)
            {
                // create Response

                var question = Manifest.GetQuestion(questionTemplate.Id);
                var liveResponse = new Response(Encounter.Id);
                liveResponse.SetQuestion(question);
                liveResponse.SetObs(Encounter.Id, questionTemplate.Id, question.Concept.ConceptTypeId,
                    questionTemplate.GetResponse());

                //update encounter with Response

                Encounter.AddOrUpdate(liveResponse.Obs);

                //update manifest from Encounter

                Manifest.UpdateEncounter(Encounter);

                //temp store serialized Manifest + Encounter

                var encounterJson = JsonConvert.SerializeObject(Encounter);
                _settings.AddOrUpdateValue("client.encounter", encounterJson);

                var manifestJson = JsonConvert.SerializeObject(Manifest);
                _settings.AddOrUpdateValue("client.manifest", manifestJson);



                //determine next Live Question

                if (null != Manifest)
                {
                    var liveSkipQs = new List<QuestionTemplateWrap>();

                    // get all remaining Questions

                    var nextQuestions = _obsService.GetLiveQuestions(Manifest, questionTemplate.Id);

                    if (null == nextQuestions||nextQuestions.Count==0)
                        return;

                    // process remaining Questions

                    foreach (var nextQ in nextQuestions)
                    {
                        // get all Questions to be skipped

                        var skipQs = nextQ.SkippedQuestionIds;

                        var liveQ = Questions.FirstOrDefault(x => x.QuestionTemplate.Id == nextQ.Id);

                        if (skipQs.Count > 0)
                            liveSkipQs = Questions.Where(x => skipQs.Contains(x.QuestionTemplate.Id)).ToList();

                        if (null != liveQ)
                        {
                            foreach (var skipQ in liveSkipQs)
                            {
                                // disable skipped Question

                                if(skipQ.QuestionTemplate.Allow)
                                    skipQ.QuestionTemplate.Allow = false;
                            }

                            // enable current nextQuestion

                            if (!liveQ.QuestionTemplate.Allow)
                            {
                                liveQ.QuestionTemplate.Allow = true;
                            }

                            // restore response from Manifest

                            var response = Manifest.GetResponse(liveQ.QuestionTemplate.Id);

                            var responseValue = null == response ? null : response.GetValue().Value;
                            if (null != responseValue)
                                liveQ.QuestionTemplate.SetResponse(responseValue);
                        }
                    }
                    
                }
            }
        }
        
        private static ObservableCollection<QuestionTemplateWrap> ConvertToQuestionWrapperClass(List<Question> questions, IClientEncounterViewModel clientDashboardViewModel)
        {
            ObservableCollection<QuestionTemplateWrap> list = new ObservableCollection<QuestionTemplateWrap>();
            foreach (var r in questions)
            {
                list.Add(new QuestionTemplateWrap(clientDashboardViewModel,new QuestionTemplate(r)));
            }
            return list;
        }
        private bool CanSaveChanges()
        {
            if (null != Manifest)
                return string.IsNullOrWhiteSpace(FormError)&& Manifest.IsComplete();
            return false;
        }
        private void SaveChanges()
        {

            //TODO : Save Enconter + Obs

            //readResponses

            var allowedQuestions = Questions.Where(x => x.QuestionTemplate.Allow).ToList();

            if (allowedQuestions.Count > 0)
            {
                foreach (var q in allowedQuestions)
                {
                    if (!ValidateResponse(q.QuestionTemplate))
                        return;
                }
            }

            foreach (var q in allowedQuestions)
            {
                _obsService.SaveResponse(Encounter.Id, q.QuestionTemplate.Id, q.QuestionTemplate.GetResponse());
                Manifest = _obsService.Manifest;
            }

            Encounter = Manifest.Encounter;
            var encounterJson = JsonConvert.SerializeObject(Encounter);
            _settings.AddOrUpdateValue("client.encounter", encounterJson);

            var manifestJson = JsonConvert.SerializeObject(Manifest);
            _settings.AddOrUpdateValue("client.manifest", manifestJson);
        }
    }
}