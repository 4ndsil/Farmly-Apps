using AutoMapper;
using AutoMapper.QueryableExtensions;
using FarmlyCore.Application.DTOs.Adverts;
using FarmlyCore.Application.Paging;
using FarmlyCore.Application.Queries.Adverts.QueryFilters;
using FarmlyCore.Application.Requests.Adverts;
using FarmlyCore.Infrastructure.FarmlyDbContext;
using FarmlyCore.Infrastructure.Queries;
using Microsoft.EntityFrameworkCore;

namespace FarmlyCore.Application.Queries.Adverts
{
    public class FindAdvertsQueryHandler : IQueryHandler<FindAdvertsRequest, PagedResponse<IReadOnlyList<AdvertDto>>>
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

        public async Task<PagedResponse<IReadOnlyList<AdvertDto>>> HandleAsync(FindAdvertsRequest request, CancellationToken cancellationToken = default)
        {
            var baseRequest = _farmlyEntityDataContext.Adverts
                .Where(e => e.Available == true)
                .AsNoTracking()
                .AsQueryable();

            foreach (var filter in _advertFilters.Where(e => e.CanFilter(request)))
            {
                baseRequest = filter.Filter(request, baseRequest);
            }

            var totalRecords = baseRequest.Count();

            var totalPages = totalRecords / (double)request.PageSize;

            var requestResult = await baseRequest
                .OrderByDescending(e => e.Id)
                .ProjectTo<AdvertDto>(_mapper.ConfigurationProvider)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToArrayAsync(cancellationToken);

            var response = new PagedResponse<IReadOnlyList<AdvertDto>>(requestResult, request.PageNumber, request.PageSize)
            {
                TotalPages = (int)Math.Ceiling(totalPages),
                TotalRecords = totalRecords
            };

            return response;
        }
    }
}