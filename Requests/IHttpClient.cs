using System;
using System.Net.Http;

namespace Vik_WeatherService.Requests{
    public interface IHttpClient
    {
        HttpClient HttpClient{get;}
        
    }
    public class WeatherServicettpClient : IHttpClient
    {
        public HttpClient HttpClient => new HttpClient();
    }
}