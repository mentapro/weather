using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weather.Core.Services.Impl
{
	public class WeatherService : IWeatherService
	{
		private readonly IWeatherProvider _weatherProvider;

		public WeatherService(IWeatherProvider weatherProvider)
		{
			_weatherProvider = weatherProvider;
		}

		public Task<WeatherItem> GetCurrentWeatherAsync(string cityName, string units)
		{
			return _weatherProvider.GetCurrentWeatherAsync(cityName, units);
		}

		public async Task<IEnumerable<WeatherItem>> GetDetailedWeatherForecastAsync(string cityName, string units)
		{
			return (await _weatherProvider.GetForecastWeatherAsync(cityName, units)).OrderBy(x => x.Date);
		}

		public async Task<IEnumerable<WeatherItem>> GetDayAverageWeatherForecastAsync(string cityName, string units)
		{
			var detailedForecast = await _weatherProvider.GetForecastWeatherAsync(cityName, units);
			var groupedByDays = (
				from weatherItem in detailedForecast
				group weatherItem by weatherItem.Date.Date
				into gr
				select new WeatherItem
				{
					Date = gr.Key,
					Temperature = gr.Average(x => x.Temperature),
					MinimumTemperature = gr.Average(x => x.MinimumTemperature),
					MaximumTemperature = gr.Average(x => x.MaximumTemperature),
					Humidity = (int) Math.Round(gr.Average(x => x.Humidity)),
					Cloudiness = (int) Math.Round(gr.Average(x => x.Cloudiness)),
					Pressure = gr.Average(x => x.Pressure),
					WindSpeed = gr.Average(x => x.WindSpeed),
					WindDirectionDegrees = gr.Average(x => x.WindDirectionDegrees)
				}).OrderBy(x => x.Date);

			return groupedByDays;
		}
	}
}