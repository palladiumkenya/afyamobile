using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Interview;

namespace LiveHTS.Core.Interfaces.Services.Interview
{
    public interface IHIVTestingService
    {
        Encounter OpenEncounter(Guid encounterId);

        Encounter StartEncounter(Guid formId, Guid encounterTypeId, Guid clientId, Guid providerId, Guid userId,
            Guid practiceId, Guid deviceId);
        IEnumerable<Encounter> LoadEncounter(Guid clientId,Guid encounterTypeId);
        void SaveTest(ObsTestResult testResult);
        void SaveFinalTest(ObsFinalTestResult testResult);
        void DeleteTest(ObsTestResult testResult);
        void UpdateFinalResult(Guid encounterId);
        void MarkEncounterCompleted(Guid encounterId, bool completed);
        void UpdateEncounterDate(Guid encounterId, Guid clientId);
        bool IsIndividual(Guid clientId);

    }
}