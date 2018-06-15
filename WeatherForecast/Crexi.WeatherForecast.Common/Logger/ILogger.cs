using System;

namespace Crexi.WeatherForecast.Common.Logger
{
	public interface ILogger
	{
		void Error(string message, params object[] args);

		void Error(Exception exception);

		void Error(string message, Exception exception);

		void Message(string message, params object[] args);

		void Warn(Exception exception);

		void Warn(string message, Exception exception);

		void Warn(string message, params object[] args);
	}
}
