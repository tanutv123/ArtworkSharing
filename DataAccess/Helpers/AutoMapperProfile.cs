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
<<<<<<< HEAD
            CreateMap<ArtworkImage, UpdateArtworkImageDTO>().ReverseMap();
            CreateMap<AppUser, AppUserProfileDTO>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.UserImage.Url))
                .ReverseMap();
            CreateMap<CommissionRequest, CommissionRequestHistoryDTO>()
                .ForMember(dest => dest.ReceiverEmail, opt => opt.MapFrom(src => src.Receiver.Email))
                .ForMember(dest => dest.SenderEmail, opt => opt.MapFrom(src => src.Sender.Email))
                .ForMember(dest => dest.CommissionStatusDescription, opt => opt.MapFrom(src => src.CommissionStatus.Description))
                .ReverseMap();
            CreateMap<CommissionRequest, CommissionRequestHistoryAdminDTO>()
                .ForMember(dest => dest.ReceiverEmail, opt => opt.MapFrom(src => src.Receiver.Email))
                .ForMember(dest => dest.SenderEmail, opt => opt.MapFrom(src => src.Sender.Email))
                .ForMember(dest => dest.CommissionStatusDescription, opt => opt.MapFrom(src => src.CommissionStatus.Description))
                .ForMember(dest => dest.CommissionImages, opt => opt.MapFrom(src => src.CommissionImages.Select(ci => ci.Url))) 
                .ReverseMap();
            CreateMap<AppUser, AppUserDTO>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.UserRoles.FirstOrDefault().Role.Name))
                .ReverseMap();
=======
			CreateMap<ArtworkImage, UpdateArtworkImageDTO>().ReverseMap();
            CreateMap<Purchase, AddPurchaseDTO>().ReverseMap();
            CreateMap<Transaction, AddTransationDTO>().ReverseMap();
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
>>>>>>> 57f893dfcbe92d3560bc91f9270b90e570fc36a8
            CreateMap<AppUser, UserDetailDTO>().ReverseMap();
        }
    }

}
