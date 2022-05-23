using AutoMapper;
using FarmlyCore.Application.DTOs.Customer;
using FarmlyCore.Application.Requests.Customers;
using FarmlyCore.Infrastructure.FarmlyDbContext;
using FarmlyCore.Infrastructure.Queries;
using Microsoft.EntityFrameworkCore;

namespace FarmlyCore.Application.Queries.Customers
{
    public class UpdateCustomerAddressQueryHandler : IQueryHandler<UpdateCustomerAddressRequest, CustomerAddressDto>
    {
        private readonly FarmlyEntityDbContext _farmlyEntityDataContext;
        private readonly IMapper _mapper;

        public UpdateCustomerAddressQueryHandler(IMapper mapper, FarmlyEntityDbContext farmlyEntityDbContext)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _farmlyEntityDataContext = farmlyEntityDbContext ?? throw new ArgumentNullException(nameof(farmlyEntityDbContext));
        }

        public async Task<CustomerAddressDto> HandleAsync(UpdateCustomerAddressRequest request, CancellationToken cancellationToken = default)
        {
            var customerExists = await _farmlyEntityDataContext.Customers.AnyAsync(e => e.Id.Equals(request.CustomerId));

            if (customerExists == null)
            {
                return null;
            }

            var customerAddress = await _farmlyEntityDataContext.CustomerAddresses                
                .Where(e => e.Id == request.CustomerAddressId && e.FkCustomerId == request.CustomerId)
                .FirstOrDefaultAsync();

            if (customerAddress == null)
            {
                return null;
            }

            var customerAddressDto = _mapper.Map<CustomerAddressDto>(customerAddress);

            request.CustomerAddress.ApplyTo(customerAddressDto);

            _mapper.Map(customerAddressDto, customerAddress);

            await _farmlyEntityDataContext.SaveChangesAsync(cancellationToken);

            return customerAddressDto;
        }
    }
}
