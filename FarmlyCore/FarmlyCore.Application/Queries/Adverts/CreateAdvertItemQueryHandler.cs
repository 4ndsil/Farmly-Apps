using AutoMapper;
using FarmlyCore.Application.DTOs.Adverts;
using FarmlyCore.Application.Requests.Adverts;
using FarmlyCore.Infrastructure.Entities;
using FarmlyCore.Infrastructure.FarmlyDbContext;
using FarmlyCore.Infrastructure.Queries;
using Microsoft.EntityFrameworkCore;

namespace FarmlyCore.Application.Queries.Adverts
{
    public class CreateAdvertItemQueryHandler : IQueryHandler<CreateAdvertItemRequest, AdvertItemDto>
    {
        private readonly FarmlyEntityDbContext _farmlyEntityDataContext;
        private readonly IMapper _mapper;

        public CreateAdvertItemQueryHandler(IMapper mapper, FarmlyEntityDbContext farmlyEntityDbContext)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _farmlyEntityDataContext = farmlyEntityDbContext ?? throw new ArgumentNullException(nameof(farmlyEntityDbContext));
        }

        public async Task<AdvertItemDto> HandleAsync(CreateAdvertItemRequest request, CancellationToken cancellationToken = default)
        {
            var advert = await _farmlyEntityDataContext.Adverts.Where(e => e.Id == request.AdvertId).FirstOrDefaultAsync();

            if (advert == null)
            {
                return null;
            }

            var advertItem = new AdvertItem(advert)
            {
                Amount = request.AdvertItem.Amount,
                Quantity = request.AdvertItem.Quantity,
                Price = request.AdvertItem.Price,
                FkAdvertId = advert.Id,
            };

            await _farmlyEntityDataContext.AdvertItems.AddAsync(advertItem);

            await _farmlyEntityDataContext.SaveChangesAsync();

            var advertItemDto = _mapper.Map<AdvertItemDto>(advertItem);

            return advertItemDto;
        }
    }
}
