using System;
using System.Collections.Generic;
using System.Linq;
using Cheesebaron.MvxPlugins.Settings.Interfaces;
using LiveHTS.Core.Interfaces.Services.Clients;
using LiveHTS.Core.Interfaces.Services.Config;
using LiveHTS.Core.Interfaces.Services.Interview;
using LiveHTS.Core.Model.Config;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Lookup;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Presentation.DTO;
using LiveHTS.Presentation.Events;
using LiveHTS.Presentation.Interfaces;
using LiveHTS.Presentation.Interfaces.ViewModel;
using LiveHTS.Presentation.Validations;
using LiveHTS.SharedKernel.Custom;
using MvvmCross.Core.ViewModels;
using MvvmValidation;
using Newtonsoft.Json;
using Action = System.Action;

namespace LiveHTS.Presentation.ViewModel
{
    public class MemberScreeningViewModel:MvxViewModel, IMemberScreeningViewModel
    {

        private readonly ISettings _settings;
        private readonly IDialogService _dialogService;
        private readonly IMemberScreeningService _memberScreeningService;
        private ValidationHelper _validator;
      
        private string _errorSummary;
        private TraceDateDTO _selectedBookingDate;
        private  IMvxCommand _showBookingDateDialogCommand;
        private Guid _encounterTypeId;
        private Client _client;
        private Encounter _encounter;
        private DateTime _screeningDate;
        private  IMvxCommand _showScreeningDateDialogCommand;
        private TraceDateDTO _selectedScreeningDate;
        private List<CategoryItem> _hivStatus=new List<CategoryItem>();
        private CategoryItem _selectedHivStatus;
        private List<CategoryItem> _eligibility=new List<CategoryItem>();
        private CategoryItem _selectedEligibility;
        private DateTime _bookingDate;
        private string _remarks;
        private  IMvxCommand _saveScreeningCommand;
        private ObsMemberScreening _obsMemberScreening;
        private Guid _encounterId;
        private IDashboardService _dashboardService;
        private ILookupService _lookupService;


        public MemberScreeningViewModel(ISettings settings, IDialogService dialogService, IMemberScreeningService memberScreeningService, IDashboardService dashboardService, ILookupService lookupService)
        {
            _settings = settings;
            _dialogService = dialogService;
            _memberScreeningService = memberScreeningService;
            _dashboardService = dashboardService;
            _lookupService = lookupService;
            BookingDate = ScreeningDate = DateTime.Today;
        }

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


            var hivstatus = _lookupService.GetCategoryItems("HIVStatus", true, "[Select Mode]").ToList();
            HIVStatus = hivstatus;
            _settings.AddOrUpdateValue("lookup.hivstatus", JsonConvert.SerializeObject(hivstatus));
            var eligibility = _lookupService.GetCategoryItems("Eligibility", true, "[Select Outcome]").ToList();
            Eligibility = eligibility;
            _settings.AddOrUpdateValue("lookup.eligibility", JsonConvert.SerializeObject(eligibility));

            if (mode == "new")
            {
                //  New Encounter
                _settings.AddOrUpdateValue("client.ms.mode", "new");
                Encounter = _memberScreeningService.StartEncounter(new Guid(formId), EncounterTypeId, Client.Id, AppProviderId, AppUserId, AppPracticeId, AppDeviceId);
            }
            else
            {
                //  Load Encounter
                _settings.AddOrUpdateValue("client.ms.mode", "open");
                Encounter = _memberScreeningService.OpenEncounter(new Guid(encounterId));
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

            var hivstatusJson = _settings.GetValue("lookup.hivstatus", "");
            var eligibilityJson = _settings.GetValue("lookup.eligibility", "");
        

            if (null == Client && !string.IsNullOrWhiteSpace(clientJson))
            {
                Client = JsonConvert.DeserializeObject<Client>(clientJson);
            }

            if (EncounterTypeId.IsNullOrEmpty() && !string.IsNullOrWhiteSpace(encounterTypeId))
            {
                EncounterTypeId = new Guid(encounterTypeId);
            }

            if (null == Client && !string.IsNullOrWhiteSpace(clientJson))
            {
                Client = JsonConvert.DeserializeObject<Client>(clientJson);
            }

            if (HIVStatus.Count==0 && !string.IsNullOrWhiteSpace(hivstatusJson))
            {
                HIVStatus = JsonConvert.DeserializeObject<List<CategoryItem>>(hivstatusJson);
            }

            if (Eligibility.Count==0 && !string.IsNullOrWhiteSpace(eligibilityJson))
            {
                Eligibility = JsonConvert.DeserializeObject<List<CategoryItem>>(eligibilityJson);
            }


            if (null == Encounter && !string.IsNullOrWhiteSpace(clientEncounterJson))
            {
                Encounter = JsonConvert.DeserializeObject<Encounter>(clientEncounterJson);
            }

           
        }
        public ValidationHelper Validator
        {
            get { return _validator; }
            set { _validator = value; }
        }
        public ObservableDictionary<string, string> Errors { get; set; }
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

        public string ErrorSummary
        {
            get { return _errorSummary; }
            set { _errorSummary = value; RaisePropertyChanged(() => ErrorSummary); }
        }

        public Guid EncounterTypeId
        {
            get { return _encounterTypeId; }
            set { _encounterTypeId = value; }
        }

        public Client Client
        {
            get { return _client; }
            set { _client = value; }
        }

        public Encounter Encounter
        {
            get { return _encounter; }
            set
            {
                _encounter = value;
                RaisePropertyChanged(() => Encounter);
                if (null != _encounter)
                {
                    EncounterId = _encounter.Id;
                    ObsMemberScreening = _encounter.ObsMemberScreenings.FirstOrDefault();
                }
            }
        }

        public Guid EncounterId
        {
            get { return _encounterId; }
            set { _encounterId = value; RaisePropertyChanged(() => EncounterId); }
        }

        public ObsMemberScreening ObsMemberScreening
        {
            get { return _obsMemberScreening; }
            set
            {
                _obsMemberScreening = value;RaisePropertyChanged(() => ObsMemberScreening);
                if (null != ObsMemberScreening)
                {

                    ScreeningDate = ObsMemberScreening.ScreeningDate;
                    try
                    {
                        SelectedHIVStatus = HIVStatus.FirstOrDefault(x => x.ItemId == ObsMemberScreening.HivStatus);
                    }
                    catch 
                    {
                        SelectedHIVStatus = null;
                    }
                    try
                    {
                        SelectedEligibility = Eligibility.FirstOrDefault(x => x.ItemId == ObsMemberScreening.Eligibility);
                    }
                    catch 
                    {
                        SelectedEligibility = null;
                    }
                    
                    BookingDate = ObsMemberScreening.BookingDate;
                    Remarks = ObsMemberScreening.Remarks;
                }
            }
        }

       

        public DateTime ScreeningDate
        {
            get { return _screeningDate; }
            set { _screeningDate = value; RaisePropertyChanged(() => ScreeningDate);}
        }

        public TraceDateDTO SelectedScreeningDate
        {
            get { return _selectedScreeningDate; }
            set
            {
                _selectedScreeningDate = value;RaisePropertyChanged(() => SelectedScreeningDate);
                UpdateScreeningDate(SelectedScreeningDate);
            }
        }
        private void UpdateScreeningDate(TraceDateDTO selectedScreeningDate)
        {
            ScreeningDate = selectedScreeningDate.EventDate;
        }
        public IMvxCommand ShowScreeningDateDialogCommand
        {
            get
            {
                _showScreeningDateDialogCommand = _showScreeningDateDialogCommand ?? new MvxCommand(ShowScreeningDate);
                return _showScreeningDateDialogCommand;
            }
        }

        private void ShowScreeningDate()
        {
            ShowDatePickerS(Guid.Empty, BookingDate);
        }
        public void ShowDatePickerS(Guid refId, DateTime refDate)
        {
            OnChangedBookingDate(new ChangedDateEvent(refId, refDate));
        }

        public event EventHandler<ChangedDateEvent> ChangedScreeningDate;

        public List<CategoryItem> HIVStatus
        {
            get { return _hivStatus; }
            set { _hivStatus = value; RaisePropertyChanged(() => HIVStatus);}
        }

        public CategoryItem SelectedHIVStatus
        {
            get { return _selectedHivStatus; }
            set { _selectedHivStatus = value;RaisePropertyChanged(() => SelectedHIVStatus); }
        }

        public List<CategoryItem> Eligibility
        {
            get { return _eligibility; }
            set { _eligibility = value; RaisePropertyChanged(() => Eligibility);}
        }

        public CategoryItem SelectedEligibility
        {
            get { return _selectedEligibility; }
            set { _selectedEligibility = value;RaisePropertyChanged(() => SelectedEligibility); }
        }

        public DateTime BookingDate
        {
            get { return _bookingDate; }
            set { _bookingDate = value;RaisePropertyChanged(() => BookingDate); }
        }

        public TraceDateDTO SelectedBookingDate
        {
            get { return _selectedBookingDate; }
            set
            {
                _selectedBookingDate = value;RaisePropertyChanged(() => SelectedBookingDate);
                UpdateEnrollDate(SelectedBookingDate);
            }
        }
        private void UpdateEnrollDate(TraceDateDTO selectedBookingDate)
        {
            BookingDate = selectedBookingDate.EventDate;
        }

        public IMvxCommand ShowBookingDateDialogCommand
        {
            get
            {
                _showBookingDateDialogCommand = _showBookingDateDialogCommand ?? new MvxCommand(ShowDateEnrolledDialog);
                return _showBookingDateDialogCommand;
            }
        }
        private void ShowDateEnrolledDialog()
        {
            ShowDatePicker(Guid.Empty, BookingDate);
        }
        public void ShowDatePicker(Guid refId, DateTime refDate)
        {
            OnChangedBookingDate(new ChangedDateEvent(refId, refDate));
        }
      
        public event EventHandler<ChangedDateEvent> ChangedBookingDate;

        public string Remarks
        {
            get { return _remarks; }
            set { _remarks = value;RaisePropertyChanged(() => Remarks); }
        }

        public IMvxCommand SaveScreeningCommand
        {
            get
            {
                _saveScreeningCommand = _saveScreeningCommand ?? new MvxCommand(SaveScreening, CanSaveScreening);
                return _saveScreeningCommand;
            }
        }



        private bool CanSaveScreening()
        {
            return true;
        }

        private void SaveScreening()
        {
            if (Validate())
            {
                ObsMemberScreening obs;

                if (null == ObsMemberScreening)
                {
                    obs = ObsMemberScreening.Create(ScreeningDate,SelectedHIVStatus.ItemId,SelectedEligibility.ItemId,BookingDate,Remarks,EncounterId);
                }
                else
                {
                    obs = ObsMemberScreening;

                    obs.ScreeningDate = ScreeningDate;
                    obs.HivStatus = SelectedHIVStatus.ItemId;
                    obs.Eligibility = SelectedEligibility.ItemId;
                    obs.BookingDate = BookingDate;
                    obs.Remarks = Remarks;
                }

                _memberScreeningService.SaveMemberScreening(obs);
                ShowViewModel<DashboardViewModel>(new { id = Client.Id });
            }
        }

        public bool Validate()
        {
            ErrorSummary = string.Empty;

            Validator.AddRule(
                nameof(ScreeningDate),
                () => RuleResult.Assert(
                    ScreeningDate <= DateTime.Today,
                    $"{nameof(ScreeningDate)} should be a valid date"
                )
            );

            Validator.AddRule(
                "HIV Status",
                () => RuleResult.Assert(
                    null!= SelectedHIVStatus,
                    $"HIV Status is required"
                )
            );

            Validator.AddRule(
                "Eligibility",
                () => RuleResult.Assert(
                    null != SelectedEligibility,
                    $"Eligibility is required"
                )
            );

            
            Validator.AddRule(
                nameof(BookingDate),
                () => RuleResult.Assert(
                    BookingDate >= DateTime.Today,
                    $"{nameof(BookingDate)} should be a valid date"
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

        public Guid GetGuid(string key)
        {
            var guid = _settings.GetValue(key, "");

            if (string.IsNullOrWhiteSpace(guid))
                return Guid.Empty;

            return new Guid(guid);
        }

        protected virtual void OnChangedBookingDate(ChangedDateEvent e)
        {
            ChangedBookingDate?.Invoke(this, e);
        }

        protected virtual void OnChangedScreeningDate(ChangedDateEvent e)
        {
            ChangedScreeningDate?.Invoke(this, e);
        }
    }
}