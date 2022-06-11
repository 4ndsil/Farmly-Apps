using AutoMapper;
using AutoMapper.QueryableExtensions;
using FarmlyCore.Application.DTOs.Customer;
using FarmlyCore.Application.Requests.Customers;
using FarmlyCore.Infrastructure.FarmlyDbContext;
using FarmlyCore.Infrastructure.Queries;
using Microsoft.EntityFrameworkCore;

namespace FarmlyCore.Application.Queries.Customers
{
    public class GetCustomerAddressesQueryHandler : IQueryHandler<GetCustomerAddressesRequest, IReadOnlyList<CustomerAddressDto>>
    {
        private readonly FarmlyEntityDbContext _farmlyEntityDataContext;
        private readonly IMapper _mapper;

        public GetCustomerAddressesQueryHandler(IMapper mapper, FarmlyEntityDbContext farmlyEntityDbContext)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _farmlyEntityDataContext = farmlyEntityDbContext ?? throw new ArgumentNullException(nameof(farmlyEntityDbContext));
        }

        public async Task<IReadOnlyList<CustomerAddressDto>> HandleAsync(GetCustomerAddressesRequest request, CancellationToken cancellationToken = default)
        {
            return await _farmlyEntityDataContext.CustomerAddresses
                .Where(e => e.FkCustomerId == request.CustomerId)
                .AsNoTracking()
                .ProjectTo<CustomerAddressDto>(_mapper.ConfigurationProvider)
                .ToArrayAsync(cancellationToken);                
        }
    }
}