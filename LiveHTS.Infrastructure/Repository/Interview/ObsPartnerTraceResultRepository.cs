using System;
using System.Collections.Generic;
using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Interfaces.Repository.Interview;
using LiveHTS.Core.Model.Interview;

namespace LiveHTS.Infrastructure.Repository.Interview
{
    public class ObsPartnerTraceResultRepository : BaseRepository<ObsPartnerTraceResult, Guid>, IObsPartnerTraceResultRepository
    {
        public ObsPartnerTraceResultRepository(ILiveSetting liveSetting) : base(liveSetting)
        {
        }

        public void SaveOrUpdate(ObsPartnerTraceResult obs)
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

        public List<ObsPartnerTraceResult> Find(Guid clientId)
        {
            return _db.Query<ObsPartnerTraceResult>(@"
SELECT 
    ObsPartnerTraceResult.*
FROM 
    ObsPartnerTraceResult INNER JOIN Encounter ON ObsPartnerTraceResult.EncounterId =Encounter.Id
WHERE
    Encounter.ClientId =?", clientId);

        }
    }
}