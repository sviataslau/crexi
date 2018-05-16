using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Vik_WeatherService
{
    public class Program
    {
        public static void Main(string[] args) => BuildWebHost(args).Run();

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel().UseContentRoot(Directory.GetCurrentDirectory())
               .UseIISIntegration()
              .UseStartup<Startup>()
              .ConfigureAppConfiguration(configureDelegate: AppConfiguration)
                .Build();

        private static void AppConfiguration(WebHostBuilderContext context, IConfigurationBuilder configuration)
        {
            var env = context.HostingEnvironment;
            var config = configuration.SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                  .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true)
                  .AddEnvironmentVariables().Build();
        }
    }
}
