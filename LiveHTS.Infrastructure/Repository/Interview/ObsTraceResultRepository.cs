using System;
using System.Collections.Generic;
using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Interfaces.Repository.Interview;
using LiveHTS.Core.Model.Interview;

namespace LiveHTS.Infrastructure.Repository.Interview
{
    public class ObsTraceResultRepository : BaseRepository<ObsTraceResult, Guid>, IObsTraceResultRepository
    {
        public ObsTraceResultRepository(ILiveSetting liveSetting) : base(liveSetting)
        {
        }

        public void SaveOrUpdate(ObsTraceResult obs)
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

        public List<ObsTraceResult> Find(Guid clientId)
        {
            return _db.Query<ObsTraceResult>(@"
SELECT 
    ObsTraceResult.*
FROM 
    ObsTraceResult INNER JOIN Encounter ON ObsTraceResult.EncounterId =Encounter.Id
WHERE
    Encounter.ClientId =?", clientId);

        }
    }
}