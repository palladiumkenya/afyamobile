using System.Collections.Generic;
using System.Threading.Tasks;
using LiveHTS.Core.Model.Config;
using LiveHTS.Core.Model.Lookup;
using LiveHTS.Core.Model.Survey;
using LiveHTS.Core.SyncModel;

namespace LiveHTS.Core.Interfaces.Services.Sync
{
    public interface ICohortSyncService
    {
        Task<List<Cohort>> GetCohorts(string url);
    }
}