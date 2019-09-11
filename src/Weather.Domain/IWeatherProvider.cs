using System.Collections.Generic;
using System.Threading.Tasks;

namespace Weather.Domain
{
	public interface IWeatherProvider
	{
		Task<WeatherDataSource> GetCurrentWeatherAsync(string cityName);

		Task<IEnumerable<WeatherItem>> GetForecastWeatherAsync(string cityName);
	}
}