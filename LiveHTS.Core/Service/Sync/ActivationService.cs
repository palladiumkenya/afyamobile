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

      public Task<Practice> SearchLocal(string url,string code)
      {
          url = GetActivateUrl(url, $"enroll/{code}");

          return _restClient.MakeApiCall<Practice>($"{url}", HttpMethod.Get);
        }

      public Task<Practice> SearchCentral(string url, string code)
      {
          url = GetActivateUrl(url, $"enroll/{code}");

          return _restClient.MakeApiCall<Practice>($"{url}", HttpMethod.Get);
        }

      public Task<Practice> GetCentral(string url)
        {
            url = GetActivateUrl(url, "central");

            return  _restClient.MakeApiCall<Practice>($"{url}", HttpMethod.Get);
        }

        public Task<Practice> GetLocal(string url)
        {
            url =GetActivateUrl(url, "local");

            return _restClient.MakeApiCall<Practice>($"{url}", HttpMethod.Get);
        }

      public Task<Practice> Register(string device, string url)
      {
          throw new NotImplementedException();
      }

      private string GetActivateUrl(string url,string endpoint)
      {
          return $"{url.HasToEndWith("/")}api/activate/{endpoint}".HasToEndWith("/");
      }
  }
}
