using AutoMapper;
using AutoMapper.QueryableExtensions;
using FarmlyCore.Application.DTOs;
using FarmlyCore.Application.DTOs.Adverts;
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
            var orderDto = await _farmlyEntityDataContext.Orders
                .Where(e => e.Id.Equals(request.OrderId))
                .Include(e => e.OrderItems)
                .ProjectTo<OrderDto>(_mapper.ConfigurationProvider)             
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);

            if (orderDto == null)
            {
                return null;
            }

            return orderDto;
        }
    }
}