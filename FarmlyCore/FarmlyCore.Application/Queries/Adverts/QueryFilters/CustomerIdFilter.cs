using FarmlyCore.Application.Requests.Adverts;
using FarmlyCore.Infrastructure.Entities;

namespace FarmlyCore.Application.Queries.Adverts.QueryFilters
{
    public class CustomerIdFilter : IAdvertFilter
    {
        public bool CanFilter(FindAdvertsRequest request) => request.AdvertIds.Any();

        public IQueryable<Advert> Filter(FindAdvertsRequest request, IQueryable<Advert> customers)
        {
            return customers.Where(e => request.AdvertIds.Contains(e.Id));
        }
    }
}