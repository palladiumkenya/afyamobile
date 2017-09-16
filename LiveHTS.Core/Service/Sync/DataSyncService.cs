using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using LiveHTS.Core.Interfaces.Services.Sync;
using LiveHTS.Core.Model.Lookup;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Core.Model.Survey;
using LiveHTS.SharedKernel.Custom;

namespace LiveHTS.Core.Service.Sync
{
    public class DataSyncService : IDataSyncService
    {

        private readonly IRestClient _restClient;

        public DataSyncService(IRestClient restClient)
        {
            _restClient = restClient;
        }
        public Task<List<Person>> GetStaff(string url)
        {
            url = GetActivateUrl(url, "persons");

            return _restClient.MakeApiCall<List<Person>>($"{url}", HttpMethod.Get);
        }

        public Task<List<User>> GetUsers(string url)
        {
            url = GetActivateUrl(url, "users");

            return _restClient.MakeApiCall<List<User>>($"{url}", HttpMethod.Get);
        }

        public Task<List<Provider>> GetProviders(string url)
        {
            url = GetActivateUrl(url, "providers");

            return _restClient.MakeApiCall<List<Provider>>($"{url}", HttpMethod.Get);
        }

        private string GetActivateUrl(string url, string endpoint)
        {
            return $"{url.HasToEndWith("/")}api/staff/{endpoint}".HasToEndWith("/");
        }

       
    }
}