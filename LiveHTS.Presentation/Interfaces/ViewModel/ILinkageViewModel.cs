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

        Guid AppUserId { get; }
        Guid AppProviderId { get; }
        Guid AppPracticeId { get; }
        Guid AppDeviceId { get; }

        IReferralViewModel ReferralViewModel { get; set; }
        ILinkedToCareViewModel LinkedToCareViewModel { get; set; }


        Guid EncounterTypeId { get; set; }
        Client Client { get; set; }
        Encounter Encounter { get; set; }
    }
}