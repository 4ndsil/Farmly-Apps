using FarmlyCore.Application.Requests.Orders;
using FarmlyCore.Infrastructure.Entities;

namespace FarmlyCore.Application.Queries.Orders.QueryFilters
{
    public class OrderEstimatedDeliveryDateFilter : IOrderFilter
    {
        public bool CanFilter(FindOrdersRequest request) => request.EstimatedDeliveryDate.HasValue;

        public IQueryable<Order> Filter(FindOrdersRequest request, IQueryable<Order> orders)
        {
            return orders.Where(e => e.EstimatedDeliveryDate >= request.EstimatedDeliveryDate);
        }
    }
}
