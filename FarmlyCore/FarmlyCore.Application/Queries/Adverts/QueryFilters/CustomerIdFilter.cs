using FarmlyCore.Application.Requests.Adverts;
using FarmlyCore.Infrastructure.Entities;

namespace FarmlyCore.Application.Queries.Adverts.QueryFilters
{
    public class CustomerIdFilter : IAdvertFilter
    {
        public bool CanFilter(FindAdvertsRequest request) => request.CustomerId.HasValue;

        public IQueryable<Advert> Filter(FindAdvertsRequest request, IQueryable<Advert> adverts)
        {
            return adverts.Where(e => e.FkSellerId == request.CustomerId);
        }
    }
}