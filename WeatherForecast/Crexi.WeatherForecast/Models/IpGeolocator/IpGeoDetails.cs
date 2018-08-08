using Newtonsoft.Json;

namespace Crexi.WeatherForecast.Models.IpGeolocator
{
	public class IpGeoDetails
	{
		[JsonProperty(PropertyName = "query")]
		public string IpAddress { get; set; }

		[JsonProperty(PropertyName = "country")]
		public string Country { get; set; }

		[JsonProperty(PropertyName = "countryCode")]
		public string CountryCode { get; set; }

		[JsonProperty(PropertyName = "timezone")]
		public string Timezone { get; set; }
	}
}