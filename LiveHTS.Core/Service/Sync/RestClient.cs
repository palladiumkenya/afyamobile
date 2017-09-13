using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using LiveHTS.Core.Interfaces.Services.Sync;
using ModernHttpClient;
using MvvmCross.Platform.Platform;
using Newtonsoft.Json;

namespace LiveHTS.Core.Service.Sync
{
    public class RestClient : IRestClient
    {
        
        public async Task<TResult> MakeApiCall<TResult>(string url, HttpMethod method, object data = null) where TResult : class
        {
            

            using (var httpClient = new HttpClient(new NativeMessageHandler { UseCookies = false }))
            {
                using (var request = new HttpRequestMessage { RequestUri = new Uri(url), Method = method })
                {
                    // add content
                    if (method != HttpMethod.Get)
                    {
                        var json = JsonConvert.SerializeObject(data);
                        request.Content = new StringContent(json, Encoding.UTF8, "application/json");
                    }

                    HttpResponseMessage response = new HttpResponseMessage();
                    try
                    {
                        response = await httpClient.SendAsync(request).ConfigureAwait(false);
                    }
                    catch (Exception ex)
                    {
                        // log error
                    }

                    var stringSerialized = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                    // deserialize content
                    return JsonConvert.DeserializeObject<TResult>(stringSerialized);
                }
            }
        }
    }
}