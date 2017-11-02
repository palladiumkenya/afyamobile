using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Interview;

namespace LiveHTS.Core.Interfaces.Repository.Interview
{
    public interface IObsPartnerTraceResultRepository : IRepository<ObsPartnerTraceResult, Guid>
    {
        void SaveOrUpdate(ObsPartnerTraceResult obs);
        List<ObsPartnerTraceResult> Find(Guid clientId);
    }
}