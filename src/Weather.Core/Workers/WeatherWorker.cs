using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Weather.Common;
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

		public async Task<GetWeatherDto> GetWeatherAsync(string city, string units, SortingCriteria sorting = null)
		{
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

	public static class OrderingExtensions
	{
		public static IEnumerable<T> OrderByCriteria<T>(this IEnumerable<T> source, SortingCriteria sorting)
		{
			var sortingProperty = typeof(T).GetProperties().FirstOrDefault(x => string.Equals(x.Name, sorting.ColumnName, StringComparison.InvariantCultureIgnoreCase));
			if (sortingProperty == null)
				throw new WeatherValidationException($"{typeof(T).Name} does not have column name '{sorting.ColumnName}'.");

			return sorting.SortOrder == SortOrder.Ascending ? source.OrderBy(x => sortingProperty.GetValue(x)) : source.OrderByDescending(x => sortingProperty.GetValue(x));
		}
	}
}