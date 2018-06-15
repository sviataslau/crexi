using System.Web.Http;
using System.Web.Http.Filters;
using Crexi.WeatherForecast.Common.Logger;
using Crexi.WeatherForecast.Dao.Entity;
using Crexi.WeatherForecast.Dao.Impl;
using Crexi.WeatherForecast.Dao.Interfaces;
using Crexi.WeatherForecast.Infrastructure;
using Crexi.WeatherForecast.Infrastructure.Filters;
using Crexi.WeatherForecast.Service.Impl;
using Crexi.WeatherForecast.Services;
using Crexi.WeatherForecast.Services.Interfaces;
using Crexi.WeatherForecast.Shared.Cache;
using Crexi.WeatherForecast.Shared.Interfaces;
using LightInject;
using IServiceContainer = LightInject.IServiceContainer;
using ServiceContainer = LightInject.ServiceContainer;

namespace Crexi.WeatherForecast.App_Start
{
	public class IocConfig
	{
		public const int DbCommandTimeout = 300;

		private IServiceContainer _diContainer;

		public IServiceContainer InitContainer(HttpConfiguration config)
		{
			_diContainer = new ServiceContainer();
			_diContainer.RegisterApiControllers();
			_diContainer.EnableWebApi(config);

			RegisterContainer();
			RegisterCache();
			RegisterFilters();
			RegisterLogger();
			RegisterWebServices();
			RegisterDao();
			RegisterServices();

			return _diContainer;
		}

		private void RegisterContainer()
		{
			_diContainer.RegisterInstance(typeof(IServiceFactory), _diContainer);
		}

		private void RegisterCache()
		{
			_diContainer.Register<ICacheManagerFactory, CacheManagerFactory>(new PerContainerLifetime());
		}

		private void RegisterFilters()
		{
			_diContainer.Register<IExceptionFilter, ExceptionFilter>(new PerRequestLifeTime());
			_diContainer.Register<IAuthorizationFilter, AuthorizationFilter>(new PerRequestLifeTime());
		}

		private void RegisterLogger()
		{
			_diContainer.Register<LoggerFactory>(new PerContainerLifetime());
			_diContainer.Register<ILogger>(factory => new Logger(factory.GetInstance<LoggerFactory>().CreateLogger()));
		}

		private void RegisterWebServices()
		{
			_diContainer.Register<IWeatherService, ApiXuWeather>(new PerRequestLifeTime());

			RegisterService<IUserIpRateLimiter, UserIpRateLimiter>();
			RegisterService<IIpGeolocator, IpGeolocator>();
		}

		private void RegisterServices()
		{
			_diContainer.Register<IServiceInterceptor, ServiceInterceptor>(new PerScopeLifetime());

			RegisterService<IUserService, UserService>();
		}

		private void RegisterService<TService, TImplementation>()
			where TImplementation : class, TService
			where TService : class
		{
			_diContainer.Register<TService, TImplementation>();
			_diContainer.Intercept(sr => sr.ServiceType == typeof(TService), sf => sf.GetInstance<IServiceInterceptor>());
		}

		private void RegisterDao()
		{
			_diContainer.Register(factory =>
			{
				var efContext = new WeatherForecastDb();
				efContext.Database.CommandTimeout = DbCommandTimeout;
				return efContext;
			});

			_diContainer.Register<IUserDao, UserDao>();
		}
	}
}