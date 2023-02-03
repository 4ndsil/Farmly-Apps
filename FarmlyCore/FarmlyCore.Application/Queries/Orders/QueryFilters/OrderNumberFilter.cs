using FarmlyCore.Application.Requests.Orders;
using FarmlyCore.Infrastructure.Entities;

namespace FarmlyCore.Application.Queries.Orders.QueryFilters
{
    public class OrderNumberFilter : IOrderFilter
    {
        public bool CanFilter(FindOrdersRequest request) => !string.IsNullOrEmpty(request.OrderNumber);

        public IQueryable<Order> Filter(FindOrdersRequest request, IQueryable<Order> orders)
        {
            return orders.Where(e => e.OrderNumber == request.OrderNumber);
        }
    }
}