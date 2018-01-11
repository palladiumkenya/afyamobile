using System;
using System.Collections.Generic;
using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Interfaces.Repository.Interview;
using LiveHTS.Core.Model.Interview;

namespace LiveHTS.Infrastructure.Repository.Interview
{
    public class ObsPartnerScreeningRepository : BaseRepository<ObsPartnerScreening, Guid>, IObsPartnerScreeningRepository
    {
        public ObsPartnerScreeningRepository(ILiveSetting liveSetting) : base(liveSetting)
        {
        }

        public void SaveOrUpdate(ObsPartnerScreening obs)
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

        public List<ObsPartnerScreening> Find(Guid clientId, Guid encounterId)
        {
            return _db.Query<ObsPartnerScreening>(@"
SELECT 
    ObsPartnerScreening.*
FROM 
    ObsPartnerScreening INNER JOIN Encounter ON ObsPartnerScreening.EncounterId =Encounter.Id
WHERE
    Encounter.ClientId =?&&EncounterId =?", clientId, encounterId);

        }
    }
}