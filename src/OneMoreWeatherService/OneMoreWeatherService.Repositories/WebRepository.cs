using System.IO;
using System.Net;
using System.Collections.Generic;
using OneMoreWeatherService.SDK.Interfaces;
using OneMoreWeatherService.SDK.Models;
using Newtonsoft.Json;
using System.Linq;
using Microsoft.Extensions.Logging;
using System;

namespace OneMoreWeatherService.Repositories
{
    public class WebRepository : IWeatherRepository
    {
        private readonly ILogger _logger;

        string url = "http://api.openweathermap.org/data/2.5/weather?q={0}&APPID={1}&units=metric";
        string weatherApiKey = "6baac624e080c538b262b8d22b012018";

        public WebRepository(ILogger logger)
        {
            _logger = logger;
        }

        public WeatherDTO GetTodayWeatherByCity(string City)
        {
            try
            {
                WebUtility.UrlEncode(City);

                WebRequest request = WebRequest.Create(string.Format(url, City, weatherApiKey));
                WebResponse response = request.GetResponse();
                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    string responseString = streamReader.ReadToEnd();

                    var weatherResponce = JsonConvert.DeserializeObject<WeatherResponce>(responseString);

                    return weatherResponce.Weather.First();
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error! Method: GetTodayWeatherByCity | {e.Message} | Stacktrace: {e.StackTrace} | InnerException: {e.InnerException} | DateTime {DateTime.Now}");
                throw;
            }
        }

        public IEnumerable<WeatherDTO> GetWeekWeatherByCity(string City)
        {
            throw new System.NotImplementedException();
        }
    }
}
