using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace WeatherService.Services
{
    public class HttpClientFactory : IHttpClientFactory
    {
        public HttpClient _httpClient;

        public HttpClientFactory()
        {
            _httpClient = new HttpClient();

            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "WeatherService");
            _httpClient.DefaultRequestHeaders.Add("Accept-Language", "en-US,en;q=0.5");
        }

        public HttpClient GetHttpClient()
        {
            return _httpClient;
        }

        public HttpClient GetHttpClient(string baseAddress)
        {
            _httpClient.BaseAddress = new Uri(baseAddress);
            return _httpClient;
        }
    }
}
