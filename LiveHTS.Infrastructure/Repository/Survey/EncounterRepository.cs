using System;
using System.Linq;
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
            var encounter = GetAll(
                x => x.FormId == formId&& 
                x.EncounterTypeId == encounterTypeId&& 
                x.ClientId == clientId)
                .FirstOrDefault();

            if (null != encounter)
            {
                var obses = _db.Table<Obs>()
                    .Where(x=>x.EncounterId==encounter.Id)                    
                    .ToList();
                encounter.Obses = obses;
            }

            return encounter;
        }
    }
}