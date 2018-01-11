using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using LiveHTS.Core.Interfaces.Services.Sync;
using LiveHTS.Core.Model.Lookup;
using LiveHTS.Core.Model.Survey;
using LiveHTS.SharedKernel.Custom;

namespace LiveHTS.Core.Service.Sync
{
    public class FormsSyncService : IFormsSyncService
    {

        private readonly IRestClient _restClient;

        public FormsSyncService(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public Task<List<Module>> GetModules(string url)
        {
            url = GetActivateUrl(url, "modules");

            return _restClient.MakeApiCall<List<Module>>($"{url}", HttpMethod.Get);
        }

        public Task<List<Form>> GetForms(string url)
        {
            url = GetActivateUrl(url, "forms");

            return _restClient.MakeApiCall<List<Form>>($"{url}", HttpMethod.Get);
        }

        public Task<List<Concept>> GetConcepts(string url)
        {
            url = GetActivateUrl(url, "concepts");

            return _restClient.MakeApiCall<List<Concept>>($"{url}", HttpMethod.Get);
        }

        public Task<List<Question>> GetQuestions(string url)
        {
            url = GetActivateUrl(url, "questions");

            return _restClient.MakeApiCall<List<Question>>($"{url}", HttpMethod.Get);
        }


        private string GetActivateUrl(string url, string endpoint)
        {
            return $"{url.HasToEndWith("/")}api/forms/{endpoint}".HasToEndWith("/");
        }
    }
}