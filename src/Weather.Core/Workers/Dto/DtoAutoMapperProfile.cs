using AutoMapper;

namespace Weather.Core.Workers.Dto
{
	public class DtoAutoMapperProfile : Profile
	{
		public DtoAutoMapperProfile()
		{
			CreateMap<WeatherItem, WeatherItemDto>();
		}
	}
}