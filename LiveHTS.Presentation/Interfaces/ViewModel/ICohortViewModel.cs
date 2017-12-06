using System.Collections.Generic;
using LiveHTS.Core.Model.Config;
using LiveHTS.Core.Model.Subject;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.Interfaces.ViewModel
{
    public interface ICohortViewModel: IMvxViewModel
    {
        string Title { get; set; }
        IRemoteRegistryViewModel Parent { get; set; }
        string Search { get; set; }
        Cohort SelectedCohort { get; set; }
        IEnumerable<Cohort> Cohorts { get; set; }
        IMvxCommand SearchCommand { get; }
        IMvxCommand ClearSearchCommand { get; }
        IMvxCommand RefreshCohortCommand { get; }
        IMvxCommand<Cohort> CohortSelectedCommand { get; }
        bool IsBusy { get; set; }
    }
}