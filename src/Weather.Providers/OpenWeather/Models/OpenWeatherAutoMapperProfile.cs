using System;
using AutoMapper;
using Weather.Core;

namespace Weather.Providers.OpenWeather.Models
{
	internal class OpenWeatherAutoMapperProfile : Profile
	{
		public OpenWeatherAutoMapperProfile()
		{
			CreateMap<OpenWeatherItem, WeatherItem>()
				.ForMember(dest => dest.Date, opt => opt.MapFrom(x => DateTimeOffset.FromUnixTimeSeconds(x.UnixTimestamp).UtcDateTime))
				.ForMember(dest => dest.Temperature, opt => opt.MapFrom(x => x.Main.Temp))
				.ForMember(dest => dest.Pressure, opt => opt.MapFrom(x => x.Main.Pressure))
				.ForMember(dest => dest.Humidity, opt => opt.MapFrom(x => x.Main.Humidity))
				.ForMember(dest => dest.MinimumTemperature, opt => opt.MapFrom(x => x.Main.TempMin))
				.ForMember(dest => dest.MaximumTemperature, opt => opt.MapFrom(x => x.Main.TempMax))
				.ForMember(dest => dest.WindSpeed, opt => opt.MapFrom(x => x.Wind.Speed))
				.ForMember(dest => dest.WindDirectionDegrees, opt => opt.MapFrom(x => x.Wind.DirectionDegrees))
				.ForMember(dest => dest.Cloudiness, opt => opt.MapFrom(x => x.Clouds.Cloudiness));
		}
	}
}