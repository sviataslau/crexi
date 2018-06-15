using System;
using System.Globalization;
using Crexi.WeatherForecast.Common.Extensions;
using log4net;

namespace Crexi.WeatherForecast.Common.Logger
{
	public class Logger : ILogger
	{
		private readonly ILog _logger;

		public Logger(ILog logger)
		{
			_logger = logger;
		}

		public void Error(string message, params object[] args)
		{
			_logger.ErrorFormat(CultureInfo.InvariantCulture, message, args);
		}

		public void Error(Exception exception)
		{
			Error(exception.Message, exception);
		}

		public void Error(string message, Exception exception)
		{
			if (exception != null && message.HasValue())
			{
				_logger.Error(message, exception);
			}
		}

		public void Message(string message, params object[] args)
		{
			_logger.InfoFormat(CultureInfo.InvariantCulture, message, args);
		}

		public void Warn(Exception exception)
		{
			_logger.Warn(exception);
		}

		public void Warn(string message, Exception exception)
		{
			if (exception != null && message.HasValue())
			{
				_logger.Warn(message, exception);
			}
		}

		public void Warn(string message, params object[] args)
		{
			_logger.WarnFormat(CultureInfo.InvariantCulture, message, args);
		}
	}
}
