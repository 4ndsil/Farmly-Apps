using FarmlyCore.Application.Requests.Adverts;
using FarmlyCore.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmlyCore.Application.Queries.Adverts.QueryFilters
{
    public class ProductNameFilter : IAdvertFilter
    {
        public bool CanFilter(FindAdvertsRequest request) => !string.IsNullOrEmpty(request.ProductName);

        public IQueryable<Advert> Filter(FindAdvertsRequest request, IQueryable<Advert> adverts)
        {
            return adverts.Where(e => e.ProductName.Equals(request.ProductName) || e.ProductName.Contains(request.ProductName));
        }
    }
}
