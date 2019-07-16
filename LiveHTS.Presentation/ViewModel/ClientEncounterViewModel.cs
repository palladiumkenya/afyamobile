using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using LiveHTS.SharedKernel.Custom;
using LiveHTS.SharedKernel.Model;
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

        private ClientDTO _clientDTO;

        private ObservableCollection<QuestionTemplateWrap> _questions =
            new ObservableCollection<QuestionTemplateWrap>();

        private IMvxCommand _saveChangesCommand;

        private string _formError;
        private Encounter _encounter;
        private Manifest _manifest;
        private bool _isLoading;
        private string _formStatus;
        private DateTime _birthDate;
        private IMvxCommand _showDateDialogCommand;
        private TraceDateDTO _selectedDate;
        private CustomItem _selectedVisitType;
        private List<CustomItem> _visitTypes = new List<CustomItem>();

        public Guid AppUserId
        {
            get { return GetGuid("livehts.userid"); }
        }

        public Guid AppProviderId
        {
            get { return GetGuid("livehts.providerid"); }
        }

        public Guid AppPracticeId
        {
            get { return GetGuid("livehts.practiceid"); }
        }

        public Guid AppDeviceId
        {
            get { return GetGuid("livehts.deviceid"); }
        }

        public bool AtTheEnd { get; set; }

        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                RaisePropertyChanged(() => IsLoading);
            }
        }

        public Guid UserId
        {
            get { return _settings.GetValue("livehts.userid", Guid.Empty); }
        }

        public string UserName
        {
            get { return _settings.GetValue("livehts.username", "admin"); }
        }

        public Guid ProviderId
        {
            get { return _settings.GetValue("livehts.providerid", Guid.Empty); }
        }

        public string ProviderName
        {
            get { return _settings.GetValue("livehts.providername", ""); }
        }

        public string FormError
        {
            get { return _formError; }
            set
            {
                _formError = value;
                RaisePropertyChanged(() => FormError);
            }
        }

        public string FormStatus
        {
            get { return _formStatus; }
            set
            {
                _formStatus = value;
                RaisePropertyChanged(() => FormStatus);
            }
        }

        public ClientDTO ClientDTO
        {
            get { return _clientDTO; }
            set
            {
                _clientDTO = value;
                RaisePropertyChanged(() => ClientDTO);
            }
        }

        public ClientEncounterDTO ClientEncounterDTO
        {
            get { return _clientEncounterInfo; }
            set
            {
                _clientEncounterInfo = value;
                RaisePropertyChanged(() => ClientEncounterDTO);
            }
        }

        public Form Form
        {
            get { return _form; }
            set
            {
                _form = value;
                RaisePropertyChanged(() => Form);
                Questions = ConvertToQuestionWrapperClass(_form.Questions, this);
            }
        }

        public ObservableCollection<QuestionTemplateWrap> Questions
        {
            get { return _questions; }
            set
            {
                _questions = value;
                RaisePropertyChanged(() => Questions);
            }
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

        public IMvxCommand ShowDateDialogCommand
        {
            get
            {
                _showDateDialogCommand = _showDateDialogCommand ?? new MvxCommand(ShowDateDialog);
                return _showDateDialogCommand;
            }
        }

        public event EventHandler<ChangedDateEvent> ChangedDate;

        public TraceDateDTO SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                _selectedDate = value;
                RaisePropertyChanged(() => SelectedDate);
                UpdatePromiseDate(SelectedDate);
            }
        }

        private void ShowDateDialog()
        {

            ShowDatePicker(Guid.Empty, BirthDate);
        }

        private void UpdatePromiseDate(TraceDateDTO selectedDate)
        {
            BirthDate = selectedDate.EventDate;
        }

        public void ShowDatePicker(Guid refId, DateTime refDate)
        {
            OnChangedDate(new ChangedDateEvent(refId, refDate));
        }

        protected virtual void OnChangedDate(ChangedDateEvent e)
        {
            ChangedDate?.Invoke(this, e);
        }

        public DateTime BirthDate
        {
            get { return _birthDate; }
            set
            {
                _birthDate = value;
                RaisePropertyChanged(() => BirthDate);
            }
        }

        public List<CustomItem> VisitTypes
        {
            get { return _visitTypes; }
            set
            {
                _visitTypes = value;
                RaisePropertyChanged(() => VisitTypes);
            }
        }

        public CustomItem SelectedVisitType
        {
            get { return _selectedVisitType; }
            set
            {
                _selectedVisitType = value;
                RaisePropertyChanged(() => SelectedVisitType);
            }
        }

        public ClientEncounterViewModel(ISettings settings, IDialogService dialogService,
            IEncounterService encounterService, IObsService obsService)
        {
            VisitTypes = CustomLists.VisitTypeList;





            _settings = settings;
            _dialogService = dialogService;
            _encounterService = encounterService;
            _obsService = obsService;
            BirthDate = DateTime.Today;

            SelectedVisitType = VisitTypes.First();
        }

        public void Init(string formId, string encounterTypeId, string mode, string encounterId, string repmode)
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

                // TODO: Partner Obs
                if (ClientDTO.HasPartners)
                {
                    var p = ClientDTO.Partners.First();
                    //b25fd62e-852f-11e7-bb31-be2e44b06b34
                    var pObst = _obsService.GetObs(p, new Guid("b2605964-852f-11e7-bb31-be2e44b06b34"));
                    if (null != pObst && pObst.Count > 0)
                    {
                        _settings.AddOrUpdateValue("client.partner.result",
                            pObst.FirstOrDefault().ValueCoded.ToString());
                    }
                    else
                    {
                        _settings.AddOrUpdateValue("client.partner.result", "");
                    }
                }

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
                var visitType = repmode == "1" ? VisitType.Repeat : VisitType.Initial;
                Encounter = _encounterService.StartEncounter(ClientEncounterDTO.FormId,
                    ClientEncounterDTO.EncounterTypeId, ClientEncounterDTO.ClientId, AppProviderId, AppUserId,
                    AppPracticeId, AppDeviceId, null, visitType);


            }
            else
            {
                //  Load Encounter
                _settings.AddOrUpdateValue("client.form.mode", "open");
                Encounter = _encounterService.LoadEncounter(ClientEncounterDTO.FormId,
                    ClientEncounterDTO.EncounterTypeId, ClientEncounterDTO.ClientId, true);
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

            AtTheEnd = false;
            LoadView();
        }

        public void LoadView()
        {
            //set defaults

            if (null != Manifest)
            {
                BirthDate = Manifest.Encounter.EncounterDate;
                SelectedVisitType = SetVisitType(Manifest.Encounter.VisitType);

                if (Manifest.HasResponses())
                {

                    // Load saved responses

                    var responses = Manifest
                        .ResponseStore
                        .OrderBy(x => x.Question.Rank)
                        .ToList();

                    if (null != responses && responses.Count > 0)
                    {
                        var r = responses.First();

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
                                var readRespnse = r.GetValue().Value;
                                q.QuestionTemplate.SetResponse(readRespnse);
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

            SetDefualts();
        }

        public bool ValidateResponse(QuestionTemplate questionTemplate)
        {

            bool validate = false;

            try
            {
                _obsService.ValidateResponse(Encounter.Id, Encounter.ClientId, questionTemplate.Id,
                    questionTemplate.GetResponse());
                validate = true;
                questionTemplate.ErrorSummary = string.Empty;
            }
            catch (NullReferenceException ex)
            {

            }
            catch (Exception e)
            {
                questionTemplate.ErrorSummary = e.Message;
                try
                {
                    _dialogService.ShowErrorToast(e.Message, 6000);
                }
                catch (Exception exception)
                {
                }
            }

            return validate;
        }

        public void AllowNextQuestion(QuestionTemplate questionTemplate)
        {
            if (!questionTemplate.Allow)
                return;

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
                var liveResponse = new Response(Encounter.Id, Encounter.ClientId);
                liveResponse.SetQuestion(question);
                liveResponse.SetObs(Encounter.Id, Encounter.ClientId, questionTemplate.Id,
                    question.Concept.ConceptTypeId,
                    questionTemplate.GetResponse());

                //update encounter with Response

                Encounter.AddOrUpdate(liveResponse.Obs, false);

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

                    #region Partner Result Check

                    var partnerResult = _settings.GetValue("client.partner.result", "");

                    if (!string.IsNullOrWhiteSpace(partnerResult))
                    {
                        var presult = new Guid(partnerResult);
                        var inc = new Guid("b25f017c-852f-11e7-bb31-be2e44b06b34");
                        var discordant =
                            Questions.FirstOrDefault(
                                x => x.QuestionTemplate.Id ==
                                     new Guid("b2605c98-852f-11e7-bb31-be2e44b06b34"));

                        if (questionTemplate.Id == new Guid("b2605964-852f-11e7-bb31-be2e44b06b34"))
                        {
                            var obsValue = questionTemplate.GetResponse();
                            var value = null == obsValue ? Guid.Empty : new Guid(obsValue.ToString());

                            if (value != inc && presult != inc)
                            {
                                if (value == presult)
                                {
                                    //discordant

                                    discordant.QuestionTemplate.SetResponse(
                                        new Guid("b25ed04e-852f-11e7-bb31-be2e44b06b34"));
                                }
                                else
                                {
                                    //not-discordant

                                    discordant.QuestionTemplate.SetResponse(
                                        new Guid("b25eccd4-852f-11e7-bb31-be2e44b06b34"));
                                }
                            }
                        }
                    }

                    #endregion





                    #region TRANSFORMATION

                    // TRANSFORMATION FIRST

                    //REMOTE

                    var remactions = _obsService.GetTransformationActions(Manifest, questionTemplate.Id);

                    foreach (var a in remactions)
                    {
                        if (a.Action.ToLower() == "Set".ToLower())
                        {
                            var q = Questions.FirstOrDefault(x => x.QuestionTemplate.Id == a.QuestionId);
                            if (null != q)
                            {
                                q.QuestionTemplate.SetResponse(a.Response);
                            }
                        }

                        if (a.Action.ToLower() == "Block".ToLower())
                        {
                            var q = Questions.FirstOrDefault(x => x.QuestionTemplate.Id == a.QuestionId);
                            if (null != q)
                            {
                                q.QuestionTemplate.Allow = false;
                            }
                        }

                        if (a.Action.ToLower() == "Allow".ToLower())
                        {
                            var q = Questions.FirstOrDefault(x => x.QuestionTemplate.Id == a.QuestionId);
                            if (null != q)
                            {
                                q.QuestionTemplate.Allow = true;
                            }
                        }
                    }


                    //LOCAL

                    var actions = _obsService.GetTransformationActions(Manifest, questionTemplate.Id);

                    foreach (var a in actions)
                    {
                        if (a.Action.ToLower() == "Set".ToLower())
                        {
                            var q = Questions.FirstOrDefault(x => x.QuestionTemplate.Id == a.QuestionId);
                            if (null != q)
                            {
                                q.QuestionTemplate.SetResponse(a.Response);
                            }
                        }

                        if (a.Action.ToLower() == "Block".ToLower())
                        {
                            var q = Questions.FirstOrDefault(x => x.QuestionTemplate.Id == a.QuestionId);
                            if (null != q)
                            {
                                q.QuestionTemplate.Allow = false;
                            }
                        }

                        if (a.Action.ToLower() == "Allow".ToLower())
                        {
                            var q = Questions.FirstOrDefault(x => x.QuestionTemplate.Id == a.QuestionId);
                            if (null != q)
                            {
                                q.QuestionTemplate.Allow = true;
                            }
                        }
                    }

                    #endregion



                    var liveSkipQs = new List<QuestionTemplateWrap>();

                    // get all remaining Questions

                    var nextQuestions = _obsService.GetLiveQuestions(Manifest, questionTemplate.Id);

                    if (null == nextQuestions || nextQuestions.Count == 0)
                    {
                        AtTheEnd = true;
                        return;
                    }

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

                                if (skipQ.QuestionTemplate.Allow)
                                {
                                    skipQ.QuestionTemplate.ErrorSummary = string.Empty;
                                    skipQ.QuestionTemplate.Allow = false;
                                }
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
                            {
                                //if (!liveQ.QuestionTemplate.ShowMultiObs)
                                liveQ.QuestionTemplate.SetResponse(responseValue);
                            }

                        }
                    }

                }
            }
        }

        private void SetDefualts()
        {

            bool hasPartners = false;

            if (null != Manifest)
            {

                //Client

                if (null == ClientDTO)
                {
                    var clientJson = _settings.GetValue("client.dto", "");
                    if (!string.IsNullOrWhiteSpace(clientJson))
                    {
                        ClientDTO = JsonConvert.DeserializeObject<ClientDTO>(clientJson);
                    }
                }

                if (null != ClientDTO)
                    hasPartners = ClientDTO.HasPartners;


                foreach (var qq in Questions)
                {
                    var actions = _obsService.GetTransformationComplexActions(Manifest, qq.QuestionTemplate.Id);

                    if (actions.Count > 0)
                    {

                        // pre

                        var complext = hasPartners ? "Client.Partner.Yes" : "Client.Partner.No";

                        var preActions = actions
                            .Where(x => x.Condition == "Pre" &&
                                        x.Complex.ToLower() == complext.ToLower())
                            .ToList();


                        foreach (var pre in preActions)
                        {
                            if (pre.Action.ToLower() == "Set".ToLower())
                            {
                                var q = Questions.FirstOrDefault(x => x.QuestionTemplate.Id == pre.QuestionId);
                                if (null != q)
                                {
                                    q.QuestionTemplate.SetResponse(pre.Response);
                                }
                            }

                            if (pre.Action.ToLower() == "Block".ToLower())
                            {
                                var q = Questions.FirstOrDefault(x => x.QuestionTemplate.Id == pre.QuestionId);
                                if (null != q)
                                {
                                    q.QuestionTemplate.Allow = false;
                                }
                            }

                            if (pre.Action.ToLower() == "Allow".ToLower())
                            {
                                var q = Questions.FirstOrDefault(x => x.QuestionTemplate.Id == pre.QuestionId);
                                if (null != q)
                                {

                                    //q.QuestionTemplate.Allow = true;
                                }
                            }
                        }
                    }
                }
            }

        }



        private static ObservableCollection<QuestionTemplateWrap> ConvertToQuestionWrapperClass(
            List<Question> questions, IClientEncounterViewModel clientDashboardViewModel)
        {
            ObservableCollection<QuestionTemplateWrap> list = new ObservableCollection<QuestionTemplateWrap>();
            foreach (var r in questions)
            {
                list.Add(new QuestionTemplateWrap(clientDashboardViewModel, new QuestionTemplate(r)));
            }

            return list;
        }

        private bool CanSaveChanges()
        {
            FormStatus = "Status: In complete";

            if (null != Manifest)
            {
                if (string.IsNullOrWhiteSpace(FormError) && Manifest.IsComplete())
                {
                    FormStatus = "Status: Completed";
                    return true;
                }

            }

            return null != Manifest;
        }

        private void SaveChanges()
        {
            //nTODO : Save Enconter + Obs

            //readResponses

            if (!IsValidEncounterDate())
                return;

            var allowedQuestions = Questions.Where(x => x.QuestionTemplate.Allow).ToList();

            if (allowedQuestions.Count > 0)
            {
                foreach (var q in allowedQuestions)
                {
                    if (!ValidateResponse(q.QuestionTemplate))
                        return;
                }
            }

            _obsService.ClearEncounter(Encounter.Id);


            foreach (var q in allowedQuestions)
            {
                _obsService.SaveResponse(Encounter.Id, ClientDTO.Id, q.QuestionTemplate.Id,
                    q.QuestionTemplate.GetResponse());
                //Manifest = _obsService.Manifest;
            }

            _obsService.MarkEncounterCompleted(Encounter.Id, UserId, true, ClientDTO.Id);
            _obsService.UpdateEncounterDate(Encounter.Id, BirthDate, GetVisitType());
            Manifest = _obsService.Manifest;
            Manifest.Encounter.EncounterDate = BirthDate;
            Manifest.Encounter.VisitType = GetVisitType();
            Encounter = Manifest.Encounter;
            var encounterJson = JsonConvert.SerializeObject(Encounter);
            _settings.AddOrUpdateValue("client.encounter", encounterJson);

            var manifestJson = JsonConvert.SerializeObject(Manifest);
            _settings.AddOrUpdateValue("client.manifest", manifestJson);

            GoBack();
        }

        public void GoBack()
        {
            ShowViewModel<DashboardViewModel>(new {id = ClientDTO.Id});
        }

        public Guid GetGuid(string key)
        {
            var guid = _settings.GetValue(key, "");

            if (string.IsNullOrWhiteSpace(guid))
                return Guid.Empty;

            return new Guid(guid);
        }

        private VisitType GetVisitType()
        {
            if (null != SelectedVisitType)
                return (VisitType) SelectedVisitType.GetIntValue();
            return VisitType.Initial;
        }

        private CustomItem SetVisitType(VisitType visitType)
        {
            var vtype = (int) visitType;
            var v = VisitTypes.FirstOrDefault(x => x.Value == vtype.ToString());
            return v ?? VisitTypes.First();
        }

        private bool IsValidEncounterDate()
        {
            if (BirthDate.Date > DateTime.Today)
            {
                ShowErroInfo("Encounter Date cannot be in the Future");
                return false;
            }
            else
            {
                if (null != ClientDTO && !ClientDTO.DateEnrolled.IsNullOrEmpty())
                {
                    if (BirthDate.Date < ClientDTO.DateEnrolled.Value)
                    {
                        ShowErroInfo("Encounter Date before Registration Date");
                        return false;
                    }
                }
            }

            return true;
        }

        private void ShowErroInfo(string message)
        {
            try
            {
                _dialogService.ShowErrorToast(message, 6000);
            }
            catch (Exception exception)
            {
            }
        }
    }
}
