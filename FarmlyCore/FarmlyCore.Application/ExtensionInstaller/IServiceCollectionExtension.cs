using FarmlyCore.Application.DTOs.Customer;
using FarmlyCore.Application.Queries.Requests.Customer;
using FarmlyCore.Application.Queries.Customers;
using FarmlyCore.Infrastructure.Queries;

namespace FarmlyCore.Application.Extensions
{
    public static class ServiceCollectionExtenstion
    {
        public static IServiceCollection RegisterApplicationQueryHandlers(this IServiceCollection services)
        {
            services.AddTransient<IQueryHandler<GetOrderRequest, CustomerDto>, GetAdvertsQueryHandler>();
            services.AddTransient<IQueryHandler<FindOrdersRequest, CustomerDto[]>, FindAdvertsQueryHandler>();

            return services;
        }

        private static IServiceCollection RegisterFilters(this IServiceCollection services)
        {            
            return services;
        }
    }   
}
