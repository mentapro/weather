using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Weather.Core;

namespace Weather.Providers.OpenWeather
{
	public static class OpenWeatherServiceCollectionExtensions
	{
		// Extension to make usage OpenWeather more easier
		public static IServiceCollection AddOpenWeather(this IServiceCollection services, IConfiguration configuration)
		{
			services.Configure<OpenWeatherOptions>(configuration.GetSection("OpenWeather"));

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