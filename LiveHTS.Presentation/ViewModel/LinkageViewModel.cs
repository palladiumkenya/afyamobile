using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Presentation.Interfaces.ViewModel;
using LiveHTS.Presentation.ViewModel.Template;
using LiveHTS.Presentation.ViewModel.Wrapper;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.ViewModel
{
    public class LinkageViewModel:MvxViewModel,ILinkageViewModel
    {
        public string ReferredTo { get; set; }
        public DateTime? DatePromised { get; set; }
        public string FacilityHandedTo { get; set; }
        public string HandedTo { get; set; }
        public string WorkerCarde { get; set; }
        public DateTime? DateEnrolled { get; set; }
        public string EnrollmentId { get; set; }
        public string Remarks { get; set; }
        public List<TraceTemplateWrap> Traces { get; set; }
        public IMvxCommand AddTraceCommand { get; }
        public void RemoveTrace(TraceTemplate template)
        {
            throw new NotImplementedException();
        }

        public void ShowDatePicker(Guid refId, DateTime refDate)
        {
            throw new NotImplementedException();
        }
    }
}