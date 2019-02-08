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
    public class PartnerScreeningViewModel : MvxViewModel, IPartnerScreeningViewModel
    {

        private readonly ISettings _settings;
        private readonly IDialogService _dialogService;
        private readonly IPartnerScreeningService _partnerScreeningService;
        private ValidationHelper _validator;

        private string _errorSummary;
        private TraceDateDTO _selectedBookingDate;
        private IMvxCommand _showBookingDateDialogCommand;
        private Guid _encounterTypeId;
        private Client _client;
        private Encounter _encounter;
        private DateTime _screeningDate;
        private IMvxCommand _showScreeningDateDialogCommand;
        private TraceDateDTO _selectedScreeningDate;
        private List<CategoryItem> _hivStatus = new List<CategoryItem>();
        private CategoryItem _selectedHivStatus;
        private List<CategoryItem> _ipvscreening = new List<CategoryItem>();
        private CategoryItem _selectedipvscreening;
        private DateTime _bookingDate;
        private string _remarks;
        private IMvxCommand _saveScreeningCommand;
        private ObsPartnerScreening _obsPartnerScreening;
        private Guid _encounterId;
        private IDashboardService _dashboardService;
        private ILookupService _lookupService;
        
        private List<CategoryItem> _physicalAssult = new List<CategoryItem>();
        private CategoryItem _selectedPhysicalAssult;
        private List<CategoryItem> _threatened = new List<CategoryItem>();
        private CategoryItem _selectedThreatened;
        private List<CategoryItem> _sexuallyUncomfortable = new List<CategoryItem>();
        private CategoryItem _selectedSexuallyUncomfortable;
        private List<CategoryItem> _eligibility = new List<CategoryItem>();
        private CategoryItem _selectedEligibility;
        private bool _allowScreening;
        private bool _allowEligibility;
        private bool _makeEligibile;
        private List<CategoryItem> _pnsAccepted;
        private CategoryItem _selectedPnsAccepted;
        private List<CategoryItem> _ipvOutcome;
        private CategoryItem _selectedIpvOutcome;
        private string _occupation;
        private List<CategoryItem> _pnsRealtionship;
        private CategoryItem _selectedPnsRealtionship;
        private List<CategoryItem> _livingWithClient;
        private CategoryItem _selectedLivingWithClient;
        private List<CategoryItem> _pnsApproach;
        private CategoryItem _selectedPnsApproach;
        private bool _enablePnsAccepted;
        private bool _enablePnsApproach;
        private bool _enableBookingDate;
        private IndexClientDTO _indexClient;


        public PartnerScreeningViewModel(ISettings settings, IDialogService dialogService,
            IPartnerScreeningService partnerScreeningService, IDashboardService dashboardService,
            ILookupService lookupService)
        {
            _settings = settings;
            _dialogService = dialogService;
            _partnerScreeningService = partnerScreeningService;
            _dashboardService = dashboardService;
            _lookupService = lookupService;
            AllowScreening = AllowEligibility = true;
            MakeEligibile = false;
            BookingDate = ScreeningDate = DateTime.Today;
            Validator = new ValidationHelper();
        }

        public void Init(string formId, string encounterTypeId, string mode, string clientId, string encounterId, string indexclient)
        {

            // Load Client

            Client = _dashboardService.LoadClient(new Guid(clientId));

            if (null != Client)
            {
                var clientJson = JsonConvert.SerializeObject(Client);
                _settings.AddOrUpdateValue("client", clientJson);
            }

            if (!string.IsNullOrWhiteSpace(indexclient))
            {
                IndexClient = new IndexClientDTO(new Guid(indexclient));
                _settings.AddOrUpdateValue("pclientIndex", JsonConvert.SerializeObject(IndexClient));
            }

            // Load or Create Encounter

            if (!string.IsNullOrWhiteSpace(encounterTypeId))
            {
                _settings.AddOrUpdateValue("encounterTypeId", encounterTypeId);
            }

            EncounterTypeId = new Guid(encounterTypeId);


            var hivstatus = _lookupService.GetCategoryItems("PNSKnowledgeHIVStatus", true).ToList();
            HIVStatus = hivstatus;
            _settings.AddOrUpdateValue("lookup.hivstatus", JsonConvert.SerializeObject(hivstatus));


            var pnsAccepted = _lookupService.GetCategoryItems("IPVScreening", true).ToList();
            PnsAccepted = pnsAccepted;
            _settings.AddOrUpdateValue("lookup.pnsAccepted", JsonConvert.SerializeObject(pnsAccepted));


            var ipvscreening = _lookupService.GetCategoryItems("IPVScreening", true).ToList();
            IPVScreening = ipvscreening;
            _settings.AddOrUpdateValue("lookup.ipvscreening", JsonConvert.SerializeObject(ipvscreening));

            var physicalAssult = _lookupService.GetCategoryItems("IPVScreening", true).ToList();
            PhysicalAssult = physicalAssult;
            _settings.AddOrUpdateValue("lookup.physicalAssult", JsonConvert.SerializeObject(physicalAssult));

            var threatened = _lookupService.GetCategoryItems("IPVScreening", true).ToList();
            Threatened = threatened;
            _settings.AddOrUpdateValue("lookup.threatened", JsonConvert.SerializeObject(threatened));

            var sexuallyUncomfortable = _lookupService.GetCategoryItems("IPVScreening", true).ToList();
            SexuallyUncomfortable = sexuallyUncomfortable;
            _settings.AddOrUpdateValue("lookup.sexuallyUncomfortable", JsonConvert.SerializeObject(sexuallyUncomfortable));

            var ipvoutcome = _lookupService.GetCategoryItems("IPVOutcome", true).ToList();
            IPVOutcome = ipvoutcome;
            _settings.AddOrUpdateValue("lookup.ipvoutcome", JsonConvert.SerializeObject(ipvoutcome));

            var pNsRealtionship = _lookupService.GetCategoryItems("PNSRealtionship", true).ToList();
            PNSRealtionship = pNsRealtionship;
            _settings.AddOrUpdateValue("lookup.pNSRealtionship", JsonConvert.SerializeObject(pNsRealtionship));

            var livingWithClient = _lookupService.GetCategoryItems("PNSCurrentlyLivingWithClient", true).ToList();
            LivingWithClient = livingWithClient;
            _settings.AddOrUpdateValue("lookup.livingWithClient", JsonConvert.SerializeObject(livingWithClient));

            var pNsApproach = _lookupService.GetCategoryItems("PNSApproach", true).ToList();
            PNSApproach = pNsApproach;
            _settings.AddOrUpdateValue("lookup.pNSApproach", JsonConvert.SerializeObject(pNsApproach));

            var eligibility = _lookupService.GetCategoryItems("IPVScreening", true).ToList();
            Eligibility = eligibility;
            _settings.AddOrUpdateValue("lookup.eligibility", JsonConvert.SerializeObject(eligibility));


            if (mode == "new")
            {
                //  New Encounter
                _settings.AddOrUpdateValue("client.ms.mode", "new");
                Encounter = _partnerScreeningService.StartEncounter(new Guid(formId), EncounterTypeId, Client.Id,
                    AppProviderId, AppUserId, AppPracticeId, AppDeviceId,IndexClient.Id);
            }
            else
            {
                //  Load Encounter
                _settings.AddOrUpdateValue("client.ms.mode", "open");
                Encounter = _partnerScreeningService.OpenEncounter(new Guid(encounterId));
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
            var indexClientJson = _settings.GetValue("pclientIndex", "");
            var clientEncounterJson = _settings.GetValue("client.encounter", "");
            var encounterTypeId = _settings.GetValue("encounterTypeId", "");

            var pnsAcceptedJson = _settings.GetValue("lookup.pnsAccepted", "");
            var ipvscreeningJson = _settings.GetValue("lookup.ipvscreening", "");

            var physicalAssultJson = _settings.GetValue("lookup.physicalAssult", "");
            var threatenedJson = _settings.GetValue("lookup.threatened", "");
            var sexuallyUncomfortableJson = _settings.GetValue("lookup.sexuallyUncomfortable", "");
            

            var ipvoutcomeJson = _settings.GetValue("lookup.ipvoutcome", "");
            var pNsRealtionshipJson = _settings.GetValue("lookup.pNSRealtionship", "");
            var livingWithClientJson = _settings.GetValue("lookup.livingWithClient", "");
            var pNsApproachJson = _settings.GetValue("lookup.pNSApproach", "");

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
            if (null == IndexClient && !string.IsNullOrWhiteSpace(indexClientJson))
            {
                IndexClient = JsonConvert.DeserializeObject<IndexClientDTO>(indexClientJson);
            }

            if (PnsAccepted.Count == 0 && !string.IsNullOrWhiteSpace(pnsAcceptedJson))
            {
                PnsAccepted = JsonConvert.DeserializeObject<List<CategoryItem>>(pnsAcceptedJson);
            }

            if (IPVScreening.Count == 0 && !string.IsNullOrWhiteSpace(ipvscreeningJson))
            {
                IPVScreening = JsonConvert.DeserializeObject<List<CategoryItem>>(ipvscreeningJson);
            }

            if (PhysicalAssult.Count == 0 && !string.IsNullOrWhiteSpace(physicalAssultJson))
            {
                PhysicalAssult = JsonConvert.DeserializeObject<List<CategoryItem>>(physicalAssultJson);
            }

            if (Threatened.Count == 0 && !string.IsNullOrWhiteSpace(threatenedJson))
            {
                Threatened = JsonConvert.DeserializeObject<List<CategoryItem>>(threatenedJson);
            }
            if (SexuallyUncomfortable.Count == 0 && !string.IsNullOrWhiteSpace(sexuallyUncomfortableJson))
            {
                SexuallyUncomfortable = JsonConvert.DeserializeObject<List<CategoryItem>>(sexuallyUncomfortableJson);
            }

            if (IPVOutcome.Count == 0 && !string.IsNullOrWhiteSpace(ipvoutcomeJson))
            {
                IPVOutcome = JsonConvert.DeserializeObject<List<CategoryItem>>(ipvoutcomeJson);
            }
            if (PNSRealtionship.Count == 0 && !string.IsNullOrWhiteSpace(pNsRealtionshipJson))
            {
                PNSRealtionship = JsonConvert.DeserializeObject<List<CategoryItem>>(pNsRealtionshipJson);
            }
            if (LivingWithClient.Count == 0 && !string.IsNullOrWhiteSpace(livingWithClientJson))
            {
                LivingWithClient = JsonConvert.DeserializeObject<List<CategoryItem>>(livingWithClientJson);
            }
            if (PNSApproach.Count == 0 && !string.IsNullOrWhiteSpace(pNsApproachJson))
            {
                PNSApproach = JsonConvert.DeserializeObject<List<CategoryItem>>(pNsApproachJson);
            }


            if (HIVStatus.Count == 0 && !string.IsNullOrWhiteSpace(hivstatusJson))
            {
                HIVStatus = JsonConvert.DeserializeObject<List<CategoryItem>>(hivstatusJson);
            }

            if (Eligibility.Count == 0 && !string.IsNullOrWhiteSpace(eligibilityJson))
            {
                Eligibility = JsonConvert.DeserializeObject<List<CategoryItem>>(eligibilityJson);
            }
            
            if (null == Encounter && !string.IsNullOrWhiteSpace(clientEncounterJson))
            {
                Encounter = JsonConvert.DeserializeObject<Encounter>(clientEncounterJson);
            }

            try
            {
                SelectedPnsAccepted =
                    PnsAccepted.FirstOrDefault(x => x.ItemId == new Guid("B25ECCD4-852F-11E7-BB31-BE2E44B06B34"));
            }
            catch { }

            EnablePnsAccepted = false;


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
            set
            {
                _errorSummary = value;
                RaisePropertyChanged(() => ErrorSummary);
            }
        }

        public Guid EncounterTypeId
        {
            get { return _encounterTypeId; }
            set { _encounterTypeId = value; }
        }

        public IndexClientDTO IndexClient
        {
            get { return _indexClient; }
            set { _indexClient = value; RaisePropertyChanged(() => IndexClient); }
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
                    ObsPartnerScreening = _encounter.ObsPartnerScreenings.FirstOrDefault();
                }
            }
        }

        public Guid EncounterId
        {
            get { return _encounterId; }
            set
            {
                _encounterId = value;
                RaisePropertyChanged(() => EncounterId);
            }
        }

        public bool AllowScreening
        {
            get { return _allowScreening; }
            set { _allowScreening = value; RaisePropertyChanged(() => AllowScreening);}
        }

        public bool AllowEligibility
        {
            get { return _allowEligibility; }
            set { _allowEligibility = value; RaisePropertyChanged(() => AllowEligibility);}
        }

        public bool MakeEligibile
        {
            get { return _makeEligibile; }
            set
            {
                _makeEligibile = value;
                //RaisePropertyChanged(() => MakeEligibile);

                if (_makeEligibile)
                {
                    if (null != Eligibility && Eligibility.Count > 0)
                    {
                        var e= Eligibility.FirstOrDefault(x => x.Item!=null && x.Item.Code.ToLower() == "Y".ToLower());
                        SelectedEligibility = e;
                    }
                }
                else
                {
                    if (null != Eligibility && Eligibility.Count > 0)
                    {
                        var e = Eligibility.FirstOrDefault(x => x.Item != null && x.Item.Code.ToLower() == "N".ToLower());
                        SelectedEligibility = e;
                    }
                }

            }
        }

        public ObsPartnerScreening ObsPartnerScreening
        {
            get { return _obsPartnerScreening; }
            set
            {
                _obsPartnerScreening = value;
                RaisePropertyChanged(() => ObsPartnerScreening);
                if (null != ObsPartnerScreening)
                {

                    ScreeningDate = ObsPartnerScreening.ScreeningDate;
                    try
                    {
                        SelectedPnsAccepted =
                            PnsAccepted.FirstOrDefault(x => x.ItemId == ObsPartnerScreening.PnsAccepted);
                    }
                    catch
                    {
                        SelectedPnsAccepted = null;
                    }

                    try
                    {
                        SelectedIPVScreening =
                            IPVScreening.FirstOrDefault(x => x.ItemId == ObsPartnerScreening.IPVScreening);
                    }
                    catch
                    {
                        SelectedIPVScreening = null;
                    }


                    try
                    {
                        SelectedPhysicalAssult =
                            PhysicalAssult.FirstOrDefault(x => x.ItemId == ObsPartnerScreening.PhysicalAssult);
                    }
                    catch
                    {
                        SelectedPhysicalAssult = null;
                    }
                    try
                    {
                        SelectedThreatened =
                            Threatened.FirstOrDefault(x => x.ItemId == ObsPartnerScreening.Threatened);
                    }
                    catch
                    {
                        SelectedThreatened = null;
                    }
                    try
                    {
                        SelectedSexuallyUncomfortable =
                            SexuallyUncomfortable.FirstOrDefault(x => x.ItemId == ObsPartnerScreening.SexuallyUncomfortable);
                    }
                    catch
                    {
                        SelectedSexuallyUncomfortable = null;
                    }
                    try
                    {
                        SelectedIPVOutcome=
                            IPVOutcome.FirstOrDefault(x => x.ItemId == ObsPartnerScreening.IPVOutcome);
                    }
                    catch
                    {
                        SelectedIPVOutcome = null;
                    }

                    Occupation = ObsPartnerScreening.Occupation;
                    try
                    {
                        SelectedPNSRealtionship =
                            PNSRealtionship.FirstOrDefault(x => x.ItemId == ObsPartnerScreening.PNSRealtionship);
                    }
                    catch
                    {
                        SelectedPNSRealtionship = null;
                    }

                    try
                    {
                        SelectedLivingWithClient =
                            LivingWithClient.FirstOrDefault(x => x.ItemId == ObsPartnerScreening.LivingWithClient);
                    }
                    catch
                    {
                        SelectedLivingWithClient = null;
                    }


                    try
                    {
                        SelectedHIVStatus = HIVStatus.FirstOrDefault(x => x.ItemId == ObsPartnerScreening.HivStatus);
                    }
                    catch
                    {
                        SelectedHIVStatus = null;
                    }

                    try
                    {
                        SelectedPNSApproach = PNSApproach.FirstOrDefault(x => x.ItemId == ObsPartnerScreening.PNSApproach);
                    }
                    catch
                    {
                        SelectedPNSApproach = null;
                    }

                    try
                    {
                        SelectedEligibility =
                            Eligibility.FirstOrDefault(x => x.ItemId == ObsPartnerScreening.Eligibility);
                    }
                    catch
                    {
                        SelectedEligibility = null;
                    }
                    BookingDate = ObsPartnerScreening.BookingDate;
                    Remarks = ObsPartnerScreening.Remarks;
                }
            }
        }
        
        public DateTime ScreeningDate
        {
            get { return _screeningDate; }
            set
            {
                _screeningDate = value;
                RaisePropertyChanged(() => ScreeningDate);
            }
        }

        public bool EnablePnsAccepted
        {
            get { return _enablePnsAccepted; }
            set { _enablePnsAccepted = value; RaisePropertyChanged(() => EnablePnsAccepted); }
        }

        public List<CategoryItem> PnsAccepted
        {
            get { return _pnsAccepted; }
            set
            {
                _pnsAccepted = value;
                RaisePropertyChanged(() => PnsAccepted);
            }
        }

        public CategoryItem SelectedPnsAccepted
        {
            get { return _selectedPnsAccepted; }
            set
            {
                _selectedPnsAccepted = value;
                RaisePropertyChanged(() => SelectedPnsAccepted);
            }
        }

        public TraceDateDTO SelectedScreeningDate
        {
            get { return _selectedScreeningDate; }
            set
            {
                _selectedScreeningDate = value;
                RaisePropertyChanged(() => SelectedScreeningDate);
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


      

        public List<CategoryItem> IPVScreening
        {
            get { return _ipvscreening; }
            set
            {
                _ipvscreening = value;
                RaisePropertyChanged(() => IPVScreening);
            }
        }

        public CategoryItem SelectedIPVScreening
        {
            get { return _selectedipvscreening; }
            set
            {
                _selectedipvscreening = value;
                RaisePropertyChanged(() => SelectedIPVScreening);
                var screening = false;
                if (null != SelectedIPVScreening )
                {
                    screening = null!=SelectedIPVScreening.Item&& SelectedIPVScreening.Item.Code.ToLower() == "Y".ToLower();
                }

                AllowScreening = screening;
            }
        }

        public List<CategoryItem> PhysicalAssult
        {
            get { return _physicalAssult; }
            set { _physicalAssult = value; RaisePropertyChanged(() => SelectedPhysicalAssult);}
        }

        public CategoryItem SelectedPhysicalAssult
        {
            get { return _selectedPhysicalAssult; }
            set
            {
                _selectedPhysicalAssult = value;RaisePropertyChanged(() => SelectedPhysicalAssult);
               // UpdateEligibility();
            }
        }

        public List<CategoryItem> Threatened
        {
            get { return _threatened; }
            set
            {
                _threatened = value; RaisePropertyChanged(() => Threatened);
            }
        }

        public CategoryItem SelectedThreatened
        {
            get { return _selectedThreatened; }
            set
            {
                _selectedThreatened = value; RaisePropertyChanged(() => SelectedThreatened);
               //UpdateEligibility();
            }
        }
        public List<CategoryItem> SexuallyUncomfortable
        {
            get { return _sexuallyUncomfortable; }
            set { _sexuallyUncomfortable = value; RaisePropertyChanged(() => SexuallyUncomfortable);}
        }

        public CategoryItem SelectedSexuallyUncomfortable
        {
            get { return _selectedSexuallyUncomfortable; }
            set
            {
                _selectedSexuallyUncomfortable = value;RaisePropertyChanged(() => SelectedSexuallyUncomfortable);
                //UpdateEligibility();
            }
        }

        public List<CategoryItem> IPVOutcome
        {
            get { return _ipvOutcome; }
            set { _ipvOutcome = value; RaisePropertyChanged(() => IPVOutcome); }
        }

        public CategoryItem SelectedIPVOutcome
        {
            get { return _selectedIpvOutcome; }
            set { _selectedIpvOutcome = value; RaisePropertyChanged(() => SelectedIPVOutcome); }
        }

        public string Occupation
        {
            get { return _occupation; }
            set { _occupation = value; RaisePropertyChanged(() => Occupation); }
        }

        public List<CategoryItem> PNSRealtionship
        {
            get { return _pnsRealtionship; }
            set { _pnsRealtionship = value; RaisePropertyChanged(() => PNSRealtionship); }
        }

        public CategoryItem SelectedPNSRealtionship
        {
            get { return _selectedPnsRealtionship; }
            set { _selectedPnsRealtionship = value; RaisePropertyChanged(() => SelectedPNSRealtionship); }
        }

        public List<CategoryItem> LivingWithClient
        {
            get { return _livingWithClient; }
            set { _livingWithClient = value; RaisePropertyChanged(() => LivingWithClient); }
        }

        public CategoryItem SelectedLivingWithClient
        {
            get { return _selectedLivingWithClient; }
            set { _selectedLivingWithClient = value; RaisePropertyChanged(() => SelectedLivingWithClient); }
        }


        public List<CategoryItem> HIVStatus
        {
            get { return _hivStatus; }
            set
            {
                _hivStatus = value;
                RaisePropertyChanged(() => HIVStatus);
                
            }
        }
        public CategoryItem SelectedHIVStatus
        {
            get { return _selectedHivStatus; }
            set
            {
                _selectedHivStatus = value;
                RaisePropertyChanged(() => SelectedHIVStatus);
                SetEligibilityState();
            }
        }

        public bool EnablePNSApproach
        {
            get { return _enablePnsApproach; }
            set { _enablePnsApproach = value; RaisePropertyChanged(() => EnablePNSApproach); }
        }

        public List<CategoryItem> PNSApproach
        {
            get { return _pnsApproach; }
            set { _pnsApproach = value; RaisePropertyChanged(() => PNSApproach); }
        }

        public CategoryItem SelectedPNSApproach
        {
            get { return _selectedPnsApproach; }
            set { _selectedPnsApproach = value; RaisePropertyChanged(() => SelectedPNSApproach); }
        }


        public List<CategoryItem> Eligibility
        {
            get { return _eligibility; }
            set { _eligibility = value;RaisePropertyChanged(() => Eligibility); }
        }

        public CategoryItem SelectedEligibility
        {
            get { return _selectedEligibility; }
            set { _selectedEligibility = value; RaisePropertyChanged(() => SelectedEligibility);}
        }

        public bool EnableBookingDate
        {
            get { return _enableBookingDate; }
            set
            {
                _enableBookingDate = value;
                RaisePropertyChanged(() => EnableBookingDate);
            }
        }

        public DateTime BookingDate
        {
            get { return _bookingDate; }
            set
            {
                _bookingDate = value;
                RaisePropertyChanged(() => BookingDate);
            }
        }

        public TraceDateDTO SelectedBookingDate
        {
            get { return _selectedBookingDate; }
            set
            {
                _selectedBookingDate = value;
                RaisePropertyChanged(() => SelectedBookingDate);
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
            set
            {
                _remarks = value;
                RaisePropertyChanged(() => Remarks);
            }
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
                ObsPartnerScreening obs;

                if (null == ObsPartnerScreening)
                {
                    obs = ObsPartnerScreening.Create(
                        ScreeningDate,
                        SelectedIPVScreening.ItemId,
                        SelectedPhysicalAssult.ItemId,
                        SelectedThreatened.ItemId,
                        SelectedSexuallyUncomfortable.ItemId,
                        SelectedHIVStatus.ItemId,
                        SelectedEligibility.ItemId,
                        BookingDate,
                        Remarks,
                        SelectedPnsAccepted.ItemId,
                        SelectedIPVOutcome.ItemId,
                        Occupation,
                        SelectedPNSRealtionship.ItemId,
                        SelectedLivingWithClient.ItemId,
                        SelectedPNSApproach.ItemId,
                        EncounterId,
                        IndexClient.Id);
                }
                else
                {
                    obs = ObsPartnerScreening;
                    obs.ScreeningDate = ScreeningDate;
                    obs.PnsAccepted = SelectedPnsAccepted.ItemId;
                    obs.IPVScreening = SelectedIPVScreening.ItemId;
                    obs.PhysicalAssult = SelectedPhysicalAssult.ItemId;
                    obs.Threatened = SelectedThreatened.ItemId;
                    obs.SexuallyUncomfortable = SelectedSexuallyUncomfortable.ItemId;


                    obs.IPVOutcome = SelectedIPVOutcome.ItemId;
                    obs.Occupation = Occupation;

                    obs.PNSRealtionship = SelectedPNSRealtionship.ItemId;
                    obs.LivingWithClient = SelectedLivingWithClient.ItemId;
                    obs.HivStatus = SelectedHIVStatus.ItemId;

                    obs.Eligibility = SelectedEligibility.ItemId;
                    obs.BookingDate = BookingDate;
                    obs.Remarks = Remarks;

                 
                    obs.PNSApproach = SelectedPNSApproach.ItemId;

                }

                _partnerScreeningService.SavePartnerScreening(obs,Client.Id,IndexClient.Id);
                _partnerScreeningService.MarkEncounterCompleted(EncounterId,AppUserId, true);
                ShowViewModel<DashboardViewModel>(new {id = Client.Id});
            }
        }

        //TODO: HEHEHE
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
                    null != SelectedHIVStatus && !SelectedHIVStatus.ItemId.IsNullOrEmpty(),
                    $"HIV Status is required"
                )
            );

            Validator.AddRule(
                "IPVScreening",
                () => RuleResult.Assert(
                    null != SelectedIPVScreening && !SelectedIPVScreening.ItemId.IsNullOrEmpty(),
                    $"IPVScreening is required"
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

        private void SetEligibilityState()
        {
            if (null != SelectedHIVStatus && !SelectedHIVStatus.ItemId.IsNullOrEmpty() &&
                SelectedHIVStatus.ItemId == new Guid("B25EFD8A-852F-11E7-BB31-BE2E44B06B34"))  //pos
            {
                AllowEligibility = EnableBookingDate = EnablePNSApproach = false;
                try
                {
                    SelectedEligibility = Eligibility.FirstOrDefault(x => x.ItemId == new Guid("b25ed04e-852f-11e7-bb31-be2e44b06b34"));
                }
                catch
                {
                    SelectedEligibility = Eligibility.OrderBy(x => x.Rank).FirstOrDefault();
                }

            }
            else
            {
                AllowEligibility = EnableBookingDate = EnablePNSApproach =true;
            }

        }
        public void UpdateEligibility()
        {
            if (null != SelectedHIVStatus && !SelectedHIVStatus.ItemId.IsNullOrEmpty() &&
                SelectedHIVStatus.ItemId == new Guid("b25efd8a-852f-11e7-bb31-be2e44b06b34"))  //pos
            {
                EnableBookingDate = EnablePNSApproach = AllowEligibility=true;
                
            }
            else
            {
                SelectedEligibility = Eligibility.OrderBy(x => x.Rank).FirstOrDefault();
                EnableBookingDate = EnablePNSApproach = AllowEligibility = false;
            }


            if (AllowScreening)
            {
                MakeEligibile = true;
//                if (null != SelectedPhysicalAssult)
//                {
//                    assulted = null != SelectedPhysicalAssult.Item &&
//                               SelectedPhysicalAssult.Item.Code.ToLower() == "Y".ToLower();
//                }
//
//                if (null != SelectedSexuallyUncomfortable)
//                {
//                    uncomfortable = null != SelectedSexuallyUncomfortable.Item &&
//                                    SelectedSexuallyUncomfortable.Item.Code.ToLower() == "Y".ToLower();
//                }
//
//                if (null != SelectedThreatened)
//                {
//                    threatened = null != SelectedThreatened.Item &&
//                                 SelectedThreatened.Item.Code.ToLower() == "Y".ToLower();
//                }
//
//                MakeEligibile = !assulted && !uncomfortable && !threatened;
            }

            if (null != SelectedHIVStatus)
            {
                var hivpos = null != SelectedHIVStatus.Item && SelectedHIVStatus.Item.Code.ToLower() == "P".ToLower();
                if (hivpos)
                    MakeEligibile = false;
            }
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