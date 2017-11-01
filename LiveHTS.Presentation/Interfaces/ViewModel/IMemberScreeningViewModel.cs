using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Config;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Lookup;
using LiveHTS.Core.Model.Subject;

namespace LiveHTS.Presentation.Interfaces.ViewModel
{
    public interface IMemberScreeningViewModel
    {

        Guid AppUserId { get; }
        Guid AppProviderId { get; }
        Guid AppPracticeId { get; }
        Guid AppDeviceId { get; }

        string ErrorSummary { get; set; }

        EncounterType EncounterType { get; set; }
        Client Client { get; set; }
        Encounter Encounter { get; set; }

        DateTime ScreeningDate { get; set; }
        // HIV status(KP/N/DK/HEI)	
        List<CategoryItem> HIVStatus { get; set; }
        CategoryItem SelectedHIVStatus { get; set; }

        //  Family member eligible for Testing(Y/N)
        List<CategoryItem> Eligibility { get; set; }
        CategoryItem SelectedEligibility { get; set; }

        //  Date contact booked for testing (DD/MM/YYYY)
        DateTime BookingDate { get; set; }
    }
}