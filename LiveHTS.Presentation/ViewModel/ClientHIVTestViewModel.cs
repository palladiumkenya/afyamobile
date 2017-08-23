using System;
using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core.Interfaces.Services.Clients;
using LiveHTS.Core.Interfaces.Services.Config;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Presentation.Interfaces.ViewModel;
using LiveHTS.Presentation.ViewModel.Widget;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.ViewModel
{
    public class ClientHIVTestViewModel:MvxViewModel, IClientHIVTestViewModel
    {
        private Client _client;
        private string _firstTestName;
        private List<ObsTestResult> _firstTests=new List<ObsTestResult>();
        private readonly IDashboardService _dashboardService;
        private readonly  ILookupService _lookupService;
        public Client Client
        {
            get { return _client; }
            set { _client = value; RaisePropertyChanged(() => Client); }
        }

        public string FirstTestName
        {
            get { return _firstTestName; }
            set { _firstTestName = value; RaisePropertyChanged(() => FirstTestName); }
        }

        public List<ObsTestResult> FirstTests
        {
            get { return _firstTests; }
            set { _firstTests = value; RaisePropertyChanged(() => FirstTests); }
        }

        public void SaveTest(Test test)
        {
            throw new NotImplementedException();
        }

        public void DeleteTest(Test test)
        {
            throw new NotImplementedException();
        }

        public void RefreshTest()
        {
            throw new NotImplementedException();
        }

        public ClientHIVTestViewModel(ILookupService lookupService, IDashboardService dashboardService)
        {
            _lookupService = lookupService;
            _dashboardService = dashboardService;

            Client = _dashboardService.LoadClient(new Guid("4547b7e0-98c7-4c6f-9d2a-a7b7016df232"));
            InitTest();
        }

        private void InitTest()
        {
            /*
                KitName
                TestResult
                FinalResult
             */

            var kits = _lookupService.GetCategoryItems("KitName").ToList();
            var testResults = _lookupService.GetCategoryItems("TestResult").ToList();

            var newTest = Test.Create(1, kits, testResults);
            newTest.Parent = this;
            var tests=new List<Test>();
            tests.Add(newTest);
            FirstTests = tests;
        }
    }
}