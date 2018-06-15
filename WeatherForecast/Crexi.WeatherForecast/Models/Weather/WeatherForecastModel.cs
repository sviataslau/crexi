using System.Collections.Generic;
using Newtonsoft.Json;

namespace Crexi.WeatherForecast.Models.Weather
{
	public class WeatherForecastModel
	{
		[JsonProperty("city")]
		public string City { get; set; }

		[JsonProperty("current_temperature")]
		public double CurrentTemperature { get; set; }

		[JsonProperty("days")]
		public List<WeatherDayModel> Days { get; set; }

		public WeatherForecastModel()
		{
			Days = new List<WeatherDayModel>();
		}
	}
}