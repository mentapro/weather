using System.Collections.Generic;
using System.Threading.Tasks;

namespace Weather.Core
{
	public interface IWeatherProvider
	{
		Task<WeatherItem> GetCurrentWeatherAsync(string cityName, string units);

		Task<IEnumerable<WeatherItem>> GetForecastWeatherAsync(string cityName, string units);
	}
}