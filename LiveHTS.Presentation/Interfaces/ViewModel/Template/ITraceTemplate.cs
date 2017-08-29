using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Lookup;
using LiveHTS.Presentation.Interfaces.ViewModel.Wrapper;
using LiveHTS.Presentation.Validations;
using MvvmCross.Core.ViewModels;
using MvvmValidation;

namespace LiveHTS.Presentation.Interfaces.ViewModel.Template
{
    public interface ITraceTemplate
    {
        string ErrorSummary { get; set; }
        ValidationHelper Validator { get; }
        ObservableDictionary<string, string> Errors { get; set; }

        ObsTraceResult TraceResult { get; }
        ITraceTemplateWrap TraceTemplateWrap { get; set; }
        Guid Id { get; set; }
        DateTime Date { get; set; }        
        Guid Mode { get; set; }        
        Guid Outcome { get; set; }        
        Guid EncounterId { get; set; }
        
        CategoryItem SelectedMode { get; set; }
        CategoryItem SelectedOutcome { get; set; }

        List<CategoryItem> Modes { get; set; }
        List<CategoryItem> Outcomes { get; set; }
        

        bool Validate();
        bool CanSave();
        bool CanDelete();
    }
}