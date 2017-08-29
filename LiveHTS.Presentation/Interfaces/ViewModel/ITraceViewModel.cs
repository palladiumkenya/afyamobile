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
    public interface ITraceViewModel
    {
        Guid EncounterTypeId { get; set; }
        Client Client { get; set; }
        Encounter Encounter { get; set; }

     
        CategoryItem SelectedFinalTestResult { get; set; }
        List<CategoryItem> FinalTestResults { get; set; }


        event EventHandler<ChangedDateEvent> ChangedDate;

        ExpiryDateDTO SelectedDate { get; set; }

        void ShowDatePicker(Guid refId, DateTime refDate);
        void SaveTest(ObsTestResult test);
        void DeleteTest(ObsTestResult test);
        void RefreshTest();
    }
}