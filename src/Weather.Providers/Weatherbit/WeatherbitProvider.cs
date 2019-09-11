using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Extensions.Logging;
using Weather.Domain.Exceptions;
using Weather.Providers.Weatherbit.Models;

namespace Weather.Providers.Weatherbit
{
	internal class WeatherbitProvider
	{
		private const string BaseAddress = "http://api.weatherbit.io/v2.0";
		private readonly HttpClient _httpClient;
		private readonly ILogger<WeatherbitProvider> _logger;

		public WeatherbitProvider(HttpClient httpClient, ILogger<WeatherbitProvider> logger)
		{
			_httpClient = httpClient;
			_logger = logger;
		}

		internal async Task<WeatherbitItem> GetCurrentWeatherAsync(string city, CancellationToken ct = default)
		{
			_logger.LogInformation("Called '{method}' with params (city: '{city}')", nameof(GetCurrentWeatherAsync), city);
			if (string.IsNullOrWhiteSpace(city))
				throw new WeatherValidationException("City name cannot be empty.");

			var requestUriBuilder = new UriBuilder($"{BaseAddress}/current");
			var query = HttpUtility.ParseQueryString(requestUriBuilder.Query);

			query["city"] = city;

			requestUriBuilder.Query = query.ToString();

			var request = new HttpRequestMessage(HttpMethod.Get, requestUriBuilder.Uri);
			var response = await _httpClient.SendAsync(request, ct);

			var currentWeather = await response.Content.ReadAsAsync<WeatherbitResponse>();
			return currentWeather.Data.FirstOrDefault();
		}
	}
}