using System.Collections.Generic;

namespace Weather.Presentation.Classes.Dto
{
	public class GetWeatherDto
	{
		public WeatherItemDto Current { get; set; }

		public IEnumerable<WeatherItemDto> ForecastItems { get; set; }
	}
}