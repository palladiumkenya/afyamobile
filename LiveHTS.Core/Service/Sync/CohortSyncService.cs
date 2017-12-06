using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using LiveHTS.Core.Interfaces.Services.Sync;
using LiveHTS.Core.Model.Config;
using LiveHTS.Core.Model.Lookup;
using LiveHTS.Core.Model.Survey;
using LiveHTS.SharedKernel.Custom;

namespace LiveHTS.Core.Service.Sync
{
    public class CohortSyncService : ICohortSyncService
    {

        private readonly IRestClient _restClient;

        public CohortSyncService(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public Task<List<Cohort>> GetCohorts(string url)
        {
            url = GetActivateUrl(url, "lists");

            return _restClient.MakeApiCall<List<Cohort>>($"{url}", HttpMethod.Get);
        }

        private string GetActivateUrl(string url, string endpoint)
        {
            return $"{url.HasToEndWith("/")}api/cohorts/{endpoint}".HasToEndWith("/");
        }
    }
}