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
    public class FirstHIVTestViewModel : MvxViewModel, IFirstHIVTestViewModel
    {
        private string _firstTestName = "HIV Test 1";
        private ObservableCollection<HIVTestTemplateWrap> _firstTests = new ObservableCollection<HIVTestTemplateWrap>();
        private IMvxCommand _addTestCommand;
        private CategoryItem _selectedFirstTestResult;
        private List<CategoryItem> _firstTestResults;
        private IHIVTestViewModel _parent;

        public IHIVTestViewModel Parent
        {
            get { return _parent; }
            set { _parent = value; }
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
            set { _selectedFirstTestResult = value; RaisePropertyChanged(() => SelectedFirstTestResult); }
        }

        public List<CategoryItem> FirstTestResults
        {
            get { return _firstTestResults; }
            set { _firstTestResults = value; RaisePropertyChanged(() => FirstTestResults); }
        }

        public IMvxCommand AddFirstTestCommand
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
                    FirstTests.Any(x => x.HIVTestTemplate.SelectedResult.Item.Code == "P" ||
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
            var obs = ObsTestResult.CreateNew(FirstTestName, lastAttempt, Parent.Encounter.Id);

            var list = Parent.Encounter.ObsTestResults.ToList();
            list.Add(obs);
            Parent.Encounter.ObsTestResults = list;
            Parent.Encounter = Parent.Encounter;
        }



    }
}