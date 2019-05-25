using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Weather.Core;

namespace Weather.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class WeatherController : ControllerBase
	{
		private readonly IWeatherProvider _weatherProvider;

		public WeatherController(IWeatherProvider weatherProvider)
		{
			_weatherProvider = weatherProvider;
		}

		[HttpGet]
		public async Task<IActionResult> Get()
		{
			var current = await _weatherProvider.GetCurrentWeatherAsync();
			return Ok(current);
		}
	}
}
