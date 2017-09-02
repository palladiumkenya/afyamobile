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
using LiveHTS.Presentation.DTO;
using LiveHTS.Presentation.Interfaces.ViewModel;
using LiveHTS.Presentation.ViewModel.Wrapper;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.ViewModel
{
    public class SecondTestEpisodeViewModel : MvxViewModel, ITestEpisodeViewModel
    {
        private string _testName = "HIV Test 2";
        private List<ObsTestResult> _tests = new List<ObsTestResult>();
        private IMvxCommand _addTestCommand;
        private CategoryItem _selectedTestResult;
        private List<CategoryItem> _testResults;
        private ITestingViewModel _parent;
        private string _testNameResult;
        private IMvxCommand _closeTestCommand;
        private ObsTestResult _selectedTest;
        private IMvxCommand<ObsTestResult> _editTestCommand;
        private IMvxCommand _deleteTestCommand;
        private IMvxCommand<ObsTestResult> _selectedTestCommand;
        private List<TestTemplateWrap> _hivTests;

        public ITestingViewModel Parent
        {
            get { return _parent; }
            set { _parent = value; }
        }

        public string TestName
        {
            get { return _testName; }
            set
            {
                _testName = value; RaisePropertyChanged(() => TestName);
                TestNameResult = $"{TestName} Result";
            }
        }

        public string TestNameResult
        {
            get { return _testNameResult; }
            set { _testNameResult = value; RaisePropertyChanged(() => TestNameResult); }
        }
        public List<ObsTestResult> Tests
        {
            get { return _tests; }
            set
            {
                _tests = value; RaisePropertyChanged(() => Tests);
                AddTestCommand.RaiseCanExecuteChanged();
            }
        }

        public List<TestTemplateWrap> HivTests
        {
            get { return _hivTests; }
            set { _hivTests = value; }
        }


        public CategoryItem SelectedTestResult
        {
            get { return _selectedTestResult; }
            set { _selectedTestResult = value; RaisePropertyChanged(() => SelectedTestResult); }
        }

        public List<CategoryItem> TestResults
        {
            get { return _testResults; }
            set { _testResults = value; RaisePropertyChanged(() => TestResults); }
        }

        public IMvxCommand CloseTestCommand
        {
            get
            {
                _closeTestCommand = _closeTestCommand ?? new MvxCommand(CloseTest);
                return _closeTestCommand;
            }
        }

        public IMvxCommand<ObsTestResult> SelectedTestCommand
        {
            get { return _selectedTestCommand; }
        }

        public IMvxCommand<ObsTestResult> EditTestCommand
        {
            get { return _editTestCommand; }
        }

        public IMvxCommand DeleteTestCommand
        {
            get { return _deleteTestCommand; }
        }

        private void CloseTest()
        {
            CloseTestCommandAction?.Invoke();
        }

        public Action AddTestCommandAction { get; set; }

        public Action CloseTestCommandAction { get; set; }
        public void DeleteTest(ObsTestResult testResult)
        {
            throw new NotImplementedException();
        }

        public void EditTest(ObsTestResult testResult)
        {
            throw new NotImplementedException();
        }

        public IMvxCommand AddTestCommand
        {
            get
            {
                _addTestCommand = _addTestCommand ?? new MvxCommand(AddFirstTest, CanAddFirstTest);
                return _addTestCommand;
            }
        }

        private bool CanAddFirstTest()
        {
            //No Tests
            if (null == Tests)
                return true;

            if (null != Tests)
            {
                //No Tests
                if (Tests.Count == 0)
                    return true;

                //Is initial add
                if (Tests.Count > 0 && Tests.Any(x => x.Result == Guid.Empty))
                    return false;

                //Has invalid
                if (
                    Tests.Count > 0 &&
                    Tests.Any(x => x.ResultCode == "P" ||
                                        x.ResultCode == "N")

                )
                    return false;
            }


            return true;
        }

        private void AddFirstTest()
        {
            AddTestCommandAction?.Invoke();
            //            int lastAttempt = FirstTests.Max(x => x.HIVTestTemplate.Attempt);
            //            lastAttempt++;
            //            var obs = ObsTestResult.CreateNew(FirstTestName, lastAttempt, Parent.Encounter.Id);
            //
            //            var list = Parent.Encounter.ObsTestResults.ToList();
            //            list.Add(obs);
            //            Parent.Encounter.ObsTestResults = list;
            //            Parent.Encounter = Parent.Encounter;
        }



    }
}