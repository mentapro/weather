using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Weather.Core.Services;
using Weather.Core.Workers.Dto;

namespace Weather.Core.Workers
{
	public class WeatherWorker
	{
		private readonly IWeatherService _weatherService;
		private readonly IMapper _mapper;

		public WeatherWorker(IWeatherService weatherService, IMapper mapper)
		{
			_weatherService = weatherService;
			_mapper = mapper;
		}

		public async Task<GetWeatherDto> GetWeatherAsync(string city, string units)
		{
			// Create two async calls
			var currentWeatherTask = _weatherService.GetCurrentWeatherAsync(city, units);
			var forecastWeatherTask = _weatherService.GetDayAverageWeatherForecastAsync(city, units);

			// Wait until all calls will be completed
			var currentWeather = await currentWeatherTask;
			var forecastWeather = await forecastWeatherTask;

			var dto = new GetWeatherDto
			{
				Current = _mapper.Map<WeatherItem, WeatherItemDto>(currentWeather),
				ForecastItems = _mapper.Map<IEnumerable<WeatherItem>, IEnumerable<WeatherItemDto>>(forecastWeather)
			};

			return dto;
		}
	}
}