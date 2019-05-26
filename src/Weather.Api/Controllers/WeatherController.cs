using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Weather.Presentation.Classes.Dto;
using Weather.Presentation.Workers;

namespace Weather.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class WeatherController : ControllerBase
	{
		private readonly WeatherWorker _worker;

		public WeatherController(WeatherWorker worker)
		{
			_worker = worker;
		}

		[HttpGet]
		[Route("get")]
		public async Task<ActionResult<GetWeatherDto>> GetWeather([Required] string city, string units, string sortColumn = null, SortOrder sortOrder = SortOrder.Ascending)
		{
			return await _worker.GetWeatherAsync(city, units, sortColumn, sortOrder);
		}
	}
}