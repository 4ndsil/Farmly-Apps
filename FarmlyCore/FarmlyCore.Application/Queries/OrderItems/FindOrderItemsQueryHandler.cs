using AutoMapper;
using AutoMapper.QueryableExtensions;
using FarmlyCore.Application.DTOs.Order;
using FarmlyCore.Application.Queries.OrderItems.QueryFilters;
using FarmlyCore.Application.Requests.OrderItems;
using FarmlyCore.Infrastructure.FarmlyDbContext;
using FarmlyCore.Infrastructure.Queries;
using Microsoft.EntityFrameworkCore;

namespace FarmlyCore.Application.Queries.OrderItems
{
    public class FindOrderItemsQueryHandler : IQueryHandler<FindOrderItemsRequest, OrderItemDto[]>
    {
        private readonly FarmlyEntityDbContext _farmlyEntityDataContext;
        private readonly IMapper _mapper;
        private readonly IEnumerable<IOrderItemFilter> _orderItemFilters;

        public FindOrderItemsQueryHandler(IMapper mapper, FarmlyEntityDbContext farmlyEntityDbContext, IEnumerable<IOrderItemFilter> orderItemFilters)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _farmlyEntityDataContext = farmlyEntityDbContext ?? throw new ArgumentNullException(nameof(farmlyEntityDbContext));
            _orderItemFilters = orderItemFilters ?? throw new ArgumentNullException(nameof(orderItemFilters));
        }

        public async Task<OrderItemDto[]> HandleAsync(FindOrderItemsRequest request, CancellationToken cancellationToken = default)
        {
            var baseRequest = _farmlyEntityDataContext.OrderItems.AsNoTracking().AsQueryable();

            foreach (var filter in _orderItemFilters.Where(e => e.CanFilter(request)))
            {
                baseRequest = filter.Filter(request, baseRequest);
            }

            var response = await baseRequest
                .OrderByDescending(e => e.Id)
                .ProjectTo<OrderItemDto>(_mapper.ConfigurationProvider)
                .ToArrayAsync(cancellationToken);

            return response;
        }
    }
}