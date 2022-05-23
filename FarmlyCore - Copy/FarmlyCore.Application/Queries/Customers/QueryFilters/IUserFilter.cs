using FarmlyCore.Application.Requests.Customers;
using FarmlyCore.Infrastructure.Entities;

namespace FarmlyCore.Application.Queries.Customers.QueryFilters
{
    public interface IUserFilter 
    {
        bool CanFilter(FindUsersRequest request);

        IQueryable<User> Filter(FindUsersRequest request, IQueryable<User> users);
    }
}