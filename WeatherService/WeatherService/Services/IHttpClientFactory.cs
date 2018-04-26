using System.Net.Http;

namespace WeatherService.Services
{
    public interface IHttpClientFactory
    {
        HttpClient GetHttpClient();
        HttpClient GetHttpClient(string baseAddress);
    }
}
