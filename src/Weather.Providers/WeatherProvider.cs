using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Weather.Domain;
using Weather.Providers.OpenWeather;
using Weather.Providers.OpenWeather.Models;
using Weather.Providers.Weatherbit;
using Weather.Providers.Weatherbit.Models;

namespace Weather.Providers
{
    internal class WeatherProvider : IWeatherProvider
    {
        private readonly OpenWeatherProvider _openWeatherProvider;
        private readonly WeatherbitProvider _weatherbitProvider;
        private readonly IMapper _mapper;

        public WeatherProvider(
            OpenWeatherProvider openWeatherProvider,
            WeatherbitProvider weatherbitProvider,
            IMapper mapper)
        {
            _openWeatherProvider = openWeatherProvider ?? throw new ArgumentNullException(nameof(openWeatherProvider));
            _weatherbitProvider = weatherbitProvider ?? throw new ArgumentNullException(nameof(weatherbitProvider));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<WeatherDataSource> GetCurrentWeatherAsync(string cityName)
        {
            using (var cts = new CancellationTokenSource())
            {
                var ct = cts.Token;

                var requestTimer = Stopwatch.StartNew();

                // make two requests
                var openWeatherTask = _openWeatherProvider.GetCurrentWeatherAsync(cityName, ct);
                var weatherbitTask = _weatherbitProvider.GetCurrentWeatherAsync(cityName, ct);

                // get first completed response and cancel another request
                var firstResponse = await Task.WhenAny(openWeatherTask, weatherbitTask);
                requestTimer.Stop();
                cts.Cancel();

                var dataSource = new WeatherDataSource
                {
                    RequestTime = requestTimer.Elapsed
                };

                if (firstResponse == openWeatherTask)
                {
                    dataSource.ProviderName = "Open Weather provider";
                    var response = await openWeatherTask;
                    dataSource.Item = _mapper.Map<OpenWeatherItem, WeatherItem>(response);
                }
                else
                {
                    dataSource.ProviderName = "Weatherbit provider";
                    var response = await weatherbitTask;
                    dataSource.Item = _mapper.Map<WeatherbitItem, WeatherItem>(response);
                }

                return dataSource;
            }
        }

        public async Task<IEnumerable<WeatherItem>> GetForecastWeatherAsync(string cityName)
        {
            var forecastWeather = await _openWeatherProvider.GetForecastWeatherAsync(cityName);
            var forecastCore = _mapper.Map<IEnumerable<OpenWeatherItem>, IEnumerable<WeatherItem>>(forecastWeather.Items);
            return forecastCore;
        }
    }
}