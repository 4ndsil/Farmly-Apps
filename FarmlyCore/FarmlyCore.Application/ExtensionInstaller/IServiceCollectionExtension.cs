using FarmlyCore.Application.DTOs.Adverts;
using FarmlyCore.Application.DTOs.Customer;
using FarmlyCore.Application.DTOs.Order;
using FarmlyCore.Application.Queries.Adverts;
using FarmlyCore.Application.Queries.Adverts.QueryFilters;
using FarmlyCore.Application.Queries.Customers;
using FarmlyCore.Application.Queries.Customers.QueryFilters;
using FarmlyCore.Application.Queries.Orders;
using FarmlyCore.Application.Queries.Orders.QueryFilters;
using FarmlyCore.Application.Requests.Adverts;
using FarmlyCore.Application.Requests.Customers;
using FarmlyCore.Application.Requests.Orders;
using FarmlyCore.Infrastructure.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace FarmlyCore.Application.Extensions
{
    public static class ServiceCollectionExtenstion
    {
        public static IServiceCollection RegisterApplicationQueryHandlers(this IServiceCollection services)
        {
            services.AddTransient<IQueryHandler<GetCustomerRequest, CustomerDto>, GetCustomersQueryHandler>();
            services.AddTransient<IQueryHandler<FindCustomerUsersRequest, UserDto[]>, FindCustomerUsersQueryHandler>();
            services.AddTransient<IQueryHandler<CreateCustomerRequest, CustomerDto>, CreateCustomerQueryHandler>();
            services.AddTransient<IQueryHandler<CreateCustomerAddressRequest, CustomerAddressDto>, CreateCustomerAddressQueryHandler>();
            services.AddTransient<IQueryHandler<UpdateCustomerRequest, CustomerDto>, UpdateCustomerQueryHandler>();

            services.AddTransient<IQueryHandler<GetAdvertRequest, AdvertDto>, GetAdvertQueryHandler>();
            services.AddTransient<IQueryHandler<FindAdvertsRequest, AdvertDto[]>, FindAdvertsQueryHandler>();
            services.AddTransient<IQueryHandler<CreateAdvertRequest, AdvertDto>, CreateAdvertQueryHandler>();
            services.AddTransient<IQueryHandler<UpdateAdvertRequest, AdvertDto>, UpdateAdvertQueryHandler>();

            services.AddTransient<IQueryHandler<GetOrderRequest, OrderDto>, GetOrderQueryHandler>();
            services.AddTransient<IQueryHandler<FindOrdersRequest, OrderDto[]>, FindOrdersQueryHandler>();
            services.AddTransient<IQueryHandler<CreateOrderRequest, OrderDto>, CreateOrderQueryHandler>();
            services.AddTransient<IQueryHandler<UpdateOrderRequest, OrderDto>, UpdateOrderQueryHandler>();

            services.RegisterFilters();

            return services;
        }

        private static IServiceCollection RegisterFilters(this IServiceCollection services)
        {
            services.AddSingleton<ICustomerFilter, Queries.Customers.QueryFilters.CustomerIdFilter>();
            services.AddSingleton<IAdvertFilter, Queries.Adverts.QueryFilters.CustomerIdFilter>();
            services.AddSingleton<IAdvertFilter, CategoryIdFilter>();
            services.AddSingleton<IOrderFilter, Queries.Orders.QueryFilters.CustomerIdFilter>();

            return services;
        }
    }
}
