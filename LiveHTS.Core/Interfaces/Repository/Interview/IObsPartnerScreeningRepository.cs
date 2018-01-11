using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Interview;

namespace LiveHTS.Core.Interfaces.Repository.Interview
{
    public interface IObsPartnerScreeningRepository : IRepository<ObsPartnerScreening, Guid>
    {
        void SaveOrUpdate(ObsPartnerScreening obs);
        List<ObsPartnerScreening> Find(Guid clientId,Guid encounterId);
    }
}