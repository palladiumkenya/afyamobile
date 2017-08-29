using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Interview;

namespace LiveHTS.Core.Interfaces.Repository.Interview
{
    public interface IObsTraceResultRepository : IRepository<ObsTraceResult, Guid>
    {
        void SaveOrUpdate(ObsTraceResult obs);
        List<ObsTraceResult> Find(Guid clientId);
    }
}