using System.Collections.Generic;

namespace Weather.Core.Workers.Dto
{
	public class GetWeatherDto
	{
		public WeatherItemDto Current { get; set; }

		public IEnumerable<WeatherItemDto> ForecastItems { get; set; }
	}
}