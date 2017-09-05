using System.Net.Http;
using System.Threading.Tasks;
using LiveHTS.Core.Interfaces.Services.Sync;
using LiveHTS.Core.Model;
using LiveHTS.Core.Model.Subject;
using Microsoft.VisualBasic;

namespace LiveHTS.Core.Service.Sync
{
    public class SyncUser:ISyncUser
    {


        private readonly IRestClient _restClient;

        public SyncUser(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public Task<PagedResult<User>> GetPlanetsAsync(string url = null)
        {
            return string.IsNullOrEmpty(url)
                ? _restClient.MakeApiCall<PagedResult<User>>($"http://192.168.1.192:1575/Api/User/", HttpMethod.Get)
                : _restClient.MakeApiCall<PagedResult<User>>(url, HttpMethod.Get);
        }

        public void Pull()
        {
            throw new System.NotImplementedException();
        }

        public void Push()
        {
            throw new System.NotImplementedException();
        }
    }
}