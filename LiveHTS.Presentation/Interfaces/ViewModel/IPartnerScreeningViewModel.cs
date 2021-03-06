﻿using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Config;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Lookup;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Presentation.DTO;
using LiveHTS.Presentation.Events;
using MvvmCross.Core.ViewModels;
using Action = System.Action;

namespace LiveHTS.Presentation.Interfaces.ViewModel
{
    public interface IPartnerScreeningViewModel
    {

        Guid AppUserId { get; }
        Guid AppProviderId { get; }
        Guid AppPracticeId { get; }
        Guid AppDeviceId { get; }

        string ErrorSummary { get; set; }

        Guid EncounterTypeId { get; set; }
        IndexClientDTO IndexClient { get; set; }
        Client Client { get; set; }
        Encounter Encounter { get; set; }
        Guid EncounterId { get; set; }

        bool AllowScreening { get; set; }
        bool AllowEligibility { get; set; }
        bool MakeEligibile { get; set; }

        ObsPartnerScreening ObsPartnerScreening { get; set; }
        TraceDateDTO SelectedScreeningDate { get; set; }
        IMvxCommand ShowScreeningDateDialogCommand { get; }
        event EventHandler<ChangedDateEvent> ChangedScreeningDate;

        DateTime ScreeningDate { get; set; }

        //  Pns Accepted
        bool EnablePnsAccepted { get; set; }
        List<CategoryItem> PnsAccepted { get; set; }
        CategoryItem SelectedPnsAccepted { get; set; }


        //  Screened for IPV
        List<CategoryItem> IPVScreening { get; set; }
        CategoryItem SelectedIPVScreening { get; set; }

        //  IPV Screening
        //Has [partner’s name] ever hit, kicked, slapped, or otherwise physically hurt you?
        List<CategoryItem> PhysicalAssult { get; set; }
        CategoryItem SelectedPhysicalAssult { get; set; }
        //Has [partner’s name] ever threatened to hurt you?
        List<CategoryItem> Threatened { get; set; }
        CategoryItem SelectedThreatened { get; set; }
        //Has [partner’s name] ever forced you to do something sexually that made you feel uncomfortable?
        List<CategoryItem> SexuallyUncomfortable { get; set; }
        CategoryItem SelectedSexuallyUncomfortable { get; set; }

        //  Screened for IPV Outcome
        List<CategoryItem> IPVOutcome { get; set; }
        CategoryItem SelectedIPVOutcome { get; set; }

       

        //Occupation
        string Occupation { get; set; }

        // PNS Relations
        List<CategoryItem> PNSRealtionship { get; set; }
        CategoryItem SelectedPNSRealtionship { get; set; }
        // Living with client
        List<CategoryItem> LivingWithClient { get; set; }
        CategoryItem SelectedLivingWithClient { get; set; }

        // HIV status(KP/N/DK/HEI)	
        List<CategoryItem> HIVStatus { get; set; }
        CategoryItem SelectedHIVStatus { get; set; }

        bool EnablePNSApproach { get; set; }
        // PNSApproach
        List<CategoryItem> PNSApproach { get; set; }
        CategoryItem SelectedPNSApproach { get; set; }

        
        //  Eligible for Testing(Y/N)
        List<CategoryItem> Eligibility { get; set; }
        CategoryItem SelectedEligibility { get; set; }

        bool EnableBookingDate { get; set; }
        //  Date contact booked for testing (DD/MM/YYYY)
        DateTime BookingDate { get; set; }
        TraceDateDTO SelectedBookingDate { get; set; }
        IMvxCommand ShowBookingDateDialogCommand { get; }
        event EventHandler<ChangedDateEvent> ChangedBookingDate;
        string Remarks { get; set; }

        IMvxCommand SaveScreeningCommand { get; }
     
        bool Validate();
        void UpdateEligibility();
    }
}