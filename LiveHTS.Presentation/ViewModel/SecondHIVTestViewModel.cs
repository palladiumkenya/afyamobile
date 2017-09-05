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
    public class SecondHIVTestViewModel : MvxViewModel, ISecondHIVTestViewModel
    {
        private string _SecondTestName = "HIV Test 1";
        private List<ObsTestResult> _SecondTests = new List<ObsTestResult>();
        private IMvxCommand _addTestCommand;
        private CategoryItem _selectedSecondTestResult;
        private List<CategoryItem> _SecondTestResults;
        private ITestingViewModel _parent;

        public ITestingViewModel Parent
        {
            get { return _parent; }
            set { _parent = value; }
        }

        public string SecondTestName
        {
            get { return _SecondTestName; }
            set { _SecondTestName = value; RaisePropertyChanged(() => SecondTestName); }
        }

        public List<ObsTestResult> SecondTests
        {
            get { return _SecondTests; }
            set
            {
                _SecondTests = value; RaisePropertyChanged(() => SecondTests);
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
            get { return _SecondTestResults; }
            set { _SecondTestResults = value; RaisePropertyChanged(() => SecondTestResults); }
        }
        public Action AddTestCommandAction { get; set; }

        public IMvxCommand AddSecondTestCommand
        {
            get
            {
                _addTestCommand = _addTestCommand ?? new MvxCommand(AddSecondTest, CanAddSecondTest);
                return _addTestCommand;
            }
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
                if (SecondTests.Count > 0 && SecondTests.Any(x => x.Result == Guid.Empty))
                    return false;

                //Has invalid
                if (
                    SecondTests.Count > 0 &&
                    SecondTests.Any(x => x.ResultCode == "P" ||
                                        x.ResultCode == "N")

                )
                    return false;
            }


            return true;
        }

        private void AddSecondTest()
        {
            AddTestCommandAction?.Invoke();
            //            int lastAttempt = SecondTests.Max(x => x.HIVTestTemplate.Attempt);
            //            lastAttempt++;
            //            var obs = ObsTestResult.CreateNew(SecondTestName, lastAttempt, Parent.Encounter.Id);
            //
            //            var list = Parent.Encounter.ObsTestResults.ToList();
            //            list.Add(obs);
            //            Parent.Encounter.ObsTestResults = list;
            //            Parent.Encounter = Parent.Encounter;
        }



    }
}