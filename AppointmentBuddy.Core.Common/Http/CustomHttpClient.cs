using AppointmentBuddy.Core.Common.Helper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBuddy.Core.Common.Http
{
    public class CustomHttpClient : IHttpClient
    {
        private AuthenticationHeaderValue _authorization;
        private readonly HttpClient _client;
        private readonly ILogger _logger;

        public CustomHttpClient()
        {
            _client = new HttpClient();
        }

        public CustomHttpClient(HttpClient client, ILogger logger)
        {
            _client = client;
            _logger = logger;
        }

        public HttpClient Client
        {
            get
            {
                return _client;
            }
        }

        public AuthenticationHeaderValue Authorization
        {
            set
            {
                _authorization = value;
            }
        }

        public async Task<HttpResponseMessage> GetAsync(string uri)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            var response = await _client.SendAsync(requestMessage);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                _logger.LogInformation($"{response.StatusCode} GetAsync() RequestUri:[{uri}]");
            }

            return response;
        }

        public async Task<string> GetStringAsync(string uri)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, Validator.IsSafeHost(uri));

            _logger.LogInformation("[" + uri + "] " + requestMessage.Headers.ToString());
            var response = await _client.SendAsync(requestMessage);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                _logger.LogInformation($"{response.StatusCode} GetStringAsync() RequestUri:[{uri}]");
            }

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<HttpResponseMessage> PostAsync(string uri, StringContent content, string connectionId = null)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, uri)
            {
                Content = content
            };

            _logger.LogInformation("[" + uri + "] " + requestMessage.Headers.ToString());
            var response = await _client.SendAsync(requestMessage);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                _logger.LogInformation($"{response.StatusCode} PostAsync() RequestUri:[{uri}]");
                //throw new HttpRequestException();
            }

            return response;
        }

        public HttpRequestHeaders DefaultRequestHeaders
        {
            get
            {
                return _client.DefaultRequestHeaders;
            }
        }
    }
}
