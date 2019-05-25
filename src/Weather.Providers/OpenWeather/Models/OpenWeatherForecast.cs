using System.Collections.Generic;
using Newtonsoft.Json;

namespace Weather.Providers.OpenWeather.Models
{
	internal class OpenWeatherForecast
	{
		[JsonProperty("list")] internal List<OpenWeatherItem> Items { get; set; }
	}
}