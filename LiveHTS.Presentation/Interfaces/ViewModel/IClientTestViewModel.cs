using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using LiveHTS.Core.Model.Config;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Lookup;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Presentation.DTO;
using LiveHTS.Presentation.Events;
using LiveHTS.Presentation.ViewModel.Wrapper;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.Interfaces.ViewModel
{
    public interface IClientHIVTestViewModel
    {

        Guid EncounterTypeId  { get; set; }
        Client Client { get; set; }
        Encounter Encounter { get; set; }

        string FirstTestName { get; set; }
        ObservableCollection<HIVTestTemplateWrap> FirstTests { get; set; }
        CategoryItem SelectedFirstTestResult { get; set; }
        List<CategoryItem> FirstTestResults { get; set; }

        string SecondTestName { get; set; }
        ObservableCollection<HIVTestTemplateWrap> SecondTests { get; set; }
        CategoryItem SelectedSecondTestResult { get; set; }
        List<CategoryItem> SecondTestResults { get; set; }

        CategoryItem SelectedFinalTestResult { get; set; }
        List<CategoryItem> FinalTestResults { get; set; }

        IMvxCommand AddFirstTestCommand { get; }
        IMvxCommand AddSecondTestCommand { get; }
                
        event EventHandler<ChangedDateEvent> ChangedDate;

        ExpiryDateDTO SelectedDate { get; set; }

        void ShowDatePicker(Guid refId);
        void SaveTest(ObsTestResult test);
        void DeleteTest(ObsTestResult test);
        void RefreshTest();

        //        List<CategoryItem> FirstTestResults { get; set; }
        //        CategoryItem SelectedFirstTestResult { get; set; }
        //
        //        string SecondTestName { get; set; }
        //        List<Test> SecondTests { get; set; }
        //        List<CategoryItem> SecondTestResults { get; set; }
        //        CategoryItem SelectedSecondTestResult { get; set; }
        //
        //        List<CategoryItem> FinalTestResults { get; set; }
        //        CategoryItem SelectedFinalTestResult { get; set; }
    }
}