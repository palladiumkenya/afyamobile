using System;
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

        public Task SendClients(string url, SyncClientDTO client)
        {
            url = GetActivateUrl(url, "demographics");

            return _restClient.MakeApiCall($"{url}", HttpMethod.Post,client);
        }

        public Task SendClientEncounters(string url, List<SyncClientEncounterDTO> encounters)
        {
            url = GetActivateUrl(url, "encounters");

            return _restClient.MakeApiCall($"{url}", HttpMethod.Post,encounters);
        }

        public Task<List<RemoteClientDTO>> SearchClients(string url, string name)
        {
            
                url = GetActivateUrl(url, $"name/{name}");

                return _restClient.MakeApiCall<List<RemoteClientDTO>>($"{url}", HttpMethod.Get);
            
        }


        public Task<RemoteClientDTO> DownloadClient(string url, Guid id)
        {
            url = GetActivateUrl(url, $"download/{id}");

            return _restClient.MakeApiCall<RemoteClientDTO>($"{url}", HttpMethod.Get);
        }

        private string GetActivateUrl(string url, string endpoint)
        {
            return $"{url.HasToEndWith("/")}api/clients/{endpoint}".HasToEndWith("/");
        }


    }
}