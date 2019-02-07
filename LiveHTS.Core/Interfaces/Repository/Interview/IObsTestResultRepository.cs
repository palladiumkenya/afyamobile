using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Interview;

namespace LiveHTS.Core.Interfaces.Repository.Interview
{
    public interface IObsTestResultRepository : IRepository<ObsTestResult, Guid>
    {
        void SaveOrUpdate(ObsTestResult obs);
        List<ObsTestResult> Find(Guid clientId);
       
    }
}