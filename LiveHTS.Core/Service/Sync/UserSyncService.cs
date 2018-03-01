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
    public class UserSyncService : IUserSyncService
    {

        private readonly IRestClient _restClient;

        public UserSyncService(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public Task<List<UserSummary>> DownloadSummary(string url, Guid id)
        {
            url = GetActivateUrl(url, $"user/{id}");

            return _restClient.MakeApiCall<List<UserSummary>>($"{url}", HttpMethod.Get);
        }
        private string GetActivateUrl(string url, string endpoint)
        {
            return $"{url.HasToEndWith("/")}api/summary/{endpoint}".HasToEndWith("/");
        }
    }
}