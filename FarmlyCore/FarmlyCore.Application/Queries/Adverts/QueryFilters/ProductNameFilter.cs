using FarmlyCore.Application.Requests.Adverts;
using FarmlyCore.Infrastructure.Entities;

namespace FarmlyCore.Application.Queries.Adverts.QueryFilters
{
    public class ProductNameFilter : IAdvertFilter
    {
        public bool CanFilter(FindAdvertsRequest request) => !string.IsNullOrEmpty(request.ProductName);

        public IQueryable<Advert> Filter(FindAdvertsRequest request, IQueryable<Advert> adverts)
        {
            return adverts.Where(e => e.ProductName.ToLower().Contains(request.ProductName.ToLower()));
        }
    }
}