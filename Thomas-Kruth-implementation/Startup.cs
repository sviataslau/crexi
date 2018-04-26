using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ThomasKruthimplementation.Middleware;
using ThomasKruthimplementation.Models;

namespace Thomas_Kruth_implementation
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddMvc();

            services.AddCors();

            services.AddScoped(o => new ThomasKruthimplementation.Services.WeatherService());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(opt => 
                            opt.AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowAnyOrigin()
                       );


            var appConfig = new AppSettings();
            Configuration.Bind(appConfig);

            app.UseRequestFloodMiddleware(appConfig);

            app.UseMvc();
        }
    }
}
