using FarmlyCore.Application.Requests.AdvertItems;
using FarmlyCore.Infrastructure.Entities;

namespace FarmlyCore.Application.Queries.Adverts.QueryFilters
{
    public interface IAdvertItemFilter
    {
        bool CanFilter(FindAdvertItemsRequest request);

        IQueryable<AdvertItem> Filter(FindAdvertItemsRequest request, IQueryable<AdvertItem> advertItem);
    }
}
