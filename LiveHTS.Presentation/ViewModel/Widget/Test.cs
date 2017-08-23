using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Lookup;
using LiveHTS.Presentation.Interfaces.ViewModel;
using LiveHTS.SharedKernel.Custom;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.ViewModel.Widget
{
    public class Test : MvxNotifyPropertyChanged
    {
        private CategoryItem _selectedTestKit;
        private List<CategoryItem> _testKits;
        private bool _showOtherTestKit;
        private string _otherTestKit;
        private string _lotNumber;
        private DateTime _expiry;
        private List<CategoryItem> _testResults;
        private CategoryItem _selectedTestResult;
        private IMvxCommand<Test> _saveTestCommand;
        private IMvxCommand<Test> _deleteTestCommand;
        private int _attempt;
        private string _testName;
        private Guid _id;

        public IClientHIVTestViewModel Parent { get; set; }

        public Guid Id
        {
            get { return _id; }
            set { _id = value; RaisePropertyChanged(() => Id); }
        }

        public string TestName
        {
            get { return _testName; }
            set { _testName = value; RaisePropertyChanged(() => TestName); }
        }

        public int Attempt
        {
            get { return _attempt; }
            set
            {
                _attempt = value;
                RaisePropertyChanged(() => Attempt);
            }
        }

        public List<CategoryItem> TestKits
        {
            get { return _testKits; }
            set
            {
                _testKits = value;
                RaisePropertyChanged(() => TestKits);
            }
        }

        public CategoryItem SelectedTestKit
        {
            get { return _selectedTestKit; }
            set
            {
                _selectedTestKit = value;
                RaisePropertyChanged(() => SelectedTestKit);
                ShowOther();
            }
        }

        public bool ShowOtherTestKit
        {
            get { return _showOtherTestKit; }
            set
            {
                _showOtherTestKit = value;
                RaisePropertyChanged(() => ShowOtherTestKit);
            }
        }

        public string OtherTestKit
        {
            get { return _otherTestKit; }
            set
            {
                _otherTestKit = value;
                RaisePropertyChanged(() => OtherTestKit);
            }
        }

        public string LotNumber
        {
            get { return _lotNumber; }
            set
            {
                _lotNumber = value;
                RaisePropertyChanged(() => LotNumber);
            }
        }

        public DateTime Expiry
        {
            get { return _expiry; }
            set
            {
                _expiry = value;
                RaisePropertyChanged(() => Expiry);
            }
        }

        public List<CategoryItem> TestResults
        {
            get { return _testResults; }
            set
            {
                _testResults = value;
                RaisePropertyChanged(() => TestResults);
            }
        }

        public CategoryItem SelectedTestResult
        {
            get { return _selectedTestResult; }
            set
            {
                _selectedTestResult = value;
                RaisePropertyChanged(() => SelectedTestResult);
            }
        }
        public IMvxCommand<Test> SaveTestCommand
        {
            get
            {
                _saveTestCommand = _saveTestCommand ?? new MvxCommand<Test>(SaveTest, CanSaveTest);
                return _saveTestCommand;
            }
        }
        public IMvxCommand<Test> DeleteTestCommand
        {
            get
            {
                _deleteTestCommand = _deleteTestCommand ?? new MvxCommand<Test>(DeleteTest, CanDeleteTest);
                return _deleteTestCommand;
            }
        }

        private bool CanSaveTest(Test arg)
        {
            return true;
        }

        private void SaveTest(Test obj)
        {
            obj = this;
            Parent.SaveTest(obj);
        }
        private bool CanDeleteTest(Test arg)
        {
            return true;
        }

        private void DeleteTest(Test obj)
        {
            obj = this;
            Parent.DeleteTest(obj);
        }


        private Test(int attempt, List<CategoryItem> testKits, List<CategoryItem> testResults)
        {
            Id = LiveGuid.NewGuid();
            Attempt = attempt;
            TestKits = testKits;
            TestResults = testResults;
        }

        public static Test Create(int attempt, List<CategoryItem> testKits, List<CategoryItem> testResults)
        {
            return new Test(attempt, testKits, testResults);
        }

        private void ShowOther()
        {
            ShowOtherTestKit = false;
            if (null != SelectedTestKit && SelectedTestKit.Item.Display.ToLower().Contains("other".ToLower()))
            {
                ShowOtherTestKit = true;
            }
        }

        public ObsTestResult ObsTestResult
        {
            get
            {
                return ObsTestResult.Create(Id, TestName, Attempt, SelectedTestKit.ItemId, OtherTestKit, LotNumber, Expiry,
                    SelectedTestResult.ItemId);
            }
        }
    }
}