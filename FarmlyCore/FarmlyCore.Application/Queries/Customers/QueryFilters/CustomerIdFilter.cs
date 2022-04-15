using FarmlyCore.Application.Requests.Customers;
using FarmlyCore.Infrastructure.Entities;

namespace FarmlyCore.Application.Queries.Customers.QueryFilters
{
    public class CustomerIdFilter : ICustomerFilter
    {
        public bool CanFilter(FindCustomerUsersRequest request) => request.CustomerId.HasValue;

        public IQueryable<User> Filter(FindCustomerUsersRequest request, IQueryable<User> users)
        {
            return users.Where(e => e.FkCustomerId == request.CustomerId);
        }
    }
}
