using FarmlyCore.Application.Requests.Adverts;
using FarmlyCore.Infrastructure.Entities;

namespace FarmlyCore.Application.Queries.Adverts.QueryFilters
{
    public class CategoryIdFilter : IAdvertFilter
    {
        public bool CanFilter(FindAdvertsRequest request) => request.CategoryId.HasValue;

        public IQueryable<Advert> Filter(FindAdvertsRequest request, IQueryable<Advert> adverts)
        {
            return adverts.Where(e => e.FkCategoryId == request.CategoryId);
        }
    }
}
