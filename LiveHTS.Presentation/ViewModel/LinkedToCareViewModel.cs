using System;
using System.Linq;
using LiveHTS.Core.Interfaces.Services.Interview;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Presentation.DTO;
using LiveHTS.Presentation.Events;
using LiveHTS.Presentation.Interfaces;
using LiveHTS.Presentation.Interfaces.ViewModel;
using LiveHTS.Presentation.Validations;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmValidation;

namespace LiveHTS.Presentation.ViewModel
{
    public class LinkedToCareViewModel : MvxViewModel, ILinkedToCareViewModel
    {
        private readonly ILinkageService _linkageService;
        private Guid _linkageId;
        private string _title = "LINKAGE";
        private string _facilityHandedTo;
        private string _handedTo;
        private string _workerCarde;
        private DateTime _dateEnrolled;
        private string _enrollmentId;
        private string _remarks;
        private ILinkageViewModel _parentViewModel;
        private IMvxCommand _saveLinkingCommand;
        private IMvxCommand _showDateEnrolledDialogCommand;
        private string _errorSummary;
        private ValidationHelper _validator;
        private ObservableDictionary<string, string> _errors;
        private ObsLinkage _obsLinkage;
        private TraceDateDTO _selectedEnrolDate;
        private IDialogService _dialogService;
        private DateTime _artStartDate;
        private TraceDateDTO _selectedArtDate;
        private IMvxCommand _showArtDateDialogCommand;

        public ValidationHelper Validator
        {
            get { return _validator; }
            set { _validator = value; }
        }

        public ObservableDictionary<string, string> Errors
        {
            get { return _errors; }
            set
            {
                _errors = value;
                RaisePropertyChanged(() => Errors);
            }
        }

        public ILinkageViewModel ParentViewModel
        {
            get { return _parentViewModel; }
            set { _parentViewModel = value; }
        }

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public string ErrorSummary
        {
            get { return _errorSummary; }
            set
            {
                _errorSummary = value;
                RaisePropertyChanged(() => ErrorSummary);
            }
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
                    FacilityHandedTo = ObsLinkage.FacilityHandedTo;
                    HandedTo = ObsLinkage.HandedTo;
                    WorkerCarde = ObsLinkage.WorkerCarde;

                    if (ObsLinkage.DateEnrolled.HasValue)
                        DateEnrolled = ObsLinkage.DateEnrolled.Value;
                    if (ObsLinkage.ARTStartDate.HasValue)
                        ARTStartDate = ObsLinkage.ARTStartDate.Value;

                    EnrollmentId = ObsLinkage.EnrollmentId;
                    Remarks = ObsLinkage.Remarks;
                }
            }
        }

        public Guid LinkageId
        {
            get { return _linkageId; }
            set
            {
                _linkageId = value;
                RaisePropertyChanged(() => LinkageId);
            }
        }

        public string FacilityHandedTo
        {
            get { return _facilityHandedTo; }
            set
            {
                _facilityHandedTo = value;
                RaisePropertyChanged(() => FacilityHandedTo);
            }
        }

        public string HandedTo
        {
            get { return _handedTo; }
            set
            {
                _handedTo = value;
                RaisePropertyChanged(() => HandedTo);
            }
        }

        public string WorkerCarde
        {
            get { return _workerCarde; }
            set
            {
                _workerCarde = value;
                RaisePropertyChanged(() => WorkerCarde);
            }
        }

        public DateTime DateEnrolled
        {
            get { return _dateEnrolled; }
            set
            {
                _dateEnrolled = value;
                RaisePropertyChanged(() => DateEnrolled);
            }
        }

        public DateTime ARTStartDate
        {
            get { return _artStartDate; }
            set
            {
                _artStartDate = value;
                RaisePropertyChanged(() => ARTStartDate);
            }
        }

        public string EnrollmentId
        {
            get { return _enrollmentId; }
            set
            {
                _enrollmentId = value;
                RaisePropertyChanged(() => EnrollmentId);
            }
        }

        public string Remarks
        {
            get { return _remarks; }
            set
            {
                _remarks = value;
                RaisePropertyChanged(() => Remarks);
            }
        }

        public TraceDateDTO SelectedEnrolDate
        {
            get { return _selectedEnrolDate; }
            set
            {
                _selectedEnrolDate = value;
                RaisePropertyChanged(() => SelectedEnrolDate);
                UpdateEnrollDate(SelectedEnrolDate);
            }
        }

        public TraceDateDTO SelectedArtDate
        {
            get { return _selectedArtDate; }
            set
            {
                _selectedArtDate = value;
                RaisePropertyChanged(() => SelectedArtDate);
                UpdateArtDate(SelectedArtDate);
            }
        }

        private void UpdateEnrollDate(TraceDateDTO selectedEnrolDate)
        {
            DateEnrolled = selectedEnrolDate.EventDate;
        }

        private void UpdateArtDate(TraceDateDTO selectedArtDate)
        {
            ARTStartDate = selectedArtDate.EventDate;
        }

        public IMvxCommand SaveLinkingCommand
        {
            get
            {
                _saveLinkingCommand = _saveLinkingCommand ?? new MvxCommand(SaveLinking, CanSaveLinking);
                return _saveLinkingCommand;
            }
        }

        private bool CanSaveLinking()
        {
            return true;
        }

        private void SaveLinking()
        {
            if (Validate())
            {
                ObsLinkage obs;

                if (null == ObsLinkage)
                {
                    obs = ObsLinkage.CreateNew(FacilityHandedTo, HandedTo, WorkerCarde, DateEnrolled, EnrollmentId,
                        Remarks, ParentViewModel.Encounter.Id, ARTStartDate);
                }
                else
                {
                    obs = ObsLinkage;

                    obs.FacilityHandedTo = FacilityHandedTo;
                    obs.HandedTo = HandedTo;
                    obs.WorkerCarde = WorkerCarde;
                    obs.DateEnrolled = DateEnrolled;
                    obs.ARTStartDate = ARTStartDate;
                    obs.EnrollmentId = EnrollmentId;
                    obs.Remarks = Remarks;

                    _linkageService.SaveLinkage(obs, ParentViewModel.Client.Id, false);
                }

                _linkageService.SaveLinkage(obs, ParentViewModel.Client.Id, false);
                ParentViewModel.Encounter = _linkageService.OpenEncounter(ParentViewModel.Encounter.Id);

                _dialogService.ShowToast("Linkage info saved successfully");
                ParentViewModel.GoBack();
            }
        }

        public IMvxCommand ShowDateEnrolledDialogCommand
        {
            get
            {
                _showDateEnrolledDialogCommand =
                    _showDateEnrolledDialogCommand ?? new MvxCommand(ShowDateEnrolledDialog);
                return _showDateEnrolledDialogCommand;
            }
        }

        private void ShowDateEnrolledDialog()
        {
            ShowDatePicker(LinkageId, DateEnrolled);
        }

        public event EventHandler<ChangedDateEvent> ChangedEnrollDate;

        public IMvxCommand ShowArtDateDialogCommand
        {
            get
            {
                _showArtDateDialogCommand = _showArtDateDialogCommand ?? new MvxCommand(ShowArtDateDialog);
                return _showArtDateDialogCommand;
            }
        }

        private void ShowArtDateDialog()
        {
            ShowArtDatePicker(LinkageId, ARTStartDate);
        }

        public event EventHandler<ChangedDateEvent> ChangedArtDate;


        public LinkedToCareViewModel()
        {
            _dialogService = Mvx.Resolve<IDialogService>();
            DateEnrolled = ARTStartDate = DateTime.Today;
            _validator = new ValidationHelper();
            _linkageService = Mvx.Resolve<ILinkageService>();
        }

        public void ShowDatePicker(Guid refId, DateTime refDate)
        {
            OnChangedEnrollDate(new ChangedDateEvent(refId, refDate));
        }

        public void ShowArtDatePicker(Guid refId, DateTime refDate)
        {
            OnChangedArtDate(new ChangedDateEvent(refId, refDate));
        }

        protected virtual void OnChangedEnrollDate(ChangedDateEvent e)
        {
            ChangedEnrollDate?.Invoke(this, e);
        }

        protected virtual void OnChangedArtDate(ChangedDateEvent e)
        {
            ChangedArtDate?.Invoke(this, e);
        }

        public bool Validate()
        {
            ErrorSummary = string.Empty;

            Validator.AddRule(
                nameof(FacilityHandedTo),
                () => RuleResult.Assert(
                    !string.IsNullOrWhiteSpace(FacilityHandedTo),
                    $"{nameof(FacilityHandedTo)} is required"
                )
            );

            Validator.AddRule(
                nameof(HandedTo),
                () => RuleResult.Assert(
                    !string.IsNullOrWhiteSpace(HandedTo),
                    $"{nameof(HandedTo)} is required"
                )
            );


            Validator.AddRule(
                nameof(EnrollmentId),
                () => RuleResult.Assert(
                    !string.IsNullOrWhiteSpace(EnrollmentId)&&
                      EnrollmentId.Trim().Length == 10 && isNumeric(EnrollmentId),
                    $"CCC {nameof(EnrollmentId)} is invalid"
                )
            );

            //Validator.AddRule(
            //    nameof(DateEnrolled),
            //    () => RuleResult.Assert(
            //        DateEnrolled >= DateTime.Today,
            //        $"{nameof(DateEnrolled)} should be a valid date"
            //    )
            //);

            //Validator.AddRule(
            //    nameof(ARTStartDate),
            //    () => RuleResult.Assert(
            //        ARTStartDate >= DateTime.Today,
            //        $"{nameof(ARTStartDate)} should be a valid date"
            //    )
            //);

            var result = Validator.ValidateAll();
            Errors = result.AsObservableDictionary();
            if (null != Errors && Errors.Count > 0)
            {
                ErrorSummary = Errors.First().Value;
            }

            return result.IsValid;
        }

        private bool isNumeric(string enrollmentId)
        {
            return long.TryParse(enrollmentId.Trim(), out long n);
        }
    }
}
