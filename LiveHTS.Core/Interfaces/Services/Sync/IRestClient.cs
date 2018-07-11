using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace LiveHTS.Core.Interfaces.Services.Sync
{
    public interface IRestClient
    {
        Task<TResult> MakeApiCall<TResult>(string url, HttpMethod method, object data = null)
            where TResult : class;
        Task MakeApiCall(string url, HttpMethod method, object data = null);
        Task<bool> AttemptMakeApiCall(string url, HttpMethod method, object data = null);
        Task<string> AttemptMakeApiCallResult(string url, HttpMethod method, object data = null);
        Exception Error { get; }
    }
}