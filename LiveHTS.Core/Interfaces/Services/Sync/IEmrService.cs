using System.Collections.Generic;
using System.Threading.Tasks;
using LiveHTS.Core.Model.Config;
using LiveHTS.Core.Model.Subject;

namespace LiveHTS.Core.Interfaces.Services.Sync
{
    public interface IEmrService
    {
        Task<Practice> GetDefault(string url);
        Task<List<User>> GetUsers( string url);
    }
}