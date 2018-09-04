using System;
using System.Collections.Generic;
using Cheesebaron.MvxPlugins.Settings.Interfaces;

using LiveHTS.Core.Interfaces.Services.Config;
using LiveHTS.Core.Interfaces.Services.Interview;
using LiveHTS.Core.Model.Config;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Presentation.Interfaces.ViewModel;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace LiveHTS.Presentation.ViewModel
{
    public class CohortViewModel:MvxViewModel,ICohortViewModel
    {
        private readonly ISettings _settings;
        private readonly ICohortService _cohortService;
        private IEnumerable<Cohort> _cohorts;
        private bool _isBusy;
        private string _search;
        private IMvxCommand _searchCommand;
        private IMvxCommand _clearSearchCommand;
        
        private Cohort _selectedCohort;
        private IMvxCommand<Cohort> _cohortSelectedCommand;
        private  IMvxCommand _refreshCohortCommand;

        public string Title { get; set; } = "COHORT";
        public IRemoteRegistryViewModel Parent { get; set; }
      
        public string Search
        {
            get { return _search; }
            set
            {
                _search = value; 
                RaisePropertyChanged(() => Search);
                SearchCommand.RaiseCanExecuteChanged();
            }
        }

        public Cohort SelectedCohort
        {
            get { return _selectedCohort; }
            set { _selectedCohort = value; RaisePropertyChanged(() => SelectedCohort);}
        }

        public IEnumerable<Cohort> Cohorts
        {
            get { return _cohorts; }
            set
            {
                _cohorts = value;
                RaisePropertyChanged(() => Cohorts);
            }
        }

        public IMvxCommand SearchCommand
        {
            get
            {
                _searchCommand = _searchCommand ?? new MvxCommand(SearchCohorts, CanSearch);
                return _searchCommand;
            }
        }

        public IMvxCommand ClearSearchCommand
        {
            get
            {
                _clearSearchCommand = _clearSearchCommand ?? new MvxCommand(ClearSearch);
                return _clearSearchCommand;
            }
        }
        public IMvxCommand RefreshCohortCommand
        {
            get
            {
                _refreshCohortCommand = _refreshCohortCommand ?? new MvxCommand(RefreshCohort);
                return _refreshCohortCommand;
            }
        }
        public IMvxCommand<Cohort> CohortSelectedCommand
        {
            get
            {
                _cohortSelectedCommand = _cohortSelectedCommand ?? new MvxCommand<Cohort>(SelectCohort);
                return _cohortSelectedCommand;
            }
        }
  
        private void SelectCohort(Cohort selectedCohort)
        {
            if(null==selectedCohort)
                return;
            SelectedCohort = selectedCohort;
            ///////////////////
            ShowViewModel<CohortClientsViewModel>(new {id = SelectedCohort.Id});
        }
        private void SearchCohorts()
        {
            IsBusy = true;
            Cohorts = _cohortService.GetAllCohorts(Search);
            IsBusy = false;
        }

        private void ClearSearch()
        {
            Search = string.Empty;
            LoadCohorts();
        }

      
        private bool CanSearch()
        {
            return !string.IsNullOrWhiteSpace(Search) && Search.Length > 0;
        }
        private void RefreshCohort()
        {
//            ClearCache(_settings);
//            ShowViewModel<CohortRegistrationViewModel>(new { mode = "new" });
        }

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value; 
                RaisePropertyChanged(() => IsBusy);
            }
        }

        public CohortViewModel()
        {
            _cohortService = Mvx.Resolve<ICohortService>();
            _settings = Mvx.Resolve<ISettings>();
        }
        public override void ViewAppeared()
        {
            LoadCohorts();
        }
        private void LoadCohorts()
        {
            IsBusy = true;
            Cohorts = _cohortService.GetAllCohorts();
            IsBusy = false;
        }

       
    }
}