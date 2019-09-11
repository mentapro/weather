using AutoMapper;
using Weather.Domain;

namespace Weather.Providers.Weatherbit.Models
{
    internal class WeatherbitAutoMapperProfile : Profile
    {
        public WeatherbitAutoMapperProfile()
        {
            CreateMap<WeatherbitItem, WeatherItem>()
                .ForMember(dest => dest.Temperature, opt => opt.MapFrom(x => x.Temp))
                .ForMember(dest => dest.Cloudiness, opt => opt.MapFrom(x => x.Clouds))
                .ForMember(dest => dest.Humidity, opt => opt.MapFrom(x => x.Humidity))
                .ForMember(dest => dest.Pressure, opt => opt.MapFrom(x => x.Pressure))
                .ForMember(dest => dest.WindSpeed, opt => opt.MapFrom(x => x.WindSpeed));
        }
    }
}