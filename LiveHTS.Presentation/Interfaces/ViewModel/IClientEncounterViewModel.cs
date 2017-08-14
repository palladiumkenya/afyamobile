using System;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Survey;
using LiveHTS.Presentation.DTO;

namespace LiveHTS.Presentation.Interfaces.ViewModel
{
    public interface IClientEncounterViewModel
    {
        Guid UserId { get; }
        string UserName { get; }
        Guid ProviderId { get; }
        string ProviderName { get; }

        ClientDTO ClientDTO { get; set; }
        ClientEncounterDTO ClientEncounterDTO { get; set; }
        Form Form { get; set; }
        Encounter Encounter { get; set; }
    }
}