using System.Threading.Tasks;
using LiveHTS.Core.Model;
using LiveHTS.Core.Model.Subject;

namespace LiveHTS.Core.Interfaces.Services.Sync
{
    public interface ISyncUser

    {
        Task<PagedResult<User>> GetPlanetsAsync(string url = null);
        void Pull();
        void Push();

    }
}