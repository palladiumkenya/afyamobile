using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Survey;
using LiveHTS.SharedKernel.Model;

namespace LiveHTS.Core.Interfaces.Services.Clients
{
    public interface IEncounterService
    {
        Form LoadForm(Guid formId, bool includeMetadata = true);
        Encounter LoadEncounter(Guid formId, Guid encounterTypeId, Guid clientId, bool includeObs = false);
        IEnumerable<Encounter> LoadEncounters(Guid formId, Guid clientId, bool includeObs = false);
        Encounter StartEncounter(Encounter encounter);
        Encounter StartEncounter(Guid formId, Guid encounterTypeId, Guid clientId, Guid providerId, Guid userId,Guid practiceId, Guid deviceId, Guid? indexClientId, VisitType visitType);
        Encounter OpenEncounter(Guid encounterId);
        Encounter LoadTesting(Guid encounterId);
        void Save(List<Encounter> encounters);
        void DiscardEncounter(Guid encounterId);
    }
}