using System.Collections.Generic;
using System.Threading.Tasks;
using LiveHTS.Core.Model.Config;
using LiveHTS.Core.Model.Lookup;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Core.Model.Survey;
using LiveHTS.Core.SyncModel;

namespace LiveHTS.Core.Interfaces.Services.Sync
{
    public interface IStaffSyncService
    {
        Task<List<Person>> GetStaff(string url);
        Task<List<User>> GetUsers(string url);
        Task<List<Provider>> GetProviders(string url);
    }
}