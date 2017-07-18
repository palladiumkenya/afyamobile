using System;
using LiveHTS.Core.Model.Interview;

namespace LiveHTS.Core.Interfaces.Repository.Survey
{
    public interface IEncounterRepository:IRepository<Encounter,Guid>
    {
        Encounter GetActiveEncounter(Guid formId, Guid encounterTypeId, Guid clientId);
    }
}