using System.Linq;
using APIXULib;
using Crexi.WeatherForecast.Models.Weather;
using Crexi.WeatherForecast.Services.Interfaces;
using Crexi.WeatherForecast.Shared.Config;

namespace Crexi.WeatherForecast.Services
{
	public class ApiXuWeather : IWeatherService
	{
		private readonly APIXUWeatherRepository _client;

		public ApiXuWeather()
		{
			_client = new APIXUWeatherRepository();
		}

		WeatherForecastModel IWeatherService.GetCurrentWeather(string city)
		{
			var weather = GetForecastByCityName(city, Days.One);
			return MapWeatherModel(weather);
		}

		WeatherForecastModel IWeatherService.GetCurrentWeather(string latitude, string longitude)
		{
			var weather = GetForecastByCoordinates(latitude, longitude, Days.One);
			return MapWeatherModel(weather);
		}

		WeatherForecastModel IWeatherService.GetForecastOnWeek(string city)
		{
			var weather = GetForecastByCityName(city, Days.Seven);
			return MapWeatherModel(weather);
		}

		WeatherForecastModel IWeatherService.GetForecastOnWeek(string latitude, string longitude)
		{
			var weather = GetForecastByCoordinates(latitude, longitude, Days.Seven);
			return MapWeatherModel(weather);
		}

		private WeatherForecastModel MapWeatherModel(WeatherModel weather)
		{
			return new WeatherForecastModel
			{
				City = weather.location.name,
				CurrentTemperature = weather.current.temp_c,
				Days = weather.forecast.forecastday.Select(o => new WeatherDayModel
				{
					Date = o.date,
					AvgTemperature = o.day.avgtemp_c,
					MinTemperature = o.day.mintemp_c,
					MaxTemperature = o.day.maxtemp_c
				}).ToList()
			};
		}

		private WeatherModel GetForecastByCityName(string city, Days days)
		{
			return _client.GetWeatherData(AppConfig.ApiXuKey, GetBy.CityName, city, days);
		}

		private WeatherModel GetForecastByCoordinates(string latitude, string longitude, Days days)
		{
			return _client.GetWeatherDataByLatLong(AppConfig.ApiXuKey, latitude, longitude, days);
		}
	}
}