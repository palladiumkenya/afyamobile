using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Lookup;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Presentation.DTO;
using LiveHTS.Presentation.Events;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.Interfaces.ViewModel
{
    public interface IHIVTestViewModel
    {
        IFirstHIVTestViewModel FirstHIVTestViewModel { get; set; }
        ISecondHIVTestViewModel SecondHIVTestViewModel { get; set; }


        Guid EncounterTypeId { get; set; }
        Client Client { get; set; }
        Encounter Encounter { get; set; }

        ObsFinalTestResult ObsFinalTestResult { get; set; }
        Guid ObsFinalTestResultId { get; set; }

        Guid EndResult { get; set; }
        CategoryItem SelectedFinalTestResult { get; set; }
        List<CategoryItem> FinalTestResults { get; set; }

        Guid ResultGiven { get; set; }
        CategoryItem SelectedResultGiven { get; set; }
        List<CategoryItem> ResultGivenOptions { get; set; }

        Guid CoupleDiscordant { get; set; }
        CategoryItem SelectedCoupleDiscordant { get; set; }
        List<CategoryItem> CoupleDiscordantOptions { get; set; }

        Guid SelfTestOption { get; set; }
        CategoryItem SelectedSelfTest { get; set; }
        List<CategoryItem> SelfTestOptions { get; set; }

        IMvxCommand SaveTestInfoCommand { get; }
        event EventHandler<ChangedDateEvent> ChangedDate;

        ExpiryDateDTO SelectedDate { get; set; }

        void ShowDatePicker(Guid refId, DateTime refDate);
        void SaveTest(ObsTestResult test);
        void DeleteTest(ObsTestResult test);
        void RefreshTest();
        bool Validate();
    }
}