using System.Collections.Generic;
using System.Threading.Tasks;

namespace Weather.Core.Services
{
	public interface IWeatherService
	{
		Task<WeatherItem> GetCurrentWeatherAsync(string cityName, string units);

		Task<IEnumerable<WeatherItem>> GetDetailedWeatherForecastAsync(string cityName, string units);

		Task<IEnumerable<WeatherItem>> GetDayAverageWeatherForecastAsync(string cityName, string units);
	}
}