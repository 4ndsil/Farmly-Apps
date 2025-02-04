﻿using FarmlyCore.Application.DTOs;
using FarmlyCore.Application.DTOs.Adverts;
using FarmlyCore.Application.DTOs.Customer;
using FarmlyCore.Application.DTOs.Order;
using FarmlyCore.Application.Paging;
using FarmlyCore.Application.Queries.Adverts;
using FarmlyCore.Application.Queries.Adverts.QueryFilters;
using FarmlyCore.Application.Queries.Categories;
using FarmlyCore.Application.Queries.Categories.FarmlyCore.Application.Queries.Categories;
using FarmlyCore.Application.Queries.Customers;
using FarmlyCore.Application.Queries.Customers.QueryFilters;
using FarmlyCore.Application.Queries.OrderItems;
using FarmlyCore.Application.Queries.OrderItems.QueryFilters;
using FarmlyCore.Application.Queries.Orders;
using FarmlyCore.Application.Queries.Orders.QueryFilters;
using FarmlyCore.Application.Queries.Users;
using FarmlyCore.Application.Queries.Users.QueryFilters;
using FarmlyCore.Application.Requests.AdvertItems;
using FarmlyCore.Application.Requests.Adverts;
using FarmlyCore.Application.Requests.Categories;
using FarmlyCore.Application.Requests.Customers;
using FarmlyCore.Application.Requests.OrderItems;
using FarmlyCore.Application.Requests.Orders;
using FarmlyCore.Application.Requests.Users;
using FarmlyCore.Infrastructure.Queries;

namespace FarmlyCore.Application.Extentions
{
    public static class ServiceCollectionExtentions
    {
        public static IServiceCollection RegisterApplicationQueryHandlers(this IServiceCollection services)
        {
            //customers
            services.AddTransient<IQueryHandler<GetCustomerRequest, CustomerDto>, GetCustomerQueryHandler>();
            services.AddTransient<IQueryHandler<CreateCustomerRequest, CustomerDto>, CreateCustomerQueryHandler>();
            services.AddTransient<IQueryHandler<CreateCustomerAddressRequest, CustomerAddressDto>, CreateCustomerAddressQueryHandler>();
            services.AddTransient<IQueryHandler<GetCustomerAddressesRequest, CustomerAddressDto[]>, GetCustomerAddressesQueryHandler>();
            services.AddTransient<IQueryHandler<UpdateCustomerRequest, CustomerDto>, UpdateCustomerQueryHandler>();
            services.AddTransient<IQueryHandler<UpdateCustomerAddressRequest, CustomerAddressDto>, UpdateCustomerAddressQueryHandler>();
            services.AddTransient<IQueryHandler<FindCustomersRequest, CustomerDto[]>, FindCustomersQueryHandler>();

            //users
            services.AddTransient<IQueryHandler<FindUsersRequest, UserDto[]>, FindUsersQueryHandler>();
            services.AddTransient<IQueryHandler<GetUserRequest, UserDto>, GetUserQueryHandler>();
            services.AddTransient<IQueryHandler<GetUserByEmailRequest, UserDto>, GetUserByEmailQueryHandler>();
            services.AddTransient<IQueryHandler<UpdateUserRequest, UserDto>, UpdateUserQueryHandler>();
            services.AddTransient<IQueryHandler<UserAuthenticationRequest, UserAuthenticationResponse>, UserAuthenticationQueryHandler>();

            //adverts
            services.AddTransient<IQueryHandler<GetAdvertRequest, AdvertDto>, GetAdvertQueryHandler>();
            services.AddTransient<IQueryHandler<AdvertDto[]>, GetAllAdvertsQueryHandler>();
            services.AddTransient<IQueryHandler<FindAdvertsRequest, PagedResponse<IReadOnlyList<AdvertDto>>>, FindAdvertsQueryHandler >();
            services.AddTransient<IQueryHandler<CreateAdvertRequest, AdvertDto>, CreateAdvertQueryHandler>();
            services.AddTransient<IQueryHandler<UpdateAdvertRequest, AdvertDto>, UpdateAdvertQueryHandler>();
            services.AddTransient<IQueryHandler<CreateAdvertItemRequest, AdvertItemDto>, CreateAdvertItemQueryHandler>();
            services.AddTransient<IQueryHandler<DeleteAdvertItemRequest, DeleteAdvertItemResult>, DeleteAdvertItemQueryHandler>();

            //advertItems
            services.AddTransient<IQueryHandler<FindAdvertItemsRequest, AdvertItemDto[]>, FindAdvertItemsQueryHandler>();
            services.AddTransient<IQueryHandler<UpdateAdvertItemRequest, AdvertItemDto>, UpdateAdvertItemQueryHandler>();

            //order
            services.AddTransient<IQueryHandler<GetOrderRequest, OrderDto>, GetOrderQueryHandler>();
            services.AddTransient<IQueryHandler<CreateOrderRequest, CreateOrderResponse>, CreateOrderQueryHandler>();
            services.AddTransient<IQueryHandler<FindOrdersRequest, OrderDto[]>, FindOrdersQueryHandler>();            

            //orderItems
            services.AddTransient<IQueryHandler<FindOrderItemsRequest, OrderItemDto[]>, FindOrderItemsQueryHandler>();
            services.AddTransient<IQueryHandler<RespondToOrderItemRequest, OrderItemStatusResponse>, RespondToOrderItemQueryHandler>();

            //categories
            services.AddTransient<IQueryHandler<GetCategoryRequest, CategoryDto>, GetCategoryQueryHandler>();
            services.AddTransient<IQueryHandler<CategoryDto[]>, GetCategoriesQueryHandler>();

            services.RegisterFilters();

            return services;
        }

        private static IServiceCollection RegisterFilters(this IServiceCollection services)
        {
            services.AddSingleton<ICustomerFilter, CustomerTypeFilter>();

            services.AddSingleton<IAdvertFilter, Queries.Adverts.QueryFilters.CustomerIdFilter>();
            services.AddSingleton<IAdvertFilter, CategoryIdFilter>();
            services.AddSingleton<IAdvertFilter, ProductNameFilter>();
            services.AddSingleton<IAdvertFilter, PriceFilter>();
            services.AddSingleton<IAdvertFilter, PriceTypeFilter>();

            services.AddSingleton<ICustomerFilter, CustomerTypeFilter>();
            services.AddSingleton<ICustomerFilter, CompanyNameFilter>();
            services.AddSingleton<ICustomerFilter, Queries.Customers.QueryFilters.CustomerIdFilter>();

            services.AddSingleton<IUserFilter, UserEmailFilter>();
            services.AddSingleton<IUserFilter, UserCustomerIdFilter>();

            services.AddSingleton<IOrderFilter, Queries.Orders.QueryFilters.CustomerIdFilter>();

            services.AddSingleton<IOrderItemFilter, SellerIdFilter>();
            services.AddSingleton<IOrderItemFilter, ResponseStatusFilter>();

            return services;
        }
    }
}