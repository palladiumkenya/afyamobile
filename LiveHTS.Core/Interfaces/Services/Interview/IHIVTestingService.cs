using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Interview;

namespace LiveHTS.Core.Interfaces.Services.Interview
{
    public interface IHIVTestingService
    {
        Encounter OpenEncounter(Guid encounterId);
        Encounter StartEncounter(Guid encounterTypeId, Guid clientId, Guid providerId, Guid userId);
        IEnumerable<Encounter> LoadEncounter(Guid clientId,Guid encounterTypeId);
        void SaveTest(ObsTestResult testResult);
        void DeleteTest(ObsTestResult testResult);
        void UpdateFinalResult(Guid encounterId);
    }
}