using Crexi.WeatherForecast.Models.Weather;

namespace Crexi.WeatherForecast.Services.Interfaces
{
	public interface IWeatherService
	{
		WeatherForecastModel GetCurrentWeather(string city);

		WeatherForecastModel GetCurrentWeather(string latitude, string longitude);

		WeatherForecastModel GetForecastOnWeek(string city);

		WeatherForecastModel GetForecastOnWeek(string latitude, string longitude);
	}
}
