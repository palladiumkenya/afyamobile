using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Interview;

namespace LiveHTS.Core.Interfaces.Repository.Survey
{
    public interface IEncounterRepository : IRepository<Encounter, Guid>
    {
        Encounter Load(Guid id, bool includeObs = false);
        Encounter Load(Guid formId, Guid encounterTypeId, Guid clientId,bool includeObs=false);
        IEnumerable<Encounter> LoadAll(Guid formId, Guid clientId, bool includeObs = false);
        IEnumerable<Encounter> GetWithObs(Guid formId, Guid encounterTypeId, Guid clientId);
    }
}