using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Weather.Providers.OpenWeather.Models;

namespace Weather.Providers.OpenWeather
{
	public class OpenWeatherProvider
	{
		private const string Host = "http://api.openweathermap.org";
		private readonly HttpClient _httpClient;

		public OpenWeatherProvider(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		internal async Task<OpenWeatherItem> GetCurrentWeatherAsync()
		{
			var requestUriBuilder = new UriBuilder($"{Host}/data/2.5/weather");
			var query = HttpUtility.ParseQueryString(requestUriBuilder.Query);

			query["q"] = "London,gb";
			requestUriBuilder.Query = query.ToString();

			var request = new HttpRequestMessage(HttpMethod.Get, requestUriBuilder.Uri);
			var response = await _httpClient.SendAsync(request);
			response.EnsureSuccessStatusCode();

			var currentWeather = await response.Content.ReadAsAsync<OpenWeatherItem>();
			return currentWeather;
		}
	}
}