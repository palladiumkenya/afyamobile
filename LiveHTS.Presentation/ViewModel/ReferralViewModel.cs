using System;
using System.Collections.Generic;
using System.Linq;
using Cheesebaron.MvxPlugins.Settings.Interfaces;
using LiveHTS.Core.Interfaces.Services.Interview;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Presentation.DTO;
using LiveHTS.Presentation.Events;
using LiveHTS.Presentation.Interfaces;
using LiveHTS.Presentation.Interfaces.ViewModel;
using LiveHTS.Presentation.Validations;
using LiveHTS.Presentation.ViewModel.Template;
using LiveHTS.Presentation.ViewModel.Wrapper;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmValidation;

namespace LiveHTS.Presentation.ViewModel
{
    public class ReferralViewModel:MvxViewModel, IReferralViewModel
    {
        private readonly ILinkageService _linkageService;
        private readonly ISettings _settings;
        private readonly IDialogService _dialogService;
        private string _referredTo;
        private DateTime _datePromised;
        private string _title = "REFFERALS";
        private List<TraceTemplateWrap> _traces;
        private IMvxCommand _addTraceCommand;
        private TraceDateDTO _selectedDate;
        private IMvxCommand _saveReferralCommand;
        private ValidationHelper _validator;
        private ObservableDictionary<string, string> _errors;
        private string _errorSummary;
        private Guid _linkageId;
        private ObsLinkage _obsLinkage;
        private IMvxCommand _showDateDialogCommand;
        private TraceDateDTO _selectedPromiseDate;
        private ILinkageViewModel _parentViewModel;
        private ObsTraceResult _trace;
        private IMvxCommand _closeTestCommand;
        private Action _closeTestCommandAction;
        private Action _addTraceCommandAction;
        private Action _editTestCommandAction;


        public ILinkageViewModel ParentViewModel
        {
            get { return _parentViewModel; }
            set { _parentViewModel = value; }
        }

        public ValidationHelper Validator
        {
            get { return _validator; }
        }

        public ObservableDictionary<string, string> Errors
        {
            get { return _errors; }
            set { _errors = value; RaisePropertyChanged(() => Errors);}
        }

        public string Title
        {
            get { return _title; }
            set { _title = value; RaisePropertyChanged(() => Title);}
        }

        public string ErrorSummary
        {
            get { return _errorSummary; }
            set { _errorSummary = value; RaisePropertyChanged(() => ErrorSummary); }
        }

        public ObsLinkage ObsLinkage
        {
            get { return _obsLinkage; }
            set
            {
                _obsLinkage = value;
                RaisePropertyChanged(() => ObsLinkage);
                if (null != ObsLinkage)
                {
                    LinkageId = ObsLinkage.Id;
                    ReferredTo = ObsLinkage.ReferredTo;
                    DatePromised = ObsLinkage.DatePromised.Value;
                }
            }
        }

        public Guid LinkageId
        {
            get { return _linkageId; }
            set { _linkageId = value; RaisePropertyChanged(() => LinkageId);}
        }

        public string ReferredTo
        {
            get { return _referredTo; }
            set { _referredTo = value; RaisePropertyChanged(() => ReferredTo); }
        }

        public DateTime DatePromised
        {
            get { return _datePromised; }
            set { _datePromised = value; RaisePropertyChanged(() => DatePromised); }
        }

        public ObsTraceResult Trace
        {
            get { return _trace; }
            set { _trace = value; RaisePropertyChanged(() => Trace);}
        }

        public List<TraceTemplateWrap> Traces
        {
            get { return _traces; }
            set
            {
                _traces = value; RaisePropertyChanged(() => Traces);
                AddTraceCommand.RaiseCanExecuteChanged();
            }
        }

        public IMvxCommand AddTraceCommand
        {
            get
            {
                _addTraceCommand = _addTraceCommand ?? new MvxCommand(AddTrace, CanAddTrace);
                return _addTraceCommand;
            }
        }

        public IMvxCommand SaveReferralCommand
        {
            get
            {
                _saveReferralCommand = _saveReferralCommand ?? new MvxCommand(SaveReferral, CanSaveReferral);
                return _saveReferralCommand;
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

    private void ShowDateDialog()
            {

                ShowDatePicker(LinkageId, DatePromised);
            }
        

        public ReferralViewModel()
        {
            DatePromised=DateTime.Today;
            _validator = new ValidationHelper();
            _linkageService = Mvx.Resolve<ILinkageService>();
            _dialogService = Mvx.Resolve<IDialogService>();
            _settings = Mvx.Resolve<ISettings>();

        }
        /*
        public void Init(string formId, string encounterTypeId, string mode, string clientId, string encounterId)
        {

            // Load Client
            Client = _dashboardService.LoadClient(new Guid(clientId));

            if (null != Client)
            {
                var clientJson = JsonConvert.SerializeObject(Client);
                _settings.AddOrUpdateValue("client", clientJson);
            }

            // Load or Create Encounter

            if (!string.IsNullOrWhiteSpace(encounterTypeId))
            {
                _settings.AddOrUpdateValue("encounterTypeId", encounterTypeId);
            }

            EncounterTypeId = new Guid(encounterTypeId);

            if (mode == "new")
            {
                //  New Encounter
                _settings.AddOrUpdateValue("client.link.mode", "new");
                Encounter = _linkageService.StartEncounter(new Guid(formId), EncounterTypeId, Client.Id, Guid.Empty, Guid.Empty);
            }
            else
            {
                //  Load Encounter
                _settings.AddOrUpdateValue("client.link.mode", "open");
                Encounter = _linkageService.OpenEncounter(new Guid(encounterId));
            }

            if (null == Encounter)
            {
                throw new ArgumentException("Encounter has not been Initialized");
            }
            //Store Encounter 

            var encounterJson = JsonConvert.SerializeObject(Encounter);
            _settings.AddOrUpdateValue("client.encounter", encounterJson);

            
        }

        public override void ViewAppeared()
        {

            var clientJson = _settings.GetValue("client.dto", "");
            var clientEncounterJson = _settings.GetValue("client.encounter", "");
            var encounterTypeId = _settings.GetValue("encounterTypeId", "");

            if (null == Client && !string.IsNullOrWhiteSpace(clientJson))
            {
                Client = JsonConvert.DeserializeObject<Client>(clientJson);
            }

            if (EncounterTypeId.IsNullOrEmpty() && !string.IsNullOrWhiteSpace(encounterTypeId))
            {
                EncounterTypeId = new Guid(encounterTypeId);
            }


            if (null == Encounter && !string.IsNullOrWhiteSpace(clientEncounterJson))
            {
                Encounter = JsonConvert.DeserializeObject<Encounter>(clientEncounterJson);
            }
        }
        */
        private bool CanSaveReferral()
        {
            return true;
        }

        private void SaveReferral()
        {
            if (Validate())
            {
                ObsLinkage obs;

                if (null == ObsLinkage)
                {
                    obs = ObsLinkage.CreateNew(ReferredTo, DatePromised, ParentViewModel.Encounter.Id);
                }
                else
                {
                    obs = ObsLinkage;
                    obs.ReferredTo = ReferredTo;
                    obs.DatePromised = DatePromised;
                    _linkageService.SaveLinkage(obs);
                }
                _linkageService.SaveLinkage(obs);
                ParentViewModel.Encounter = _linkageService.OpenEncounter(ParentViewModel.Encounter.Id);

                _dialogService.ShowToast("Referrall info saved successfully");
            }
        }

        private void AddTrace()
        {
           AddTraceCommandAction?.Invoke();
        }

        private bool CanAddTrace()
        {
//            //No Tests
//            if (null == Traces)
//                return true;
//
//            if (null != Traces)
//            {
//                //No Tests
//                if (Traces.Count == 0)
//                    return true;
//
//                //Is initial add
//                if (Traces.Count > 0 && Traces.Any(x => x.TraceTemplate.Outcome == Guid.Empty))
//                    return false;
//
//                //Has invalid
//                if (
//                    Traces.Count > 0 &&
//                    Traces.Any(x => x.TraceTemplate.OutcomeDisplay.ToLower() == "C" )
//                )
//                    return false;
//            }


            return true;
        }

      

        public event EventHandler<ChangedDateEvent> ChangedDate;

        public TraceDateDTO SelectedPromiseDate
        {
            get { return _selectedPromiseDate; }
            set { _selectedPromiseDate = value; RaisePropertyChanged(() => SelectedPromiseDate);}
        }

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

        public void SaveTrace(ObsTraceResult test)
        {
            _linkageService.SaveTest(test);
            ParentViewModel.Encounter = _linkageService.OpenEncounter(ParentViewModel.Encounter.Id);
        }

        public async void DeleteTrace(ObsTraceResult testResult)
        {
            if (null != testResult)
            {
                var result = await _dialogService.ConfirmAction("Are you sure ?", "Delete this Trace");
                if (result)
                {

                    _linkageService.DeleteTest(testResult);
                    Referesh(testResult.EncounterId);
                }
            }
        }

        public bool Validate()
        {
            ErrorSummary = string.Empty;
            
            Validator.AddRule(
                nameof(ReferredTo),
                () => RuleResult.Assert(
                    !string.IsNullOrWhiteSpace(ReferredTo),
                    $"{nameof(ReferredTo)} is required"
                )
            );

            Validator.AddRule(
                nameof(DatePromised),
                () => RuleResult.Assert(
                    DatePromised >= DateTime.Today,
                    $"{nameof(DatePromised)} should be a valid date"
                )
            );
            
            var result = Validator.ValidateAll();
            Errors = result.AsObservableDictionary();
            if (null != Errors && Errors.Count > 0)
            {
                ErrorSummary = Errors.First().Value;
            }
            return result.IsValid;
        }

        public void EditTrace(ObsTraceResult testResult)
        {
            Trace = testResult;
            EditTestCommandAction?.Invoke();
        }

        public void Referesh(Guid encounterId)
        {
            ParentViewModel.Encounter = _linkageService.OpenEncounter(encounterId);
        }

        public IMvxCommand CloseTestCommand
        {
            get
            {
                _closeTestCommand = _closeTestCommand ?? new MvxCommand(CloseTest);
                return _closeTestCommand;
            }
        }

        private void CloseTest()
        {
            CloseTestCommandAction?.Invoke();
        }

        public Action AddTraceCommandAction
        {
            get { return _addTraceCommandAction; }
            set { _addTraceCommandAction = value; }
        }

        public Action EditTestCommandAction
        {
            get { return _editTestCommandAction; }
            set { _editTestCommandAction = value; }
        }

        public Action CloseTestCommandAction
        {
            get { return _closeTestCommandAction; }
            set { _closeTestCommandAction = value; }
        }

        private void UpdatePromiseDate(TraceDateDTO selectedDate)
        {
            DatePromised = selectedDate.EventDate;
        }




        public void ShowDatePicker(Guid refId, DateTime refDate)
        {
            OnChangedDate(new ChangedDateEvent(refId, refDate));
        }
        protected virtual void OnChangedDate(ChangedDateEvent e)
        {
            ChangedDate?.Invoke(this, e);
        }
    }
}