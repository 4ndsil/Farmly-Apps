using FarmlyCore.Application.Requests.Customers;
using FarmlyCore.Infrastructure.Entities;

namespace FarmlyCore.Application.Queries.Customers.QueryFilters
{
    public interface ICustomerFilter
    {
        bool CanFilter(FindCustomersRequest request);

        IQueryable<Customer> Filter(FindCustomersRequest request, IQueryable<Customer> users);
    }
}