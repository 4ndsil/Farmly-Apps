using AutoMapper;
using AutoMapper.QueryableExtensions;
using FarmlyCore.Application.DTOs.Customer;
using FarmlyCore.Application.Queries.Requests.Customer;
using FarmlyCore.Application.Queries.Customers.QueryFilters;
using FarmlyCore.Infrastructure.FarmlyDbContext;
using FarmlyCore.Infrastructure.Queries;
using Microsoft.EntityFrameworkCore;

namespace FarmlyCore.Application.Queries.Orders
{
    public class FindAdvertsQueryHandler : IQueryHandler<FindOrdersRequest, CustomerDto[]>
    {
        private readonly FarmlyEntityDbContext _farmlyEntityDataContext;
        private readonly IMapper _mapper;
        private readonly IReadOnlyList<ICustomerFilter> _customerFilters;

        public FindAdvertsQueryHandler(IMapper mapper, FarmlyEntityDbContext farmlyEntityDbContext, IReadOnlyList<ICustomerFilter> customerFilters)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _farmlyEntityDataContext = farmlyEntityDbContext ?? throw new ArgumentNullException(nameof(farmlyEntityDbContext));
            _customerFilters = customerFilters ?? throw new ArgumentNullException(nameof(customerFilters));
        }

        public async Task<CustomerDto[]> HandleAsync(FindOrdersRequest request, CancellationToken cancellationToken = default)
        {
            var baseRequest = _farmlyEntityDataContext.Customers.AsNoTracking().AsQueryable();

            foreach (var filter in _customerFilters.Where(e => e.CanFilter(request)))
            {
                baseRequest = filter.Filter(request, baseRequest);
            }

            var response = await baseRequest
                .OrderByDescending(e => e.Id)
                .ProjectTo<CustomerDto>(_mapper.ConfigurationProvider)
                .ToArrayAsync(cancellationToken);

            return response;
        }
    }
}
