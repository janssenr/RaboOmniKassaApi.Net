using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using RaboOmniKassaApi.Net.Exceptions;
using RaboOmniKassaApi.Net.Helpers;

namespace RaboOmniKassaApi.Net.Connectors.Http
{
    /// <summary>
    /// Summary description for HttpClientRestTemplate
    /// </summary>
    public class HttpClientRestTemplate : IRestTemplate
    {
        private readonly HttpClient _client;
        private string _token;

        public HttpClientRestTemplate(string baseUrl)
        {
            _client = new HttpClient { BaseAddress = new Uri(baseUrl) };
        }

        public void SetToken(string token)
        {
            _token = token;
        }

        public string Get(string path, string[] parameters = null)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var response = _client.GetAsync(path).Result;
            if (response.IsSuccessStatusCode)
                return response.Content.ReadAsStringAsync().Result;
            HandleResponseError(response);
            return null;
        }

        public string Post(string path, string body = null)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var content = new StringContent(body, Encoding.UTF8, "application/json");
            var response = _client.PostAsync(path, content).Result;
            if (response.IsSuccessStatusCode)
                return response.Content.ReadAsStringAsync().Result;
            HandleResponseError(response);
            return null;
        }

        private void HandleResponseError(HttpResponseMessage response)
        {
            string message;
            switch (response.StatusCode)
            {
                case HttpStatusCode.NotFound:
                    message = "Page not found";
                    break;
                default:
                    var responseAsJson = response.Content.ReadAsStringAsync().Result;
                    var result = JsonHelper.Deserialize<RaboOmniKassaError>(responseAsJson);
                    message = result.ConsumerMessage ?? result.ErrorMessage ?? $"ERROR CODE {result.ErrorCode}";
                    break;
            }
            throw new RaboOmniKassaException(message);
        }
    }
}