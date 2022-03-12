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
            CreateMap<Order, OrderDto>().ForMember(e => e.OrderItemsIds, v => v.MapFrom(src => src.OrderItems.Select(c => c.Id))).ReverseMap();
        }
    }
}
