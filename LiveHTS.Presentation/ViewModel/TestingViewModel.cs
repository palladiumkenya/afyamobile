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
using LiveHTS.Presentation.Interfaces.ViewModel;
using LiveHTS.Presentation.Validations;
using LiveHTS.Presentation.ViewModel.Template;
using LiveHTS.Presentation.ViewModel.Wrapper;
using LiveHTS.SharedKernel.Custom;
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

        public ValidationHelper Validator { get; set; }
        public string ErrorSummary
        {
            get { return _errorSummary; }
            set { _errorSummary = value; RaisePropertyChanged(() => ErrorSummary); }
        }
        public ITestEpisodeViewModel FirstTestEpisodeViewModel { get; set; }
        public ITestEpisodeViewModel SecondTestEpisodeViewModel { get; set; }

        public EncounterType EncounterType
        {
            get { return _encounterType; }
            set { _encounterType = value; RaisePropertyChanged(() => EncounterType);}
        }
        public Client Client
        {
            get { return _client; }
            set { _client = value; RaisePropertyChanged(() => Client); }
        }
        public Encounter Encounter
        {
            get { return _encounter; }
            set
            {
                _encounter = value; RaisePropertyChanged(() => Encounter);
                LoadTests();
            }
        }
        public ObsFinalTestResult ObsFinalTestResult
        {
            get { return _obsFinalTestResult; }
            set { _obsFinalTestResult = value; RaisePropertyChanged(() => ObsFinalTestResult);}
        }

        public bool EnableFinalResult
        {
            get { return _enableFinalResult; }
            set { _enableFinalResult = value; RaisePropertyChanged(() => EnableFinalResult);}
        }

        public Guid FinalResult
        {
            get { return _endResult; }
            set { _endResult = value; RaisePropertyChanged(() => FinalResult); }
        }      
        public CategoryItem SelectedFinalTestResult
        {
            get { return _selectedFinalTestResult; }
            set { _selectedFinalTestResult = value; RaisePropertyChanged(() => SelectedFinalTestResult); }
        }
        public List<CategoryItem> FinalTestResults
        {
            get { return _finalTestResults; }
            set { _finalTestResults = value; RaisePropertyChanged(() => FinalTestResults); }
        }

        public Guid ResultGiven
        {
            get { return _resultGiven; }
            set { _resultGiven = value; RaisePropertyChanged(() => ResultGiven); }
        }

        public CategoryItem SelectedResultGiven
        {
            get { return _selectedResultGiven; }
            set { _selectedResultGiven = value; RaisePropertyChanged(() => SelectedResultGiven);}
        }

        public List<CategoryItem> ResultGivenOptions
        {
            get { return _resultGivenOptions; }
            set { _resultGivenOptions = value; RaisePropertyChanged(() => ResultGivenOptions);}
        }

        public Guid CoupleDiscordant
        {
            get { return _coupleDiscordant; }
            set { _coupleDiscordant = value; RaisePropertyChanged(() => CoupleDiscordant); }
        }

        public CategoryItem SelectedCoupleDiscordant
        {
            get { return _selectedCoupleDiscordant; }
            set { _selectedCoupleDiscordant = value; RaisePropertyChanged(() => SelectedCoupleDiscordant);}
        }

        public List<CategoryItem> CoupleDiscordantOptions
        {
            get { return _coupleDiscordantOptions; }
            set { _coupleDiscordantOptions = value; RaisePropertyChanged(() => CoupleDiscordantOptions); }
        }

        public Guid SelfTestOption
        {
            get { return _selfTestOption; }
            set { _selfTestOption = value; RaisePropertyChanged(() => SelfTestOption);}
        }

        public CategoryItem SelectedSelfTest
        {
            get { return _selectedSelfTest; }
            set { _selectedSelfTest = value; RaisePropertyChanged(() => SelectedSelfTest); }
        }

        public List<CategoryItem> SelfTestOptions
        {
            get { return _selfTestOptions; }
            set { _selfTestOptions = value; RaisePropertyChanged(() => SelfTestOptions); }
        }

        public List<CategoryItem> Kits
        {
            get { return _kits; }
            set { _kits = value; RaisePropertyChanged(() => Kits);}
        }

        public IMvxCommand SaveTestingCommand
        {
            get
            {
                _saveTestingCommand = _saveTestingCommand ?? new MvxCommand(SaveTesting);
                return _saveTestingCommand;
            }
        }

        private void SaveTesting()
        {
            if (Validate())
            {

            }
        }

        public event EventHandler<ChangedDateEvent> ChangedDate;


        public ObservableDictionary<string, string> Errors
        {
            get { return _errors; }
            set { _errors = value; RaisePropertyChanged(() => Errors); }
        }

        public ExpiryDateDTO SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                _selectedDate = value;
                RaisePropertyChanged(() => SelectedDate);
                UpdateExpiryDate(SelectedDate);
            }
        }

       
        public TestingViewModel(ILookupService lookupService, IDashboardService dashboardService, IHIVTestingService testingService, ISettings settings)
        {
            _lookupService = lookupService;
            _dashboardService = dashboardService;
            _testingService = testingService;
            _settings = settings;
            EnableFinalResult = false;
            FirstTestEpisodeViewModel = new FirstTestEpisodeViewModel();
            FirstTestEpisodeViewModel.Parent = this;
            SecondTestEpisodeViewModel = new SecondTestEpisodeViewModel();
            SecondTestEpisodeViewModel.Parent = this;
            Validator=new ValidationHelper();
        }


        public void Init(string formId,string encounterTypeId, string mode, string clientId, string encounterId)
        {
            var results = _lookupService.GetCategoryItems("TestResult", true, "[Select Result]").ToList();
            SecondTestEpisodeViewModel.TestResults = FirstTestEpisodeViewModel.TestResults = results;

            FinalTestResults = _lookupService.GetCategoryItems("FinalResult", true).ToList();
            ResultGivenOptions = _lookupService.GetCategoryItems("YesNo", true).ToList();
            CoupleDiscordantOptions = _lookupService.GetCategoryItems("YesNoNa", true).ToList();
            SelfTestOptions = _lookupService.GetCategoryItems("YesNo", true).ToList();

            Kits = _lookupService.GetCategoryItems("KitName", true, "[Select Kit]").ToList();
            
            EncounterType = _lookupService.GetDefaultEncounterType(new Guid(encounterTypeId));

            _settings.AddOrUpdateValue("lookup.TestResult", JsonConvert.SerializeObject(results));
            _settings.AddOrUpdateValue("lookup.FinalResult", JsonConvert.SerializeObject(FinalTestResults));
            _settings.AddOrUpdateValue("lookup.ResultGivenOptions", JsonConvert.SerializeObject(ResultGivenOptions));
            _settings.AddOrUpdateValue("lookup.CoupleDiscordantOptions", JsonConvert.SerializeObject(CoupleDiscordantOptions));
            _settings.AddOrUpdateValue("lookup.SelfTestOptions", JsonConvert.SerializeObject(SelfTestOptions));
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
                Encounter = _testingService.StartEncounter(new Guid(formId), EncounterType.Id, Client.Id, Guid.Empty, Guid.Empty);
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

            //RaisePropertyChanged(() => FirstHIVTestViewModel.FirstTestName);
        }

        public void ShowDatePicker(Guid refId, DateTime refDate)
        {
            OnChangedDate(new ChangedDateEvent(refId, refDate));
        }

        public void SaveTest(ObsTestResult test)
        {

            _testingService.SaveTest(test);
            Encounter = _testingService.OpenEncounter(Encounter.Id);
        }

        public void DeleteTest(ObsTestResult test)
        {
            _testingService.DeleteTest(test);
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

            //Results Given
            var given = SelectedResultGiven.ItemId;

            Validator.AddRule(
                "Result Given",
                () => RuleResult.Assert(
                    !given.IsNullOrEmpty(),
                    $"Result Given is required"
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

        private void LoadTests()
        {
          
            
            if (null != Encounter)
            {
                var tests= Encounter.ObsTestResults.Where(x => x.TestName == FirstTestEpisodeViewModel.TestName).ToList();
                foreach (var obsTestResult in tests)
                {
                    try
                    {
                        obsTestResult.ResultDisplay = FirstTestEpisodeViewModel.TestResults
                            .FirstOrDefault(x => x.ItemId == obsTestResult.Result).Display;

                        obsTestResult.KitDisplay = Kits.FirstOrDefault(x => x.ItemId == obsTestResult.Kit).Display;
                    }
                    catch (Exception e)
                    {
                    }
                      
                }
                FirstTestEpisodeViewModel.Tests = tests;
                

                var finalTestResult = Encounter.ObsFinalTestResults.ToList().FirstOrDefault();

                if (null != finalTestResult)
                {
                    var result = FirstTestEpisodeViewModel.TestResults.FirstOrDefault(x => x.ItemId == finalTestResult.FirstTestResult);
                    if (null != result)
                    {
                        FirstTestEpisodeViewModel.SelectedTestResult = result;
                    }
                    else
                    {
                        FirstTestEpisodeViewModel.SelectedTestResult = FirstTestEpisodeViewModel.TestResults.OrderBy(x => x.Rank).FirstOrDefault();
                    }
                }

                var tests2 = Encounter.ObsTestResults.Where(x => x.TestName == SecondTestEpisodeViewModel.TestName).ToList();
                foreach (var obsTestResult in tests2)
                {
                    try
                    {
                        obsTestResult.ResultDisplay = FirstTestEpisodeViewModel.TestResults
                            .FirstOrDefault(x => x.ItemId == obsTestResult.Result).Item.Display;

                        obsTestResult.KitDisplay = Kits.FirstOrDefault(x => x.ItemId == obsTestResult.Kit).Display;
                    }
                    catch (Exception e)
                    {
                    }

                }

                SecondTestEpisodeViewModel.Tests = tests2;

                var finalSecondResult = Encounter.ObsFinalTestResults.ToList().FirstOrDefault();

                if (null != finalSecondResult)
                {
                    var result = SecondTestEpisodeViewModel.TestResults.FirstOrDefault(x => x.ItemId == finalSecondResult.SecondTestResult);
                    if (null != result)
                    {
                        SecondTestEpisodeViewModel.SelectedTestResult = result;
                    }
                    else
                    {
                        SecondTestEpisodeViewModel.SelectedTestResult = SecondTestEpisodeViewModel.TestResults.OrderBy(x => x.Rank).FirstOrDefault();
                    }
                }


                var finalResult = Encounter.ObsFinalTestResults.ToList().FirstOrDefault();

                if (null != finalResult)
                {
                    var result = FinalTestResults.FirstOrDefault(x => x.ItemId == finalResult.FinalResult);
                    if (null != result)
                    {
                        SelectedFinalTestResult = result;
                    }
                    else
                    {
                        SelectedFinalTestResult = FinalTestResults.OrderBy(x => x.Rank).FirstOrDefault();
                    }
                }
            }
            
        }

      
  
        protected virtual void OnChangedDate(ChangedDateEvent e)
        {
            ChangedDate?.Invoke(this, e);
        }
        private void UpdateExpiryDate(ExpiryDateDTO selectedDate)
        {
//            var test1 = FirstHIVTestViewModel.FirstTests.FirstOrDefault(x => x.HIVTestTemplate.Id == selectedDate.Id);
//            if (null != test1)
//            {
//                test1.HIVTestTemplate.Expiry = selectedDate.EventDate;
//                return;
//            }
//
//            var test2 = SecondHIVTestViewModel.SecondTests.FirstOrDefault(x => x.HIVTestTemplate.Id == selectedDate.Id);
//            if (null != test2)
//            {
//                test2.HIVTestTemplate.Expiry = selectedDate.EventDate;
//            }
        }

        public void GoBack()
        {
            ShowViewModel<DashboardViewModel>();
        }
    }
}