using System;

namespace Crexi.WeatherForecast.Infrastructure
{
	public class ServiceErrorEventArgs : EventArgs
	{
		public Exception Error { get; set; }

		public string ErrorMessage { get; set; }

		public bool Handled { get; set; }
	}
}