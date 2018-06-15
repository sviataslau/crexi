using System;
using Crexi.WeatherForecast.Services.Interfaces;
using Crexi.WeatherForecast.Shared.Cache;
using Crexi.WeatherForecast.Shared.Cache.Data;
using Crexi.WeatherForecast.Shared.Config;

namespace Crexi.WeatherForecast.Services
{
	public class UserIpRateLimiter : IUserIpRateLimiter
	{
		private readonly ICacheManagerFactory _cacheManagerFactory;

		private ICacheManager _cache;
		public ICacheManager Cache => _cache ?? (_cache = _cacheManagerFactory.CreateCacheManager());

		public UserIpRateLimiter(ICacheManagerFactory cacheManagerFactory)
		{
			_cacheManagerFactory = cacheManagerFactory;
		}

		public bool VerifyIpAccess(string action, string ipAddress, TimeSpan timePeriod, int maxAttemptsInPeriod)
		{
			var userAccessAttempts = GetUserAccessAttempts(action, ipAddress);
			if (userAccessAttempts.AttemptsInPeriod < maxAttemptsInPeriod)
			{
				userAccessAttempts.AttemptsInPeriod++;
				return true;
			}

			return false;
		}

		private UserAccessAttempts GetUserAccessAttempts(string action, string ipAddress)
		{
			string cacheKey = $"{action}:{ipAddress}";
			var userAccessAttempts = Cache.Get<UserAccessAttempts>(cacheKey);
			if (userAccessAttempts != null)
			{
				return userAccessAttempts;
			}

			userAccessAttempts = new UserAccessAttempts();
			Cache.Put(cacheKey, userAccessAttempts, AppConfig.AccessTimePeriod);
			return userAccessAttempts;
		}
	}
}