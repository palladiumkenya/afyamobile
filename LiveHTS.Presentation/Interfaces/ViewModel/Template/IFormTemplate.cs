using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Config;
using LiveHTS.Core.Model.Interview;

namespace LiveHTS.Presentation.Interfaces.ViewModel.Template
{
    public interface IFormTemplate
    {
        bool ShowFormName { get; set; }
        Guid Id { get; set; }
        string Display { get; set; }
        string EncounterDisplay { get; set; }
        Guid EncounterTypeId { get; set; }
        string EncounterTypeDisplay { get; set; }
        string EncounterTypeDescription { get; set; }
        decimal Rank { get; set; }
        bool ConsentRequired { get; set; }
        bool HasConsent { get; set; }
        bool Block { get; set; }
    }
}