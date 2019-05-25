using System.Threading.Tasks;
using AutoMapper;
using Weather.Core;
using Weather.Providers.OpenWeather.Models;

namespace Weather.Providers.OpenWeather
{
	public class OpenWeatherAdapter : IWeatherProvider
	{
		private readonly OpenWeatherProvider _provider;
		private readonly IMapper _mapper;

		public OpenWeatherAdapter(OpenWeatherProvider provider, IMapper mapper)
		{
			_provider = provider;
			_mapper = mapper;
		}

		public async Task<WeatherItem> GetCurrentWeatherAsync()
		{
			var currentWeather = await _provider.GetCurrentWeatherAsync();
			var coreWeather = _mapper.Map<OpenWeatherItem, WeatherItem>(currentWeather);
			return coreWeather;
		}
	}
}