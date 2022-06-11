using FarmlyCore.Application.Requests.Users;
using FarmlyCore.Infrastructure.Entities;

namespace FarmlyCore.Application.Queries.Users.QueryFilters
{
    public interface IUserFilter
    {
        bool CanFilter(FindUsersRequest request);

        IQueryable<User> Filter(FindUsersRequest request, IQueryable<User> users);
    }
}