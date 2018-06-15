using System.IO;
using System.Net;
using Crexi.WeatherForecast.Common.Serialization;
using Crexi.WeatherForecast.Models.IpGeolocator;
using Crexi.WeatherForecast.Services.Interfaces;
using Crexi.WeatherForecast.Shared.Config;

namespace Crexi.WeatherForecast.Services
{
	public class IpGeolocator: IIpGeolocator
	{
		string IIpGeolocator.GetIpCountryCode(string ip)
		{
			string uri = $"{AppConfig.IpGeolocatorHost}/{ip}";
			var request = WebRequest.Create(uri);
			request.ContentType = "application/json; charset=utf-8";

			using (var response = (HttpWebResponse)request.GetResponse())
			{
				if (response.StatusCode != HttpStatusCode.OK)
				{
					return null;
				}

				var stream = response.GetResponseStream();
				if (stream == null)
				{
					return null;
				}

				using (var reader = new StreamReader(stream))
				{
					string json = reader.ReadToEnd();

					var ipGeoDetails = ObjectSerializer.ToObject<IpGeoDetails>(json);
					return ipGeoDetails?.CountryCode;
				}
			}
		}
	}
}