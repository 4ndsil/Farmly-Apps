using AutoMapper;
using FarmlyCore.Application.DTOs;
using FarmlyCore.Application.DTOs.Adverts;
using FarmlyCore.Application.DTOs.Customer;
using FarmlyCore.Application.DTOs.Order;
using FarmlyCore.Application.Requests.Orders;
using FarmlyCore.Infrastructure.FarmlyDbContext;
using FarmlyCore.Infrastructure.Queries;
using Microsoft.EntityFrameworkCore;

namespace FarmlyCore.Application.Queries.Orders
{
    public class GetOrderQueryHandler : IQueryHandler<GetOrderRequest, OrderDto>
    {
        private readonly FarmlyEntityDbContext _farmlyEntityDataContext;
        private readonly IMapper _mapper;

        public GetOrderQueryHandler(IMapper mapper, FarmlyEntityDbContext farmlyEntityDbContext)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _farmlyEntityDataContext = farmlyEntityDbContext ?? throw new ArgumentNullException(nameof(farmlyEntityDbContext));
        }

        public async Task<OrderDto> HandleAsync(GetOrderRequest request, CancellationToken cancellationToken = default)
        {
            var data = await _farmlyEntityDataContext.Orders
                .Where(e => e.Id.Equals(request.OrderId))
                .Select(e => new
                {
                    e.Id,
                    e.OrderNumber,
                    e.PlacementDate,
                    e.DeliveryDate,
                    e.FkBuyerId,
                    Buyer = new
                    {
                        e.Buyer.Id,
                        e.Buyer.OrgNumber,
                        e.Buyer.Email,
                        e.Buyer.BankGiro,
                        e.Buyer.CompanyName,
                        e.Buyer.CustomerType,
                        CustomerAddresses = e.Buyer.CustomerAddresses.Select(d => new
                        {
                            d.Id,
                            d.Street,
                            d.City,
                            d.State,
                            d.Zip,
                            d.FkCustomerId
                        }),
                    },
                    DeliverpointId = e.DeliveryPoint.Id,
                    OrderItems = e.OrderItems.Select(f => new
                    {
                        f.Id,
                        f.ProductName,
                        f.Quantity,
                        f.Price,
                        f.PriceType,
                        f.FkOrderId,
                        Category = new
                        {
                            f.Category.Id,
                            f.Category.CategoryName
                        },
                        AdvertItem = new
                        {
                            f.AdvertItem.Id,
                            f.AdvertItem.Price,
                            f.AdvertItem.Quantity,
                            f.AdvertItem.FkAdvertId,
                            f.AdvertItem.Weight,
                            f.AdvertItem.InsertDate
                        }
                    })
                })
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);

            if (data == null)
            {
                return null;
            }

            return new OrderDto
            {
                Id = data.Id,
                OrderNumber = data.OrderNumber,
                PlacementDate = data.PlacementDate,
                DeliveryDate = data.DeliveryDate,
                Buyer = new CustomerDto
                {
                    Id = data.Buyer.Id,
                    OrgNumber = data.Buyer.OrgNumber,
                    Email = data.Buyer.Email,
                    BankGiro = data.Buyer.BankGiro,
                    CompanyName = data.Buyer.CompanyName,
                    CustomerType = (CustomerTypeDto)data.Buyer.CustomerType,
                    CustomerAddresses = data.Buyer.CustomerAddresses.Select(d => new CustomerAddressDto
                    {
                        Id = d.Id,
                        Street = d.Street,
                        City = d.City,
                        State = d.State,
                        Zip = d.Zip,
                        FKCustomerId = d.FkCustomerId
                    })
                },
                DeliveryPointId = data.DeliverpointId,
                OrderItems = data.OrderItems.Select(d => new OrderItemDto
                {
                    Id = d.Id,
                    ProductName = d.ProductName,
                    Price = d.Price,
                    PriceType = (OrderPriceTypeDto)d.PriceType,
                    Quantity = d.Quantity,
                    OrderId = d.FkOrderId,
                    Category = new CategoryDto
                    {
                        CategoryName = d.Category.CategoryName,
                        Id = d.Category.Id
                    },
                    AdvertItem = new AdvertItemDto
                    {
                        Id = d.AdvertItem.Id,
                        Price = d.AdvertItem.Price,
                        Quantity = d.AdvertItem.Quantity,
                        AdvertId = d.AdvertItem.FkAdvertId,
                        Weight = d.AdvertItem.Weight,
                    }
                }).ToArray()
            };
        }
    }
}