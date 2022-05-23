using FarmlyCore.Application.Requests.Customers;
using FarmlyCore.Infrastructure.Entities;

namespace FarmlyCore.Application.Queries.Customers.QueryFilters
{
    public class CustomerNameFilter : ICustomerFilter
    {
        public bool CanFilter(FindCustomersRequest request) => !string.IsNullOrEmpty(request.CompanyName);

        public IQueryable<Customer> Filter(FindCustomersRequest request, IQueryable<Customer> customers)
        {
            return customers.Where(e => e.CompanyName.ToLower().Contains(request.CompanyName.ToLower()));
        }
    }
}