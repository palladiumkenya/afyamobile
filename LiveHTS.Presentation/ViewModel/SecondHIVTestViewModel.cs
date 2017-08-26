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

        private Client _client;
        private string _firstTestName = "HIV Test 1";
        private ObservableCollection<HIVTestTemplateWrap> _firstTests = new ObservableCollection<HIVTestTemplateWrap>();
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
        private string _secondTestName = "HIV Test 2";
        private ObservableCollection<HIVTestTemplateWrap> _secondTests;
        private CategoryItem _selectedSecondTestResult;
        private List<CategoryItem> _secondTestResults;
        private IMvxCommand _addSecondTestCommand;
        private CategoryItem _selectedFinalTestResult;
        private List<CategoryItem> _finalTestResults;
        private IMvxCommand _showDateCommand;
        private IMvxCommand _showDateDialogCommand;
        private ExpiryDateDTO _selectedDate;
        private IHIVTestViewModel _parent;


        public IHIVTestViewModel Parent
        {
            get { return _parent; }
            set { _parent = value; }
        }

        public string SecondTestName
        {
            get { return _secondTestName; }
            set { _secondTestName = value; RaisePropertyChanged(() => SecondTestName); }
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
            set { _selectedFinalTestResult = value; RaisePropertyChanged(() => SelectedFinalTestResult); }
        }

        public List<CategoryItem> FinalTestResults
        {
            get { return _finalTestResults; }
            set { _finalTestResults = value; RaisePropertyChanged(() => FinalTestResults); }
        }

        public IMvxCommand AddSecondTestCommand
        {
            get
            {
                _addSecondTestCommand = _addSecondTestCommand ?? new MvxCommand(AddSecondTest, CanAddSecondTest);
                return _addSecondTestCommand;
            }
        }

        private void AddSecondTest()
        {
            int lastAttempt = SecondTests.Max(x => x.HIVTestTemplate.Attempt);
            lastAttempt++;
            var obs = ObsTestResult.CreateNew(SecondTestName, lastAttempt, Parent.Encounter.Id);

            var list = Parent.Encounter.ObsTestResults.ToList();
            list.Add(obs);
            Parent.Encounter.ObsTestResults = list;
            Parent.Encounter = Parent.Encounter;
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

    }
}