using AutoMapper;
using FarmlyCore.Application.DTOs.Customer;
using FarmlyCore.Application.DTOs.Order;
using FarmlyCore.Application.Requests.Orders;
using FarmlyCore.Infrastructure.Entities;
using FarmlyCore.Infrastructure.FarmlyDbContext;
using FarmlyCore.Infrastructure.Queries;

namespace FarmlyCore.Application.Queries.Orders
{
    public class CreateOrderQueryHandler : IQueryHandler<CreateOrderRequest, OrderDto>
    {
        private readonly FarmlyEntityDbContext _farmlyEntityDataContext;
        private readonly IMapper _mapper;

        public CreateOrderQueryHandler(IMapper mapper, FarmlyEntityDbContext farmlyEntityDbContext)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _farmlyEntityDataContext = farmlyEntityDbContext ?? throw new ArgumentNullException(nameof(farmlyEntityDbContext));
        }

        public async Task<OrderDto> HandleAsync(CreateOrderRequest request, CancellationToken cancellationToken = default)
        {
            List<OrderItem> orderItems = null;

            if (request.Order.OrderItems != null)
            {
                orderItems.AddRange(_mapper.Map<OrderItem[]>(request.Order.OrderItems));
            }

            var deliveryPoint = _farmlyEntityDataContext.CustomerAddresses.FirstOrDefault(e => e.Id == request.Order.DeliveryPoint.Id);

            if (deliveryPoint == null)
            {
                return null;
            }

            var buyer = _farmlyEntityDataContext.Customers.FirstOrDefault(e => e.Id == request.Order.Buyer.Id);

            if (buyer == null)
            {
                return null;
            }

            var order = new Order
            {
                OrderItems = orderItems,
                OrderNumber = request.Order.OrderNumber,
                PlacementDate = request.Order.PlacementDate,
                DeliveryDate = request.Order.DeliveryDate,
                Delivered = request.Order.Delivered,
                DeliveryPoint = deliveryPoint,
                FkDeliveryPointId = deliveryPoint.Id,
                TotalPrice = request.Order.TotalPrice,
                Buyer = buyer,
                FkBuyerId = buyer.Id,
            };

            await _farmlyEntityDataContext.AddAsync(order);

            await _farmlyEntityDataContext.SaveChangesAsync();

            var customerDto = _mapper.Map<OrderDto>(order);

            return customerDto;
        }
    }
}