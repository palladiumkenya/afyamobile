using System.Collections.Generic;
using System.Threading.Tasks;
using LiveHTS.Core.Model.Config;

namespace LiveHTS.Core.Interfaces.Services.Sync
{
    public interface IActivationService
    {
        Task<string> AttempCheckVersion(string url);

        Task<Practice> SearchLocal(string url, string code);
        Task<Practice> SearchCentral( string url,string code);

        Task<Practice> GetCentral(string url);
        Task<Practice> GetLocal(string url);
        Task<Practice> Register(string device, string url);
        Task<bool> AttemptEnrollPractice(string url, List<Practice> practices);
        Task<string> AttemptEnrollDevice(string url,Device device);
    }
}