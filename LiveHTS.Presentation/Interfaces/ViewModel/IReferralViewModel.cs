using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Presentation.DTO;
using LiveHTS.Presentation.Events;
using LiveHTS.Presentation.Validations;
using LiveHTS.Presentation.ViewModel.Template;
using LiveHTS.Presentation.ViewModel.Wrapper;
using MvvmCross.Core.ViewModels;
using MvvmValidation;

namespace LiveHTS.Presentation.Interfaces.ViewModel
{
    public interface IReferralViewModel:IMvxViewModel
    {
        ILinkageViewModel ParentViewModel { get; set; }
        ValidationHelper Validator { get; }
        ObservableDictionary<string, string> Errors { get; set; }

        string Title { get; set; }
        string ErrorSummary { get; set; }
        ObsLinkage ObsLinkage { get; set; }
        Guid LinkageId { get; set; }
        string ReferredTo { get; set; }
        DateTime DatePromised { get; set; }
        ObsTraceResult Trace { get; set; }
        List<TraceTemplateWrap> Traces { get; set; }

        IMvxCommand AddTraceCommand { get; }
        IMvxCommand CloseTestCommand { get; }
        Action AddTraceCommandAction { get; set; }
        Action EditTestCommandAction { get; set; }
        Action CloseTestCommandAction { get; set; }
        IMvxCommand SaveReferralCommand { get; }

        IMvxCommand ShowDateDialogCommand { get; }

        
        event EventHandler<ChangedDateEvent> ChangedDate;

        TraceDateDTO SelectedPromiseDate { get; set; }

        TraceDateDTO SelectedDate { get; set; }
        void ShowDatePicker(Guid refId, DateTime refDate);
        void SaveTrace(ObsTraceResult test);
        void DeleteTrace(ObsTraceResult test);
        bool Validate();
        void EditTrace(ObsTraceResult testResult);
        void Referesh(Guid encounterId);
        
        
    }
}