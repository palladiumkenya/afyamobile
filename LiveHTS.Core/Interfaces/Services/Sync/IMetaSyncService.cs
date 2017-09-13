using System.Collections.Generic;
using System.Threading.Tasks;
using LiveHTS.Core.Model.Config;

namespace LiveHTS.Core.Interfaces.Services.Sync
{
    public interface IMetaSyncService
    {
        Task<List<County>> GetAll(string url = null);
    }
}