using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Interview;

namespace LiveHTS.Core.Interfaces.Services.Interview
{
    public interface IPartnerScreeningService
    {
        Encounter OpenEncounter(Guid encounterId);
        Encounter StartEncounter(Guid formId, Guid encounterTypeId, Guid clientId, Guid providerId, Guid userId, Guid practiceId, Guid deviceId, Guid indexClientId);
        IEnumerable<Encounter> LoadEncounter(Guid clientId, Guid encounterTypeId);
        void SavePartnerScreening(ObsPartnerScreening testResult,Guid clientId);
        void MarkEncounterCompleted(Guid encounterId, Guid userId, bool completed);
    }
}