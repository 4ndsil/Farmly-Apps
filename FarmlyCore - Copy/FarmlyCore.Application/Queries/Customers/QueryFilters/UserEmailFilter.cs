using FarmlyCore.Application.Requests.Customers;
using FarmlyCore.Infrastructure.Entities;

namespace FarmlyCore.Application.Queries.Customers.QueryFilters
{
    public class UserEmailFilter : IUserFilter
    {
        public bool CanFilter(FindUsersRequest request) => !string.IsNullOrEmpty(request.Email);

        public IQueryable<User> Filter(FindUsersRequest request, IQueryable<User> users)
        {
            return users.Where(e => e.Email.Equals(e.Email));
        }
    }
}