using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Interview;

namespace LiveHTS.Core.Interfaces.Services.Interview
{
    public interface IMemberTracingService
    {
        Encounter OpenEncounter(Guid encounterId);
        Encounter StartEncounter(Guid formId, Guid encounterTypeId, Guid clientId, Guid providerId, Guid userId, Guid practiceId, Guid deviceId);
        IEnumerable<Encounter> LoadEncounter(Guid clientId, Guid encounterTypeId);
        void SaveTest(ObsFamilyTraceResult testResult);
        void DeleteTest(ObsFamilyTraceResult testResult);
        void MarkEncounterCompleted(Guid encounterId, Guid userId, bool completed);
    }
}