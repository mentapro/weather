using System;
using Weather.Domain.Contracts;

namespace Weather.Infrastructure.Repositories
{
	public class WeatherItemRepository : IWeatherItemRepository
	{
		private readonly WeatherContext _context;
		
		public IUnitOfWork UnitOfWork => _context;

		public WeatherItemRepository(WeatherContext context)
		{
			_context = context ?? throw new ArgumentNullException(nameof(context));
		}
	}
}