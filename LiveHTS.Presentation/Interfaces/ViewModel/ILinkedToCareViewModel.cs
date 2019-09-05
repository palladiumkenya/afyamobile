using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Presentation.DTO;
using LiveHTS.Presentation.Events;
using LiveHTS.Presentation.ViewModel.Template;
using LiveHTS.Presentation.ViewModel.Wrapper;
using MvvmCross.Core.ViewModels;
using MvvmValidation;

namespace LiveHTS.Presentation.Interfaces.ViewModel
{
    public interface ILinkedToCareViewModel: IMvxViewModel
    {
        ObsLinkage ObsLinkage { get; set; }
        ValidationHelper Validator { get; set; }
        ILinkageViewModel ParentViewModel { get; set; }
        string Title { get; set; }
        string ErrorSummary { get; set; }
        Guid LinkageId { get; set; }
        string FacilityHandedTo { get; set; }
        string HandedTo { get; set; }
        string WorkerCarde { get; set; }
        DateTime DateEnrolled { get; set; }
        bool HasArtStartDate { get; set; }
        DateTime ARTStartDate { get; set; }
        bool AllowARTStartDate { get; set; }
        string EnrollmentId { get; set; }
        string Remarks { get; set; }
        TraceDateDTO SelectedEnrolDate { get; set; }
        TraceDateDTO SelectedArtDate { get; set; }
        IMvxCommand SaveLinkingCommand { get; }

        IMvxCommand ShowDateEnrolledDialogCommand { get; }
        event EventHandler<ChangedDateEvent> ChangedEnrollDate;

        IMvxCommand ShowArtDateDialogCommand { get; }
        event EventHandler<ChangedDateEvent> ChangedArtDate;

        bool Validate();
    }
}
