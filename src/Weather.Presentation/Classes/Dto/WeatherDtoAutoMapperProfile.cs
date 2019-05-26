using AutoMapper;
using Weather.Domain;

namespace Weather.Presentation.Classes.Dto
{
	public class WeatherDtoAutoMapperProfile : Profile
	{
		public WeatherDtoAutoMapperProfile()
		{
			CreateMap<WeatherItem, WeatherItemDto>();
		}
	}
}