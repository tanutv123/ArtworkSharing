using AutoMapper;
using BusinessObject.DTOs;
using BusinessObject.Entities;

namespace DataAccess.Helpers
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<Commission, AddCommisionDTO>().ReverseMap();
		}
	}
}
