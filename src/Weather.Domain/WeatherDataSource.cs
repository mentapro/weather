using System;

namespace Weather.Domain
{
    public class WeatherDataSource
    {
        public WeatherItem Item { get; set; }
        
        public string ProviderName { get; set; }
        
        public TimeSpan RequestTime { get; set; }
    }
}