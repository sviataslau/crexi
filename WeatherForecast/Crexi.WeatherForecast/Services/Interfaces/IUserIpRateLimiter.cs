using System;

namespace Crexi.WeatherForecast.Services.Interfaces
{
	public interface IUserIpRateLimiter
	{
		bool VerifyIpAccess(string action, string ipAddress, TimeSpan timePeriod, int maxAttemptsInPeriod);
	}
}
