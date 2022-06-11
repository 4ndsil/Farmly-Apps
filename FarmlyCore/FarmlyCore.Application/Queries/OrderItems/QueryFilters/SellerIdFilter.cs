using FarmlyCore.Application.Queries.Orders.QueryFilters;
using FarmlyCore.Application.Requests.OrderItems;
using FarmlyCore.Application.Requests.Orders;
using FarmlyCore.Infrastructure.Entities;

namespace FarmlyCore.Application.Queries.OrderItems.QueryFilters
{
    public class SellerIdFilter : IOrderItemFilter
    {
        public bool CanFilter(FindOrderItemsRequest request) => request.SellerId.HasValue;

        public IQueryable<OrderItem> Filter(FindOrderItemsRequest request, IQueryable<OrderItem> orders)
        {
            return orders.Where(e => e.FkSellerId == request.SellerId);
        }
    }
}