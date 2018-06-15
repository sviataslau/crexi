using System;
using Crexi.WeatherForecast.Dao.Entity;
using Crexi.WeatherForecast.Dao.Interfaces;

namespace Crexi.WeatherForecast.Dao.Impl
{
	public class UserDao : BaseDao, IUserDao
	{
		#region IoC

		public UserDao(WeatherForecastDb db)
			: base(db)
		{
		}

		#endregion

		#region IUserDao Implementations

		void IUserDao.SaveUserAccessHistory(string userIpAddress, DateTime accessDate, string userAgent)
		{
			Db.SaveUserAccessHistory(userIpAddress, accessDate, userAgent);
		}

		#endregion
	}
}
