using AutoMapper;
using AutoMapper.QueryableExtensions;
using FarmlyCore.Application.DTOs.Customer;
using FarmlyCore.Application.Queries.Customers.QueryFilters;
using FarmlyCore.Infrastructure.FarmlyDbContext;
using FarmlyCore.Infrastructure.Queries;
using Microsoft.EntityFrameworkCore;
using FarmlyCore.Application.Requests.Customers;

namespace FarmlyCore.Application.Queries.Customers
{
    public class FindCustomerUsersQueryHandler : IQueryHandler<FindCustomerUsersRequest, UserDto[]>
    {
        private readonly FarmlyEntityDbContext _farmlyEntityDataContext;
        private readonly IMapper _mapper;
        private readonly IEnumerable<ICustomerFilter> _customerFilters;

        public FindCustomerUsersQueryHandler(IMapper mapper, FarmlyEntityDbContext farmlyEntityDbContext, IEnumerable<ICustomerFilter> customerFilters)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _farmlyEntityDataContext = farmlyEntityDbContext ?? throw new ArgumentNullException(nameof(farmlyEntityDbContext));
            _customerFilters = customerFilters ?? throw new ArgumentNullException(nameof(customerFilters));
        }

        public async Task<UserDto[]> HandleAsync(FindCustomerUsersRequest request, CancellationToken cancellationToken = default)
        {
            var baseRequest = _farmlyEntityDataContext.Users.AsNoTracking().AsQueryable();

            foreach (var filter in _customerFilters.Where(e => e.CanFilter(request)))
            {
                baseRequest = filter.Filter(request, baseRequest);
            }

            var response = await baseRequest
                .OrderByDescending(e => e.Id)
                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .ToArrayAsync(cancellationToken);

            return response;
        }
    }
}