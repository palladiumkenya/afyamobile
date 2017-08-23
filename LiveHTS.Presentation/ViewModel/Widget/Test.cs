using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Lookup;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.ViewModel.Widget
{
    public class Test:MvxNotifyPropertyChanged
    {
        private CategoryItem _selectedTestKit;
        private List<CategoryItem> _testKits;
        private bool _showOtherTestKit;
        private string _otherTestKit;
        private string _lotNumber;
        private DateTime _expiry;
        private List<CategoryItem> _testResults;
        private CategoryItem _selectedTestResult;
        public int Attempt { get; set; }

        public List<CategoryItem> TestKits
        {
            get { return _testKits; }
            set { _testKits = value;
                RaisePropertyChanged(() => TestKits);
            }
        }

        public CategoryItem SelectedTestKit
        {
            get { return _selectedTestKit; }
            set
            {
                _selectedTestKit = value; RaisePropertyChanged(() => SelectedTestKit);
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
            set { _otherTestKit = value; RaisePropertyChanged(() => OtherTestKit); }
        }

        public string LotNumber
        {
            get { return _lotNumber; }
            set { _lotNumber = value; RaisePropertyChanged(() => LotNumber); }
        }

        public DateTime Expiry
        {
            get { return _expiry; }
            set { _expiry = value; RaisePropertyChanged(() => Expiry); }
        }

        public List<CategoryItem> TestResults
        {
            get { return _testResults; }
            set { _testResults = value; RaisePropertyChanged(() => TestResults); }
        }

        public CategoryItem SelectedTestResult
        {
            get { return _selectedTestResult; }
            set { _selectedTestResult = value; RaisePropertyChanged(() => SelectedTestResult); }
        }

        public IMvxCommand SaveTestCommand { get; set; }
        public IMvxCommand DeleteTestCommand { get; set; }

        private Test(int attempt, List<CategoryItem> testKits, List<CategoryItem> testResults)
        {
            Attempt = attempt;
            TestKits = testKits;
            TestResults = testResults;
        }

        public static Test Create(int attempt, List<CategoryItem> testKits, List<CategoryItem> testResults)
        {
            return new Test(attempt,testKits,testResults);
        }

        private void ShowOther()
        {
            ShowOtherTestKit = false;
            if (null != SelectedTestKit && SelectedTestKit.Item.Display.ToLower().Contains("other".ToLower()))
            {
                ShowOtherTestKit = true;
            }
        }
    }
}