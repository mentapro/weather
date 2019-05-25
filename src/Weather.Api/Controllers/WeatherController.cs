using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Weather.Core.Workers;
using Weather.Core.Workers.Dto;

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
			SortingCriteria sorting = null;
			if (!string.IsNullOrWhiteSpace(sortColumn))
				sorting = new SortingCriteria {ColumnName = sortColumn, SortOrder = sortOrder};

			return await _worker.GetWeatherAsync(city, units, sorting);
		}
	}
}