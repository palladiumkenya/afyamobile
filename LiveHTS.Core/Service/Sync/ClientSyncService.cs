using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using LiveHTS.Core.Interfaces.Services.Sync;
using LiveHTS.Core.Model;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Lookup;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Core.Model.Survey;
using LiveHTS.SharedKernel.Custom;

namespace LiveHTS.Core.Service.Sync
{
    public class ClientSyncService : IClientSyncService
    {

        private readonly IRestClient _restClient;

        public ClientSyncService(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public Task SendClients(string url, List<SyncClientDTO> clients)
        {
            url = GetActivateUrl(url, "demographics");

            return _restClient.MakeApiCall<List<Module>>($"{url}", HttpMethod.Post,clients);
        }

        public Task SendClientEncounters(string url, List<Encounter> encounters)
        {
            url = GetActivateUrl(url, "encounters");

            return _restClient.MakeApiCall<List<Module>>($"{url}", HttpMethod.Post,encounters);
        }

        private string GetActivateUrl(string url, string endpoint)
        {
            return $"{url.HasToEndWith("/")}api/clients/{endpoint}".HasToEndWith("/");
        }


    }
}