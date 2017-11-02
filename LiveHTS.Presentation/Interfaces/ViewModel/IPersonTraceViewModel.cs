using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Lookup;
using LiveHTS.Presentation.Validations;
using MvvmCross.Core.ViewModels;
using MvvmValidation;

namespace LiveHTS.Presentation.Interfaces.ViewModel
{
   
    public interface IPersonTraceViewModel
    {
        bool EditMode { get; set; }
        IMemberTracingViewModel Parent { get; set; }
        string ErrorSummary { get; set; }
        ValidationHelper Validator { get; }
        ObservableDictionary<string, string> Errors { get; set; }
        ObsFamilyTraceResult TestResult { get; set; }

        Guid Id { get; set; }

        DateTime Date { get; set; }
        Guid Mode { get; set; }
        List<CategoryItem> Modes { get; set; }
        CategoryItem SelectedMode { get; set; }

        Guid Outcome { get; set; }
        List<CategoryItem> Outcomes { get; set; }
        CategoryItem SelectedOutcome { get; set; }
        Guid EncounterId { get; set; }
        IMvxCommand SaveTraceCommand { get; }
    }
}