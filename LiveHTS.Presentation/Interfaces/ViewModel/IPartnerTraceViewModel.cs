using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Lookup;
using LiveHTS.Presentation.Validations;
using MvvmCross.Core.ViewModels;
using MvvmValidation;

namespace LiveHTS.Presentation.Interfaces.ViewModel
{
   
    public interface IPartnerTraceViewModel
    {
        bool EditMode { get; set; }
        IPartnerTracingViewModel Parent { get; set; }
        string ErrorSummary { get; set; }
        ValidationHelper Validator { get; }
        ObservableDictionary<string, string> Errors { get; set; }
        ObsPartnerTraceResult TestResult { get; set; }

        Guid Id { get; set; }

        DateTime Date { get; set; }
        Guid Mode { get; set; }
        List<CategoryItem> Modes { get; set; }
        CategoryItem SelectedMode { get; set; }

        Guid Outcome { get; set; }
        List<CategoryItem> Outcomes { get; set; }
        CategoryItem SelectedOutcome { get; set; }

        bool EnableConsent { get; set; }
        Guid Consent { get; set; }
        List<CategoryItem> Consents { get; set; }
        CategoryItem SelectedConsent { get; set; }

        bool EnableBooking { get; set; }
        DateTime? BookingDate { get; set; }

        Guid EncounterId { get; set; }
        IMvxCommand SaveTraceCommand { get; }
    }
}