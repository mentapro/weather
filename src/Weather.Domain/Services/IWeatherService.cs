using System.Collections.Generic;
using System.Threading.Tasks;

namespace Weather.Domain.Services
{
	public interface IWeatherService
	{
		Task<WeatherDataSource> GetCurrentWeatherAsync(string cityName);

		Task<IEnumerable<WeatherItem>> GetDetailedWeatherForecastAsync(string cityName, string units);

		Task<IEnumerable<WeatherItem>> GetDayAverageWeatherForecastAsync(string cityName, string units);
	}
}