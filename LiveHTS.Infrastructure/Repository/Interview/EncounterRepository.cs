using System;
using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Interfaces.Repository.Interview;
using LiveHTS.Core.Model.Interview;

namespace LiveHTS.Infrastructure.Repository.Interview
{
    public class EncounterRepository : BaseRepository<Encounter, Guid>, IEncounterRepository
    {
        public EncounterRepository(ILiveSetting liveSetting) : base(liveSetting)
        {
        }

        public Encounter Load(Guid id, bool includeObs = false)
        {
            var encounter = Get(id);

            if (includeObs)
            {
                if (null != encounter)
                {
                    var obses = _db.Table<Obs>()
                        .Where(x => x.EncounterId == encounter.Id)
                        .ToList();
                    encounter.Obses = obses;
                }
            }
            
            return encounter;
        }

        public Encounter Load(Guid formId, Guid encounterTypeId, Guid clientId, bool includeObs = false)
        {
            var encounter= GetAll(x => x.FormId == formId &&
                               x.EncounterTypeId == encounterTypeId &&
                               x.ClientId == clientId)
                .FirstOrDefault();

            if (includeObs)
            {
                if (null != encounter)
                {
                    var obses = _db.Table<Obs>()
                        .Where(x => x.EncounterId == encounter.Id)
                        .ToList();
                    encounter.Obses = obses;
                }
            }

            return encounter;
        }

        public IEnumerable<Encounter> LoadAll(Guid formId, Guid clientId, bool includeObs = false)
        {
            var encounters = GetAll(x => x.FormId == formId &&
                               x.ClientId == clientId)
                .ToList();

            if (includeObs)
            {
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
            }
            return encounters;
        }

        [System.Obsolete("User LoadAll instead.")]
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