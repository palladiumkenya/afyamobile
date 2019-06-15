using System;
using System.Collections.Generic;
using LiveHTS.Core.Model;
using LiveHTS.Core.Model.Interview;
using LiveHTS.SharedKernel.Model;

namespace LiveHTS.Core.Interfaces.Repository.Interview
{
    public interface IEncounterRepository : IRepository<Encounter, Guid>
    {
        Encounter Load(Guid id, bool includeObs = false);
        Encounter Load(Guid formId, Guid encounterTypeId, Guid clientId,bool includeObs=false);
        IEnumerable<Encounter> LoadAll(Guid clientId);
        IEnumerable<Encounter> LoadAllKey(Guid clientId);
        IEnumerable<Encounter> LoadAll(Guid formId, Guid clientId, bool includeObs = false);
        IEnumerable<Encounter> LoadAll(Guid formId, Guid clientId, Guid indexClientId, bool includeObs = false);
        Encounter LoadTest(Guid id, bool includeObs = false);
        Encounter LoadFinalTest(Guid id, bool includeObs = false);
        Encounter LoadTest(Guid encounterTypeId, Guid clientId, bool includeObs = false);
        List<Encounter> LoadTestAll(Guid encounterTypeId, Guid clientId, bool includeObs = false);
        DateTime GetPretestEncounterDate(Guid clientId);
        bool CheckPretestComplete(Guid clientId,bool downloaded=false);
        bool GetIndividual(Guid clientId);

        void ClearObs(Guid id);
        void UpdateStatus(Guid id, bool completed);
        void UpdateStatus(Guid id,Guid userId,bool completed);
        void UpdateEncounterDate(Guid id,DateTime encounterDate, VisitType visitType);
        void UpdateEncounterDate(Guid id, DateTime encounterDate);
        void Upload(Encounter encounter);

        void Purge(Guid id, string obsName);
        void PurgeAny(Guid id);
    }
}
