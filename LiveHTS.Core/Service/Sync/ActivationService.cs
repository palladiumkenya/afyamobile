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
  public  class ActivationService : IActivationService
  {
      

        private readonly IRestClient _restClient;

        public ActivationService(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public Task<Practice> GetCentral(string url = null)
        {
            return string.IsNullOrEmpty(url)
                ? _restClient.MakeApiCall<Practice>($"http://data.kenyahmis.org:6000/api/activate/central/", HttpMethod.Get)
                : _restClient.MakeApiCall<Practice>($"{url.HasToEndWith("/")}api/activate/central/", HttpMethod.Get);
        }

        public Task<Practice> GetLocal(string url = null)
        {
            return string.IsNullOrEmpty(url)
                ? _restClient.MakeApiCall<Practice>($"http://192.168.1.167:6000/api/activate/local/", HttpMethod.Get)
                : _restClient.MakeApiCall<Practice>($"{url.HasToEndWith("/")}api/activate/local/", HttpMethod.Get);
        }
    }
}
