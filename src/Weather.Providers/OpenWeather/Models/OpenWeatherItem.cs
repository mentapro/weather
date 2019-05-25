using Newtonsoft.Json;

namespace Weather.Providers.OpenWeather.Models
{
	internal class OpenWeatherItem
	{
		[JsonProperty("main")] internal MainPart Main { get; set; }
		[JsonProperty("dt")] internal long UnixTimestamp { get; set; }

		[JsonProperty("wind")] internal WindPart Wind { get; set; }

		[JsonProperty("clouds")] internal CloudsPart Clouds { get; set; }

		internal class MainPart
		{
			[JsonProperty("temp")] public double Temp { get; set; }

			[JsonProperty("pressure")] public double Pressure { get; set; }

			[JsonProperty("humidity")] public int Humidity { get; set; }

			[JsonProperty("temp_min")] public double? TempMin { get; set; }

			[JsonProperty("temp_max")] public double? TempMax { get; set; }
		}

		internal class WindPart
		{
			[JsonProperty("speed")] public double Speed { get; set; }

			[JsonProperty("deg")] public double DirectionDegrees { get; set; }
		}

		internal class CloudsPart
		{
			[JsonProperty("all")] public double Cloudiness { get; set; }
		}
	}
}