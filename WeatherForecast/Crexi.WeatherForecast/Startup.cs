using System.Web.Http;
using Crexi.WeatherForecast.App_Start;
using Crexi.WeatherForecast.Infrastructure.Filters;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Crexi.WeatherForecast.Startup))]
namespace Crexi.WeatherForecast
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			var config = new HttpConfiguration();

			new IocConfig().InitContainer(config);

			WebApiConfig.Register(config);
			config.Filters.Add(new FilterFactory());

			app.UseWebApi(config);
		}
	}
}