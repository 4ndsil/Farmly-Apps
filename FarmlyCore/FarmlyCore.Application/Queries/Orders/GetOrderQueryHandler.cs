using AutoMapper;
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
            var customer = await _farmlyEntityDataContext.Orders
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == request.OrderId, cancellationToken);

            if (customer == null)
            {
                return null;
            }

            return _mapper.Map<OrderDto>(customer);
        }
    }
}
