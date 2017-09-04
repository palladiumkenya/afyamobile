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
using LiveHTS.Presentation.Interfaces;
using LiveHTS.Presentation.Interfaces.ViewModel;
using LiveHTS.Presentation.ViewModel.Template;
using LiveHTS.Presentation.ViewModel.Wrapper;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace LiveHTS.Presentation.ViewModel
{
    public class SecondTestEpisodeViewModel : MvxViewModel, ITestEpisodeViewModel
    {
        private readonly IHIVTestingService _testingService;
        private readonly IDialogService _dialogService;

        private string _testName = "HIV Test 2";
        private List<ObsTestResult> _tests = new List<ObsTestResult>();
        private IMvxCommand _addTestCommand;
        
        private ITestingViewModel _parent;
        
        private IMvxCommand _closeTestCommand;
        
        
        private List<TestTemplateWrap> _hivTests;
        private ObsTestResult _test;
        private bool _enableResult;

        public ITestingViewModel Parent
        {
            get { return _parent; }
            set { _parent = value; }
        }

        public bool EnableResult
        {
            get { return _enableResult; }
            set { _enableResult = value; }
        }

        public string TestName
        {
            get { return _testName; }
            set
            {
                _testName = value; RaisePropertyChanged(() => TestName);
            }
        }

        public List<ObsTestResult> Tests
        {
            get { return _tests; }
            set
            {
                _tests = value; RaisePropertyChanged(() => Tests);
                AddTestCommand.RaiseCanExecuteChanged();
                HivTests = ConvertToWrapperClass(Tests, this);
            }
        }

        public ObsTestResult Test
        {
            get { return _test; }
            set { _test = value; RaisePropertyChanged(() => Test); }
        }

        public List<TestTemplateWrap> HivTests
        {
            get { return _hivTests; }
            set { _hivTests = value; RaisePropertyChanged(() => HivTests); }
        }
     
  

        public IMvxCommand CloseTestCommand
        {
            get
            {
                _closeTestCommand = _closeTestCommand ?? new MvxCommand(CloseTest);
                return _closeTestCommand;
            }
        }


        private void CloseTest()
        {
            CloseTestCommandAction?.Invoke();
            
        }

        public Action AddTestCommandAction { get; set; }

        public Action EditTestCommandAction { get; set; }


        public Action CloseTestCommandAction { get; set; }

        public IMvxCommand AddTestCommand
        {
            get
            {
                _addTestCommand = _addTestCommand ?? new MvxCommand(AddTest, CanAddTest);
                return _addTestCommand;
            }
        }

        public SecondTestEpisodeViewModel()
        {
            _testingService = Mvx.Resolve<IHIVTestingService>();
            _dialogService = Mvx.Resolve<IDialogService>();
            EnableResult = false;
        }
 
        public async void DeleteTest(ObsTestResult testResult)
        {
            if (null != testResult)
            {
                var result = await _dialogService.ConfirmAction("Are you sure ?", "Delete this Test");
                if (result)
                {

                    _testingService.DeleteTest(testResult);
                    Parent.Referesh(testResult.EncounterId);
                }
            }
        }
        private bool CanAddTest()
        {
            if (Parent.HasFinalResult)
                return false;

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

        private void AddTest()
        {
            AddTestCommandAction?.Invoke();
        }
        public void EditTest(ObsTestResult testResult)
        {
            Test = testResult;
            EditTestCommandAction?.Invoke();
        }

        private static List<TestTemplateWrap> ConvertToWrapperClass(List<ObsTestResult> forms, ITestEpisodeViewModel clientDashboardViewModel)
        {
            List<TestTemplateWrap> list = new List<TestTemplateWrap>();
            foreach (var r in forms)
            {
                var f = new TestTemplate(r);
                var fw = new TestTemplateWrap(clientDashboardViewModel, f);
                list.Add(fw);
            }
            return list;
        }

    }
}