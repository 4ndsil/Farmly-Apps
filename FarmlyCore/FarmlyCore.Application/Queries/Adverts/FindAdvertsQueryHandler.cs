using AutoMapper;
using AutoMapper.QueryableExtensions;
using FarmlyCore.Application.DTOs.Adverts;
using FarmlyCore.Application.Queries.Adverts.QueryFilters;
using FarmlyCore.Application.Requests.Adverts;
using FarmlyCore.Infrastructure.FarmlyDbContext;
using FarmlyCore.Infrastructure.Queries;
using Microsoft.EntityFrameworkCore;

namespace FarmlyCore.Application.Queries.Adverts
{
    public class FindAdvertsQueryHandler : IQueryHandler<FindAdvertsRequest, AdvertDto[]>
    {
        private readonly FarmlyEntityDbContext _farmlyEntityDataContext;
        private readonly IMapper _mapper;
        private readonly IEnumerable<IAdvertFilter> _advertFilters;

        public FindAdvertsQueryHandler(IMapper mapper, FarmlyEntityDbContext farmlyEntityDbContext, IEnumerable<IAdvertFilter> advertFilters)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _farmlyEntityDataContext = farmlyEntityDbContext ?? throw new ArgumentNullException(nameof(farmlyEntityDbContext));
            _advertFilters = advertFilters ?? throw new ArgumentNullException(nameof(advertFilters));
        }

        public async Task<AdvertDto[]> HandleAsync(FindAdvertsRequest request, CancellationToken cancellationToken = default)
        {
            var baseRequest = _farmlyEntityDataContext.Adverts.AsNoTracking().AsQueryable();

            foreach (var filter in _advertFilters.Where(e => e.CanFilter(request)))
            {
                baseRequest = filter.Filter(request, baseRequest);
            }

            var response = await baseRequest
                .OrderByDescending(e => e.Id)
                .ProjectTo<AdvertDto>(_mapper.ConfigurationProvider)
                .ToArrayAsync(cancellationToken);

            return response;
        }
    }
}
