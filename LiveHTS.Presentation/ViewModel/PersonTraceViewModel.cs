using System;
using System.Collections.Generic;
using System.Linq;
using Cheesebaron.MvxPlugins.Settings.Interfaces;
using LiveHTS.Core.Interfaces.Services.Interview;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Lookup;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Presentation.DTO;
using LiveHTS.Presentation.Events;
using LiveHTS.Presentation.Interfaces.ViewModel;
using LiveHTS.Presentation.Interfaces.ViewModel.Wrapper;
using LiveHTS.Presentation.Validations;
using LiveHTS.SharedKernel.Custom;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmValidation;
using Newtonsoft.Json;

namespace LiveHTS.Presentation.ViewModel
{
    public class PersonTraceViewModel : MvxViewModel, IPersonTraceViewModel
    {
        private Guid _id;
        private Guid _encounterId;
        private string _errorSummary;
        private ObsFamilyTraceResult _traceResult;
        private IMvxCommand _saveTestCommand;
        private readonly ISettings _settings;
        private IMemberTracingViewModel _parent;
        private bool _editMode;
        private List<CategoryItem> _modes;
        private CategoryItem _selectedMode;
        private List<CategoryItem> _outcomes;
        private CategoryItem _selectedOutcome;
        private DateTime _date;
        private IMemberTracingService _tracingService;
        private Guid _mode;
        private Guid _outcome;
        private DateTime _reminder;
        private Guid _consent;
        private List<CategoryItem> _consents;
        private CategoryItem _selectedConsent;
        private DateTime? _bookingDate;
        private bool _enableConsent;
        private bool _enableBooking;
        private CategoryItem _selectedReasoun;
        private List<CategoryItem> _reasons;
        private List<CategoryItem> _allReasons = new List<CategoryItem>();
        private Guid _reason;
        private string _reasonOther;
        private bool _showReason;
        private bool _showReasonOther;


        public bool EditMode
        {
            get { return _editMode; }
            set { _editMode = value; RaisePropertyChanged(() => EditMode);}
        }
        public IMemberTracingViewModel Parent
        {
            get { return _parent; }
            set
            {
                _parent = value; RaisePropertyChanged(() => Parent);
                //TestName = Parent.TestName;
                //EncounterId = Parent.Parent.Encounter.Id;
            }
        }
        public string ErrorSummary
        {
            get { return _errorSummary; }
            set { _errorSummary = value; RaisePropertyChanged(() => ErrorSummary); }
        }
        public ValidationHelper Validator { get; }
        public ObservableDictionary<string, string> Errors { get; set; }
        public ObsFamilyTraceResult TestResult
        {
            get { return _traceResult; }
            set { _traceResult = value; RaisePropertyChanged(() => TestResult);}
        }

        public Guid Id
        {
            get { return _id; }
            set { _id = value; RaisePropertyChanged(() => Id);}
        }

        public DateTime Date
        {
            get { return _date; }
            set { _date = value; RaisePropertyChanged(() => Date);}
        }

        public DateTime Reminder
        {
            get { return _reminder; }
            set { _reminder = value;  RaisePropertyChanged(() => Reminder);}
        }

        public Guid Mode
        {
            get { return _mode; }
            set { _mode = value; RaisePropertyChanged(() => Mode); }
        }

        public List<CategoryItem> Modes
        {
            get { return _modes; }
            set { _modes = value; RaisePropertyChanged(() => Modes);}
        }

        public CategoryItem SelectedMode
        {
            get { return _selectedMode; }
            set
            {
                _selectedMode = value; RaisePropertyChanged(() => SelectedMode);
                if (null != SelectedMode)
                    Mode = SelectedMode.ItemId;
                UpdateReasonsByMode();
            }
        }

        public Guid Outcome
        {
            get { return _outcome; }
            set { _outcome = value; RaisePropertyChanged(() => Outcome);}
        }

        public List<CategoryItem> Outcomes
        {
            get { return _outcomes; }
            set { _outcomes = value; RaisePropertyChanged(() => Outcomes); }
        }

        public CategoryItem SelectedOutcome
        {
            get { return _selectedOutcome; }
            set
            {
                _selectedOutcome = value; RaisePropertyChanged(() => SelectedOutcome);
                if (null != SelectedOutcome)
                    Outcome = SelectedOutcome.ItemId;
                SetOutcome();
                ShowReasonOption();
            }
        }

        private void SetOutcome()
        {
            //
            if (null != SelectedOutcome && !SelectedOutcome.ItemId.IsNullOrEmpty() &&
                SelectedOutcome.ItemId == new Guid("b25f0a50-852f-11e7-bb31-be2e44b06b34"))  //Contacted
            {
                EnableBooking = EnableConsent= true;
            }
            else
            {
                EnableBooking = EnableConsent = false;
                SelectedConsent = Consents.OrderBy(x => x.Rank).FirstOrDefault();
            }
        }

        public bool EnableConsent
        {
            get { return _enableConsent; }
            set { _enableConsent = value; RaisePropertyChanged(() => EnableConsent); }
        }

        public Guid Consent
        {
            get { return _consent; }
            set { _consent = value; RaisePropertyChanged(() => Consent);}
        }

        public List<CategoryItem> Consents
        {
            get { return _consents; }
            set { _consents = value; RaisePropertyChanged(() => Consents); }
        }

        public CategoryItem SelectedConsent
        {
            get { return _selectedConsent; }
            set
            {
                _selectedConsent = value;
                RaisePropertyChanged(() => SelectedConsent);
                if (null != SelectedConsent)
                    Consent = SelectedConsent.ItemId;

            }
        }
        public bool EnableBooking
        {
            get { return _enableBooking; }
            set { _enableBooking = value; RaisePropertyChanged(() => EnableBooking); }
        }

        public DateTime? BookingDate
        {
            get { return _bookingDate; }
            set { _bookingDate = value; RaisePropertyChanged(() => BookingDate);}
        }

        public Guid EncounterId
        {
            get { return _encounterId; }
            set { _encounterId = value; RaisePropertyChanged(() => EncounterId); }
        }


        public IMvxCommand SaveTraceCommand
        {
            get
            {
                _saveTestCommand = _saveTestCommand ?? new MvxCommand(SaveTest, CanSaveTest);
                return _saveTestCommand;
            }
        }
 public bool ShowReason
        {
            get { return _showReason; }
            set
            {
                _showReason = value;
                RaisePropertyChanged(() => ShowReason);
            }
        }

        public bool ShowKitOther
        {
            get { return _showReasonOther; }
            set
            {
                _showReasonOther = value;
                RaisePropertyChanged(() => ShowKitOther);
            }
        }

        public Guid ReasonNotContacted
        {
            get { return _reason; }
            set
            {
                _reason = value;
                RaisePropertyChanged(() => ReasonNotContacted);
            }
        }

        public List<CategoryItem> ReasonsNotContacted
        {
            get { return _reasons; }
            set
            {
                _reasons = value;
                RaisePropertyChanged(() => ReasonsNotContacted);
            }
        }

        public CategoryItem SelectedReasonNotContacted
        {
            get { return _selectedReasoun; }
            set
            {
                _selectedReasoun = value;
                RaisePropertyChanged(() => SelectedReasonNotContacted);
                if (null != SelectedReasonNotContacted)
                    ReasonNotContacted = SelectedReasonNotContacted.ItemId;
                ShowOther();
            }
        }

        private void ShowReasonOption()
        {
            ShowReason = false;
            if (null != SelectedOutcome &&
                !SelectedOutcome.ItemId.IsNullOrEmpty() &&
                SelectedOutcome.Item.Display.ToLower().Contains("Not".ToLower()))
            {
                ShowReason = true;
                UpdateReasonsByMode();
            }
        }
        private void UpdateReasonsByMode()
        {
            string mode = string.Empty;

            if (null != SelectedMode && _allReasons.Any())
            {
                try
                {
                    mode = SelectedMode.Item.Display;
                }
                catch{}

                if (!string.IsNullOrWhiteSpace(mode))
                {
                    if (mode.Contains("Physical"))
                    {
                        ReasonsNotContacted = _allReasons.Where(x => !x.Display.Contains("Mteja")).ToList();
                    }
                    else
                    {
                        ReasonsNotContacted = _allReasons.Where(x => !x.Display.EndsWith(".")).ToList();
                    }
                }
            }
        }
        private void ShowOther()
        {
            ShowKitOther = false;
            if (null != SelectedReasonNotContacted &&
                !SelectedReasonNotContacted.ItemId.IsNullOrEmpty() &&
                SelectedReasonNotContacted.Item.Display.ToLower().Contains("other".ToLower()))
            {
                ShowKitOther = true;
            }
        }

        public string ReasonNotContactedOther
        {
            get { return _reasonOther; }
            set
            {
                _reasonOther = value;
                RaisePropertyChanged(() => ReasonNotContactedOther);
            }
        }
        public PersonTraceViewModel()
        {
            Validator = new ValidationHelper();
            BookingDate = Date =Reminder=DateTime.Today;


            _tracingService =  Mvx.Resolve<IMemberTracingService>();
            _settings = Mvx.Resolve<ISettings>();

            var modesJson = _settings.GetValue("lookup.TMode", "");
            var outcomeJson = _settings.GetValue("lookup.TOutcome", "");
            var consentJson = _settings.GetValue("lookup.TConsent", "");
            var reasonJson = _settings.GetValue("lookup.ReasonNotContacted", "");

            if (!string.IsNullOrWhiteSpace(modesJson))
            {
                Modes = JsonConvert.DeserializeObject<List<CategoryItem>>(modesJson);
            }
            if (!string.IsNullOrWhiteSpace(outcomeJson))
            {
                Outcomes = JsonConvert.DeserializeObject<List<CategoryItem>>(outcomeJson);
            }
            if (!string.IsNullOrWhiteSpace(consentJson))
            {
                Consents = JsonConvert.DeserializeObject<List<CategoryItem>>(consentJson);
            }
            if (!string.IsNullOrWhiteSpace(reasonJson))
            {
                _allReasons = ReasonsNotContacted = JsonConvert.DeserializeObject<List<CategoryItem>>(reasonJson);
            }
        }

        private void LoadTest()
        {
            if (null != TestResult)
            {
                Id = TestResult.Id;
                Date = TestResult.Date;
                SelectedMode = Modes.FirstOrDefault(x=>x.ItemId== TestResult.Mode);
                SelectedOutcome =Outcomes.FirstOrDefault(x => x.ItemId == TestResult.Outcome);
                SelectedConsent = Consents.FirstOrDefault(x => x.ItemId == TestResult.Consent);
                BookingDate = TestResult.BookingDate;
                SelectedReasonNotContacted =
                    ReasonsNotContacted.FirstOrDefault(x => x.ItemId == TestResult.ReasonNotContacted);
                ReasonNotContactedOther = TestResult.ReasonNotContactedOther;
            }
        }

        private void Clear()
        {
            BookingDate=Date = Reminder = DateTime.Today;

            SelectedMode = Modes.OrderBy(x => x.Rank).FirstOrDefault();
            SelectedOutcome = Outcomes.OrderBy(x => x.Rank).FirstOrDefault();
            if (null != Consents)
                SelectedConsent =Consents.OrderBy(x => x.Rank).FirstOrDefault();
            SelectedReasonNotContacted = ReasonsNotContacted.OrderBy(x => x.Rank).FirstOrDefault();
            ReasonNotContactedOther = string.Empty;

        }

        public void Init(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return;
        }

        public override void ViewAppeared()
        {
            // Load Client

            var kitsJson = _settings.GetValue("lookup.TMode", "");
            var resultsJson = _settings.GetValue("lookup.TOutcome", "");
            var resultsCJson = _settings.GetValue("lookup.TConsent", "");
            var reasonJson = _settings.GetValue("lookup.ReasonNotContacted", "");

            if (!string.IsNullOrWhiteSpace(kitsJson))
            {
                Modes = JsonConvert.DeserializeObject<List<CategoryItem>>(kitsJson);
            }
            if (!string.IsNullOrWhiteSpace(resultsJson))
            {
                Outcomes = JsonConvert.DeserializeObject<List<CategoryItem>>(resultsJson);
            }
            if (!string.IsNullOrWhiteSpace(resultsCJson))
            {
                Consents = JsonConvert.DeserializeObject<List<CategoryItem>>(resultsCJson);
            }
            if (!string.IsNullOrWhiteSpace(reasonJson))
            {
                ReasonsNotContacted = JsonConvert.DeserializeObject<List<CategoryItem>>(reasonJson);
            }
            EncounterId = Parent.Encounter.Id;

            if (!EditMode)
            {
                Clear();
            }
            else
            {
                _traceResult = Parent.Trace;
                LoadTest();
            }

        }

        public bool Validate()
        {
            ErrorSummary=string.Empty;



            Validator.AddRule(
                nameof(Mode),
                () => RuleResult.Assert(
                    !Mode.IsNullOrEmpty(),
                    $"{nameof(Mode)} is required"
                )
            );


            Validator.AddRule(
                nameof(Outcome),
                () => RuleResult.Assert(
                    !Outcome.IsNullOrEmpty(),
                    $"{nameof(Outcome)} is required"
                )
            );

            Validator.AddRule(
                nameof(Reminder),
                () => RuleResult.Assert(
                    Reminder <= DateTime.Today,
                    $"{nameof(Reminder)} should be a valid date"
                )
            );



            Validator.AddRule(
                nameof(Date),
                () => RuleResult.Assert(
                    Date <= DateTime.Today,
                    $"{nameof(Date)} should be a valid date"
                )
            );

            if (EnableConsent)
            {
                Validator.AddRule(
                    nameof(Consent),
                    () => RuleResult.Assert(
                        !Consent.IsNullOrEmpty(),
                        $"{nameof(Consent)} is required"
                    )
                );
                Validator.AddRule(
                    nameof(BookingDate),
                    () => RuleResult.Assert(
                        BookingDate >= DateTime.Today,
                        $"{nameof(BookingDate)} should be a valid date"
                    )
                );
            }

            var result = Validator.ValidateAll();
            Errors = result.AsObservableDictionary();
            if (null != Errors && Errors.Count > 0)
            {
                ErrorSummary = Errors.First().Value;
            }
            return result.IsValid;
        }

        private void SaveTest()
        {
            if (Validate())
            {
                TestResult= GenerateTest();
                _tracingService.SaveTest(TestResult, Parent.Client.Id,Parent.IndexClient.Id);
                Parent.Referesh(TestResult.EncounterId);
                Parent.CloseTestCommand.Execute();
            }
        }


        public bool CanSaveTest()
        {
            return true;
        }

        private ObsFamilyTraceResult GenerateTest()
        {
            var obs= ObsFamilyTraceResult.Create(Date,Mode,Outcome,Consent,Reminder,BookingDate,EncounterId,Parent.IndexClient.Id,ReasonNotContacted,ReasonNotContactedOther);
            if (EditMode)
                obs.Id = Id;
            return obs;
        }
    }
}
