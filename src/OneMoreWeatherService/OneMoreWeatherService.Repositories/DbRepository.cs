using OneMoreWeatherService.SDK.Interfaces;
using OneMoreWeatherService.SDK.Models;

namespace OneMoreWeatherService.Repositories
{
    public class DbRepository : IWeatherRepository
    {
        //weather could be read from Db, for example
        public WeatherDTO GetTodayWeatherByCity(string City)
        {
            throw new System.NotImplementedException();
        }

        public System.Collections.Generic.IEnumerable<WeatherDTO> GetWeekWeatherByCity(string City)
        {
            throw new System.NotImplementedException();
        }
    }
}
