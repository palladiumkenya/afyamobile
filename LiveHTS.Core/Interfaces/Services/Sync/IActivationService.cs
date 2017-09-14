using System.Threading.Tasks;
using LiveHTS.Core.Model.Config;

namespace LiveHTS.Core.Interfaces.Services.Sync
{
    public interface IActivationService
    {
        Task<Practice> GetCentral(string url = null);
        Task<Practice> GetLocal(string url = null);
    }
}