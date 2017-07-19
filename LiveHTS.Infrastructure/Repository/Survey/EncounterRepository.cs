using System;
using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Interfaces.Repository.Survey;
using LiveHTS.Core.Model.Interview;

namespace LiveHTS.Infrastructure.Repository.Survey
{
    public class EncounterRepository : BaseRepository<Encounter, Guid>, IEncounterRepository
    {
        public EncounterRepository(ILiveSetting liveSetting) : base(liveSetting)
        {
        }
        public override Encounter Get(Guid id, bool voided = false)
        {
            var encounter = base.Get(id, voided);
            if (null != encounter)
            {
                var obses = _db.Table<Obs>()
                    .Where(x => x.EncounterId == encounter.Id)
                    .ToList();
                encounter.Obses = obses;
            }
            return encounter;
        }
        public IEnumerable<Encounter> GetWithObs(Guid formId, Guid encounterTypeId, Guid clientId)
        {
            var encounters = GetAll(
                x => x.FormId == formId &&
                     x.EncounterTypeId == encounterTypeId &&
                     x.ClientId == clientId)
                     .ToList();

            foreach (var e in encounters)
            {
                if (null != e)
                {
                    var obses = _db.Table<Obs>()
                        .Where(x => x.EncounterId == e.Id)
                        .ToList();
                    e.Obses = obses;
                }
            }

            return encounters;
        }       
    }
}