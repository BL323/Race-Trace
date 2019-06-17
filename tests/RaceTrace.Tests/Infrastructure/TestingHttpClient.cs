// Copyright (c) Red Bull Technology Ltd. All rights reserved.

using System;
using System.Net.Http;
using System.Threading.Tasks;
using ErgastApi.Abstractions;

namespace RaceTrace.Tests.Infrastructure
{
    public class TestingHttpClient : IHttpClient, IDisposable
    {
        private HttpClient HttpClient { get; }

        public TestingHttpClient(HttpClient client)
        {
            HttpClient = client;
        }

        public Task<HttpResponseMessage> GetAsync(string requestUri)
        {
            return HttpClient.GetAsync(requestUri);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
                return;

            HttpClient?.Dispose();
        }
    }
}