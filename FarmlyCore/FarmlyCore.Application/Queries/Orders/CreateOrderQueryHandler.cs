using AutoMapper;
using FarmlyCore.Application.DTOs.Order;
using FarmlyCore.Application.Requests.Orders;
using FarmlyCore.Infrastructure.Entities;
using FarmlyCore.Infrastructure.FarmlyDbContext;
using FarmlyCore.Infrastructure.Queries;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace FarmlyCore.Application.Queries.Orders
{
    public class CreateOrderResponse
    {
        private CreateOrderResponse(CreateOrderDetail detail, OrderSummaryDto order) { Order = order; Detail = detail; }

        private CreateOrderResponse(CreateOrderDetail detail, int[] advertItemsIds) { Detail = detail; AdvertItemIds = advertItemsIds; }

        public static CreateOrderResponse WithSuccess(CreateOrderDetail detail, OrderSummaryDto order) => new CreateOrderResponse(detail, order);

        public static CreateOrderResponse WithProblem(CreateOrderDetail detail, int[] advertItemsIds = null) => new CreateOrderResponse(detail, advertItemsIds);

        public OrderSummaryDto? Order { get; set; }

        public CreateOrderDetail? Detail { get; set; }

        public int[]? AdvertItemIds { get; set; } = Array.Empty<int>();
    }

    public enum CreateOrderDetail
    {
        AddressNotFound,
        AdvertItemsNotFound,
        ConcurrencyConflict,
        ConcurrecyFailure,
        BuyerNotFound,
        WithSuccess
    }

    public class CreateOrderQueryHandler : IQueryHandler<CreateOrderRequest, CreateOrderResponse>
    {
        private readonly FarmlyEntityDbContext _farmlyEntityDataContext;
        private readonly IMapper _mapper;

        public CreateOrderQueryHandler(IMapper mapper, FarmlyEntityDbContext farmlyEntityDbContext)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

            _farmlyEntityDataContext = farmlyEntityDbContext ?? throw new ArgumentNullException(nameof(farmlyEntityDbContext));
        }

        public async Task<CreateOrderResponse> HandleAsync(CreateOrderRequest request, CancellationToken cancellationToken = default)
        {
            var advertItemsIds = request.Order.CreateOrderItems.Select(e => e.AdvertItemId);

            var advertItems = _farmlyEntityDataContext.AdvertItems.Where(e => advertItemsIds.Contains(e.Id)).AsTracking();

            if (advertItems == null)
            {
                return CreateOrderResponse.WithProblem(CreateOrderDetail.BuyerNotFound, Array.Empty<int>());
            }

            var buyer = await _farmlyEntityDataContext.Customers.Where(e => e.Id == request.Order.BuyerId).AsTracking().FirstOrDefaultAsync();

            if (buyer == null)
            {
                return CreateOrderResponse.WithProblem(CreateOrderDetail.AdvertItemsNotFound, Array.Empty<int>());
            }

            var orderItems = request.Order.CreateOrderItems.Select(e => new
            {
                e.ProductName,
                e.Weight,
                e.Price,
                e.AdvertItemPrice,
                e.AdvertItemId,
                e.PickupPointId,
                e.CategoryId,
                e.SellerId,
                PriceType = (OrderPriceType)e.PriceType,
                e.Quantity,
                e.PlacementDate,
                e.PickupDate,
            }).Select(item => new OrderItem
            {
                ProductName = item.ProductName,
                Weight = item.Weight,
                Price = item.Price,
                ResponseStatus = ResponseStatus.Pending,
                AdvertItemPrice = item.AdvertItemPrice,
                FkAdvertItemId = item.AdvertItemId,
                FkPickupPointId = item.PickupPointId,
                FkCategoryId = item.CategoryId,
                FkSellerId = item.SellerId,
                PriceType = item.PriceType,
                Quantity = item.Quantity,
                PlacementDate = item.PlacementDate,
                PickupDate = item.PickupDate,
                
            }).ToList();

            var order = new Order
            {
                EstimatedDeliveryDate = request.Order.EstimatedDeliveryDate,
                PlacementDate = request.Order.PlacementDate,
                DeliveryDate = request.Order.DeliveryDate,
                OrderNumber = OrderNumberGenerator(request.Order.BuyerId),
                FkBuyerId = request.Order.BuyerId,
                FkDeliveryPointId = request.Order.DeliveryPointId,                
                TotalPrice = request.Order.TotalPrice,
                TotalWeight = request.Order.TotalWeight,
                OrderItems = orderItems,
                Buyer = buyer
            };

            var saved = false;

            int index = 0;

            var currentAdvert = new AdvertItem();

            while (!saved || index < 20)
            {
                try
                {
                    foreach (var orderItem in orderItems)
                    {
                        foreach (var advertItem in advertItems)
                        {
                            if (orderItem.FkAdvertItemId == advertItem.Id)
                            {
                                currentAdvert = advertItem;

                                if (orderItem.Quantity > advertItem.Quantity)
                                {
                                    return CreateOrderResponse.WithProblem(CreateOrderDetail.ConcurrencyConflict, new int[] { currentAdvert.Id });
                                }

                                advertItem.Quantity -= orderItem.Quantity;
                            }
                        }
                    }

                    await _farmlyEntityDataContext.SaveChangesAsync();

                    saved = true;

                    break;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    foreach (var entry in ex.Entries)
                    {
                        if (entry.Entity is AdvertItem)
                        {                            
                            var databaseValues = entry.GetDatabaseValues();

                            entry.CurrentValues.SetValues(databaseValues);

                            entry.OriginalValues.SetValues(databaseValues);
                        }
                        else
                        {
                            return CreateOrderResponse.WithProblem(CreateOrderDetail.ConcurrecyFailure);
                        }
                    }

                    index++;
                }
            }

            if (!saved)
            {
                return CreateOrderResponse.WithProblem(CreateOrderDetail.ConcurrencyConflict, new int[] { currentAdvert.Id });
            }

            _farmlyEntityDataContext.Add(order);

            _farmlyEntityDataContext.SaveChanges();

            var orderDto = _mapper.Map<OrderSummaryDto>(order);

            return CreateOrderResponse.WithSuccess(CreateOrderDetail.WithSuccess, orderDto);
        }

        private string OrderNumberGenerator(int customerId)
        {
            var rnd1 = new Random();

            var rnd2 = new Random();

            var first = rnd1.Next().ToString();

            var second = rnd2.Next().ToString();

            return $"{first.Substring(0, 4)}-{second.Substring(3, 4)}";
        }
    }
}