using System;
using LiveHTS.Core.Model.Interview;

namespace LiveHTS.Core.Interfaces.Repository.Interview
{
    public interface IObsRepository : IRepository<Obs, Guid>
    {
        void SaveOrUpdate(Obs obs);
    }
}