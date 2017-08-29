using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Presentation.DTO;
using LiveHTS.Presentation.Events;
using LiveHTS.Presentation.ViewModel.Template;
using LiveHTS.Presentation.ViewModel.Wrapper;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.Interfaces.ViewModel
{
    public interface ILinkedToCareViewModel: IMvxViewModel
    {
        ILinkageViewModel ParentViewModel { get; set; }
        string Title { get; set; }
       
        string FacilityHandedTo { get; set; }
        string HandedTo { get; set; }
        string WorkerCarde { get; set; }
        DateTime? DateEnrolled { get; set; }
        string EnrollmentId { get; set; }
        string Remarks { get; set; }
    }
}