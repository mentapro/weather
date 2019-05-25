using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Weather.Common;
using Weather.Providers.OpenWeather;

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
			catch (OpenWeatherHttpException ex)
			{
				var result = new ErrorResult {Message = ex.Message};
				context.Response.StatusCode = ex.StatusCode;
				await context.Response.WriteAsync(JsonConvert.SerializeObject(result));
			}
		}
	}

	public class ErrorResult
	{
		public string Message { get; set; }
	}
}