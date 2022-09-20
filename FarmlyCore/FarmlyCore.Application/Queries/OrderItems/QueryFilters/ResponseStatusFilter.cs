using FarmlyCore.Application.Requests.OrderItems;
using FarmlyCore.Infrastructure.Entities;

namespace FarmlyCore.Application.Queries.OrderItems.QueryFilters
{
    public class ResponseStatusFilter : IOrderItemFilter
    {
        public bool CanFilter(FindOrderItemsRequest request) => request.ResponseStatus.HasValue;

        public IQueryable<OrderItem> Filter(FindOrderItemsRequest request, IQueryable<OrderItem> orderItems)
        {
            return orderItems.Where(e => e.ResponseStatus == (ResponseStatus)request.ResponseStatus.Value);
        }
    }
}