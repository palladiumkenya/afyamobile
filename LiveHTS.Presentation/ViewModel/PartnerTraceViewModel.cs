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
    public class PartnerTraceViewModel : MvxViewModel, IPartnerTraceViewModel
    {
        private Guid _id;
        private Guid _encounterId;
        private string _errorSummary;
        private ObsPartnerTraceResult _traceResult;
        private IMvxCommand _saveTestCommand;
        private readonly ISettings _settings;
        private IPartnerTracingViewModel _parent;
        private bool _editMode;
        private List<CategoryItem> _modes;
        private CategoryItem _selectedMode;
        private List<CategoryItem> _outcomes;
        private CategoryItem _selectedOutcome;
        private DateTime _date;
        private IPartnerTracingService _tracingService;
        private Guid _mode;
        private Guid _outcome;

        public bool EditMode
        {
            get { return _editMode; }
            set { _editMode = value; RaisePropertyChanged(() => EditMode);}
        }
        public IPartnerTracingViewModel Parent
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
        public ObsPartnerTraceResult TestResult
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
            }
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

        public PartnerTraceViewModel()
        {
            Validator = new ValidationHelper();
            Date=DateTime.Today;
            
            _tracingService =  Mvx.Resolve<IPartnerTracingService>();
            _settings = Mvx.Resolve<ISettings>();

            var modesJson = _settings.GetValue("lookup.TMode", "");
            var outcomeJson = _settings.GetValue("lookup.TOutcome", "");

            if (!string.IsNullOrWhiteSpace(modesJson))
            {
                Modes = JsonConvert.DeserializeObject<List<CategoryItem>>(modesJson);
            }
            if (!string.IsNullOrWhiteSpace(outcomeJson))
            {
                Outcomes = JsonConvert.DeserializeObject<List<CategoryItem>>(outcomeJson);
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
            }
        }

        private void Clear()
        {
            Date = DateTime.Today;

            SelectedMode = Modes.OrderBy(x => x.Rank).FirstOrDefault();
            SelectedOutcome = Outcomes.OrderBy(x => x.Rank).FirstOrDefault();
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

            if (!string.IsNullOrWhiteSpace(kitsJson))
            {
                Modes = JsonConvert.DeserializeObject<List<CategoryItem>>(kitsJson);
            }
            if (!string.IsNullOrWhiteSpace(resultsJson))
            {
                Outcomes = JsonConvert.DeserializeObject<List<CategoryItem>>(resultsJson);
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
                nameof(Date),
                () => RuleResult.Assert(
                    Date <= DateTime.Today,
                    $"{nameof(Date)} should be a valid date"
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

        private void SaveTest()
        {
            if (Validate())
            {
                TestResult= GenerateTest();
                _tracingService.SaveTest(TestResult);
                Parent.Referesh(TestResult.EncounterId);
                Parent.CloseTestCommand.Execute();
            }
        }
     

        public bool CanSaveTest()
        {
            return true;
        }

        private ObsPartnerTraceResult GenerateTest()
        {
            var obs= ObsPartnerTraceResult.Create(Date,Mode,Outcome,EncounterId);
            if (EditMode)
                obs.Id = Id;
            return obs;
        }
    }
}