using System;
using System.Collections.Generic;
using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Interfaces.Repository.Interview;
using LiveHTS.Core.Model.Interview;

namespace LiveHTS.Infrastructure.Repository.Interview
{
    public class ObsMemberScreeningRepository : BaseRepository<ObsMemberScreening, Guid>, IObsMemberScreeningRepository
    {
        public ObsMemberScreeningRepository(ILiveSetting liveSetting) : base(liveSetting)
        {
        }

        public void SaveOrUpdate(ObsMemberScreening obs)
        {
            var existingObs = Get(obs.Id);
            if (null != existingObs)
            {
                Update(obs);
            }
            else
            {
                Save(obs);
            }
        }

        public List<ObsMemberScreening> Find(Guid clientId, Guid encounterId)
        {
            return _db.Query<ObsMemberScreening>(@"
SELECT 
    ObsMemberScreening.*
FROM 
    ObsMemberScreening INNER JOIN Encounter ON ObsMemberScreening.EncounterId =Encounter.Id
WHERE
    Encounter.ClientId =?&&EncounterId =?", clientId, encounterId);

        }
    }
}