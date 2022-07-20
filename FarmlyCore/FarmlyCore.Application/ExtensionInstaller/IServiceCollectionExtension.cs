using FarmlyCore.Application.DTOs;
using FarmlyCore.Application.DTOs.Adverts;
using FarmlyCore.Application.DTOs.Customer;
using FarmlyCore.Application.DTOs.Order;
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
using FarmlyCore.Application.Requests.Adverts;
using FarmlyCore.Application.Requests.Categories;
using FarmlyCore.Application.Requests.Customers;
using FarmlyCore.Application.Requests.OrderItems;
using FarmlyCore.Application.Requests.Orders;
using FarmlyCore.Application.Requests.Users;
using FarmlyCore.Infrastructure.Queries;

namespace FarmlyCore.Application.Extensions
{
    public static class ServiceCollectionExtenstion
    {
        public static IServiceCollection RegisterApplicationQueryHandlers(this IServiceCollection services)
        {
            services.AddTransient<IQueryHandler<GetCustomerRequest, CustomerDto>, GetCustomerQueryHandler>();
            services.AddTransient<IQueryHandler<CreateCustomerRequest, CustomerDto>, CreateCustomerQueryHandler>();
            services.AddTransient<IQueryHandler<CreateCustomerAddressRequest, CustomerAddressDto>, CreateCustomerAddressQueryHandler>();
            services.AddTransient<IQueryHandler<GetCustomerAddressesRequest, CustomerAddressDto[]>, GetCustomerAddressesQueryHandler>();
            services.AddTransient<IQueryHandler<UpdateCustomerRequest, CustomerDto>, UpdateCustomerQueryHandler>();
            services.AddTransient<IQueryHandler<UpdateCustomerAddressRequest, CustomerAddressDto>, UpdateCustomerAddressQueryHandler>();
            services.AddTransient<IQueryHandler<FindCustomersRequest, CustomerDto[]>, FindCustomersQueryHandler>();

            services.AddTransient<IQueryHandler<FindUsersRequest, UserDto[]>, FindUsersQueryHandler>();
            services.AddTransient<IQueryHandler<GetUserRequest, UserDto>, GetUserQueryHandler>();
            services.AddTransient<IQueryHandler<UpdateUserRequest, UserDto>, UpdateUserQueryHandler>();
            services.AddTransient<IQueryHandler<UserAuthenticationRequest, UserAuthenticationResponse>, UserAuthenticationQueryHandler>();

            services.AddTransient<IQueryHandler<GetAdvertRequest, AdvertDto>, GetAdvertQueryHandler>();
            services.AddTransient<IQueryHandler<AdvertDto[]>, GetAllAdvertsQueryHandler>();
            services.AddTransient<IQueryHandler<FindAdvertsRequest, IReadOnlyList<AdvertDto>>, FindAdvertsQueryHandler>();
            services.AddTransient<IQueryHandler<CreateAdvertRequest, AdvertDto>, CreateAdvertQueryHandler>();
            services.AddTransient<IQueryHandler<UpdateAdvertRequest, AdvertDto>, UpdateAdvertQueryHandler>();
            services.AddTransient<IQueryHandler<CreateAdvertItemRequest, AdvertItemDto>, CreateAdvertItemQueryHandler>();
            services.AddTransient<IQueryHandler<DeleteAdvertItemRequest, DeleteAdvertItemResult>, DeleteAdvertItemQueryHandler>();

            services.AddTransient<IQueryHandler<GetOrderRequest, OrderDto>, GetOrderQueryHandler>();
            services.AddTransient<IQueryHandler<CreateOrderRequest, CreateOrderResponse>, CreateOrderQueryHandler>();
            services.AddTransient<IQueryHandler<FindOrdersRequest, IReadOnlyList<OrderDto>>, FindOrdersQueryHandler>();
            services.AddTransient<IQueryHandler<UpdateOrderItemRequest, OrderItemDto>, UpdateOrderItemQueryHandler>();

            services.AddTransient<IQueryHandler<FindOrderItemsRequest, IReadOnlyList<OrderItemDto>>, FindOrderItemsQueryHandler>();
            services.AddTransient<IQueryHandler<RespondToOrderItemRequest, OrderItemDto>, RespondToOrderItemQueryHandler>();

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

            services.AddSingleton<IUserFilter, UserEmailFilter>();
            services.AddSingleton<IUserFilter, UserCustomerIdFilter>();

            services.AddSingleton<IOrderFilter, Queries.Orders.QueryFilters.CustomerIdFilter>();

            services.AddSingleton<IOrderItemFilter, SellerIdFilter>();
            services.AddSingleton<IOrderItemFilter, ResponseStatusFilter>();

            return services;
        }
    }
}