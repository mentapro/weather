using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Weather.Providers.OpenWeather
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
				if (response == null || response.StatusCode == HttpStatusCode.InternalServerError)
					throw new OpenWeatherHttpException("Server temporarily unavailable.", 500);

				if (response.StatusCode == HttpStatusCode.NotFound)
					throw new OpenWeatherHttpException("City was not found!", 404);

				throw;
			}

			return response;
		}
	}
}