using AutoMapper;
using FarmlyCore.Application.DTOs;
using FarmlyCore.Application.DTOs.Adverts;
using FarmlyCore.Application.DTOs.Customer;
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
            var data = await _farmlyEntityDataContext.Adverts
                .Where(e => e.Id.Equals(request.AdvertId))
                .Select(e => new
                {
                    e.Id,
                    e.ProductName,
                    e.Description,
                    e.Available,
                    e.FkSellerId,
                    e.PriceType,                    
                    Category = new
                    {
                        e.Category.Id,
                        e.Category.CategoryName
                    },
                    PickupPoint = new
                    {
                        e.PickupPoint.Id,
                        e.PickupPoint.State,
                        e.PickupPoint.City,
                        e.PickupPoint.Street,
                        e.PickupPoint.Zip,
                        e.PickupPoint.FkCustomerId
                    },
                    AdvertItems = e.AdvertItems.Select(f => new
                    {
                        f.Id,
                        f.Weight,
                        f.Quantity,
                        f.Price,
                        f.FkAdvertId
                    })
                })
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);

            if (data == null)
            {
                return null;
            }

            return new AdvertDto
            {
                Id = data.Id,
                ProductName = data.ProductName,
                Description = data.Description,
                Available = data.Available,
                SellerId = data.FkSellerId,                
                PriceType = (AdvertPriceTypeDto)data.PriceType,
                Category = new CategoryDto
                {
                    Id = data.Id,
                    CategoryName = data.Category.CategoryName
                },
                PickupPoint = new CustomerAddressDto
                {
                    Id = data.PickupPoint.Id,
                    Street = data.PickupPoint.Street,
                    Zip = data.PickupPoint.Zip,
                    City = data.PickupPoint.City,
                    State = data.PickupPoint.State,
                    FKCustomerId = data.PickupPoint.FkCustomerId
                },
                AdvertItems = data.AdvertItems.Select(d => new AdvertItemDto
                {
                    Id = d.Id,
                    Price = d.Price,
                    Quantity = d.Quantity,
                    Weight = d.Weight,
                    AdvertId = d.FkAdvertId
                }).ToArray()
            };
        }
    }
}