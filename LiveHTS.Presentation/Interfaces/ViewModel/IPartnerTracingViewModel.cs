using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Presentation.ViewModel.Wrapper;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.Interfaces.ViewModel
{
    
    public interface IPartnerTracingViewModel
    {
        Guid AppUserId { get; }
        Guid AppProviderId { get; }
        Guid AppPracticeId { get; }
        Guid AppDeviceId { get; }
        Guid EncounterTypeId { get; set; }
        Client Client { get; set; }
        Encounter Encounter { get; set; }

        ObsPartnerTraceResult Trace { get; set; }
        List<PartnerTraceTemplateWrap> Traces { get; set; }

        IMvxCommand AddTraceCommand { get; }
        IMvxCommand CloseTestCommand { get; }
        Action AddTraceCommandAction { get; set; }
        Action EditTestCommandAction { get; set; }
        Action CloseTestCommandAction { get; set; }
        void SaveTrace(ObsPartnerTraceResult test);
        void DeleteTrace(ObsPartnerTraceResult test);
        void EditTrace(ObsPartnerTraceResult testResult);
        void Referesh(Guid encounterId);
    }
}