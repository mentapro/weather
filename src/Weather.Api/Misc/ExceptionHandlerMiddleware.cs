using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Weather.Common;

namespace Weather.Api.Misc
{
	public class ExceptionHandlerMiddleware
	{
		private readonly RequestDelegate _next;

		public ExceptionHandlerMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (WeatherValidationException ex)
			{
				var result = new ErrorResult {Message = ex.Message};
				context.Response.StatusCode = 400;
				await context.Response.WriteAsync(JsonConvert.SerializeObject(result));
			}
			catch (WeatherHttpException ex)
			{
				var result = new ErrorResult {Message = ex.Message};
				context.Response.StatusCode = ex.StatusCode;
				await context.Response.WriteAsync(JsonConvert.SerializeObject(result));
			}
			catch (HttpRequestException ex)
			{
				var result = new ErrorResult {Message = "Some unhandled http error."};
				context.Response.StatusCode = 500;
				await context.Response.WriteAsync(JsonConvert.SerializeObject(result));
			}
		}
	}

	public class ErrorResult
	{
		public string Message { get; set; }
	}
}