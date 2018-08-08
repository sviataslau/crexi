using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Dependencies;
using System.Web.Http.Filters;

namespace Crexi.WeatherForecast.Infrastructure.Filters
{
	public class FilterFactory: IAuthorizationFilter, IExceptionFilter
	{
		public bool AllowMultiple { get; }

		public FilterFactory()
		{
			AllowMultiple = false;
		}

		public async Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(HttpActionContext actionContext,CancellationToken cancellationToken,Func<Task<HttpResponseMessage>> continuation)
		{
			IDependencyScope dependencyScope = actionContext.Request.GetDependencyScope();
			var filters = (IEnumerable<IAuthorizationFilter>)dependencyScope.GetServices(typeof(IAuthorizationFilter));
			filters = filters.Reverse();

			Func<Task<HttpResponseMessage>> result = filters.Aggregate(continuation, (currentContinuation, filter) =>
			{
				return () => filter.ExecuteAuthorizationFilterAsync(actionContext, cancellationToken, currentContinuation);
			});

			return await result();
		}

		public async Task ExecuteExceptionFilterAsync(HttpActionExecutedContext actionExecutedContext,CancellationToken cancellationToken)
		{
			IDependencyScope dependencyScope = actionExecutedContext.Request.GetDependencyScope();
			var filters = (IEnumerable<IExceptionFilter>)dependencyScope.GetServices(typeof(IExceptionFilter));

			foreach (var filter in filters)
			{
				await filter.ExecuteExceptionFilterAsync(actionExecutedContext, cancellationToken);
			}
		}
	}
}