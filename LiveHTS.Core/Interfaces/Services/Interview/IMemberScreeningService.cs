using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Interview;

namespace LiveHTS.Core.Interfaces.Services.Interview
{
    public interface IMemberScreeningService
    {
        Encounter OpenEncounter(Guid encounterId);
        Encounter StartEncounter(Guid formId, Guid encounterTypeId, Guid clientId, Guid providerId, Guid userId, Guid practiceId, Guid deviceId,Guid indexClientId);
        IEnumerable<Encounter> LoadEncounter(Guid clientId, Guid encounterTypeId);
        void SaveMemberScreening(ObsMemberScreening testResult,Guid clientId, Guid indexClientId);
        void MarkEncounterCompleted(Guid encounterId, Guid userId, bool completed);
    }
}