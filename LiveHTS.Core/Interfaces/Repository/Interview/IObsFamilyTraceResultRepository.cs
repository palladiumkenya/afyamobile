using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Interview;

namespace LiveHTS.Core.Interfaces.Repository.Interview
{
    public interface IObsFamilyTraceResultRepository : IRepository<ObsFamilyTraceResult, Guid>
    {
        void SaveOrUpdate(ObsFamilyTraceResult obs);
        List<ObsFamilyTraceResult> Find(Guid clientId);
    }
}