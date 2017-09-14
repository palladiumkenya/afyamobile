﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace LiveHTS.Core.Interfaces.Services.Sync
{
    public interface IRestClient
    {
        Task<TResult> MakeApiCall<TResult>(string url, HttpMethod method, object data = null)
            where TResult : class;
        Exception Error { get; }
    }
}