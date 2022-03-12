using AutoMapper;
using FarmlyCore.Application.DTOs;
using FarmlyCore.Infrastructure.Entities;

namespace FarmlyCore.Application.MapperProfile
{
    public class AdvertProfile : Profile
    {
        public AdvertProfile()
        {
            CreateMap<Advert, AdvertDto>()
                .ForMember(e => e.SellerId, v => v.MapFrom(src => src.Seller.Id))
                .ForMember(e => e.CategoryId, v => v.MapFrom(src => src.Category.Id))
                .ForMember(e => e.PickupPointId, v => v.MapFrom(src => src.PickupPoint.Id));
        }        
    }
}
