﻿using System;
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
    public interface ITraceViewModel
    {
        bool EditMode { get; set; }
        IReferralViewModel Parent { get; set; }
        string ErrorSummary { get; set; }
        ValidationHelper Validator { get; }
        ObservableDictionary<string, string> Errors { get; set; }
        ObsTraceResult TestResult { get; set; }

        Guid Id { get; set; }

        DateTime Date { get; set; }
        Guid Mode { get; set; }
        List<CategoryItem> Modes { get; set; }
        CategoryItem SelectedMode { get; set; }

        Guid Outcome { get; set; }
        List<CategoryItem> Outcomes { get; set; }
        CategoryItem SelectedOutcome { get; set; }
        bool ShowReason { get; set; }
        bool ShowKitOther { get; set; }
        Guid ReasonNotContacted { get; set; }
        List<CategoryItem> ReasonsNotContacted { get; set; }
        CategoryItem SelectedReasonNotContacted { get; set; }
        string ReasonNotContactedOther { get; set; }
        Guid EncounterId { get; set; }
        IMvxCommand SaveTraceCommand { get; }
    }
}
