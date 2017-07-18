using System;
using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Interfaces.Repository.Survey;
using LiveHTS.Core.Model.Interview;

namespace LiveHTS.Infrastructure.Repository.Survey
{
    public class EncounterRepository:BaseRepository<Encounter,Guid>, IEncounterRepository
    {
        public EncounterRepository(ILiveSetting liveSetting) : base(liveSetting)
        {
        }

        public Encounter GetActiveEncounter(Guid formId, Guid encounterTypeId, Guid clientId)
        {
            throw new NotImplementedException();
        }
    }
}