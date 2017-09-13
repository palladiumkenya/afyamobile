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

namespace LiveHTS.Core.Service.Sync
{
  public  class MetaSyncService: IMetaSyncService
    {

        private readonly IRestClient _restClient;

        public MetaSyncService(IRestClient restClient)
        {
            _restClient = restClient;
        }
        public Task<List<County>> GetAll(string url = null)
        {
            return string.IsNullOrEmpty(url)
                ? _restClient.MakeApiCall<List<County>>($"http://localhost:5000/api/counties/", HttpMethod.Get)
                : _restClient.MakeApiCall<List<County>>(url, HttpMethod.Get);
        }
    }
}
