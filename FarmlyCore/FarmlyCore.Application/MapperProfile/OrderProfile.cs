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
                .ForMember(e => e.BuyerId, v => v.MapFrom(src => src.Buyer.Id))
                .ForMember(e => e.BuyerName, v => v.MapFrom(src => src.Buyer.CompanyName))
                .ForMember(e => e.DeliveryPointId, v => v.MapFrom(src => src.FkDeliveryPointId));

            CreateMap<OrderDto, Order>()
                .ForMember(e => e.Buyer, v => v.Ignore())                
                .ForMember(e => e.OrderItems, v => v.MapFrom(src => src.OrderItems));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(e => e.SellerId, v => v.MapFrom(src => src.AdvertItem.Advert.FkSellerId))
                .ForMember(e => e.SellerName, v => v.MapFrom(src => src.Seller.CompanyName))
                .ForMember(e => e.CategoryId, v => v.MapFrom(src => src.Category.Id))
                .ForMember(e => e.Category, v => v.MapFrom(src => src.Category));

            CreateMap<OrderItemDto, OrderItem>()
                .ForMember(e => e.Category, v => v.Ignore())
                .ForMember(e => e.PriceType, v => v.MapFrom(src => src.PriceType));

            CreateMap<CreateOrderDto, Order>().ReverseMap();

            CreateMap<CreateOrderItemDto, OrderItem>().ReverseMap();

            CreateMap<OrderSummaryDto, Order>()
                .ForMember(e => e.OrderItems, v => v.MapFrom(src => src.OrderItems));

            CreateMap<Order, OrderSummaryDto>()
                .ForMember(e => e.OrderItems, v => v.MapFrom(src => src.OrderItems));

            CreateMap<OrderItemSummaryDto, OrderItem>().ReverseMap();
        }
    }
}