using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using ThomasKruthimplementation.Models;

namespace ThomasKruthimplementation.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class RequestFloodMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _settings;

        public RequestFloodMiddleware(RequestDelegate next,AppSettings Settings)
        {
            _next = next;
            _settings = Settings;
        }

        public Task Invoke(HttpContext httpContext)
        {
            string host = httpContext.Request.Host.Host;

            string path = httpContext.Request.Path.HasValue ?
                                      httpContext.Request.Path.Value.Split("/").LastOrDefault()?.ToLower() :
                                      null;
            CustomIPRule customRule;
            DefaultRequests defaultRequests;

            //get action and set custom rules
            switch(path){
                case "current":
                    customRule = _settings.CustomCurrentIPRules.FirstOrDefault(n => n.IP == host);
                    defaultRequests = _settings.DefaultCurrentRequests;
                    break;
                case "week":
                    customRule = _settings.CustomWeekIPRules.FirstOrDefault(n => n.IP == host);
                    defaultRequests = _settings.DefaultWeekRequests;
                    break;
                default:
                    return _next(httpContext);
            }

            //if a custom IP rule found
            if(customRule != null)
            {
                //increment for tries
                customRule.Tries++;

                //if above threshold
                if(customRule.Tries > customRule.AllowedTries)
                {
                    //has enough time elasped
                    if((DateTime.UtcNow - customRule.LastRequest).Minutes > customRule.Minutes )
                    {
                        customRule.Tries = 1;
                        customRule.LastRequest = DateTime.UtcNow;
                        return _next(httpContext);
                    }

                    //throw because not enough time has passed
                    httpContext.Response.StatusCode = 429;
                    return httpContext.Response.WriteAsync("Too many requests");
                }

                customRule.LastRequest = DateTime.UtcNow;
                //continue threshold not met
                return _next(httpContext);
                
            }

            //if not custom result to default rules
            //does ip have previous request
            if(defaultRequests.IPs.ContainsKey(host))
            {
                
                var lastRequest = defaultRequests.IPs[host];

                lastRequest.Tries++;

                //if tries  greater than default allowed
                if (lastRequest.Tries > defaultRequests.AllowedTries)
                {
                    //if time has elapased reset entry
                    if ((DateTime.UtcNow - lastRequest.LastRequest).Minutes >= defaultRequests.Minutes)
                    {
                        lastRequest.Tries = 1;
                        lastRequest.LastRequest = DateTime.UtcNow;
                        return _next(httpContext);
                    }
                    //throw because not enough time has passed
                    httpContext.Response.StatusCode = 429;
                    return httpContext.Response.WriteAsync("Too many requests");
                }
                lastRequest.LastRequest = DateTime.UtcNow;
                //continue under threshold
                return _next(httpContext);
                
            }

            //first entry for IP
            defaultRequests.IPs.Add(host, new IPRequest()
            {
                Tries = 1,
                LastRequest = DateTime.UtcNow
            });
            return _next(httpContext);

        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class RequestFloodMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestFloodMiddleware(this IApplicationBuilder builder, AppSettings Settings)
        {
            return builder.UseMiddleware<RequestFloodMiddleware>(Settings);
        }
    }
}
