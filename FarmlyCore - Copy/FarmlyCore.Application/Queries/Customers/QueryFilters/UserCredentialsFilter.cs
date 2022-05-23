using FarmlyCore.Application.Requests.Customers;
using FarmlyCore.Infrastructure.Entities;

namespace FarmlyCore.Application.Queries.Customers.QueryFilters
{
    public class UserCredentialsFilter : IUserFilter
    {
        public bool CanFilter(FindUsersRequest request) => string.IsNullOrEmpty(request.Credentials);

        public IQueryable<User> Filter(FindUsersRequest request, IQueryable<User> users)
        {
            return users.Where(e => request.Credentials.Equals(e.Password));
        }
    }
}