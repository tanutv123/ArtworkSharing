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
			CreateMap<CommissionRequest, CommissionRequestDTO>().ReverseMap();
			CreateMap<AppUser, AppUserProfileDTO>()
				.ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.UserImage.Url))
				.ReverseMap();
			CreateMap<CommissionRequest, CommissionRequestHistoryDTO>()
				.ForMember(dest => dest.ReceiverEmail, opt => opt.MapFrom(src => src.Receiver.Email))
				.ForMember(dest => dest.SenderEmail, opt => opt.MapFrom(src => src.Sender.Email))
				.ForMember(dest => dest.CommissionStatusDescription, opt => opt.MapFrom(src => src.CommissionStatus.Description))
				.ReverseMap();
		}
	}
}
