using System;
using System.Collections.Generic;
using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Interfaces.Repository.Interview;
using LiveHTS.Core.Model.Interview;

namespace LiveHTS.Infrastructure.Repository.Interview
{
    public class ObsFamilyTraceResultRepository : BaseRepository<ObsFamilyTraceResult, Guid>, IObsFamilyTraceResultRepository
    {
        public ObsFamilyTraceResultRepository(ILiveSetting liveSetting) : base(liveSetting)
        {
        }

        public void SaveOrUpdate(ObsFamilyTraceResult obs)
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

        public List<ObsFamilyTraceResult> Find(Guid clientId)
        {
            return _db.Query<ObsFamilyTraceResult>(@"
SELECT 
    ObsFamilyTraceResult.*
FROM 
    ObsFamilyTraceResult INNER JOIN Encounter ON ObsFamilyTraceResult.EncounterId =Encounter.Id
WHERE
    Encounter.ClientId =?", clientId);

        }
    }
}