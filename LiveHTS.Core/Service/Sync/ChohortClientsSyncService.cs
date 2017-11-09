using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using LiveHTS.Core.Interfaces.Services.Sync;
using LiveHTS.Core.Model;
using LiveHTS.Core.Model.Lookup;
using LiveHTS.Core.Model.Survey;
using LiveHTS.SharedKernel.Custom;

namespace LiveHTS.Core.Service.Sync
{
    public class ChohortClientsSyncService : IChohortClientsSyncService
    {

        private readonly IRestClient _restClient;

        public ChohortClientsSyncService(IRestClient restClient)
        {
            _restClient = restClient;
        }
         public Task<List<RemoteClientDTO>> GetClients(string url,string id)
        {
            url = GetActivateUrl(url, $"id/{id}");

            return _restClient.MakeApiCall<List<RemoteClientDTO>>($"{url}", HttpMethod.Get);
        }
   
        private string GetActivateUrl(string url, string endpoint)
        {
            return $"{url.HasToEndWith("/")}api/cohorts/{endpoint}".HasToEndWith("/");
        }
    }
}