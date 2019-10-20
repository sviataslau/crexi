using System;
using System.Collections.Generic;
using OneMoreWeatherService.SDK.Interfaces;
using OneMoreWeatherService.SDK.Models;
using Microsoft.Extensions.Logging;

namespace OneMoreWeatherService.Engines
{
    public class WeatherEngine : IWeatherEngine
    {
        private readonly IWeatherRepository _weatherRepository;
        private readonly ILogger _logger;

        public WeatherEngine(IWeatherRepository weatherRepository, ILogger logger)
        {
            //used ctor injection
            _weatherRepository = weatherRepository;
            _logger = logger;
        }
        public WeatherDTO GetTodayWeatherByCity(string City)
        {
            try
            {
                return _weatherRepository.GetTodayWeatherByCity(City);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error! Method: GetTodayWeatherByCity | {e.Message} | Stacktrace: {e.StackTrace} | InnerException: {e.InnerException} | DateTime {DateTime.Now}");
                throw;
            }
        }

        public IEnumerable<WeatherDTO> GetWeekWeatherByCity(string City)
        {
            try
            {
                return _weatherRepository.GetWeekWeatherByCity(City);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error! Method: GetWeekWeatherByCity | {e.Message} | Stacktrace: {e.StackTrace} | InnerException: {e.InnerException} | DateTime {DateTime.Now}");
                throw;
            }
        }
    }
}
