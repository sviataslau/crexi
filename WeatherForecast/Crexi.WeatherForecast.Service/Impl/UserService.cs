using System;
using Crexi.WeatherForecast.Dao.Interfaces;
using Crexi.WeatherForecast.Shared.Cache;
using Crexi.WeatherForecast.Shared.Interfaces;

namespace Crexi.WeatherForecast.Service.Impl
{
	public class UserService : BaseService, IUserService
	{
		#region IoC

		private readonly IUserDao _userDao;

		public UserService(
			ICacheManagerFactory cacheManagerFactory,
			IUserDao userDao
		) : base(cacheManagerFactory)
		{
			_userDao = userDao;
		}

		#endregion

		#region IAccountService Implementation

		void IUserService.SaveUserAccessHistory(string userIpAddress, DateTime accessDate, string userAgent)
		{
			_userDao.SaveUserAccessHistory(userIpAddress, accessDate, userAgent);
		}

		#endregion
	}
}
