using System;
using System.Collections.Generic;
using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Interfaces.Repository.Interview;
using LiveHTS.Core.Model.Interview;

namespace LiveHTS.Infrastructure.Repository.Interview
{
    public class ObsLinkageRepository : BaseRepository<ObsLinkage, Guid>, IObsLinkageRepository
    {
        public ObsLinkageRepository(ILiveSetting liveSetting) : base(liveSetting)
        {
        }

        public void SaveOrUpdate(ObsLinkage obs)
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

        public List<ObsLinkage> Find(Guid clientId, Guid encounterId)
        {
            return _db.Query<ObsLinkage>(@"
SELECT 
    ObsLinkage.*
FROM 
    ObsLinkage INNER JOIN Encounter ON ObsLinkage.EncounterId =Encounter.Id
WHERE
    Encounter.ClientId =?&&EncounterId =?", clientId, encounterId);

        }
    }
}