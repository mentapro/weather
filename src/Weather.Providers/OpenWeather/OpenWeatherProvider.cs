using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Extensions.Logging;
using Weather.Domain.Exceptions;
using Weather.Providers.OpenWeather.Models;

namespace Weather.Providers.OpenWeather
{
	internal class OpenWeatherProvider
	{
		private const string BaseAddress = "http://api.openweathermap.org/data/2.5";
		private readonly HttpClient _httpClient;
		private readonly ILogger<OpenWeatherProvider> _logger;

		public OpenWeatherProvider(HttpClient httpClient, ILogger<OpenWeatherProvider> logger)
		{
			_httpClient = httpClient;
			_logger = logger;
		}

		// Here are requests for OpenWeather API, building query, reading response
		internal async Task<OpenWeatherItem> GetCurrentWeatherAsync(string city, CancellationToken ct = default)
		{
			_logger.LogInformation("Called '{method}' with params (city: '{city}')", nameof(GetCurrentWeatherAsync), city);
			if (string.IsNullOrWhiteSpace(city))
				throw new WeatherValidationException("City name cannot be empty.");

			var requestUriBuilder = new UriBuilder($"{BaseAddress}/weather");
			var query = HttpUtility.ParseQueryString(requestUriBuilder.Query);

			query["q"] = city;

			requestUriBuilder.Query = query.ToString();

			var request = new HttpRequestMessage(HttpMethod.Get, requestUriBuilder.Uri);
			var response = await _httpClient.SendAsync(request, ct);

			var currentWeather = await response.Content.ReadAsAsync<OpenWeatherItem>();
			return currentWeather;
		}

		// Yes, here is some duplicating code, but it's ok because it will be easier to change some params when API will change
		// And, of course, these calls can act differently
		internal async Task<OpenWeatherForecast> GetForecastWeatherAsync(string city)
		{
			_logger.LogInformation("Called '{method}' with params (city: '{city}')", nameof(GetForecastWeatherAsync), city);
			if (string.IsNullOrWhiteSpace(city))
				throw new WeatherValidationException("City name cannot be empty.");

			var requestUriBuilder = new UriBuilder($"{BaseAddress}/forecast");
			var query = HttpUtility.ParseQueryString(requestUriBuilder.Query);

			query["q"] = city;

			requestUriBuilder.Query = query.ToString();

			var request = new HttpRequestMessage(HttpMethod.Get, requestUriBuilder.Uri);
			var response = await _httpClient.SendAsync(request);

			var forecastWeather = await response.Content.ReadAsAsync<OpenWeatherForecast>();
			return forecastWeather;
		}
	}
}