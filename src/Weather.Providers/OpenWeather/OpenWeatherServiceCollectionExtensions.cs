using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Weather.Domain.Contracts;
using Weather.Domain.Exceptions;

namespace Weather.Providers.OpenWeather
{
	public static class OpenWeatherServiceCollectionExtensions
	{
		// Extension to make usage OpenWeather more easier
		public static IServiceCollection AddOpenWeather(this IServiceCollection services, IConfiguration configuration)
		{
			var openWeatherSection = configuration.GetSection("OpenWeather");
			var options = openWeatherSection.Get<OpenWeatherOptions>();
			if (string.IsNullOrWhiteSpace(options.AppId))
				throw new WeatherValidationException("Please, provide 'OpenWeather__AppId' variable to your environment.");

			services.Configure<OpenWeatherOptions>(openWeatherSection);

			services
				.AddHttpClient<OpenWeatherProvider>()
				.AddHttpMessageHandler<AppIdQueryStringHandler>()
				.AddHttpMessageHandler<HttpClientErrorHandler>();

			services
				.AddTransient<AppIdQueryStringHandler>()
				.AddTransient<HttpClientErrorHandler>()
				.AddTransient<IWeatherProvider, OpenWeatherAdapter>();

			return services;
		}
	}
}