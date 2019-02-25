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
        private Exception _error;

        public async Task<TResult> MakeApiCall<TResult>(string url, HttpMethod method, object data = null) where TResult : class
        {
            using (var httpClient = new HttpClient(new NativeMessageHandler { UseCookies = false }))
            {
                using (var request = new HttpRequestMessage {RequestUri = new Uri(url), Method = method})
                {

                    // add content


                    JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
                    {
                        DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                    };

                    if (method != HttpMethod.Get)
                    {
                        var json = JsonConvert.SerializeObject(data, microsoftDateFormatSettings);
                        request.Content = new StringContent(json, Encoding.UTF8, "application/json");
                    }

                    HttpResponseMessage response = null;

                    try
                    {
                        response = await httpClient.SendAsync(request).ConfigureAwait(false);
                    }
                    catch (Exception ex)
                    {
                        _error = ex;
                        // log error
                    }

                    if (null == response)
                    {
                        return null;
                    }
                    if (null == response.Content)
                    {
                        return null;
                    }

                    var stringSerialized = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                    if (string.IsNullOrWhiteSpace(stringSerialized))
                        return null;

                    // deserialize content
                    
                    return JsonConvert.DeserializeObject<TResult>(stringSerialized);
                }
            }
        }

        public async Task MakeApiCall(string url, HttpMethod method, object data = null)
        {
            using (var httpClient = new HttpClient(new NativeMessageHandler { UseCookies = false }))
            {
                using (var request = new HttpRequestMessage { RequestUri = new Uri(url), Method = method })
                {

                    // add content


                    JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
                    {
                        DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                    };

                        if (method != HttpMethod.Get)
                    {
                        var json = JsonConvert.SerializeObject(data, microsoftDateFormatSettings);
                        request.Content = new StringContent(json, Encoding.UTF8, "application/json");
                    }

                    HttpResponseMessage response = null;

                    try
                    {
                        await httpClient.SendAsync(request).ConfigureAwait(false);
                    }
                    catch (Exception ex)
                    {
                        _error = ex;
                    }
                }
            }
        }

        public async Task<bool> AttemptMakeApiCall(string url, HttpMethod method, object data = null)
        {
            using (var httpClient = new HttpClient(new NativeMessageHandler { UseCookies = false }))
            {
                using (var request = new HttpRequestMessage { RequestUri = new Uri(url), Method = method })
                {

                    // add content


                    JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
                    {
                        DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                    };

                    if (method != HttpMethod.Get)
                    {
                        var json = JsonConvert.SerializeObject(data, microsoftDateFormatSettings);
                        request.Content = new StringContent(json, Encoding.UTF8, "application/json");
                    }


                    try
                    {
                        var response=await httpClient.SendAsync(request).ConfigureAwait(false);
                        if (null != response)
                            return response.IsSuccessStatusCode;
                    }
                    catch (Exception ex)
                    {
                        _error = ex;
                        
                    }
                }
            }

            return false;
        }

        public async Task<string> AttemptMakeApiCallResult(string url, HttpMethod method, object data = null)
        {
            string responsestring = string.Empty;

            using (var httpClient = new HttpClient(new NativeMessageHandler { UseCookies = false }))
            {
                using (var request = new HttpRequestMessage { RequestUri = new Uri(url), Method = method })
                {

                    // add content


                    JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
                    {
                        DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                    };

                    if (method != HttpMethod.Get)
                    {
                        var json = JsonConvert.SerializeObject(data, microsoftDateFormatSettings);
                        request.Content = new StringContent(json, Encoding.UTF8, "application/json");
                    }


                    try
                    {
                        var response = await httpClient.SendAsync(request).ConfigureAwait(false);
                        if (null != response)
                            responsestring = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    }
                    catch (Exception ex)
                    {
                        _error = ex;

                    }
                }
            }

            return responsestring;
        }

        public Exception Error
        {
            get { return _error; }
        }
    }
}