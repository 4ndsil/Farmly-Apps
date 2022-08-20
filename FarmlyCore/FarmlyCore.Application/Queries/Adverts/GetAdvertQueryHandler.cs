using AutoMapper;
using AutoMapper.QueryableExtensions;
using FarmlyCore.Application.DTOs.Adverts;
using FarmlyCore.Application.Requests.Adverts;
using FarmlyCore.Infrastructure.FarmlyDbContext;
using FarmlyCore.Infrastructure.Queries;
using Microsoft.EntityFrameworkCore;

namespace FarmlyCore.Application.Queries.Adverts
{
    public class GetAdvertQueryHandler : IQueryHandler<GetAdvertRequest, AdvertDto>
    {
        private readonly FarmlyEntityDbContext _farmlyEntityDataContext;
        private readonly IMapper _mapper;

        public GetAdvertQueryHandler(IMapper mapper, FarmlyEntityDbContext farmlyEntityDbContext)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _farmlyEntityDataContext = farmlyEntityDbContext ?? throw new ArgumentNullException(nameof(farmlyEntityDbContext));
        }

        public async Task<AdvertDto> HandleAsync(GetAdvertRequest request, CancellationToken cancellationToken = default)
        {
            var advertDto =  await _farmlyEntityDataContext.Adverts
                .Where(e => e.Id.Equals(request.AdvertId))
                .ProjectTo<AdvertDto>(_mapper.ConfigurationProvider)
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);

            if (advertDto == null)
            {
                return null;
            }

            return advertDto;
        }
    }
}