using AutoMapper;
using AutoMapper.QueryableExtensions;
using FarmlyCore.Application.DTOs.Customer;
using FarmlyCore.Application.Queries.Users.QueryFilters;
using FarmlyCore.Application.Requests.Users;
using FarmlyCore.Infrastructure.FarmlyDbContext;
using FarmlyCore.Infrastructure.Queries;
using Microsoft.EntityFrameworkCore;

namespace FarmlyCore.Application.Queries.Users
{
    public class FindUsersQueryHandler : IQueryHandler<FindUsersRequest, UserDto[]>
    {
        private readonly FarmlyEntityDbContext _farmlyEntityDataContext;
        private readonly IMapper _mapper;
        private readonly IEnumerable<IUserFilter> _userFilters;

        public FindUsersQueryHandler(IMapper mapper, FarmlyEntityDbContext farmlyEntityDbContext, IEnumerable<IUserFilter> customerFilters)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _farmlyEntityDataContext = farmlyEntityDbContext ?? throw new ArgumentNullException(nameof(farmlyEntityDbContext));
            _userFilters = customerFilters ?? throw new ArgumentNullException(nameof(customerFilters));
        }

        public async Task<UserDto[]> HandleAsync(FindUsersRequest request, CancellationToken cancellationToken = default)
        {
            var baseRequest = _farmlyEntityDataContext.Users.AsNoTracking().AsQueryable();

            foreach (var filter in _userFilters.Where(e => e.CanFilter(request)))
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