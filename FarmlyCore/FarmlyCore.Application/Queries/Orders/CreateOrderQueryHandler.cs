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
        private CreateOrderResponse(OrderDto order) { Order = order; }

        private CreateOrderResponse(CreateOrderProblemDetail detail, int[] advertItemsIds) { Detail = detail; AdvertItemIds = advertItemsIds; }

        public static CreateOrderResponse WithSuccess(OrderDto order) => new CreateOrderResponse(order);

        public static CreateOrderResponse WithProblem(CreateOrderProblemDetail detail, int[] advertItemsIds = null) => new CreateOrderResponse(detail, advertItemsIds);

        public OrderDto? Order { get; set; }

        public CreateOrderProblemDetail Detail { get; set; }

        public int[]? AdvertItemIds { get; set; } = Array.Empty<int>();
    }

    public enum CreateOrderProblemDetail
    {
        AddressNotFound,
        AdvertItemsNotFound,
        ConcurrencyConflict,
        ConcurrecyFailure
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
                return CreateOrderResponse.WithProblem(CreateOrderProblemDetail.AdvertItemsNotFound, Array.Empty<int>());
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
                PriceType = (OrderPriceType)e.PriceType,
                e.Quantity,
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
                PriceType = item.PriceType,
                Quantity = item.Quantity
            }).ToList();

            var order = new Order
            {
                EstimatedDeliveryDate = request.Order.EstimatedDeliveryDate,
                PlacementDate = request.Order.PlacementDate,
                DeliveryDate = request.Order.DeliveryDate,
                OrderNumber = OrderNumberGenerator(request.Order.BuyerId),
                FkBuyerId = request.Order.BuyerId,
                FkDeliveryPointId = request.Order.DeliveryPointId,                
                TotalPrice = orderItems.Select(e => e.Price).Sum(),
                TotalQuantity = orderItems.Select(e => e.Quantity).Sum(),
                OrderItems = orderItems
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
                                    return CreateOrderResponse.WithProblem(CreateOrderProblemDetail.ConcurrencyConflict, new int[] { currentAdvert.Id });
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
                            return CreateOrderResponse.WithProblem(CreateOrderProblemDetail.ConcurrecyFailure);
                        }
                    }

                    index++;
                }
            }

            if (!saved)
            {
                return CreateOrderResponse.WithProblem(CreateOrderProblemDetail.ConcurrencyConflict, new int[] { currentAdvert.Id });
            }

            _farmlyEntityDataContext.Add(order);

            _farmlyEntityDataContext.SaveChanges();

            var orderDto = _mapper.Map<OrderDto>(order);

            return CreateOrderResponse.WithSuccess(orderDto);
        }

        private string OrderNumberGenerator(int customerId)
        {
            var orderNumber = $"C{customerId}";

            var first = string.Empty;

            var second = string.Empty;

            var rnd1 = new Random();

            var rnd2 = new Random();

            first = rnd1.Next().ToString();

            second = rnd2.Next().ToString();

            int i = 0;

            while (i < 8)
            {
                orderNumber = $"{orderNumber}-{first.Substring(i, 4)}-{second.Substring(i, 4)}";

                i += 4;
            }

            return orderNumber;
        }
    }
}