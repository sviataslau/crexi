using System;
using LightInject.Interception;

namespace Crexi.WeatherForecast.Infrastructure
{
	public interface IServiceInterceptor : IInterceptor
	{
		event EventHandler<ServiceErrorEventArgs> ErrorOccured;
	}
}
