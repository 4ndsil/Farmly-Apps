using AutoMapper;
using FarmlyCore.Application.DTOs.Customer;
using FarmlyCore.Application.DTOs.Order;
using FarmlyCore.Application.Queries.Requests.Customers;
using FarmlyCore.Application.Queries.Requests.Orders;
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

            var order = new Order
            {
                OrderItems = orderItems,
                OrderNumber = request.Order.OrderNumber,
                PlacementDate = request.Order.PlacementDate,
                
            };

            await _farmlyEntityDataContext.AddAsync(order);

            await _farmlyEntityDataContext.SaveChangesAsync();

            var customerDto = _mapper.Map<OrderDto>(order);

            return customerDto;
        }
    }
}