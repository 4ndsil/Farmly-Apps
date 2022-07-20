using AutoMapper;
using FarmlyCore.Application.DTOs.Adverts;
using FarmlyCore.Application.Requests.Adverts;
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
            var category = await _farmlyEntityDataContext.Categories.Where(e => e.Id == request.Advert.CategoryId).FirstOrDefaultAsync();

            if (category == null)
            {
                return null;
            }

            var seller = await _farmlyEntityDataContext.Customers.Where(e => e.Id == request.Advert.SellerId).FirstOrDefaultAsync();

            if (seller == null)
            {
                return null;
            }

            var pickupPoint = await _farmlyEntityDataContext.CustomerAddresses.Where(e => e.Id == request.Advert.PickupPointId).FirstOrDefaultAsync();

            if (pickupPoint == null)
            {
                return null;
            }

            var advert = new Advert
            {
                Category = category,
                Description = request.Advert.Description,
                Available = true,
                PriceType = (AdvertPriceType)request.Advert.PriceType,
                ProductName = request.Advert.ProductName,                
                InsertDate = DateTime.Now,
                Price = request.Advert.Price,
                Seller = seller,
                ImageUrl = request.Advert.ImageUrl,
                IsBulk = request.Advert.IsBulk,
                FkSellerId = seller.Id,
                FkCategoryId = category.Id,
                PickupPoint = pickupPoint,
                FkPickupPointId = pickupPoint.Id,                
            };

            var advertItems = new List<AdvertItem>();

            if (request.Advert.AdvertItems != null)
            {
                advertItems.AddRange(request.Advert.AdvertItems.Select(e => new AdvertItem()
                {
                    Price = e.Price,
                    Quantity = e.Quantity,
                    Weight = e.Weight
                }));
            }

            advert.AdvertItems = advertItems;

            await _farmlyEntityDataContext.Adverts.AddAsync(advert);

            await _farmlyEntityDataContext.SaveChangesAsync();

            var advertDto = _mapper.Map<AdvertDto>(advert);

            return advertDto;

        }
    }
}