using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;

namespace Weather.Providers.OpenWeather
{
	internal class AppIdQueryStringHandler : DelegatingHandler
	{
		private readonly OpenWeatherOptions _options;

		public AppIdQueryStringHandler(IOptions<OpenWeatherOptions> options)
		{
			_options = options.Value;
		}

		// Add OpenWeather 'appid' to every request for OpenWeatherProvider
		protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			var uri = QueryHelpers.AddQueryString(request.RequestUri.OriginalString, "appid", _options.AppId);
			request.RequestUri = new Uri(uri);
			return base.SendAsync(request, cancellationToken);
		}
	}
}