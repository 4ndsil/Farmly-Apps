using AutoMapper;
using FarmlyCore.Application.DTOs;
using FarmlyCore.Application.Requests.Categories;
using FarmlyCore.Infrastructure.FarmlyDbContext;
using FarmlyCore.Infrastructure.Queries;
using Microsoft.EntityFrameworkCore;

namespace FarmlyCore.Application.Queries.Categories
{
    public class GetCategoryQueryHandler : IQueryHandler<GetCategoryRequest, CategoryDto>
    {
        private readonly FarmlyEntityDbContext _farmlyEntityDataContext;
        private readonly IMapper _mapper;

        public GetCategoryQueryHandler(IMapper mapper, FarmlyEntityDbContext farmlyEntityDbContext)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _farmlyEntityDataContext = farmlyEntityDbContext ?? throw new ArgumentNullException(nameof(farmlyEntityDbContext));
        }

        public async Task<CategoryDto> HandleAsync(GetCategoryRequest request, CancellationToken cancellationToken = default)
        {
            var category = await _farmlyEntityDataContext.Categories
              .AsNoTracking()
              .FirstOrDefaultAsync(e => e.Id == request.CategoryId, cancellationToken);

            if (category == null)
            {
                return null;
            }

            return _mapper.Map<CategoryDto>(category);
        }
    }
}
