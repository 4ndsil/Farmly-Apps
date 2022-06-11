using FarmlyCore.Application.Requests.OrderItems;
using FarmlyCore.Infrastructure.Entities;

namespace FarmlyCore.Application.Queries.OrderItems.QueryFilters
{
    public interface IOrderItemFilter
    {
        bool CanFilter(FindOrderItemsRequest request);

        IQueryable<OrderItem> Filter(FindOrderItemsRequest request, IQueryable<OrderItem> Orders);
    }
}
