using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Interview;

namespace LiveHTS.Core.Interfaces.Services.Interview
{
    public interface IPartnerTracingService
    {
        Encounter OpenEncounter(Guid encounterId);
        Encounter StartEncounter(Guid formId, Guid encounterTypeId, Guid clientId, Guid providerId, Guid userId, Guid practiceId, Guid deviceId, Guid indexClientId);
        IEnumerable<Encounter> LoadEncounter(Guid clientId, Guid encounterTypeId);
        void SaveTest(ObsPartnerTraceResult testResult, Guid clientId, Guid indexClientId);
        void DeleteTest(ObsPartnerTraceResult testResult, Guid clientId, Guid indexClientId);
        void MarkEncounterCompleted(Guid encounterId, Guid userId, bool completed);
    }
}