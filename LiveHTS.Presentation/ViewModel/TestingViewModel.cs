using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using LiveHTS.Presentation.ViewModel.Template;
using LiveHTS.Presentation.ViewModel.Wrapper;
using LiveHTS.SharedKernel.Custom;
using MvvmCross.Binding.Attributes;
using MvvmCross.Core.ViewModels;
using MvvmValidation;
using Newtonsoft.Json;

namespace LiveHTS.Presentation.ViewModel
{
    public class TestingViewModel : MvxViewModel, ITestingViewModel
    {

        private Client _client;

        private readonly ISettings _settings;
        private readonly IDashboardService _dashboardService;
        private readonly ILookupService _lookupService;
        private readonly IHIVTestingService _testingService;
        private Encounter _encounter;



        private CategoryItem _selectedFinalTestResult;
        private List<CategoryItem> _finalTestResults;

        private ExpiryDateDTO _selectedDate;
        private CategoryItem _selectedResultGiven;
        private List<CategoryItem> _resultGivenOptions;
        private CategoryItem _selectedCoupleDiscordant;
        private List<CategoryItem> _coupleDiscordantOptions;
        private CategoryItem _selectedSelfTest;
        private List<CategoryItem> _selfTestOptions;

        private string _errorSummary;
        private ObservableDictionary<string, string> _errors;
        private ObsFinalTestResult _obsFinalTestResult;
        private Guid _endResult;
        private Guid _resultGiven;
        private Guid _coupleDiscordant;
        private Guid _selfTestOption;
        private EncounterType _encounterType;
        private IMvxCommand _saveTestingCommand;
        private List<CategoryItem> _kits;
        private bool _enableFinalResult;
        private bool _enableSecondResult;
        private bool _enableFirstResult;
        private CategoryItem _selectedFirstTestResult;
        private List<CategoryItem> _FirstTestResults;
        private CategoryItem _selectedSecondTestResult;
        private List<CategoryItem> _SecondTestResults;
        private IDialogService _dialogService;
        private string _remarks;
        private bool _enableCoupleDiscordant;
        private List<CategoryItem> _pnsDeclineds;
        private CategoryItem _selectedPnsDeclined;
        private Guid _pnsDeclined;
        private bool _enablePnsDeclined;


        public ValidationHelper Validator { get; set; }

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

        public ITestEpisodeViewModel FirstTestEpisodeViewModel { get; set; }
        public ITestEpisodeViewModel SecondTestEpisodeViewModel { get; set; }

        public EncounterType EncounterType
        {
            get { return _encounterType; }
            set
            {
                _encounterType = value;
                RaisePropertyChanged(() => EncounterType);
            }
        }

        public Client Client
        {
            get { return _client; }
            set
            {
                _client = value;
                RaisePropertyChanged(() => Client);
            }
        }

        public Encounter Encounter
        {
            get { return _encounter; }
            set
            {
                _encounter = value;
                RaisePropertyChanged(() => Encounter);
                LoadTests();
            }
        }

        public ObsFinalTestResult ObsFinalTestResult
        {
            get { return _obsFinalTestResult; }
            set
            {
                _obsFinalTestResult = value;
                RaisePropertyChanged(() => ObsFinalTestResult);
            }
        }

        public bool EnableFirstResult
        {
            get { return _enableFirstResult; }
            set
            {
                _enableFirstResult = value;
                RaisePropertyChanged(() => EnableFirstResult);
            }
        }

        public Guid FirstResult
        {
            get { return _endResult; }
            set
            {
                _endResult = value;
                RaisePropertyChanged(() => FirstResult);
            }
        }

        public CategoryItem SelectedFirstTestResult
        {
            get { return _selectedFirstTestResult; }
            set
            {
                _selectedFirstTestResult = value;
                RaisePropertyChanged(() => SelectedFirstTestResult);
            }
        }

        public List<CategoryItem> FirstTestResults
        {
            get { return _FirstTestResults; }
            set
            {
                _FirstTestResults = value;
                RaisePropertyChanged(() => FirstTestResults);
            }
        }



        public bool EnableSecondResult
        {
            get { return _enableSecondResult; }
            set
            {
                _enableSecondResult = value;
                RaisePropertyChanged(() => EnableSecondResult);
            }
        }

        public Guid SecondResult
        {
            get { return _endResult; }
            set
            {
                _endResult = value;
                RaisePropertyChanged(() => SecondResult);
            }
        }

        public CategoryItem SelectedSecondTestResult
        {
            get { return _selectedSecondTestResult; }
            set
            {
                _selectedSecondTestResult = value;
                RaisePropertyChanged(() => SelectedSecondTestResult);
            }
        }

        public List<CategoryItem> SecondTestResults
        {
            get { return _SecondTestResults; }
            set
            {
                _SecondTestResults = value;
                RaisePropertyChanged(() => SecondTestResults);
            }
        }


        public bool HasFinalResult
        {
            get
            {
                return null != SelectedFinalTestResult && !SelectedFinalTestResult.ItemId.IsNullOrEmpty(); 
            }
        }

        public bool EnableFinalResult
        {
            get { return _enableFinalResult; }
            set
            {
                _enableFinalResult = value;
                RaisePropertyChanged(() => EnableFinalResult);
            }
        }

        public Guid FinalResult
        {
            get { return _endResult; }
            set
            {
                _endResult = value;
                RaisePropertyChanged(() => FinalResult);
            }
        }

        public CategoryItem SelectedFinalTestResult
        {
            get { return _selectedFinalTestResult; }
            set
            {
                _selectedFinalTestResult = value;
                RaisePropertyChanged(() => SelectedFinalTestResult);
                SaveTestingCommand.RaiseCanExecuteChanged();
                SecondTestEpisodeViewModel.AddTestCommand.RaiseCanExecuteChanged();
            }
        }

        public List<CategoryItem> FinalTestResults
        {
            get { return _finalTestResults; }
            set
            {
                _finalTestResults = value;
                RaisePropertyChanged(() => FinalTestResults);
            }
        }

        public Guid ResultGiven
        {
            get { return _resultGiven; }
            set
            {
                _resultGiven = value;
                RaisePropertyChanged(() => ResultGiven);
            }
        }

        public CategoryItem SelectedResultGiven
        {
            get { return _selectedResultGiven; }
            set
            {
                _selectedResultGiven = value;
                RaisePropertyChanged(() => SelectedResultGiven);
                SaveTestingCommand.RaiseCanExecuteChanged();
            }
        }

        public List<CategoryItem> ResultGivenOptions
        {
            get { return _resultGivenOptions; }
            set
            {
                _resultGivenOptions = value;
                RaisePropertyChanged(() => ResultGivenOptions);
            }
        }
        
        public bool EnableCoupleDiscordant
        {
            get { return _enableCoupleDiscordant; }
            set
            {
                _enableCoupleDiscordant = value;
                RaisePropertyChanged(() => EnableCoupleDiscordant);
                if (!_enableCoupleDiscordant)
                    SetNA();
            }
        }

        private void SetNA()
        {
            try
            {
                var id = new Guid("B25ED1C0-852F-11E7-BB31-BE2E44B06B34");
                SelectedCoupleDiscordant = CoupleDiscordantOptions.FirstOrDefault(x => x.ItemId == id);
            }
            catch 
            {
                
            }
            //throw new NotImplementedException();
        }

        public Guid CoupleDiscordant
        {
            get { return _coupleDiscordant; }
            set
            {
                _coupleDiscordant = value;
                RaisePropertyChanged(() => CoupleDiscordant);
            }
        }

        public CategoryItem SelectedCoupleDiscordant
        {
            get { return _selectedCoupleDiscordant; }
            set
            {
                _selectedCoupleDiscordant = value;
                RaisePropertyChanged(() => SelectedCoupleDiscordant);
            }
        }

        public List<CategoryItem> CoupleDiscordantOptions
        {
            get { return _coupleDiscordantOptions; }
            set
            {
                _coupleDiscordantOptions = value;
                RaisePropertyChanged(() => CoupleDiscordantOptions);
            }
        }

        public Guid SelfTestOption
        {
            get { return _selfTestOption; }
            set
            {
                _selfTestOption = value;
                RaisePropertyChanged(() => SelfTestOption);
                
            }
        }

        public CategoryItem SelectedSelfTest
        {
            get { return _selectedSelfTest; }
            set
            {
                _selectedSelfTest = value;
                RaisePropertyChanged(() => SelectedSelfTest);
                SetDeclincedState();
                SaveTestingCommand.RaiseCanExecuteChanged();
            }
        }

        private void SetDeclincedState()
        {
            if (null != SelectedSelfTest && !SelectedSelfTest.ItemId.IsNullOrEmpty() &&
                SelectedSelfTest.ItemId == new Guid("B25ED04E-852F-11E7-BB31-BE2E44B06B34"))
            {
                EnablePnsDeclined = true;
            }
            else
            {
                SelectedPnsDeclined = PnsDeclineds.OrderBy(x => x.Rank).FirstOrDefault();
                EnablePnsDeclined = false;
            }

        }

        public List<CategoryItem> SelfTestOptions
        {
            get { return _selfTestOptions; }
            set
            {
                _selfTestOptions = value;
                RaisePropertyChanged(() => SelfTestOptions);
            }
        }

        public bool EnablePnsDeclined
        {
            get { return _enablePnsDeclined; }
            set
            {
                _enablePnsDeclined = value;
                RaisePropertyChanged(() => EnablePnsDeclined);
                //SetDeclincedState();
            }
        }

        public Guid PnsDeclined
        {
            get { return _pnsDeclined; }
            set { _pnsDeclined = value; RaisePropertyChanged(() => PnsDeclined); }
        }

        public List<CategoryItem> PnsDeclineds
        {
            get { return _pnsDeclineds; }
            set { _pnsDeclineds = value; RaisePropertyChanged(() => PnsDeclined); }
        }

        public CategoryItem SelectedPnsDeclined
        {
            get { return _selectedPnsDeclined; }
            set { _selectedPnsDeclined = value; RaisePropertyChanged(() => SelectedPnsDeclined); SaveTestingCommand.RaiseCanExecuteChanged(); }
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

        public List<CategoryItem> Kits
        {
            get { return _kits; }
            set
            {
                _kits = value;
                RaisePropertyChanged(() => Kits);
            }
        }

        public IMvxCommand SaveTestingCommand
        {
            get
            {
                _saveTestingCommand = _saveTestingCommand ?? new MvxCommand(SaveTesting, CanSaveTesting);
                return _saveTestingCommand;
            }
        }

        private bool CanSaveTesting()
        {
            //return Validate();
            if (null != SelectedResultGiven && null != SelectedFinalTestResult && null != SelectedSelfTest)
            {
                var final = SelectedResultGiven.ItemId;
                var given = SelectedFinalTestResult.ItemId;
                var pnsAccepted = SelectedSelfTest.ItemId;
                var required= !final.IsNullOrEmpty() && !given.IsNullOrEmpty() && !pnsAccepted.IsNullOrEmpty();

                if (EnablePnsDeclined && null != SelectedPnsDeclined)
                {
                    var pnsDeclined = SelectedPnsDeclined.ItemId;
                    return required && !pnsDeclined.IsNullOrEmpty();
                }
                else
                {
                    return required;
                }
            }

            return false;
        }

        private void SaveTesting()
        {
            if (Validate())
            {
                if (null != ObsFinalTestResult)
                {
                    ObsFinalTestResult.ResultGiven = SelectedResultGiven.ItemId;

                    var isIndividial = _settings.GetValue("client.disco", false);
                    EnableCoupleDiscordant = !isIndividial;
                    ObsFinalTestResult.CoupleDiscordant = SelectedCoupleDiscordant.ItemId;

                    ObsFinalTestResult.SelfTestOption = SelectedSelfTest.ItemId;

                    ObsFinalTestResult.PnsDeclined = SelectedPnsDeclined.ItemId;
                    ObsFinalTestResult.Remarks = Remarks;
                    ObsFinalTestResult.ClientId = Client.Id;
                    _testingService.SaveFinalTest(ObsFinalTestResult);
                    _testingService.MarkEncounterCompleted(ObsFinalTestResult.EncounterId, AppUserId,true);
                    _testingService.UpdateEncounterDate(ObsFinalTestResult.EncounterId, Client.Id);
                    Encounter = _testingService.OpenEncounter(Encounter.Id);

                   // _dialogService.ShowToast("Tests saved successfully");
                    GoBack();
                }


            }
        }

        public event EventHandler<ChangedDateEvent> ChangedDate;


        public ObservableDictionary<string, string> Errors
        {
            get { return _errors; }
            set
            {
                _errors = value;
                RaisePropertyChanged(() => Errors);
            }
        }

   

        public TestingViewModel(ILookupService lookupService, IDashboardService dashboardService,
            IHIVTestingService testingService, ISettings settings, IDialogService dialogService)
        {
            _lookupService = lookupService;
            _dashboardService = dashboardService;
            _testingService = testingService;
            _settings = settings;
            _dialogService = dialogService;
            EnableFinalResult = false;
            FirstTestEpisodeViewModel = new FirstTestEpisodeViewModel();
            FirstTestEpisodeViewModel.Parent = this;
            SecondTestEpisodeViewModel = new SecondTestEpisodeViewModel();
            SecondTestEpisodeViewModel.Parent = this;
            Validator = new ValidationHelper();
        }


        public void Init(string formId, string encounterTypeId, string mode, string clientId, string encounterId)
        {
            var results = _lookupService.GetCategoryItems("TestResult", true, "[Select Result]").ToList();
            FirstTestResults = SecondTestResults = results;

            FinalTestResults = _lookupService.GetCategoryItems("FinalResult", true).ToList();
            ResultGivenOptions = _lookupService.GetCategoryItems("YesNo", true).ToList();
            CoupleDiscordantOptions = _lookupService.GetCategoryItems("YesNoNa", true).ToList();
            SelfTestOptions = _lookupService.GetCategoryItems("YesNo", true).ToList();
            PnsDeclineds = _lookupService.GetCategoryItems("PNSDecline", true).ToList();
            Kits = _lookupService.GetCategoryItems("KitName", true, "[Select Kit]").ToList();

            EncounterType = _lookupService.GetDefaultEncounterType(new Guid(encounterTypeId));

            _settings.AddOrUpdateValue("lookup.TestResult", JsonConvert.SerializeObject(results));
            _settings.AddOrUpdateValue("lookup.FinalResult", JsonConvert.SerializeObject(FinalTestResults));
            _settings.AddOrUpdateValue("lookup.ResultGivenOptions", JsonConvert.SerializeObject(ResultGivenOptions));
            _settings.AddOrUpdateValue("lookup.CoupleDiscordantOptions",JsonConvert.SerializeObject(CoupleDiscordantOptions));
            _settings.AddOrUpdateValue("lookup.SelfTestOptions", JsonConvert.SerializeObject(SelfTestOptions));
            _settings.AddOrUpdateValue("lookup.PNSDecline", JsonConvert.SerializeObject(PnsDeclined));
            _settings.AddOrUpdateValue("lookup.KitName", JsonConvert.SerializeObject(Kits));
            _settings.AddOrUpdateValue("lookup.EncounterType", JsonConvert.SerializeObject(EncounterType));


            // Load Client

            var clientJson = _settings.GetValue("client", "");

            

            if (null == Client)
            {
                if (!string.IsNullOrWhiteSpace(clientJson))
                {
                    Client = JsonConvert.DeserializeObject<Client>(clientJson);
                }
                else
                {
                    Client = _dashboardService.LoadClient(new Guid(clientId));
                    _settings.AddOrUpdateValue("client", JsonConvert.SerializeObject(Client));
                }
            }

            // Load or Create Encounter

            var etypeJson = _settings.GetValue("lookup.EncounterType", "");

            if (null == EncounterType)
            {
                if (!string.IsNullOrWhiteSpace(etypeJson))
                {
                    EncounterType = JsonConvert.DeserializeObject<EncounterType>(etypeJson);
                }
                else
                {
                    EncounterType = _lookupService.GetDefaultEncounterType(new Guid(encounterTypeId));
                    _settings.AddOrUpdateValue("client", JsonConvert.SerializeObject(EncounterType));
                }
            }

            if (mode == "new")
            {
                //  New Encounter
                _settings.AddOrUpdateValue("client.test.mode", "new");
                Encounter = _testingService.StartEncounter(new Guid(formId), EncounterType.Id, Client.Id,AppProviderId,AppUserId,AppPracticeId,AppDeviceId);
            }
            else
            {
                //  Load Encounter
                _settings.AddOrUpdateValue("client.test.mode", "open");
                Encounter = _testingService.OpenEncounter(new Guid(encounterId));
            }

            if (null == Encounter)
            {
                throw new ArgumentException("Encounter has not been Initialized");
            }

            var encounterJson = JsonConvert.SerializeObject(Encounter);
            _settings.AddOrUpdateValue("client.encounter", encounterJson);


            var isIndividial = _testingService.IsIndividual(Client.Id);
            _settings.AddOrUpdateValue("client.disco", isIndividial);
            EnableCoupleDiscordant = !isIndividial;

            //RaisePropertyChanged(() => FirstHIVTestViewModel.FirstTestName);
        }

 
        public void SaveTest(ObsTestResult test)
        {

            _testingService.SaveTest(test,Encounter.ClientId);
            Encounter = _testingService.OpenEncounter(Encounter.Id);
        }

        public void DeleteTest(ObsTestResult test)
        {
            _testingService.DeleteTest(test,Encounter.ClientId);
            Encounter = _testingService.OpenEncounter(Encounter.Id);
        }

        public void Referesh(Guid encounterId)
        {
            Encounter = _testingService.OpenEncounter(encounterId);
            if (null == Encounter)
            {
                throw new ArgumentException("Encounter has not been Initialized");
            }

            var encounterJson = JsonConvert.SerializeObject(Encounter);
            _settings.AddOrUpdateValue("client.encounter", encounterJson);
        }


        public bool Validate()
        {
            ErrorSummary = string.Empty;

            //FInal Result Given
            var final = SelectedFinalTestResult.ItemId;

            Validator.AddRule(
                "Final Result",
                () => RuleResult.Assert(
                    !final.IsNullOrEmpty(),
                    $"Final Result is required"
                )
            );

            //Results Given
            var given = SelectedResultGiven.ItemId;

            Validator.AddRule(
                "Result Given",
                () => RuleResult.Assert(
                    !given.IsNullOrEmpty(),
                    $"Result Given is required"
                )
            );

            //Pns Accepted
            var pnsaccepted = SelectedSelfTest.ItemId;

            Validator.AddRule(
                "Partner Listing",
                () => RuleResult.Assert(
                    !pnsaccepted.IsNullOrEmpty(),
                    $"Partner Listing is required"
                )
            );

            if (EnablePnsDeclined)
            {
                //Decline
                var declined = SelectedPnsDeclined.ItemId;

                Validator.AddRule(
                    "Decline Reason",
                    () => RuleResult.Assert(
                        !declined.IsNullOrEmpty(),
                        $"Decline Reason is required"
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

        private void LoadTests()
        {
            if (null != Encounter)
            {
                var tests = Encounter.ObsTestResults.Where(x => x.TestName == FirstTestEpisodeViewModel.TestName)
                    .ToList();
                foreach (var obsTestResult in tests)
                {
                    try
                    {
                        obsTestResult.ResultDisplay =
                            FirstTestResults.FirstOrDefault(x => x.ItemId == obsTestResult.Result).Display;

                        obsTestResult.KitDisplay = Kits.FirstOrDefault(x => x.ItemId == obsTestResult.Kit).Display;
                    }
                    catch (Exception e)
                    {
                    }

                }
                FirstTestEpisodeViewModel.Tests = tests;

                var tests2 = Encounter.ObsTestResults.Where(x => x.TestName == SecondTestEpisodeViewModel.TestName)
                    .ToList();
                foreach (var obsTestResult in tests2)
                {
                    try
                    {
                        obsTestResult.ResultDisplay = SecondTestResults
                            .FirstOrDefault(x => x.ItemId == obsTestResult.Result).Item.Display;

                        obsTestResult.KitDisplay = Kits.FirstOrDefault(x => x.ItemId == obsTestResult.Kit).Display;
                    }
                    catch (Exception e)
                    {
                    }

                }
                SecondTestEpisodeViewModel.Tests = tests2;

                var finalResult =ObsFinalTestResult= Encounter.ObsFinalTestResults.ToList().FirstOrDefault();

                if (null != finalResult)
                {
                    //  Test 1
                    var result1 = FirstTestResults.FirstOrDefault(x => x.ItemId == finalResult.FirstTestResult);

                    if (null != result1)
                    {
                        SelectedFirstTestResult = result1;
                    }
                    else
                    {
                        SelectedFirstTestResult = FinalTestResults.OrderBy(x => x.Rank).FirstOrDefault();
                    }
                    //  Test 2

                    var result2 = SecondTestResults.FirstOrDefault(x => x.ItemId == finalResult.SecondTestResult);

                    if (null != result2)
                    {
                        SelectedSecondTestResult = result2;
                    }
                    else
                    {
                        SelectedSecondTestResult = SecondTestResults.OrderBy(x => x.Rank).FirstOrDefault();
                    }
                    //  Final
                    var result3 = FinalTestResults.FirstOrDefault(x => x.ItemId == finalResult.FinalResult);

                    if (null != result3)
                    {
                        SelectedFinalTestResult = result3;
                    }
                    else
                    {
                        SelectedFinalTestResult = FinalTestResults.OrderBy(x => x.Rank).FirstOrDefault();
                    }

                    //  Result given
                    
                    var resultg = ResultGivenOptions.FirstOrDefault(x => x.ItemId == finalResult.ResultGiven);
                    if (null != resultg)
                    {
                        SelectedResultGiven = resultg;
                    }
                    else
                    {
                        SelectedResultGiven = ResultGivenOptions.OrderBy(x => x.Rank).FirstOrDefault();
                    }

                    //  couple discodant

                    var resultcd = CoupleDiscordantOptions.FirstOrDefault(x => x.ItemId == finalResult.CoupleDiscordant);
                    if (null != resultcd)
                    {
                        SelectedCoupleDiscordant = resultcd;
                    }
                    else
                    {
                        SelectedCoupleDiscordant = CoupleDiscordantOptions.OrderBy(x => x.Rank).FirstOrDefault();
                    }
                    var isIndividial = _settings.GetValue("client.disco", false);
                    EnableCoupleDiscordant = !isIndividial;

                    //  Self test
                    var resultst = SelfTestOptions.FirstOrDefault(x => x.ItemId == finalResult.SelfTestOption);
                    if (null != resultst)
                    {
                        SelectedSelfTest = resultst;
                    }
                    else
                    {
                        SelectedSelfTest = SelfTestOptions.OrderBy(x => x.Rank).FirstOrDefault();
                    }

                    //  PnsDeclined
                    var resultpst = PnsDeclineds.FirstOrDefault(x => x.ItemId == finalResult.PnsDeclined);
                    if (null != resultpst)
                    {
                        SelectedPnsDeclined = resultpst;
                    }
                    else
                    {
                        SelectedPnsDeclined = PnsDeclineds.OrderBy(x => x.Rank).FirstOrDefault();
                    }


                    //  Remarks
                    Remarks = finalResult.Remarks;

                }

            }
        }

        public override void ViewAppeared()
        {
            try
            {
                var isIndividial = _settings.GetValue("client.disco", false);
                EnableCoupleDiscordant = !isIndividial;
            }
            catch
            {

            }
        }

        public void GoBack()
        {
            ShowViewModel<DashboardViewModel>(new { id = Client.Id });
        }

        public Guid GetGuid(string key)
        {
            var guid=_settings.GetValue(key, "");

            if(string.IsNullOrWhiteSpace(guid))
                return Guid.Empty;
            
            return new Guid(guid);
        }
    }
}