using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Interview;

namespace LiveHTS.Presentation.Interfaces.ViewModel.Template
{
    public interface IEncounterTemplate
    {
        Guid Id { get; set; }
        DateTime EncounterDate { get; set; }
        string Status { get; set; }
        string Provider { get; set; }
        string ElapsedTime { get; set; }
    }
}