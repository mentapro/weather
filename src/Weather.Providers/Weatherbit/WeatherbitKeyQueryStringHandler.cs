using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;

namespace Weather.Providers.Weatherbit
{
	internal class WeatherbitKeyQueryStringHandler : DelegatingHandler
	{
		private readonly WeatherbitOptions _options;

		public WeatherbitKeyQueryStringHandler(IOptions<WeatherbitOptions> options)
		{
			_options = options.Value;
		}

		// Add OpenWeather 'appid' to every request for OpenWeatherProvider
		protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			var uri = QueryHelpers.AddQueryString(request.RequestUri.OriginalString, "key", _options.Key);
			request.RequestUri = new Uri(uri);
			return base.SendAsync(request, cancellationToken);
		}
	}
}