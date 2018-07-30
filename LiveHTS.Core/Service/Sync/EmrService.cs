using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using LiveHTS.Core.Interfaces.Services.Sync;
using LiveHTS.Core.Model;
using LiveHTS.Core.Model.Config;
using LiveHTS.Core.Model.Subject;
using LiveHTS.SharedKernel.Custom;

namespace LiveHTS.Core.Service.Sync
{
    public class EmrService : IEmrService
    {
        private readonly IRestClient _restClient;

        public EmrService(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public Task<Practice> GetDefault(string url)
        {
            url = GetActivateUrl(url, $"fac");

            return _restClient.MakeApiCall<Practice>($"{url}", HttpMethod.Get);
        }
        public Task<List<Practice>> GetAllDefault(string url)
        {
            url = GetActivateUrl(url, $"facall");

            return _restClient.MakeApiCall<List<Practice>>($"{url}", HttpMethod.Get);
        }

        public Task<List<User>> GetUsers(string url)
        {
            url = GetActivateUrl(url, $"user");

            return _restClient.MakeApiCall<List<User>>($"{url}", HttpMethod.Get);
        }


        private string GetActivateUrl(string url, string endpoint)
        {
            return $"{url.HasToEndWith("/")}api/setup/{endpoint}".HasToEndWith("/");
        }
    }
}
