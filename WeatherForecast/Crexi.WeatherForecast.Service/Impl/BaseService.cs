using Crexi.WeatherForecast.Shared.Cache;

namespace Crexi.WeatherForecast.Service.Impl
{
	public abstract class BaseService
	{
		private readonly ICacheManagerFactory _cacheManagerFactory;

		private ICacheManager _cache;
		public ICacheManager Cache => _cache ?? (_cache = _cacheManagerFactory.CreateCacheManager());

		protected BaseService(ICacheManagerFactory cacheManagerFactory)
		{
			_cacheManagerFactory = cacheManagerFactory;
		}
	}
}
