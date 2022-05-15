using AutoMapper;
using AutoMapper.QueryableExtensions;
using FarmlyCore.Application.DTOs;
using FarmlyCore.Infrastructure.FarmlyDbContext;
using FarmlyCore.Infrastructure.Queries;
using Microsoft.EntityFrameworkCore;

namespace FarmlyCore.Application.Queries.Categories
{
    namespace FarmlyCore.Application.Queries.Categories
    {
        public class GetCategoriesQueryHandler : IQueryHandler<CategoryDto[]>
        {
            private readonly FarmlyEntityDbContext _farmlyEntityDataContext;
            private readonly IMapper _mapper;

            public GetCategoriesQueryHandler(IMapper mapper, FarmlyEntityDbContext farmlyEntityDbContext)
            {
                _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
                _farmlyEntityDataContext = farmlyEntityDbContext ?? throw new ArgumentNullException(nameof(farmlyEntityDbContext));            
            }

            public async Task<CategoryDto[]> HandleAsync(CancellationToken cancellationToken = default)
            {
                var baseRequest = _farmlyEntityDataContext.Categories.AsNoTracking().AsQueryable();

                if (baseRequest == null)
                {
                    return null;
                }

                return await baseRequest
                  .OrderByDescending(e => e.Id)
                  .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
                  .ToArrayAsync(cancellationToken);
            }
        }
    }
}
