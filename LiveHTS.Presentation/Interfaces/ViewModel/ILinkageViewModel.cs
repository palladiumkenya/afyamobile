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
    public interface ILinkageViewModel
    {

        IReferralViewModel ReferralViewModel { get; set; }
        ILinkedToCareViewModel LinkedToCareViewModel { get; set; }


        Guid EncounterTypeId { get; set; }
        Client Client { get; set; }
        Encounter Encounter { get; set; }


        string ReferredTo { get; set; }
         DateTime? DatePromised { get; set; }
           
         string FacilityHandedTo { get; set; }
         string HandedTo { get; set; }
         string WorkerCarde { get; set; }
         DateTime? DateEnrolled { get; set; }
         string EnrollmentId { get; set; }
         string Remarks { get; set; }

        List<TraceTemplateWrap> Traces { get; set; }

        IMvxCommand AddTraceCommand { get; }

        void RemoveTrace(TraceTemplate template);

        event EventHandler<ChangedDateEvent> ChangedDate;

        TraceDateDTO SelectedDate { get; set; }
        void ShowDatePicker(Guid refId, DateTime refDate);
        void SaveTrace(ObsTraceResult test);
        void DeleteTrace(ObsTraceResult test);

    }
}