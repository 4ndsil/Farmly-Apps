using FarmlyCore.Application.Queries.Requests.Customer;
using FarmlyCore.Infrastructure.Entities;

namespace FarmlyCore.Application.Queries.Orders.QueryFilters
{
    public class CustomerIdFilter : IOrderFilter
    {
        public bool CanFilter(FindOrdersRequest request) => request.CustomerId.HasValue;

        public IQueryable<Order> Filter(FindOrdersRequest request, IQueryable<Order> orders)
        {
            return orders.Where(e => e.BuyerId == request.CustomerId);
        }
    }
}