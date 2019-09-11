using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Weather.Domain;
using Weather.Domain.Services;

namespace Weather.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class WeatherController : ControllerBase
	{
		private readonly IWeatherService _weatherService;

		public WeatherController(IWeatherService weatherService)
		{
			_weatherService = weatherService ?? throw new ArgumentNullException(nameof(weatherService));
		}

		[HttpGet]
		[Route("get")]
		public async Task<ActionResult<WeatherDataSource>> GetWeather([Required] string city)
		{
			return await _weatherService.GetCurrentWeatherAsync(city);
		}
	}
}