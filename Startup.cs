using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Vik_WeatherService.MiddleWare;
using Vik_WeatherService.Models;
using Vik_WeatherService.Redis;
using Vik_WeatherService.Requests;

namespace Vik_WeatherService
{
    public class Startup
    {
        private readonly ILogger<Startup> _logger;
        public Startup(IConfiguration configuration, ILogger<Startup> logger)
        {
            Configuration = configuration;
            _logger = logger;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddSingleton<IRedisConnectionProvider, RedisConnectionProvider>();
            services.AddSingleton<IHttpClient, WeatherServicettpClient>();//singleton helps with socket exception
            services.AddScoped<IRequest, RequestImp>();
            services.AddOptions();
            services.AddSession();
            services.Configure<WeatherApiConfig>(Configuration.GetSection("WeatherApiConfiguration"));//Configures neccessary api properties
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor |
           ForwardedHeaders.XForwardedProto
            });
            app.UseWeatherServiceMiddleWare();
            app.UseMvc();
        }
    }
}
