using AutoMapper;
using FarmlyCore.Application.DTOs;
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
            var customer = await _farmlyEntityDataContext.Customers
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == request.AdvertId, cancellationToken);

            if (customer == null)
            {
                return null;
            }

            return _mapper.Map<AdvertDto>(customer);
        }
    }
}