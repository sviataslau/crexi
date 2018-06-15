using System;
using System.Configuration;

namespace Crexi.WeatherForecast.Shared.Config
{
	public class AppConfig
	{
		#region Weather Api

		public static string ApiXuKey => ConfigurationManager.AppSettings["ApiXu.Key"];

		#endregion

		#region Access

		public static bool CheckIpCountry => bool.Parse(ConfigurationManager.AppSettings["Access.CheckIpCountry"]);

		public static TimeSpan AccessTimePeriod => TimeSpan.Parse(ConfigurationManager.AppSettings["Access.TimePeriod"]);

		public static int AccessAttemptCount => int.Parse(ConfigurationManager.AppSettings["Access.AttemptCount"]);

		public static string[] AccessBlockedCountryCodes
		{
			get
			{
				string countryCodes = ConfigurationManager.AppSettings["Access.BlockedCountryCodes"];
				return countryCodes.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
			}
		}

		#endregion

		#region Ip Geolocator

		public static string IpGeolocatorHost => ConfigurationManager.AppSettings["IpGeolocator.Host"];

		#endregion

		#region Cache

		public static bool CacheEnable => bool.Parse(ConfigurationManager.AppSettings["Cache.Enable"]);

		public static string CacheName => ConfigurationManager.AppSettings["Cache.Name"];

		public static TimeSpan CacheExpirationPointInterval => TimeSpan.Parse(ConfigurationManager.AppSettings["Cache.ExpirationPointInterval"]);

		#endregion
	}
}