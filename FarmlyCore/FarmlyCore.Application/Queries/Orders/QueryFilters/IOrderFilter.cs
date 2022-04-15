using FarmlyCore.Application.Requests.Orders;
using FarmlyCore.Infrastructure.Entities;

namespace FarmlyCore.Application.Queries.Orders.QueryFilters
{
    public interface IOrderFilter
    {
        bool CanFilter(FindOrdersRequest request);

        IQueryable<Order> Filter(FindOrdersRequest request, IQueryable<Order> Orders);
    }

}
