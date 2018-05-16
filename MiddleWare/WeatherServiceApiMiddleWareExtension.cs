using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.HttpOverrides;
using System.Linq;
using System.Collections.Generic;
using Vik_WeatherService.Models;

namespace Vik_WeatherService.MiddleWare
{
    public static class WeatherServiceApiMiddleWareExtension
    {
        public static IApplicationBuilder UseWeatherServiceMiddleWare(this IApplicationBuilder builder) => builder.UseMiddleware<WeatherServiceMiddleWare>();
    }
    public class WeatherServiceMiddleWare
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly ILogger<WeatherServiceMiddleWare> _logger;
        private readonly ObjectResultExecutor _executor;
        private static int retries;
        private static readonly Dictionary<string, Dictionary<int, DateTime>> RouteLimit = new Dictionary<string, Dictionary<int, DateTime>>();
        private enum ErrorCodes { InvalidInput = 400, InternalError = 500 };
        public WeatherServiceMiddleWare(RequestDelegate requestDelegate, ILogger<WeatherServiceMiddleWare> logger, ObjectResultExecutor executor)
        {
            _requestDelegate = requestDelegate ?? throw new ArgumentNullException(nameof(requestDelegate));
            _executor = executor ?? throw new ArgumentNullException(nameof(executor));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }



        public async Task Invoke(HttpContext context)
        {
            try
            {

                var ipAddress = context.Connection.RemoteIpAddress?.MapToIPv4()?.ToString();

                var RouteName = context.Request.Path.Value?.Split('/').Last();
                if (!string.IsNullOrEmpty(RouteName))
                {
                    /*Create a key value pair based on user's route and IP address to determine the number of attempts and datetime*/

                    var key = $"{RouteName}-{ipAddress}";

                    if (!RouteLimit.ContainsKey(key))
                    {
                        var value = new Dictionary<int, DateTime>() { { retries++, DateTime.UtcNow } };
                        RouteLimit.Add(key, value);
                    }
                    else
                    {
                        RouteLimit.TryGetValue(key, out Dictionary<int, DateTime> elapsedValue);
                        DateTime dateTime = Convert.ToDateTime(elapsedValue.Values.First());
                        var retryAttempt = elapsedValue.Keys.First();

                        var diff = DateTime.Now - dateTime;
                        if (diff.TotalMinutes > RateLimitSettings.interval)
                        {

                            var newValue = new Dictionary<int, DateTime>() { { --retryAttempt, DateTime.UtcNow } };
                            RouteLimit[key] = newValue;
                        }
                        else
                        {
                            var newValue = new Dictionary<int, DateTime>() { { ++retries, DateTime.UtcNow } };
                            RouteLimit[key] = newValue;
                        }
                    }
                    var keyValues = RouteLimit[key];
                    var attemptNumber = keyValues.Keys.First();
                    var allowedTriesforRoute = RateLimitSettings.numberOfTriesFor30min[RouteName];
                    if (attemptNumber >= allowedTriesforRoute)
                        throw new Exception($"user {ipAddress} has exceeded the allowed attempts");

                }

                await _requestDelegate(context);
            }
            catch (ArgumentNullException aex)
            {
                _logger.LogError(aex, aex.Message);
                const string invalidInputMsg = "Please check your request body and try again.";
                await ReturnErrorResponse(context, ErrorCodes.InvalidInput, invalidInputMsg);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                const string internalErrorMsg = "Some Internal Server Error Occured";
                await ReturnErrorResponse(context, ErrorCodes.InternalError, ex?.Message ?? internalErrorMsg);
            }
        }

        private async Task ReturnErrorResponse(HttpContext context, object invalidInput, string invalidInputMsg)
        {
            var response = new { invalidInput, invalidInputMsg };
            await _executor.ExecuteAsync(new ActionContext { HttpContext = context }, new ObjectResult(response));
        }
    }
}