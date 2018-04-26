using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using WeatherService.Models;

namespace WeatherService.Services
{
    public class OpenWeatherMapService : IWeatherService
    {
        private readonly HttpClient _httpClient;

        public OpenWeatherMapService()
            : this(new HttpClientFactory())
        {

        }

        public OpenWeatherMapService(IHttpClientFactory clientFactory)
        {
            _httpClient = clientFactory.GetHttpClient();
            _httpClient.BaseAddress = new Uri("http://api.openweathermap.org/data/2.5");
        }

        /// <summary>
        /// Fetch the weather data from OpenWeatherMap API. The weatherMode parameter dictates whether
        /// the data returned is the current weather or the 5 day forecast.
        /// Current Weather Template: http://api.openweathermap.org/data/2.5/weather?q={city name},{state},{country code}
        /// 5-Day Forecast Template: http://api.openweathermap.org/data/2.5/forecast?q={city name},{state},{country code}
        /// </summary>
        /// <param name="city"></param>
        /// <param name="state"></param>
        /// <param name="country"></param>
        /// <returns>weatherData</returns>
        public async Task<T> FetchWeather<T>(string city, string state, string country, string weatherMode)
        {
            HttpClient httpClient = _httpClient;
            T weatherData = default(T);
            
            UriBuilder uriBuilder = new UriBuilder(httpClient.BaseAddress);
            uriBuilder.Path += string.Format("/{0}", weatherMode);//current weather or 5 day forecast?

            // build the query strings
            var query = HttpUtility.ParseQueryString(string.Empty);
            if (!string.IsNullOrEmpty(state))
            {
                query["q"] = string.Format("{0},{1},{2}", city, state, country);
            }
            else
            {
                query["q"] = string.Format("{0},{1},", city, country);
            }

            query["APPID"] = "71554fa4f20bca95915d02a0c8c26e59";
            uriBuilder.Query = query.ToString();

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uriBuilder.ToString());
            HttpResponseMessage response = await httpClient.SendAsync(request);

            if (response != null && response.IsSuccessStatusCode)
            {
                weatherData = JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
            }

            return weatherData;
        }
    }
}
