using AutoMapper;
using FarmlyCore.Application.DTOs.Customer;
using FarmlyCore.Application.DTOs.Order;
using FarmlyCore.Application.Queries.Requests.Customer;
using FarmlyCore.Infrastructure.FarmlyDbContext;
using FarmlyCore.Infrastructure.Queries;
using Microsoft.EntityFrameworkCore;

namespace FarmlyCore.Application.Queries.Orders
{
    public class GetCustomersQueryHandler : IQueryHandler<GetOrderRequest, OrderDto>
    {
        private readonly FarmlyEntityDbContext _farmlyEntityDataContext;
        private readonly IMapper _mapper;

        public GetCustomersQueryHandler(IMapper mapper, FarmlyEntityDbContext farmlyEntityDbContext)
        {            
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _farmlyEntityDataContext = farmlyEntityDbContext ?? throw new ArgumentNullException(nameof(farmlyEntityDbContext));
        }

        public async Task<CustomerDto> HandleAsync(GetOrderRequest request, CancellationToken cancellationToken = default)
        {
            var customer = await _farmlyEntityDataContext.Order
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == request.CustomerId, cancellationToken);

            if (customer == null)
            {
                return null;
            }

            return _mapper.Map<CustomerDto>(customer);
        }       
    }
}
