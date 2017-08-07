using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Interview;

namespace LiveHTS.Presentation.Interfaces.ViewModel.Template
{
    public interface IFormTemplate
    {
        Guid Id { get; set; }
        string Display { get; set; }
        string EncounterDisplay { get; set; }
    }
}