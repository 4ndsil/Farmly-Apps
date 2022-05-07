using AutoMapper;
using FarmlyCore.Application.DTOs.Adverts;
using FarmlyCore.Infrastructure.Entites;
using FarmlyCore.Infrastructure.Entities;

namespace FarmlyCore.Application.MapperProfile
{
    public class AdvertProfile : Profile
    {
        public AdvertProfile()
        {
            CreateMap<Advert, AdvertDto>()
                .ForMember(e => e.Seller, v => v.MapFrom(src => src.Seller))
                .ForMember(e => e.Category, v => v.MapFrom(src => src.Category))
                .ForMember(e => e.PickupPoint, v => v.MapFrom(src => src.PickupPoint))
                .ForMember(e => e.AdvertItems, v => v.MapFrom(src => src.AdvertItems))
                .ReverseMap();

            CreateMap<AdvertItem, AdvertItemDto>().ReverseMap();

            CreateMap<CreateAdvertDto, AdvertItem>().ReverseMap();
        }
    }
}