using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Weather.Providers.OpenWeather;

namespace Weather.Providers
{
	internal class HttpClientErrorHandler : DelegatingHandler
	{
		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			HttpResponseMessage response = null;
			try
			{
				response = await base.SendAsync(request, cancellationToken);
				response.EnsureSuccessStatusCode();
			}
			catch (HttpRequestException)
			{
				var serverError = new[] {HttpStatusCode.InternalServerError, HttpStatusCode.BadGateway, HttpStatusCode.ServiceUnavailable};
				if (response == null || serverError.Contains(response.StatusCode))
					throw new WeatherProviderHttpException("Server temporarily unavailable.", 500);

				if (response.StatusCode == HttpStatusCode.NotFound)
					throw new WeatherProviderHttpException("City was not found!", 404);

				throw;
			}

			return response;
		}
	}
}