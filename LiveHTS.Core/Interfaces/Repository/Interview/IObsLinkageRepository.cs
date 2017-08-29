using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Interview;

namespace LiveHTS.Core.Interfaces.Repository.Interview
{
    public interface IObsLinkageRepository : IRepository<ObsLinkage, Guid>
    {
        void SaveOrUpdate(ObsLinkage obs);
        List<ObsLinkage> Find(Guid clientId,Guid encounterId);
    }
}