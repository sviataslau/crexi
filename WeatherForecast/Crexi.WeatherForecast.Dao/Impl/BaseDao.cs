using Crexi.WeatherForecast.Dao.Entity;

namespace Crexi.WeatherForecast.Dao.Impl
{
	public class BaseDao
	{
		protected WeatherForecastDb Db { get; }

		protected BaseDao(WeatherForecastDb db)
		{
			Db = db;
		}
	}
}
