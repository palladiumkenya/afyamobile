using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Interview;

namespace LiveHTS.Core.Interfaces.Services.Interview
{
    public interface IMemberTracingService
    {
        Encounter OpenEncounter(Guid encounterId);
        Encounter StartEncounter(Guid formId, Guid encounterTypeId, Guid clientId, Guid providerId, Guid userId, Guid practiceId, Guid deviceId, Guid indexClientId);
        IEnumerable<Encounter> LoadEncounter(Guid clientId, Guid encounterTypeId);
        void SaveTest(ObsFamilyTraceResult testResult, Guid clientId, Guid indexClientId);
        void DeleteTest(ObsFamilyTraceResult testResult, Guid clientId, Guid indexClientId);
        void MarkEncounterCompleted(Guid encounterId, Guid userId, bool completed);
    }
}