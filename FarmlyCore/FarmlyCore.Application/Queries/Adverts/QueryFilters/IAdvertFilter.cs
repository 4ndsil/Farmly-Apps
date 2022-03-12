using FarmlyCore.Application.Requests.Adverts;
using FarmlyCore.Infrastructure.Entities;

namespace FarmlyCore.Application.Queries.Adverts.QueryFilters
{
    public interface IAdvertFilter
    {
        bool CanFilter(FindAdvertsRequest request);

        IQueryable<Advert> Filter(FindAdvertsRequest request, IQueryable<Advert> adverts);
    }
}
