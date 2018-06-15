using System;

namespace Crexi.WeatherForecast.Shared.Cache
{
	public interface ICacheManager
	{
		bool IsCacheAllowed();

		TResult Get<TResult>(object key);

		TResult Get<TResult>(object[] keys);

		void Put<TData>(object key, TData data, TimeSpan? timeToLive = null);

		void Put<TData>(object[] keys, TData data, TimeSpan? timeToLive = null);

		void Remove<TData>(object key);

		void Remove<TData>(object[] keys);

		void RemoveAll();
	}
}
