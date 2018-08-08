using System;
using System.Runtime.Caching;
using System.Text;

namespace Crexi.WeatherForecast.Shared.Cache
{
	public class CacheManager : ICacheManager
	{
		private const string Delimiter = ":";

		private readonly bool _enabled;
		private readonly TimeSpan _defaultCacheExpiration;
		private readonly string _cacheName;

		private static readonly object CacheClientLocker = new object();

		private static MemoryCache _cacheClient;

		public CacheManager(
			string cacheName,
			bool enabled,
			TimeSpan defaultCacheExpiration)
		{
			_enabled = enabled;
			_cacheName = cacheName;
			_defaultCacheExpiration = defaultCacheExpiration;
		}

		protected MemoryCache CacheClient => EnsureCacheClientCreated();

		public bool IsCacheAllowed()
		{
			return _enabled;
		}

		private MemoryCache EnsureCacheClientCreated()
		{
			if (_cacheClient == null && IsCacheAllowed())
			{
				lock (CacheClientLocker)
				{
					if (_cacheClient == null && IsCacheAllowed())
					{
						_cacheClient = new MemoryCache(_cacheName);
					}
				}
			}

			return _cacheClient;
		}

		public TResult Get<TResult>(object key)
		{
			return Get<TResult>(new[] { key });
		}

		public TResult Get<TResult>(object[] keys)
		{
			TResult result = default(TResult);

			if (IsCacheAllowed())
			{
				string cacheKey = BuildCacheKey(typeof(TResult).ToString(), keys);
				object cacheItem = CacheClient.Get(cacheKey);

				if (cacheItem is TResult)
				{
					result = (TResult)cacheItem;
				}
			}

			return result;
		}

		public void Put<TData>(object key, TData data, TimeSpan? timeToLive = null)
		{
			Put(new[] { key }, data, timeToLive);
		}

		public void Put<TData>(object[] keys, TData data, TimeSpan? timeToLive = null)
		{
			if (!IsCacheAllowed() || Equals(data, null))
			{
				return;
			}

			string cacheKey = BuildCacheKey(typeof(TData).ToString(), keys);
			DateTime expiration = GetExpirationPoint(timeToLive);

			CacheClient.Set(cacheKey, data, expiration);
		}

		private DateTime GetExpirationPoint(TimeSpan? customTimeToLiveInterval)
		{
			DateTime expiration = customTimeToLiveInterval != null
				? DateTime.UtcNow + customTimeToLiveInterval.GetValueOrDefault()
				: DateTime.UtcNow + _defaultCacheExpiration;

			return expiration;
		}

		public void Remove<TData>(object key)
		{
			Remove<TData>(new[] { key });
		}

		public void Remove<TData>(object[] keys)
		{
			if (!IsCacheAllowed())
			{
				return;
			}

			string cacheKey = BuildCacheKey(typeof(TData).ToString(), keys);
			CacheClient.Remove(cacheKey);
		}

		public void RemoveAll()
		{
			if (!IsCacheAllowed())
			{
				return;
			}

			_cacheClient = new MemoryCache(_cacheName);
		}

		private static string BuildCacheKey(string masterKey, object[] keys)
		{
			string combinedKeys = string.Join(Delimiter, keys);

			var builder = new StringBuilder(masterKey);

			builder.Append(Delimiter);
			builder.Append(combinedKeys);

			return builder.ToString();
		}
	}
}
