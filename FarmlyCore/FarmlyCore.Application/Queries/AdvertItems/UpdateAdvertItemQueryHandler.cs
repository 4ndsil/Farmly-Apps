using AutoMapper;
using FarmlyCore.Application.DTOs.Adverts;
using FarmlyCore.Application.Requests.AdvertItems;
using FarmlyCore.Infrastructure.FarmlyDbContext;
using FarmlyCore.Infrastructure.Queries;
using Microsoft.EntityFrameworkCore;

namespace FarmlyCore.Application.Queries.Adverts
{
    public class UpdateAdvertItemQueryHandler : IQueryHandler<UpdateAdvertItemRequest, AdvertItemDto>
    {
        private readonly FarmlyEntityDbContext _farmlyEntityDataContext;
        private readonly IMapper _mapper;

        public UpdateAdvertItemQueryHandler(IMapper mapper, FarmlyEntityDbContext farmlyEntityDbContext)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _farmlyEntityDataContext = farmlyEntityDbContext ?? throw new ArgumentNullException(nameof(farmlyEntityDbContext));
        }

        public async Task<AdvertItemDto> HandleAsync(UpdateAdvertItemRequest request, CancellationToken cancellationToken = default)
        {
            var advertItem = await _farmlyEntityDataContext.AdvertItems
                .Where(e => e.Id == request.AdvertItemId)
                .Include(e => e.Advert)
                .FirstOrDefaultAsync();

            if (advertItem == null)
            {
                return null;
            }

            var advertItemDto = _mapper.Map<AdvertItemDto>(advertItem);

            request.AdvertItem.ApplyTo(advertItemDto);

            _mapper.Map(advertItemDto, advertItem);

            await _farmlyEntityDataContext.SaveChangesAsync(cancellationToken);

            return advertItemDto;
        }
    }
}