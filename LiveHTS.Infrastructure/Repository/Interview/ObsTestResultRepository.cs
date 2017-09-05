using System;
using System.Collections.Generic;
using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Interfaces.Repository.Interview;
using LiveHTS.Core.Model.Interview;

namespace LiveHTS.Infrastructure.Repository.Interview
{
    public class ObsTestResultRepository : BaseRepository<ObsTestResult, Guid>, IObsTestResultRepository
    {
        public ObsTestResultRepository(ILiveSetting liveSetting) : base(liveSetting)
        {
        }

        public void SaveOrUpdate(ObsTestResult obs)
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

        public List<ObsTestResult> Find(Guid clientId)
        {
            return _db.Query<ObsTestResult>(@"
SELECT 
    ObsTestResult.*
FROM 
    ObsTestResult INNER JOIN Encounter ON ObsTestResult.EncounterId =Encounter.Id
WHERE
    Encounter.ClientId =?", clientId);

        }
    }
}