using System;
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
    }
}