using System;
using System.Collections.Generic;
using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Interfaces.Repository.Interview;
using LiveHTS.Core.Model.Interview;

namespace LiveHTS.Infrastructure.Repository.Interview
{
    public class ObsRepository : BaseRepository<Obs, Guid>, IObsRepository
    {
        public ObsRepository(ILiveSetting liveSetting) : base(liveSetting)
        {
        }

        public void SaveOrUpdate(Obs obs)
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

        public List<Obs> Find(Guid clientId, Guid questionId)
        {
            return _db.Query<Obs>(@"
SELECT 
    Obs.*
FROM 
    Obs INNER JOIN Encounter ON Obs.EncounterId =Encounter.Id
WHERE
    Encounter.ClientId =? AND
    Obs.QuestionId = ?
", clientId,questionId);

        }
    }
}