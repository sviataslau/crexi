using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Dependencies;
using Unity;
using Unity.Lifetime;
using WeatherService.Services;
using WebApiThrottle;

namespace WeatherService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.Formatters.JsonFormatter.SupportedMediaTypes
                .Add(new MediaTypeHeaderValue("text/html"));// Make Json the default

            // register services for DI
            var container = new UnityContainer();
            container.RegisterType<IWeatherService, OpenWeatherMapService>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //Throttling filter
            config.Filters.Add(new ThrottlingFilter()
            {
                Policy = new ThrottlePolicy(perSecond: 1, perMinute: 20,
                    perHour: 200, perDay: 2000, perWeek: 10000)
                {
                    //scope to IPs
                    IpThrottling = true,
                    IpRules = new Dictionary<string, RateLimits>
                    {
                        { "::1/10", new RateLimits { PerSecond = 2 } },
                        { "192.168.2.1", new RateLimits { PerMinute = 30, PerHour = 30*60, PerDay = 30*60*24 } }
                    },
                    //white list the "::1" IP to disable throttling on localhost
                    IpWhitelist = new List<string> { "127.0.0.1", "192.168.0.0/24" },

                    //scope to clients (if IP throttling is applied then the scope becomes a combination of IP and client key)
                    ClientThrottling = true,
                    ClientRules = new Dictionary<string, RateLimits>
                    {
                        { "api-client-key-demo", new RateLimits { PerDay = 5000 } }
                    },
                    //white list API keys that don’t require throttling
                    ClientWhitelist = new List<string> { "admin-key" },

                    //Endpoint rate limits will be loaded from EnableThrottling attribute
                    EndpointThrottling = true
                }
            });
        }
    }
}
