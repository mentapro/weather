using System.Threading.Tasks;

namespace Weather.Core
{
	public interface IWeatherProvider
	{
		Task<WeatherItem> GetCurrentWeatherAsync();
	}
}