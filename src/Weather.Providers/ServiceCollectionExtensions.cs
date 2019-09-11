using Microsoft.Extensions.DependencyInjection;
using Weather.Domain;
using Weather.Providers.OpenWeather;
using Weather.Providers.Weatherbit;

namespace Weather.Providers
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddWeatherProvider(this IServiceCollection services)
        {
            services
                .AddHttpClient<OpenWeatherProvider>()
                .AddHttpMessageHandler<OpenWeatherAppIdQueryStringHandler>()
                .AddHttpMessageHandler<HttpClientErrorHandler>();

            services
                .AddHttpClient<WeatherbitProvider>()
                .AddHttpMessageHandler<WeatherbitKeyQueryStringHandler>()
                .AddHttpMessageHandler<HttpClientErrorHandler>();

            services
                .AddTransient<OpenWeatherAppIdQueryStringHandler>()
                .AddTransient<WeatherbitKeyQueryStringHandler>()
                .AddTransient<HttpClientErrorHandler>();

            services.AddTransient<IWeatherProvider, WeatherProvider>();

            return services;
        }
    }
}