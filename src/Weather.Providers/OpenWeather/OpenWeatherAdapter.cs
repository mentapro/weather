using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Weather.Core;
using Weather.Providers.OpenWeather.Models;

namespace Weather.Providers.OpenWeather
{
	internal class OpenWeatherAdapter : IWeatherProvider
	{
		private readonly OpenWeatherProvider _provider;
		private readonly IMapper _mapper;

		public OpenWeatherAdapter(OpenWeatherProvider provider, IMapper mapper)
		{
			_provider = provider;
			_mapper = mapper;
		}

		// Adapting (mapping) 'OpenWeather' model to 'Weather.Core' model
		public async Task<WeatherItem> GetCurrentWeatherAsync(string cityName, string units)
		{
			var currentWeather = await _provider.GetCurrentWeatherAsync(cityName, units);
			var coreWeather = _mapper.Map<OpenWeatherItem, WeatherItem>(currentWeather);
			return coreWeather;
		}

		public async Task<IEnumerable<WeatherItem>> GetForecastWeatherAsync(string cityName, string units)
		{
			var forecastWeather = await _provider.GetForecastWeatherAsync(cityName, units);
			var forecastCore = _mapper.Map<IEnumerable<OpenWeatherItem>, IEnumerable<WeatherItem>>(forecastWeather.Items);
			return forecastCore;
		}
	}
}