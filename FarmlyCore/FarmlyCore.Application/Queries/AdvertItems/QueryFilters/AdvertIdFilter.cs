using FarmlyCore.Application.Queries.Adverts.QueryFilters;
using FarmlyCore.Application.Requests.AdvertItems;
using FarmlyCore.Infrastructure.Entities;

namespace FarmlyCore.Application.Queries.AdvertItems.QueryFilters
{
    public class AdvertIdFilter : IAdvertItemFilter
    {
        public bool CanFilter(FindAdvertItemsRequest request) => request.AdvertId.HasValue;

        public IQueryable<AdvertItem> Filter(FindAdvertItemsRequest request, IQueryable<AdvertItem> orders)
        {
            return orders.Where(e => e.FkAdvertId == request.AdvertId);
        }
    }
}
