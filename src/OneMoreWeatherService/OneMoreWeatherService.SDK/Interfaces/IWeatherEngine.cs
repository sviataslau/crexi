using System.Collections.Generic;
using OneMoreWeatherService.SDK.Models;

namespace OneMoreWeatherService.SDK.Interfaces
{
    public interface IWeatherEngine
    {
        IEnumerable<WeatherDTO> GetWeekWeatherByCity(string City);
        WeatherDTO GetTodayWeatherByCity(string City);
    }
}
