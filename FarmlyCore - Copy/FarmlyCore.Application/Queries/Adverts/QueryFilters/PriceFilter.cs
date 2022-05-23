using FarmlyCore.Application.Requests.Adverts;
using FarmlyCore.Infrastructure.Entities;

namespace FarmlyCore.Application.Queries.Adverts.QueryFilters
{
    public class PriceFilter : IAdvertFilter
    {
        public bool CanFilter(FindAdvertsRequest request) => request.CategoryId.HasValue;

        public IQueryable<Advert> Filter(FindAdvertsRequest request, IQueryable<Advert> adverts)
        {
            return adverts.Where(e => e.Price >= request.Price);
        }
    }
}