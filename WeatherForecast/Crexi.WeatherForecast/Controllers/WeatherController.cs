using System.Web.Http;
using Crexi.WeatherForecast.Models.Weather;
using Crexi.WeatherForecast.Services.Interfaces;

namespace Crexi.WeatherForecast.Controllers
{
	[RoutePrefix("weather")]
	public class WeatherController : ApiController
	{
		#region IoC

		private readonly IWeatherService _weatherService;

		public WeatherController(IWeatherService weatherService)
		{
			_weatherService = weatherService;
		}

		#endregion

		#region Today

		/// <summary>
		/// Get today weather forecast by city name
		/// </summary>
		/// <param name="city">
		/// City Name
		/// <example>london</example>
		/// </param>
		/// <returns></returns>
		[HttpGet]
		[Route("today")]
		[Route("today/{city}")]
		public WeatherForecastModel TodayByCity(string city)
		{
			return _weatherService.GetCurrentWeather(city);
		}

		/// <summary>
		/// Get today weather forecast by coordinates
		/// </summary>
		/// <param name="latitude">
		/// Latitude
		/// <example>48.8567</example>
		/// </param>
		/// <param name="longitude">
		/// Longitude
		/// <example>2.3508</example>
		/// </param>
		/// <returns></returns>
		[HttpGet]
		[Route("today/coordinates")]
		public WeatherForecastModel TodayByCoordinates(string latitude, string longitude)
		{
			return _weatherService.GetCurrentWeather(latitude, longitude);
		}

		#endregion

		#region Week

		/// <summary>
		/// Get week weather forecast by city name
		/// </summary>
		/// <param name="city">
		/// City Name
		/// <example>london</example>
		/// </param>
		/// <returns></returns>
		[HttpGet]
		[Route("week")]
		[Route("week/{city}")]
		public WeatherForecastModel WeekByCity(string city)
		{
			return _weatherService.GetForecastOnWeek(city);
		}

		/// <summary>
		/// Get week weather forecast by coordinates
		/// </summary>
		/// <param name="latitude">
		/// Latitude
		/// <example>48.8567</example>
		/// </param>
		/// <param name="longitude">
		/// Longitude
		/// <example>2.3508</example>
		/// </param>
		/// <returns></returns>
		[HttpGet]
		[Route("week/coordinates")]
		public WeatherForecastModel WeekByCoordinates(string latitude, string longitude)
		{
			return _weatherService.GetForecastOnWeek(latitude, longitude);
		}

		#endregion
	}
}
