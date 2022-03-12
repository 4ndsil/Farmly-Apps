using AutoMapper;
using FarmlyCore.Application.DTOs;
using FarmlyCore.Application.Queries.Requests.Adverts;
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
                CategoryId = request.Advert.CategoryId,
                Description = request.Advert.Description,
                Price = request.Advert.Price,
                PriceType = (PriceType)request.Advert.PriceType,
                ProductName = request.Advert.ProductName,
                Quantity = request.Advert.Quantity,
                Seller = seller,
                PickupPoint = pickupPoint,
                PickupPointId = request.Advert.PickupPointId,
            };

            await _farmlyEntityDataContext.AddAsync(advert);

            await _farmlyEntityDataContext.SaveChangesAsync();

            var advertDto = _mapper.Map<AdvertDto>(advert);

            return advertDto;
        }
    }
}