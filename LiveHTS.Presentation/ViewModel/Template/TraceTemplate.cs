using System;
using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Lookup;
using LiveHTS.Presentation.Interfaces.ViewModel.Template;
using LiveHTS.Presentation.Interfaces.ViewModel.Wrapper;
using LiveHTS.Presentation.Validations;
using LiveHTS.SharedKernel.Custom;
using MvvmCross.Core.ViewModels;
using MvvmValidation;

namespace LiveHTS.Presentation.ViewModel.Template
{
    public class TraceTemplate:MvxNotifyPropertyChanged, ITraceTemplate
    {
        private string _errorSummary;
        
        
        
        private ITraceTemplateWrap _traceTemplateWrap;
        private Guid _id;
        private DateTime _date;
        private Guid _mode;
        private Guid _outcome;
        private Guid _encounterId;
        private CategoryItem _selectedMode;
        private CategoryItem _selectedOutcome;
        private List<CategoryItem> _modes;
        private List<CategoryItem> _outcomes;

        public string ErrorSummary
        {
            get { return _errorSummary; }
            set { _errorSummary = value; RaisePropertyChanged(() => ErrorSummary); }
        }
        public ValidationHelper Validator { get; }
        public ObservableDictionary<string, string> Errors { get; set; }

        public ObsTraceResult TraceResult
        {
            get { return GenerateTraceResult(); }
        }

      

        public ITraceTemplateWrap TraceTemplateWrap
        {
            get { return _traceTemplateWrap; }
            set { _traceTemplateWrap = value; }
        }

        public Guid Id
        {
            get { return _id; }
            set { _id = value; RaisePropertyChanged(() => Id); }
        }

        public DateTime Date
        {
            get { return _date; }
            set { _date = value; RaisePropertyChanged(() => Date); }
        }

        public Guid Mode
        {
            get { return _mode; }
            set { _mode = value; RaisePropertyChanged(() => Mode); }
        }

        public Guid Outcome
        {
            get { return _outcome; }
            set { _outcome = value; RaisePropertyChanged(() => Outcome); }
        }

        public Guid EncounterId
        {
            get { return _encounterId; }
            set { _encounterId = value; RaisePropertyChanged(() => EncounterId); }
        }

        public CategoryItem SelectedMode
        {
            get { return _selectedMode; }
            set { _selectedMode = value; RaisePropertyChanged(() => SelectedMode); }
        }

        public CategoryItem SelectedOutcome
        {
            get { return _selectedOutcome; }
            set { _selectedOutcome = value; RaisePropertyChanged(() => SelectedOutcome); }
        }

        public List<CategoryItem> Modes
        {
            get { return _modes; }
            set { _modes = value; RaisePropertyChanged(() => Modes); }
        }

        public List<CategoryItem> Outcomes
        {
            get { return _outcomes; }
            set { _outcomes = value; RaisePropertyChanged(() => Outcomes); }
        }

        public TraceTemplate(ObsTraceResult testResult, List<CategoryItem> modes, List<CategoryItem> outcomes)
        {
            Validator = new ValidationHelper();

            Modes = modes;
            Outcomes = outcomes;

            if (null != Modes && Modes.Count > 0)
            {
                var kit = Modes.FirstOrDefault(x => x.ItemId == testResult.Mode);
                if (null != kit)
                {
                    SelectedMode = kit;
                }
                else
                {
                    SelectedMode = Modes.OrderBy(x => x.Rank).First();
                }
            }

            if (null != Outcomes && Outcomes.Count > 0)
            {
                var result = Outcomes.FirstOrDefault(x => x.ItemId == testResult.Outcome);
                if (null != result)
                {

                    SelectedOutcome = result;
                }
                else
                {
                    SelectedMode = Outcomes.OrderBy(x => x.Rank).First();
                }

            }

            Id = testResult.Id;
            Date = testResult.Date;
            Mode = testResult.Mode;
            Outcome = testResult.Outcome;
            EncounterId = testResult.EncounterId;            
        }

        public bool Validate()
        {
            ErrorSummary = string.Empty;


            Validator.AddRule(
                nameof(Date),
                () => RuleResult.Assert(
                    Date <= DateTime.Today,
                    $"{nameof(Date)} should be a valid date"
                )
            );

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
                    !Mode.IsNullOrEmpty(),
                    $"{nameof(Outcome)} is required"
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

        public bool CanSave()
        {
            return true;
        }

        public bool CanDelete()
        {
            return true;
        }

        private ObsTraceResult GenerateTraceResult()
        {
            var obs = ObsTraceResult.Create(Id,Date,Mode,Outcome,EncounterId);
            return obs;
        }
    }
}