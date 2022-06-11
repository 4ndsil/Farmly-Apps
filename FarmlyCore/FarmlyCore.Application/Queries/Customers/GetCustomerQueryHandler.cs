using AutoMapper;
using FarmlyCore.Application.DTOs.Customer;
using FarmlyCore.Application.Requests.Customers;
using FarmlyCore.Infrastructure.FarmlyDbContext;
using FarmlyCore.Infrastructure.Queries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FarmlyCore.Application.Queries.Customers
{
    public class GetCustomerQueryHandler : IQueryHandler<GetCustomerRequest, CustomerDto>
    {
        private readonly FarmlyEntityDbContext _farmlyEntityDataContext;
        private readonly IMapper _mapper;

        public GetCustomerQueryHandler(IMapper mapper, FarmlyEntityDbContext farmlyEntityDbContext)
        {            
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _farmlyEntityDataContext = farmlyEntityDbContext ?? throw new ArgumentNullException(nameof(farmlyEntityDbContext));
        }

        public async Task<CustomerDto> HandleAsync(GetCustomerRequest request, CancellationToken cancellationToken = default)
        {
            var customer = await _farmlyEntityDataContext.Customers
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
