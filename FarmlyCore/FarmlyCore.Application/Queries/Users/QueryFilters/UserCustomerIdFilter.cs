﻿using FarmlyCore.Application.Requests.Users;
using FarmlyCore.Infrastructure.Entities;

namespace FarmlyCore.Application.Queries.Users.QueryFilters
{
    public class UserCustomerIdFilter : IUserFilter
    {
        public bool CanFilter(FindUsersRequest request) => request.CustomerId.HasValue;

        public IQueryable<User> Filter(FindUsersRequest request, IQueryable<User> users)
        {
            return users.Where(e => e.FkCustomerId == request.CustomerId);
        }
    }
}