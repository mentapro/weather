using Newtonsoft.Json;

namespace Weather.Providers.Weatherbit.Models
{
    internal class WeatherbitResponse
    {
        [JsonProperty("data")] internal WeatherbitItem[] Data { get; set; }
    }

    internal class WeatherbitItem
    {
        [JsonProperty("clouds")] public double Clouds { get; set; }

        [JsonProperty("wind_spd")] public double WindSpeed { get; set; }

        [JsonProperty("pres")] public double Pressure { get; set; }

        [JsonProperty("temp")] public double Temp { get; set; }

        [JsonProperty("rh")] public int Humidity { get; set; }
    }
}