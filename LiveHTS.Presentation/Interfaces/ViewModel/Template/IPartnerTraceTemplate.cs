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
    public interface IPartnerTraceTemplate
    {
        ObsPartnerTraceResult TraceResult { get; set; }
        Guid Id { get; set; }
        DateTime Date { get; set; }        
        Guid Mode { get; set; }
        string ModeDisplay { get; set; }
        Guid Outcome { get; set; }
        string OutcomeDisplay { get; set; }
        Guid EncounterId { get; set; }
    }
}