using AutoMapper;
using FarmlyCore.Application.DTOs.Order;
using FarmlyCore.Application.Requests.OrderItems;
using FarmlyCore.Infrastructure.FarmlyDbContext;
using FarmlyCore.Infrastructure.Queries;
using Microsoft.EntityFrameworkCore;

namespace FarmlyCore.Application.Queries.OrderItems
{
    public class OrderItemStatusResponse
    {
        private OrderItemStatusResponse(OrderItemResponseDetail detail) { Detail = detail; }

        public static OrderItemStatusResponse WithProblem(OrderItemResponseDetail detail) => new OrderItemStatusResponse(detail);

        public static OrderItemStatusResponse OrderAccepted(OrderItemResponseDetail detail) => new OrderItemStatusResponse(detail);

        public static OrderItemStatusResponse OrderRejected(OrderItemResponseDetail detail) => new OrderItemStatusResponse(detail);

        public static OrderItemStatusResponse OrderDelivered(OrderItemResponseDetail detail) => new OrderItemStatusResponse(detail);

        public OrderItemResponseDetail? Detail { get; set; }
    }

    public enum OrderItemResponseDetail
    {
        Problem,
        OrderAccepted,
        OrderRejected,
        OrderDelivered
    }

    public class RespondToOrderItemQueryHandler : IQueryHandler<RespondToOrderItemRequest, OrderItemStatusResponse>
    {
        private readonly FarmlyEntityDbContext _farmlyEntityDataContext;
        private readonly IMapper _mapper;

        public RespondToOrderItemQueryHandler(IMapper mapper, FarmlyEntityDbContext farmlyEntityDbContext)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _farmlyEntityDataContext = farmlyEntityDbContext ?? throw new ArgumentNullException(nameof(farmlyEntityDbContext));
        }

        public async Task<OrderItemStatusResponse> HandleAsync(RespondToOrderItemRequest request, CancellationToken cancellationToken = default)
        {
            var orderItem = await _farmlyEntityDataContext.OrderItems
                .Where(e => e.Id == request.OrderItemId)
                .Include(e => e.Seller)
                .Include(e => e.Category)
                .FirstOrDefaultAsync();

            if (orderItem == null)
            {
                return OrderItemStatusResponse.WithProblem(OrderItemResponseDetail.Problem);
            }

            var orderItemDto = _mapper.Map<OrderItemDto>(orderItem);

            request.OrderItem.ApplyTo(orderItemDto);

            _mapper.Map(orderItemDto, orderItem);

            await _farmlyEntityDataContext.SaveChangesAsync(cancellationToken);

            if (orderItemDto.ResponseStatus == ResponseStatusDto.Accepted)
            {
                return OrderItemStatusResponse.OrderAccepted(OrderItemResponseDetail.OrderAccepted);
            }

            if (orderItemDto.ResponseStatus == ResponseStatusDto.Rejected)
            {
                return OrderItemStatusResponse.OrderAccepted(OrderItemResponseDetail.OrderRejected);
            }

            return OrderItemStatusResponse.OrderAccepted(OrderItemResponseDetail.OrderDelivered);
        }
    }
}