using AutoMapper;
using AutoMapper.QueryableExtensions;
using FarmlyCore.Application.DTOs;
using FarmlyCore.Application.DTOs.Adverts;
using FarmlyCore.Infrastructure.FarmlyDbContext;
using FarmlyCore.Infrastructure.Queries;
using Microsoft.EntityFrameworkCore;

namespace FarmlyCore.Application.Queries.Categories
{
    namespace FarmlyCore.Application.Queries.Categories
    {
        public class GetAllAdvertsQueryHandler : IQueryHandler<AdvertDto[]>
        {
            private readonly FarmlyEntityDbContext _farmlyEntityDataContext;
            private readonly IMapper _mapper;

            public GetAllAdvertsQueryHandler(IMapper mapper, FarmlyEntityDbContext farmlyEntityDbContext)
            {
                _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
                _farmlyEntityDataContext = farmlyEntityDbContext ?? throw new ArgumentNullException(nameof(farmlyEntityDbContext));            
            }

            public async Task<AdvertDto[]> HandleAsync(CancellationToken cancellationToken = default)
            {
                var baseRequest = _farmlyEntityDataContext.Adverts
                    .Where(e => e.Available == true)
                    .AsNoTracking()
                    .AsQueryable();

                if (baseRequest == null)
                {
                    return null;
                }

                return await baseRequest
                  .OrderByDescending(e => e)
                  .ProjectTo<AdvertDto>(_mapper.ConfigurationProvider)
                  .ToArrayAsync(cancellationToken);
            }
        }
    }
}
