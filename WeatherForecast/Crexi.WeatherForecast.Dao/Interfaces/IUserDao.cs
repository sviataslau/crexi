using System;

namespace Crexi.WeatherForecast.Dao.Interfaces
{
	public interface IUserDao
	{
		void SaveUserAccessHistory(string userIpAddress, DateTime accessDate, string userAgent);
	}
}
