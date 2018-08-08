using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Crexi.WeatherForecast.Common.Extensions;
using Crexi.WeatherForecast.Models;
using Crexi.WeatherForecast.Services.Interfaces;
using Crexi.WeatherForecast.Shared.Config;
using Crexi.WeatherForecast.Shared.Enums;
using Crexi.WeatherForecast.Shared.Interfaces;
using Microsoft.Owin;

namespace Crexi.WeatherForecast.Infrastructure.Filters
{
	public class AuthorizationFilter : IAuthorizationFilter
	{
		private readonly IIpGeolocator _ipGeolocator;
		private readonly IUserIpRateLimiter _userIpRateLimiter;
		private readonly IUserService _userService;

		public AuthorizationFilter(IIpGeolocator ipGeolocator, IUserIpRateLimiter userIpRateLimiter, IUserService userService)
		{
			_ipGeolocator = ipGeolocator;
			_userIpRateLimiter = userIpRateLimiter;
			_userService = userService;

			AllowMultiple = false;
		}

		public bool AllowMultiple { get; }

		public Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(
			HttpActionContext actionContext,
			CancellationToken cancellationToken,
			Func<Task<HttpResponseMessage>> continuation)
		{
			if (IsAuthorizated(actionContext))
			{
				return continuation();
			}

			return Task.FromResult(actionContext.Response);
		}

		private bool IsAuthorizated(HttpActionContext actionContext)
		{
			string userIp;
			if (!TryGetUserIpAddress(actionContext, out userIp))
			{
				CreateErrorResponse(actionContext, "Invalid IP address");
				return false;
			}

			_userService.SaveUserAccessHistory(userIp, DateTime.Now, actionContext.Request.Headers.UserAgent.ToString());

			if (AppConfig.CheckIpCountry)
			{
				string ipCountryCode = _ipGeolocator.GetIpCountryCode(userIp);
				if (AppConfig.AccessBlockedCountryCodes.Contains(ipCountryCode))
				{
					CreateErrorResponse(actionContext, "Service is not available in your country");
					return false;
				}
			}

			if (!_userIpRateLimiter.VerifyIpAccess(actionContext.ActionDescriptor.ActionName, userIp, AppConfig.AccessTimePeriod, AppConfig.AccessAttemptCount))
			{
				CreateErrorResponse(actionContext, "User rate limit exceeded");
				return false;
			}

			return true;
		}

		private void CreateErrorResponse(HttpActionContext actionContext, string message, HttpStatusCode httpStatusCode = HttpStatusCode.Forbidden)
		{
			actionContext.Response = actionContext.Request.CreateResponse(
				httpStatusCode,
				new ErrorResponse(ResponseStatus.Error, message)
			);
		}

		private bool TryGetUserIpAddress(HttpActionContext actionContext, out string userIp)
		{
			string ip = null;
			HttpRequestMessage request = actionContext.Request;

			const string owinContextKey = "MS_OwinContext";
			if (request.Properties.ContainsKey(owinContextKey))
			{
				var context = request.Properties[owinContextKey] as OwinContext;
				if (context != null)
				{
					ip = context.Request.RemoteIpAddress;
				}
			}

			IPAddress ipAddress;
			if (ip.HasValue() && IPAddress.TryParse(ip, out ipAddress))
			{
				userIp = ipAddress.ToString();
				return true;
			}

			userIp = null;
			return false;
		}
	}
}