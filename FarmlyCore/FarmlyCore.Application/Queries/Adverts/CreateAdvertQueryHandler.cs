using AutoMapper;
using FarmlyCore.Application.DTOs.Adverts;
using FarmlyCore.Application.Requests.Adverts;
using FarmlyCore.Infrastructure.Entites;
using FarmlyCore.Infrastructure.Entities;
using FarmlyCore.Infrastructure.FarmlyDbContext;
using FarmlyCore.Infrastructure.Queries;
using Microsoft.EntityFrameworkCore;

namespace FarmlyCore.Application.Queries.Adverts
{
    public class CreateAdvertQueryHandler : IQueryHandler<CreateAdvertRequest, AdvertDto>
    {
        private readonly FarmlyEntityDbContext _farmlyEntityDataContext;
        private readonly IMapper _mapper;

        public CreateAdvertQueryHandler(IMapper mapper, FarmlyEntityDbContext farmlyEntityDbContext)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _farmlyEntityDataContext = farmlyEntityDbContext ?? throw new ArgumentNullException(nameof(farmlyEntityDbContext));
        }

        public async Task<AdvertDto> HandleAsync(CreateAdvertRequest request, CancellationToken cancellationToken = default)
        {
            List<AdvertItem> advertItems = null;

            if (request.Advert.AdvertItems != null)
            {
                advertItems.AddRange(_mapper.Map<AdvertItem[]>(request.Advert.AdvertItems));
            }

            var category = await _farmlyEntityDataContext.Categories.Where(e => e.Id == request.Advert.Category.Id).FirstOrDefaultAsync();

            if (category == null)
            {
                return null;
            }

            var seller = await _farmlyEntityDataContext.Customers.Where(e => e.Id == request.Advert.Seller.Id).FirstOrDefaultAsync();

            if (seller == null)
            {
                return null;
            }

            var pickupPoint = await _farmlyEntityDataContext.CustomerAddresses.Where(e => e.Id == request.Advert.PickupPoint.Id).FirstOrDefaultAsync();

            if (pickupPoint == null)
            {
                return null;
            }

            var advert = new Advert
            {
                Category = category,
                Description = request.Advert.Description,                
                PriceType = (AdvertPriceType)request.Advert.PriceType,
                ProductName = request.Advert.ProductName,
                TotalQuantity = request.Advert.TotalQuantity,
                Seller = seller,
                FkSellerId = seller.Id,
                FkCategoryId = category.Id,
                PickupPoint = pickupPoint,
                FkPickupPointId = pickupPoint.Id,
                AdvertItems = advertItems
            };

            await _farmlyEntityDataContext.AddAsync(advert);

            await _farmlyEntityDataContext.SaveChangesAsync();

            var advertDto = _mapper.Map<AdvertDto>(advert);

            return advertDto;
        }
    }
}