using Newtonsoft.Json;

namespace Crexi.WeatherForecast.Models.Weather
{
	public class WeatherDayModel
	{
		[JsonProperty("date")]
		public string Date { get; set; }

		[JsonProperty("avg_temperature")]
		public double AvgTemperature { get; set; }

		[JsonProperty("min_temperature")]
		public double MinTemperature { get; set; }

		[JsonProperty("max_temperature")]
		public double MaxTemperature { get; set; }
	}
}