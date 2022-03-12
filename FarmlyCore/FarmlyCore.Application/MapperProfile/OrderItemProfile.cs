using AutoMapper;
using FarmlyCore.Application.DTOs.Order;
using FarmlyCore.Infrastructure.Entities;

namespace FarmlyCore.Application.MapperProfile
{
    public class OrderItemProfile : Profile
    {
        public OrderItemProfile()
        {
            CreateMap<OrderItem, OrderItemDto>().ReverseMap();
        }
    }
}
