using System;

namespace Crexi.WeatherForecast.Shared.Interfaces
{
	public interface IUserService
	{
		void SaveUserAccessHistory(string userIpAddress, DateTime accessDate, string userAgent);
	}
}
