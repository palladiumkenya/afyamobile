using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Interview;

namespace LiveHTS.Core.Interfaces.Repository.Interview
{
    public interface IObsMemberScreeningRepository : IRepository<ObsMemberScreening, Guid>
    {
        void SaveOrUpdate(ObsMemberScreening obs);
        List<ObsMemberScreening> Find(Guid clientId,Guid encounterId);
    }
}