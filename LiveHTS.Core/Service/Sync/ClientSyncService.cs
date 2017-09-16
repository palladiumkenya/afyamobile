using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using LiveHTS.Core.Interfaces.Services.Sync;
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

        public Task<List<Module>> GetModules(string url)
        {
            url = GetActivateUrl(url, "modules");

            return _restClient.MakeApiCall<List<Module>>($"{url}", HttpMethod.Get);
        }

        public Task SendClients(string url, List<Client> clients)
        {
            throw new System.NotImplementedException();
        }

        public Task SendClientEncounters(string url, List<Encounter> encounters)
        {
            throw new System.NotImplementedException();
        }

        private string GetActivateUrl(string url, string endpoint)
        {
            return $"{url.HasToEndWith("/")}api/forms/{endpoint}".HasToEndWith("/");
        }


    }
}