using Microsoft.EntityFrameworkCore;
using Weather.Domain;

namespace Weather.Infrastructure
{
	public class WeatherContext : DbContext
	{
		public DbSet<WeatherItem> WeatherItems { get; set; }
	}
}