using System.Threading.Tasks;

namespace WeatherService.Services
{
    public interface IWeatherService
    {
        Task<T> FetchWeather<T>(string city, string state, string country, string mode);
    }
}
