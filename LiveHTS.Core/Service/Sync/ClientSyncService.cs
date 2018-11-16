using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using LiveHTS.Core.Interfaces.Services.Sync;
using LiveHTS.Core.Model;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Lookup;
using LiveHTS.Core.Model.SmartCard;
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

        public Task SendClientShrs(string url, List<PSmartStore> pSmartStores)
        {
            url = GetActivateUrl(url, "shrs");

            return _restClient.MakeApiCall($"{url}", HttpMethod.Post, pSmartStores);
        }

        public async Task<bool> AttempSendClients(string url, SyncClientDTO client)
        {
            url = GetActivateUrl(url, "demographics");

            return await _restClient.AttemptMakeApiCall($"{url}", HttpMethod.Post, client);
        }

        public async Task<bool> AttempSendClientEncounters(string url, List<SyncClientEncounterDTO> encounters)
        {

            url = GetActivateUrl(url, "encounters");

            return await _restClient.AttemptMakeApiCall($"{url}", HttpMethod.Post, encounters);
        }

        public async Task<bool> AttempSendClientShrs(string url, List<PSmartStore> pSmartStores)
        {
            url = GetActivateUrl(url, "shrs");

            return await _restClient.AttemptMakeApiCall($"{url}", HttpMethod.Post, pSmartStores);
        }

        public Task<List<RemoteClientDTO>> SearchClients(string url, string name, Guid? practiceId=null)
        {
            if (practiceId.IsNullOrEmpty())
            {
                url = GetActivateUrl(url, $"name/{name}");
            }
            else
            {
                url = GetActivateUrl(url, $"name/{practiceId.Value}/{name}");
            }

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