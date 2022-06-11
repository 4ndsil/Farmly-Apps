using AutoMapper;
using FarmlyCore.Application.DTOs.Order;
using FarmlyCore.Application.Requests.Orders;
using FarmlyCore.Infrastructure.FarmlyDbContext;
using FarmlyCore.Infrastructure.Queries;
using Microsoft.EntityFrameworkCore;

namespace FarmlyCore.Application.Queries.OrderItems
{
    public class RespondToOrderItemQueryHandler : IQueryHandler<RespondToOrderItemRequest, OrderItemDto>
    {
        private readonly FarmlyEntityDbContext _farmlyEntityDataContext;
        private readonly IMapper _mapper;

        public RespondToOrderItemQueryHandler(IMapper mapper, FarmlyEntityDbContext farmlyEntityDbContext)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _farmlyEntityDataContext = farmlyEntityDbContext ?? throw new ArgumentNullException(nameof(farmlyEntityDbContext));
        }

        public async Task<OrderItemDto> HandleAsync(RespondToOrderItemRequest request, CancellationToken cancellationToken = default)
        {
            var orderItem = await _farmlyEntityDataContext.OrderItems
                .Where(e => e.Id == request.OrderItemId)
                .FirstOrDefaultAsync();

            if (orderItem == null)
            {
                return null;
            }

            var orderItemDto = _mapper.Map<OrderItemDto>(orderItem);

            request.OrderItem.ApplyTo(orderItemDto);

            _mapper.Map(orderItemDto, orderItem);

            await _farmlyEntityDataContext.SaveChangesAsync(cancellationToken);

            return orderItemDto;
        }
    }
}