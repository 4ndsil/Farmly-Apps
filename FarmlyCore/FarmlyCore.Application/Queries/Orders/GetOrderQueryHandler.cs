using AutoMapper;
using FarmlyCore.Application.DTOs.Customer;
using FarmlyCore.Application.DTOs.Order;
using FarmlyCore.Application.Requests.Orders;
using FarmlyCore.Infrastructure.FarmlyDbContext;
using FarmlyCore.Infrastructure.Queries;
using Microsoft.EntityFrameworkCore;

namespace FarmlyCore.Application.Queries.Orders
{
    public class GetOrderQueryHandler : IQueryHandler<GetOrderRequest, OrderDto>
    {
        private readonly FarmlyEntityDbContext _farmlyEntityDataContext;
        private readonly IMapper _mapper;

        public GetOrderQueryHandler(IMapper mapper, FarmlyEntityDbContext farmlyEntityDbContext)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _farmlyEntityDataContext = farmlyEntityDbContext ?? throw new ArgumentNullException(nameof(farmlyEntityDbContext));
        }

        public async Task<OrderDto> HandleAsync(GetOrderRequest request, CancellationToken cancellationToken = default)
        {
            var data = await _farmlyEntityDataContext.Orders
                .Where(e => e.Id.Equals(request.OrderId))
                .Select(e => new
                {
                    e.Id,
                    e.OrderNumber,
                    e.PlacementDate,
                    e.DeliveryDate,                    
                    e.FkBuyerId,
                    DeliveryPoint = new
                    {
                        e.DeliveryPoint.Id,
                        e.DeliveryPoint.Street,
                        e.DeliveryPoint.City,
                        e.DeliveryPoint.State,
                        e.DeliveryPoint.Zip,
                        e.DeliveryPoint.FkCustomerId
                    },
                    OrderItems = e.OrderItems.Select(f => new
                    {
                        f.Id,
                        f.ProductName,
                        f.Quantity,
                        f.Price,
                        f.PriceType,
                        f.FkOrderId,
                        f.FkAdvertItemId
                    })
                })
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);

            if (data == null)
            {
                return null;
            }

            return new OrderDto
            {
                Id = data.Id,
                OrderNumber = data.OrderNumber,
                PlacementDate = data.PlacementDate,
                DeliveryDate = data.DeliveryDate,                
                BuyerId = data.FkBuyerId,                
                DeliveryPoint = new CustomerAddressDto
                {
                    Id = data.DeliveryPoint.Id,
                    Street = data.DeliveryPoint.Street,
                    Zip = data.DeliveryPoint.Zip,
                    City = data.DeliveryPoint.City,
                    State = data.DeliveryPoint.State,
                    FKCustomerId = data.DeliveryPoint.FkCustomerId
                },
                OrderItems = data.OrderItems.Select(d => new OrderItemDto
                {
                    Id = d.Id,
                    ProductName = d.ProductName,
                    Price = d.Price,
                    PriceType = (OrderPriceTypeDto)d.PriceType,
                    Quantity = d.Quantity,
                    OrderId = d.FkOrderId,
                    AdvertItemId = d.FkAdvertItemId
                }).ToArray()
            };
        }
    }
}
