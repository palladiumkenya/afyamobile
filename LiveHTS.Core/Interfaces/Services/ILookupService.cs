using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Config;

namespace LiveHTS.Core.Interfaces.Services
{
    public interface ILookupService
    {
        IEnumerable<County> GetCounties();
        IEnumerable<SubCounty> GetSubCounties(Guid[] countyIds);
        IEnumerable<PracticeType> GetPracticeTypes();
        IEnumerable<Practice> GetPractices(Guid[] typeIds);
    }
}