using FarmlyCore.Application.Requests.Customers;
using FarmlyCore.Infrastructure.Entities;

namespace FarmlyCore.Application.Queries.Customers.QueryFilters
{
    public class CustomerIdFilter : ICustomerFilter
    {
        public bool CanFilter(FindCustomersRequest request) => request.CustomerIds.Any();

        public IQueryable<Customer> Filter(FindCustomersRequest request, IQueryable<Customer> customers)
        {
            return customers.Where(e => request.CustomerIds.Contains(e.Id));
        }
    }
}
