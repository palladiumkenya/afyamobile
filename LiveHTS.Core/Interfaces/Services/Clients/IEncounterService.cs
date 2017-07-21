using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Interview;

namespace LiveHTS.Core.Interfaces.Services.Clients
{
    public interface IEncounterService
    {
        Encounter LoadEncounter(Guid formId, Guid encounterTypeId, Guid clientId, bool includeObs = false);
        IEnumerable<Encounter> LoadEncounters(Guid formId, Guid clientId, bool includeObs = false);
        Encounter StartEncounter(Encounter encounter);
        Encounter StartEncounter(Guid formId, Guid encounterTypeId, Guid clientId, Guid providerId, Guid userId);
        Encounter OpenEncounter(Guid encounterId);
        void DiscardEncounter(Guid encounterId);
    }
}