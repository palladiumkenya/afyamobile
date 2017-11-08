using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Config;

namespace LiveHTS.Core.Interfaces.Services.Config
{
    public interface ICohortService
    {
        IEnumerable<Cohort> GetAllCohorts(string search);
        IEnumerable<Cohort> GetAllCohorts();
    }
}