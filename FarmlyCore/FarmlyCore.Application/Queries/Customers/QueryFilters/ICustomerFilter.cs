using FarmlyCore.Application.Requests.Customers;
using FarmlyCore.Infrastructure.Entities;

namespace FarmlyCore.Application.Queries.Customers.QueryFilters
{
    public interface ICustomerFilter
    {
        bool CanFilter(FindCustomerUsersRequest request);

        IQueryable<User> Filter(FindCustomerUsersRequest request, IQueryable<User> users);
    }
}
