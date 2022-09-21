using FarmlyCore.Application.Requests.Adverts;
using FarmlyCore.Infrastructure.Entities;

namespace FarmlyCore.Application.Queries.Adverts.QueryFilters
{
    public class PriceTypeFilter : IAdvertFilter
    {
        public bool CanFilter(FindAdvertsRequest request) => request.PriceType.HasValue;

        public IQueryable<Advert> Filter(FindAdvertsRequest request, IQueryable<Advert> adverts)
        {
            return adverts.Where(e => e.PriceType == (AdvertPriceType)request.PriceType);
        }
    }
}