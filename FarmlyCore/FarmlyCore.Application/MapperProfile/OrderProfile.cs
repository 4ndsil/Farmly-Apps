using AutoMapper;
using FarmlyCore.Application.DTOs.Order;
using FarmlyCore.Infrastructure.Entities;

namespace FarmlyCore.Application.MapperProfile
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderDto>()
                .ForMember(e => e.OrderItems, v => v.MapFrom(src => src.OrderItems))
                .ForMember(e => e.Buyer, v => v.MapFrom(src => src.Buyer))
                .ForMember(e => e.DeliveryPointId, v => v.MapFrom(src => src.FkDeliveryPointId));

            CreateMap<OrderDto, Order>()
                .ForMember(e => e.Buyer, v => v.Ignore())
                .ForMember(e => e.OrderItems, v => v.MapFrom(src => src.OrderItems));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(e => e.AdvertItem, v => v.MapFrom(src => src.AdvertItem))
                .ForMember(e => e.Category, v => v.MapFrom(src => src.Category))
                .ForMember(e => e.PriceType, v => v.MapFrom(src => src.PriceType))
                .ForMember(e => e.PickupPointId, v => v.MapFrom(src => src.FkPickupPointId));

            CreateMap<OrderItemDto, OrderItem>()
              .ForMember(e => e.AdvertItem, v => v.Ignore())
              .ForMember(e => e.Category, v => v.Ignore())
              .ForMember(e => e.PriceType, v => v.MapFrom(src => src.PriceType));
        }
    }
}