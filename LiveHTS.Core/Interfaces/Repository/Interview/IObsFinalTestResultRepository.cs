using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Interview;

namespace LiveHTS.Core.Interfaces.Repository.Interview
{
    public interface IObsFinalTestResultRepository : IRepository<ObsFinalTestResult, Guid>
    {
        void SaveOrUpdate(ObsFinalTestResult obs);
        List<ObsFinalTestResult> Find(Guid clientId,Guid encounterId);
    }
}