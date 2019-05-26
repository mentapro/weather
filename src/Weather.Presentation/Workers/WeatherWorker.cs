using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using AutoMapper;
using Weather.Domain;
using Weather.Domain.Services;
using Weather.Presentation.Classes;
using Weather.Presentation.Classes.Dto;

namespace Weather.Presentation.Workers
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

		public async Task<GetWeatherDto> GetWeatherAsync(string city, string units, string sortColumn = null, SortOrder sortOrder = SortOrder.Ascending)
		{
			SortingCriteria sorting = null;
			if (!string.IsNullOrWhiteSpace(sortColumn))
				sorting = new SortingCriteria {ColumnName = sortColumn, SortOrder = sortOrder};
			
			// Create two async calls
			var currentWeatherTask = _weatherService.GetCurrentWeatherAsync(city, units);
			var forecastWeatherTask = _weatherService.GetDayAverageWeatherForecastAsync(city, units);

			// Wait until all calls will be completed
			var currentWeather = await currentWeatherTask;
			var forecastWeather = await forecastWeatherTask;

			if (sorting != null)
				forecastWeather = forecastWeather.OrderByCriteria(sorting);

			var dto = new GetWeatherDto
			{
				Current = _mapper.Map<WeatherItem, WeatherItemDto>(currentWeather),
				ForecastItems = _mapper.Map<IEnumerable<WeatherItem>, IEnumerable<WeatherItemDto>>(forecastWeather)
			};

			return dto;
		}
	}
}