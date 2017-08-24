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
using LiveHTS.Presentation.Interfaces.ViewModel;
using LiveHTS.Presentation.ViewModel.Template;
using LiveHTS.Presentation.ViewModel.Wrapper;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.ViewModel
{
    public class ClientHIVTestViewModel:MvxViewModel, IClientHIVTestViewModel
    {
        private Client _client;
        private string _firstTestName = "HIV Test 1";
        private ObservableCollection<HIVTestTemplateWrap> _hivTests=new ObservableCollection<HIVTestTemplateWrap>();
        private readonly ISettings _settings;
        private readonly IDashboardService _dashboardService;
        private readonly ILookupService _lookupService;
        private readonly IHIVTestingService _testingService;
        private Guid _encounterTypeId;
        private IMvxCommand _addTestCommand;
        private Encounter _encounter;
        private IMvxCommand _saveCommand;



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
            get { return _hivTests; }
            set { _hivTests = value; RaisePropertyChanged(() => FirstTests); }
        }

        public IMvxCommand SaveChangesCommand
        {
            get
            {
                _saveCommand = _saveCommand ?? new MvxCommand(SaveChanges, CanSaveChanges);
                return _saveCommand;
            }
        }

        private bool CanSaveChanges()
        {
            throw new NotImplementedException();
        }

        private void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public IMvxCommand AddTestCommand
        {
            get
            {
                _addTestCommand = _addTestCommand ?? new MvxCommand(AddTest, CanAddTest);
                return _addTestCommand;
            }
        }

        private bool CanAddTest()
        {
            return true;
        }

        private void AddTest()
        {
            int lastAttempt = FirstTests.Max(x => x.HIVTestTemplate.Attempt);
            lastAttempt++;
            var obs = ObsTestResult.CreateNew(FirstTestName, lastAttempt, Encounter.Id);

            var list = Encounter.ObsTestResults.ToList();
            list.Add(obs);
            Encounter.ObsTestResults = list;
            Encounter = Encounter;
        }

        public void SaveTest(ObsTestResult test)
        {
            _testingService.SaveTest(test);
            Encounter = _testingService.OpenEncounter(Client.Id);
        }

        public void DeleteTest(ObsTestResult test)
        {
            _testingService.DeleteTest(test);
            Encounter = _testingService.OpenEncounter(Client.Id);
        }

        public void RefreshTest()
        {
            throw new NotImplementedException();
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
            // Load Client
            Client = _dashboardService.LoadClient(new Guid(clientId));

            // Load or Create Encounter

            EncounterTypeId = new Guid(encounterTypeId);

            if (mode == "new")
            {
                //  New Encounter
                _settings.AddOrUpdateValue("client.test.mode", "new");
                Encounter = _testingService.StartEncounter(EncounterTypeId,Client.Id,Guid.Empty, Guid.Empty);
            }
            else
            {
                //  Load Encounter
                _settings.AddOrUpdateValue("client.test.mode", "open");
                Encounter = _testingService.OpenEncounter(Client.Id);
            }

            if (null == Encounter)
            {
                throw new ArgumentException("Encounter has not been Initialized");
            }
            RaisePropertyChanged(() => FirstTestName);
        }

        private void LoadTests()
        {
            var kits = _lookupService.GetCategoryItems("KitName").ToList();
            var results = _lookupService.GetCategoryItems("TestResult").ToList();

            if (null != Encounter)
            {
                FirstTests = ConvertToHIVTestWrapperClass(this, Encounter, kits, results);
            }
        }

        private static ObservableCollection<HIVTestTemplateWrap> ConvertToHIVTestWrapperClass( IClientHIVTestViewModel clientDashboardViewModel, Encounter encounter, List<CategoryItem> kits, List<CategoryItem> results)
        {
            ObservableCollection<HIVTestTemplateWrap> list = new ObservableCollection<HIVTestTemplateWrap>();

            var testResults = encounter.ObsTestResults.ToList();

            if (testResults.Count == 0)
            {
                testResults = new List<ObsTestResult>();
                testResults.Add(ObsTestResult.CreateNew(clientDashboardViewModel.FirstTestName,1,encounter.Id));
            }
            foreach (var r in testResults)
            {
                list.Add(new HIVTestTemplateWrap(clientDashboardViewModel, new HIVTestTemplate(r,kits,results)));
            }

            return list;
        }
    }
}