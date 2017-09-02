using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Lookup;
using LiveHTS.Presentation.DTO;
using LiveHTS.Presentation.Events;
using LiveHTS.Presentation.Interfaces.ViewModel.Wrapper;
using LiveHTS.Presentation.Validations;
using MvvmCross.Core.ViewModels;
using MvvmValidation;

namespace LiveHTS.Presentation.Interfaces.ViewModel
{
    public interface ITestViewModel
    { 
        bool EditMode { get; set; }
        ITestEpisodeViewModel Parent { get; set; }
        string ErrorSummary { get; set; }
        ValidationHelper Validator { get; }
        ObservableDictionary<string, string> Errors { get; set; }
        ObsTestResult TestResult { get; set; }
        
        Guid Id { get; set; }
        string TestName { get; set; }
        int Attempt { get; set; }
        List<CategoryItem> Kits { get; set; }
        CategoryItem SelectedKit { get; set; }
        Guid Kit { get; set; }
        bool ShowKitOther { get; set; }
        string KitOther { get; set; }
        string LotNumber { get; set; }
        DateTime Expiry { get; set; }
        List<CategoryItem> Results { get; set; }
        CategoryItem SelectedResult { get; set; }
        Guid Result { get; set; }
        Guid EncounterId { get; set; }
        string ResultCode { get; set; }
        IMvxCommand SaveTestCommand { get; }

        IMvxCommand ShowDateDialogCommand { get; }
        event EventHandler<ChangedDateEvent> ChangedDate;
        TraceDateDTO SelectedDate { get; set; }
        void ShowDatePicker(Guid refId, DateTime refDate);
    }
}