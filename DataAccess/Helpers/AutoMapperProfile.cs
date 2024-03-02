using AutoMapper;
using BusinessObject.DTOs;
using BusinessObject.Entities;
using System.Linq;

namespace DataAccess.Helpers
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<Commission, AddCommisionDTO>().ReverseMap();
			CreateMap<CommissionImage, AddCommissionImageDTO>().ReverseMap();
			CreateMap<AppUser, UserDetailDTO>().ReverseMap();
			CreateMap<CommissionRequest, CommissionRequestDTO>().ReverseMap();
            CreateMap<Artwork, AddArtworkDTO>().ReverseMap();
            CreateMap<ArtworkImage, AddArtworkImageDTO>().ReverseMap();
			CreateMap<ArtworkImage, UpdateArtworkImageDTO>().ReverseMap();
			CreateMap<AppUser, AppUserProfileDTO>()
				.ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.UserImage.Url))
				.ReverseMap();
			CreateMap<CommissionRequest, CommissionRequestHistoryDTO>()
				.ForMember(dest => dest.ReceiverEmail, opt => opt.MapFrom(src => src.Receiver.Email))
				.ForMember(dest => dest.SenderEmail, opt => opt.MapFrom(src => src.Sender.Email))
				.ForMember(dest => dest.CommissionStatusDescription, opt => opt.MapFrom(src => src.CommissionStatus.Description))
				.ReverseMap();
			CreateMap<AppUser, AppUserDTO>()
				.ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.UserRoles.FirstOrDefault().Role.Name))
				.ReverseMap();
            CreateMap<AppUser, UserDetailDTO>().ReverseMap();
        }
        }
	
}
