using Crexi.WeatherForecast.Shared.Config;

namespace Crexi.WeatherForecast.Shared.Cache
{
	public class CacheManagerFactory : ICacheManagerFactory
	{
		public ICacheManager CreateCacheManager()
		{
			return new CacheManager(
				AppConfig.CacheName,
				AppConfig.CacheEnable,
				AppConfig.CacheExpirationPointInterval
			);
		}
	}
}
