using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Cheesebaron.MvxPlugins.Settings.Interfaces;
using LiveHTS.Core.Interfaces.Services.Clients;
using LiveHTS.Core.Interfaces.Services.Config;
using LiveHTS.Core.Interfaces.Services.Interview;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Lookup;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Core.Model.Survey;
using LiveHTS.Presentation.DTO;
using LiveHTS.Presentation.Events;
using LiveHTS.Presentation.Interfaces.ViewModel;
using LiveHTS.Presentation.ViewModel.Template;
using LiveHTS.Presentation.ViewModel.Wrapper;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform.Platform;

namespace LiveHTS.Presentation.ViewModel
{
    public class ClientHIVTestViewModel:MvxViewModel, IClientHIVTestViewModel
    {
        private Client _client;
        private string _firstTestName = "HIV Test 1";
        private ObservableCollection<HIVTestTemplateWrap> _firstTests=new ObservableCollection<HIVTestTemplateWrap>();
        private readonly ISettings _settings;
        private readonly IDashboardService _dashboardService;
        private readonly ILookupService _lookupService;
        private readonly IHIVTestingService _testingService;
        private Guid _encounterTypeId;
        private IMvxCommand _addTestCommand;
        private Encounter _encounter;
        private IMvxCommand _saveCommand;
        private CategoryItem _selectedFirstTestResult;
        private List<CategoryItem> _firstTestResults;
        private string _secondTestName= "HIV Test 2";
        private ObservableCollection<HIVTestTemplateWrap> _secondTests;
        private CategoryItem _selectedSecondTestResult;
        private List<CategoryItem> _secondTestResults;
        private IMvxCommand _addSecondTestCommand;
        private CategoryItem _selectedFinalTestResult;
        private List<CategoryItem> _finalTestResults;
        private IMvxCommand _showDateCommand;
        private IMvxCommand _showDateDialogCommand;
        private ExpiryDateDTO _selectedDate;

        public Guid EncounterTypeId
        {
            get { return _encounterTypeId; }
            set { _encounterTypeId = value; }
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
     
        public string FirstTestName
        {
            get { return _firstTestName; }
            set { _firstTestName = value; RaisePropertyChanged(() => FirstTestName); }
        }

        public ObservableCollection<HIVTestTemplateWrap> FirstTests
        {
            get { return _firstTests; }
            set
            {
                _firstTests = value; RaisePropertyChanged(() => FirstTests);
                AddFirstTestCommand.RaiseCanExecuteChanged();
            }
        }

        public CategoryItem SelectedFirstTestResult
        {
            get { return _selectedFirstTestResult; }
            set { _selectedFirstTestResult = value; RaisePropertyChanged(() => SelectedFirstTestResult);}
        }

        public List<CategoryItem> FirstTestResults
        {
            get { return _firstTestResults; }
            set { _firstTestResults = value; RaisePropertyChanged(() => FirstTestResults);}
        }

        public string SecondTestName
        {
            get { return _secondTestName; }
            set { _secondTestName = value; RaisePropertyChanged(() => SecondTestName);}
        }

        public ObservableCollection<HIVTestTemplateWrap> SecondTests
        {
            get { return _secondTests; }
            set
            {
                _secondTests = value; RaisePropertyChanged(() => SecondTests);
                AddSecondTestCommand.RaiseCanExecuteChanged();
            }
        }

        public CategoryItem SelectedSecondTestResult
        {
            get { return _selectedSecondTestResult; }
            set { _selectedSecondTestResult = value; RaisePropertyChanged(() => SelectedSecondTestResult); }
        }

        public List<CategoryItem> SecondTestResults
        {
            get { return _secondTestResults; }
            set { _secondTestResults = value; RaisePropertyChanged(() => SecondTestResults); }
        }

        public CategoryItem SelectedFinalTestResult
        {
            get { return _selectedFinalTestResult; }
            set { _selectedFinalTestResult = value; RaisePropertyChanged(() => SelectedFinalTestResult);}
        }

        public List<CategoryItem> FinalTestResults
        {
            get { return _finalTestResults; }
            set { _finalTestResults = value; RaisePropertyChanged(() => FinalTestResults);}
        }

        public IMvxCommand AddFirstTestCommand
        {
            get
            {
                _addTestCommand = _addTestCommand ?? new MvxCommand(AddFirstTest, CanAddFirstTest);
                return _addTestCommand;
            }
        }

        public IMvxCommand AddSecondTestCommand
        {
            get
            {
                _addSecondTestCommand = _addSecondTestCommand ?? new MvxCommand(AddSecondTest, CanAddSecondTest);
                return _addSecondTestCommand;
            }
        }

        public event EventHandler<ChangedDateEvent> ChangedDate;
        

        

        public ExpiryDateDTO SelectedDate
        {
            get { return _selectedDate; }
            set { _selectedDate = value;
                RaisePropertyChanged(() => SelectedDate);
                UpdateExpiryDate(SelectedDate);
            }
        }

        private void UpdateExpiryDate(ExpiryDateDTO selectedDate)
        {
            var test1 = FirstTests.FirstOrDefault(x => x.HIVTestTemplate.Id == selectedDate.Id);
            if (null != test1)
            {
                test1.HIVTestTemplate.Expiry = selectedDate.EventDate;
                return;
            }

            var test2 = SecondTests.FirstOrDefault(x => x.HIVTestTemplate.Id == selectedDate.Id);
            if (null != test2)
            {
                test2.HIVTestTemplate.Expiry =selectedDate.EventDate;
            }
        }

        public ClientHIVTestViewModel(ILookupService lookupService, IDashboardService dashboardService, IHIVTestingService testingService, ISettings settings)
        {
            _lookupService = lookupService;
            _dashboardService = dashboardService;
            _testingService = testingService;
            _settings = settings;
        }


        public void Init(string encounterTypeId, string mode, string clientId, string encounterId)
        {
            SecondTestResults= FirstTestResults = _lookupService.GetCategoryItems("TestResult", true, "").ToList();
            FinalTestResults= _lookupService.GetCategoryItems("FinalResult", true, "").ToList();

            // Load Client
            Client = _dashboardService.LoadClient(new Guid(clientId));

            // Load or Create Encounter

            EncounterTypeId = new Guid(encounterTypeId);

            if (mode == "new")
            {
                //  New Encounter
                _settings.AddOrUpdateValue("client.test.mode", "new");
                Encounter = _testingService.StartEncounter(Guid.Empty, EncounterTypeId, Client.Id, Guid.Empty, Guid.Empty);
            }
            else
            {
                //  Load Encounter
                _settings.AddOrUpdateValue("client.test.mode", "open");
                Encounter = _testingService.OpenEncounter(Encounter.Id);
            }

            if (null == Encounter)
            {
                throw new ArgumentException("Encounter has not been Initialized");
            }

            RaisePropertyChanged(() => FirstTestName);
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

        public void RefreshTest()
        {
            throw new NotImplementedException();
        }

        private void LoadTests()
        {
            var kits = _lookupService.GetCategoryItems("KitName", true, "[Select Kit]").ToList();
            var results = _lookupService.GetCategoryItems("TestResult", true, "[Select Result]").ToList();

            if (null != Encounter)
            {
                FirstTests = ConvertToHIVTestWrapperClass(this, Encounter,FirstTestName, kits, results);

                var finalTestResult = Encounter.ObsFinalTestResults.ToList().FirstOrDefault();

                if (null != finalTestResult)
                {
                    var result = FirstTestResults.FirstOrDefault(x => x.ItemId == finalTestResult.FirstTestResult);
                    if (null != result)
                    {
                        SelectedFirstTestResult = result;
                    }
                    else
                    {
                        SelectedFirstTestResult = FirstTestResults.OrderBy(x => x.Rank).FirstOrDefault();
                    }
                }

                SecondTests = ConvertToHIVTestWrapperClass(this, Encounter, SecondTestName, kits, results);

               var finalSecondResult = Encounter.ObsFinalTestResults.ToList().FirstOrDefault();

                if (null != finalSecondResult)
                {
                    var result = SecondTestResults.FirstOrDefault(x => x.ItemId == finalSecondResult.SecondTestResult);
                    if (null != result)
                    {
                        SelectedSecondTestResult = result;
                    }
                    else
                    {
                        SelectedSecondTestResult = SecondTestResults.OrderBy(x => x.Rank).FirstOrDefault();
                    }
                }


                var finalResult = Encounter.ObsFinalTestResults.ToList().FirstOrDefault();

                if (null != finalResult)
                {
                    var result = FinalTestResults.FirstOrDefault(x => x.ItemId == finalResult.EndResult);
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

        private bool CanAddFirstTest()
        {
            //No Tests
            if (null == FirstTests)
                return true;

            if (null != FirstTests)
            {
                //No Tests
                if (FirstTests.Count == 0)
                    return true;

                //Is initial add
                if (FirstTests.Count > 0 && FirstTests.Any(x => x.HIVTestTemplate.Result == Guid.Empty))
                    return false;

                //Has invalid
                if (
                    FirstTests.Count > 0 && 
                    FirstTests.Any(x => x.HIVTestTemplate.SelectedResult.Item.Code =="P"||
                                        x.HIVTestTemplate.SelectedResult.Item.Code == "N")
                    
                    )
                    return false;
            }
                

            return true;
        }

        private void AddFirstTest()
        {
            int lastAttempt = FirstTests.Max(x => x.HIVTestTemplate.Attempt);
            lastAttempt++;
            var obs = ObsTestResult.CreateNew(FirstTestName, lastAttempt, Encounter.Id);

            var list = Encounter.ObsTestResults.ToList();
            list.Add(obs);
            Encounter.ObsTestResults = list;
            Encounter = Encounter;
        }

        private void AddSecondTest()
        {
            int lastAttempt = SecondTests.Max(x => x.HIVTestTemplate.Attempt);
            lastAttempt++;
            var obs = ObsTestResult.CreateNew(SecondTestName, lastAttempt, Encounter.Id);

            var list = Encounter.ObsTestResults.ToList();
            list.Add(obs);
            Encounter.ObsTestResults = list;
            Encounter = Encounter;
        }

        private bool CanAddSecondTest()
        {
            //No Tests
            if (null == SecondTests)
                return true;

            if (null != SecondTests)
            {
                //No Tests
                if (SecondTests.Count == 0)
                    return true;

                //Is initial add
                if (SecondTests.Count > 0 && SecondTests.Any(x => x.HIVTestTemplate.Result == Guid.Empty))
                    return false;

                //Has invalid
                if (
                    SecondTests.Count > 0 &&
                    SecondTests.Any(x => x.HIVTestTemplate.SelectedResult.Item.Code == "P" ||
                                        x.HIVTestTemplate.SelectedResult.Item.Code == "N")

                )
                    return false;
            }


            return true;
        }


        private static ObservableCollection<HIVTestTemplateWrap> ConvertToHIVTestWrapperClass( IClientHIVTestViewModel clientDashboardViewModel, Encounter encounter, string testname,List<CategoryItem> kits, List<CategoryItem> results)
        {
            ObservableCollection<HIVTestTemplateWrap> list = new ObservableCollection<HIVTestTemplateWrap>();

            var testResults = encounter.ObsTestResults.Where(x => x.TestName == testname).ToList();

            if (testResults.Count == 0)
            {
                testResults = new List<ObsTestResult>();
                testResults.Add(ObsTestResult.CreateNew(testname, 1,encounter.Id));
            }
            foreach (var r in testResults)
            {
              //  list.Add(new HIVTestTemplateWrap(clientDashboardViewModel, new HIVTestTemplate(r,kits,results)));
            }

            return list;
        }

      
        protected virtual void OnChangedDate(ChangedDateEvent e)
        {
            ChangedDate?.Invoke(this, e);
        }
    }
}