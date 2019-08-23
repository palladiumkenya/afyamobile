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
    public interface IFamilyTraceTemplate
    {
        ObsFamilyTraceResult TraceResult { get; set; }
        Guid Id { get; set; }
        DateTime Date { get; set; }
        Guid Mode { get; set; }
        string ModeDisplay { get; set; }
        Guid Outcome { get; set; }
        string OutcomeDisplay { get; set; }

        Guid? Consent { get; set; }
        string ConsentDisplay { get; set; }
        DateTime? Reminder { get; set; }
        DateTime? BookingDate { get; set; }
        Guid ReasonNotContacted { get; set; }
        string ReasonNotContactedDisplay { get; set; }
        string ReasonNotContactedOther { get; set; }
        Guid EncounterId { get; set; }
    }
}
