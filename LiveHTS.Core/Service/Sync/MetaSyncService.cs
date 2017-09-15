using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using LiveHTS.Core.Interfaces.Services.Sync;
using LiveHTS.Core.Model;
using LiveHTS.Core.Model.Config;
using LiveHTS.Core.Model.Lookup;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Core.SyncModel;
using LiveHTS.SharedKernel.Custom;

namespace LiveHTS.Core.Service.Sync
{
  public  class MetaSyncService: IMetaSyncService
    {

        private readonly IRestClient _restClient;

        public MetaSyncService(IRestClient restClient)
        {
            _restClient = restClient;
        }
     

        Task<Meta> IMetaSyncService.GetMetaData(string url)
        {
            url = GetActivateUrl(url, "data");

            return _restClient.MakeApiCall<Meta>($"{url}", HttpMethod.Get);
        }

        public Task<List<County>> GetCounties(string url)
        {
            url = GetActivateUrl(url, "counties");

            return _restClient.MakeApiCall<List<County>>($"{url}", HttpMethod.Get);
        }

        public Task<List<Category>> GetCategories(string url)
        {
            url = GetActivateUrl(url, "categories");

            return _restClient.MakeApiCall<List<Category>>($"{url}", HttpMethod.Get);
        }

        public Task<List<Item>> GetItems(string url)
        {
            url = GetActivateUrl(url, "items");

            return _restClient.MakeApiCall<List<Item>>($"{url}", HttpMethod.Get);
        }

        public Task<List<CategoryItem>> GetCatItems(string url)
        {
            url = GetActivateUrl(url, "catitems");

            return _restClient.MakeApiCall<List<CategoryItem>>($"{url}", HttpMethod.Get);
        }

        private string GetActivateUrl(string url, string endpoint)
        {
            return $"{url.HasToEndWith("/")}api/meta/{endpoint}".HasToEndWith("/");
        }
    }
}
