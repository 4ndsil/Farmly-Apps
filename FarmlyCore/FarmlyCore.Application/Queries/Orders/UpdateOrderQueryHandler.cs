using AutoMapper;
using FarmlyCore.Application.DTOs;
using FarmlyCore.Application.DTOs.Order;
using FarmlyCore.Application.Requests.Orders;
using FarmlyCore.Infrastructure.FarmlyDbContext;
using FarmlyCore.Infrastructure.Queries;
using Microsoft.EntityFrameworkCore;

namespace FarmlyCore.Application.Queries.Orders
{
    public class UpdateOrderQueryHandler : IQueryHandler<UpdateOrderRequest, OrderDto>
    {
        private readonly FarmlyEntityDbContext _farmlyEntityDataContext;
        private readonly IMapper _mapper;

        public UpdateOrderQueryHandler(IMapper mapper, FarmlyEntityDbContext farmlyEntityDbContext)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _farmlyEntityDataContext = farmlyEntityDbContext ?? throw new ArgumentNullException(nameof(farmlyEntityDbContext));
        }

        public async Task<OrderDto> HandleAsync(UpdateOrderRequest request, CancellationToken cancellationToken = default)
        {
            var order = await _farmlyEntityDataContext.Orders
                .Include(e => e.OrderItems)
                .Include(e => e.DeliveryPoint)
                .Where(e => e.Id == request.OrderId)
                .FirstOrDefaultAsync();

            if (order == null)
            {
                return null;
            }

            var orderDto = _mapper.Map<OrderDto>(order);

            request.Order.ApplyTo(orderDto);

            _mapper.Map(orderDto, order);

            await _farmlyEntityDataContext.SaveChangesAsync(cancellationToken);

            return orderDto;
        }
    }
}
