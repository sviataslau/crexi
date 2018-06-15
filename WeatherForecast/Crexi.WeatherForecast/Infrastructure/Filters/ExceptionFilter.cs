using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using Crexi.WeatherForecast.Common.Logger;
using Crexi.WeatherForecast.Models;
using Crexi.WeatherForecast.Shared.Enums;

namespace Crexi.WeatherForecast.Infrastructure.Filters
{
	public class ExceptionFilter : IExceptionFilter
	{
		private readonly ILogger _logger;

		public bool AllowMultiple { get; }

		public ExceptionFilter(ILogger logger)
		{
			_logger = logger;
			AllowMultiple = false;
		}

		public Task ExecuteExceptionFilterAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
		{
			_logger.Error(actionExecutedContext.Exception);

			actionExecutedContext.Response = actionExecutedContext
				.Request
				.CreateResponse(
					HttpStatusCode.InternalServerError,
					new ErrorResponse(ResponseStatus.Error, "An error has occurred")
				);

			return Task.FromResult<object>(null);
		}
	}
}