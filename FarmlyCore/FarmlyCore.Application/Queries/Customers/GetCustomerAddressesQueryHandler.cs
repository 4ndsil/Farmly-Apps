using AutoMapper;
using AutoMapper.QueryableExtensions;
using FarmlyCore.Application.DTOs.Customer;
using FarmlyCore.Application.Requests.Customers;
using FarmlyCore.Infrastructure.FarmlyDbContext;
using FarmlyCore.Infrastructure.Queries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FarmlyCore.Application.Queries.Customers
{
    public class GetCustomerAddressesQueryHandler : IQueryHandler<GetCustomerAddressesRequest, CustomerAddressDto[]>
    {
        private readonly FarmlyEntityDbContext _farmlyEntityDataContext;
        private readonly IMapper _mapper;

        public GetCustomerAddressesQueryHandler(IMapper mapper, FarmlyEntityDbContext farmlyEntityDbContext)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _farmlyEntityDataContext = farmlyEntityDbContext ?? throw new ArgumentNullException(nameof(farmlyEntityDbContext));
        }

        public async Task<CustomerAddressDto[]> HandleAsync(GetCustomerAddressesRequest request, CancellationToken cancellationToken = default)
        {
            var baseRequest = _farmlyEntityDataContext.CustomerAddresses
                .Where(e => e.FkCustomerId == request.CustomerId)
                .AsNoTracking()
                .AsQueryable();

            if (baseRequest == null)
            {
                return null;
            }

            var response = await baseRequest
                .OrderByDescending(e => e.Id)
                .ProjectTo<CustomerAddressDto>(_mapper.ConfigurationProvider)
                .ToArrayAsync(cancellationToken);

            return response;
        }
    }
}

