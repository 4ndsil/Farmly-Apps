using AutoMapper;
using FarmlyCore.Application.DTOs.Order;
using FarmlyCore.Infrastructure.Entities;
using System.Linq;

namespace FarmlyCore.Application.MapperProfile
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderDto>().ForMember(e => e.OrderItems, v => v.MapFrom(src => src.OrderItems));

            CreateMap<OrderDto, Order>().ForMember(e => e.OrderItems, v => v.MapFrom(src => src.OrderItems));

            CreateMap<OrderItem, OrderItemDto>().ForMember(e => e.PriceType, v => v.MapFrom(src => src.PriceType));
        }
    }
}
