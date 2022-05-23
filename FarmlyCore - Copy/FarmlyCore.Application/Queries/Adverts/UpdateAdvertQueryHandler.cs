using AutoMapper;
using FarmlyCore.Application.DTOs.Adverts;
using FarmlyCore.Application.Requests.Adverts;
using FarmlyCore.Infrastructure.FarmlyDbContext;
using FarmlyCore.Infrastructure.Queries;
using Microsoft.EntityFrameworkCore;

namespace FarmlyCore.Application.Queries.Adverts
{
    public class UpdateAdvertQueryHandler : IQueryHandler<UpdateAdvertRequest, AdvertDto>
    {
        private readonly FarmlyEntityDbContext _farmlyEntityDataContext;
        private readonly IMapper _mapper;

        public UpdateAdvertQueryHandler(IMapper mapper, FarmlyEntityDbContext farmlyEntityDbContext)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _farmlyEntityDataContext = farmlyEntityDbContext ?? throw new ArgumentNullException(nameof(farmlyEntityDbContext));
        }

        public async Task<AdvertDto> HandleAsync(UpdateAdvertRequest request, CancellationToken cancellationToken = default)
        {
            var advert = await _farmlyEntityDataContext.Adverts
                .Include(e => e.PickupPoint)
                .Include(e => e.Seller)
                .Where(e => e.Id == request.AdvertId)
                .FirstOrDefaultAsync();

            if (advert == null)
            {
                return null;
            }

            var advertDto = _mapper.Map<AdvertDto>(advert);

            request.Advert.ApplyTo(advertDto);

            _mapper.Map(advertDto, advert);

            await _farmlyEntityDataContext.SaveChangesAsync(cancellationToken);

            return advertDto;
        }
    }
}