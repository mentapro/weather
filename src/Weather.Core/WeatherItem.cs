using System;

namespace Weather.Core
{
	public class WeatherItem
	{
		public DateTime Date { get; set; }

		public double Temperature { get; set; }

		public double? MinimumTemperature { get; set; }

		public double? MaximumTemperature { get; set; }

		public double Pressure { get; set; }

		public int Humidity { get; set; }

		public double WindSpeed { get; set; }

		public double WindDirectionDegrees { get; set; }

		public int Cloudiness { get; set; }
	}
}