using AutoMapper;
using FarmlyCore.Application.DTOs.Adverts;
using FarmlyCore.Infrastructure.Entities;

namespace FarmlyCore.Application.MapperProfile
{
    public class AdvertProfile : Profile
    {
        public AdvertProfile()
        {
            CreateMap<Advert, AdvertDto>()
                .ForMember(e => e.SellerId, v => v.MapFrom(src => src.Seller.Id))
                .ForMember(e => e.SellerName, v => v.MapFrom(src => src.Seller.CompanyName))
                .ForMember(e => e.Category, v => v.MapFrom(src => src.Category))
                .ForMember(e => e.PickupPointId, v => v.MapFrom(src => src.FkPickupPointId))
                .ForMember(e => e.AdvertItems, v => v.MapFrom(src => src.AdvertItems));

            CreateMap<AdvertDto, Advert>()
               .ForMember(e => e.Seller, v => v.Ignore())
               .ForMember(e => e.PickupPoint, v => v.Ignore())
               .ForMember(e => e.Category, v => v.MapFrom(src => src.Category))               
               .ForMember(e => e.AdvertItems, v => v.MapFrom(src => src.AdvertItems));

            CreateMap<AdvertItem, AdvertItemDto>()
                .ForMember(e => e.AdvertId, v => v.MapFrom(src => src.Advert.Id));

            CreateMap<AdvertItemDto, AdvertItem>();

            CreateMap<CreateAdvertDto, Advert>().ReverseMap();

            CreateMap<CreateAdvertItemDto, AdvertItem>().ReverseMap();
        }
    }
}