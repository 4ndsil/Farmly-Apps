using AutoMapper;
using AutoMapper.QueryableExtensions;
using FarmlyCore.Application.DTOs.Adverts;
using FarmlyCore.Application.Queries.Adverts.QueryFilters;
using FarmlyCore.Application.Requests.AdvertItems;
using FarmlyCore.Infrastructure.FarmlyDbContext;
using FarmlyCore.Infrastructure.Queries;
using Microsoft.EntityFrameworkCore;

namespace FarmlyCore.Application.Queries.Adverts
{
    public class FindAdvertItemsQueryHandler : IQueryHandler<FindAdvertItemsRequest, AdvertItemDto[]>
    {
        private readonly FarmlyEntityDbContext _farmlyEntityDataContext;
        private readonly IMapper _mapper;
        private readonly IEnumerable<IAdvertItemFilter> _advertItemFilters;

        public FindAdvertItemsQueryHandler(IMapper mapper, FarmlyEntityDbContext farmlyEntityDbContext, IEnumerable<IAdvertItemFilter> advertItemFilters)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _farmlyEntityDataContext = farmlyEntityDbContext ?? throw new ArgumentNullException(nameof(farmlyEntityDbContext));
            _advertItemFilters = advertItemFilters ?? throw new ArgumentNullException(nameof(advertItemFilters));
        }

        public async Task<AdvertItemDto[]> HandleAsync(FindAdvertItemsRequest request, CancellationToken cancellationToken = default)
        {
            var baseRequest = _farmlyEntityDataContext.AdvertItems     
                .AsNoTracking()
                .AsQueryable();

            foreach (var filter in _advertItemFilters.Where(e => e.CanFilter(request)))
            {
                baseRequest = filter.Filter(request, baseRequest);
            }

            var response = await baseRequest
             .OrderByDescending(e => e.Id)
             .ProjectTo<AdvertItemDto>(_mapper.ConfigurationProvider)
             .ToArrayAsync(cancellationToken);

            return response;
        }
    }
}