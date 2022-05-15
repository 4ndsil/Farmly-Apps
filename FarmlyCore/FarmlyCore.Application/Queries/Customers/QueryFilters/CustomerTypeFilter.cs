using FarmlyCore.Application.Requests.Customers;
using FarmlyCore.Infrastructure.Entities;

namespace FarmlyCore.Application.Queries.Customers.QueryFilters
{
    public class CustomerTypeFilter : ICustomerFilter
    {
        public bool CanFilter(FindCustomersRequest request) => request.CustomerType.HasValue;

        public IQueryable<Customer> Filter(FindCustomersRequest request, IQueryable<Customer> customers)
        {
            return customers.Where(e => e.CustomerType == (CustomerType)request.CustomerType);
        }
    }
}
