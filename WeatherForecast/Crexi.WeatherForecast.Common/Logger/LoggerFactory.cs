using System.Diagnostics;
using log4net;
using log4net.Config;

namespace Crexi.WeatherForecast.Common.Logger
{
	public class LoggerFactory
	{
		private readonly ILog _logger = LogManager.GetLogger("CrexiWeather");

		public LoggerFactory()
		{
			XmlConfigurator.Configure();
			GlobalContext.Properties["ProcessId"] = Process.GetCurrentProcess().Id;
		}

		public virtual ILog CreateLogger()
		{
			return _logger;
		}
	}
}
