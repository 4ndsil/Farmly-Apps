using AutoMapper;
using FarmlyCore.Application.DTOs.Customer;
using FarmlyCore.Application.Requests.Customers;
using FarmlyCore.Infrastructure.Entities;
using FarmlyCore.Infrastructure.FarmlyDbContext;
using FarmlyCore.Infrastructure.Queries;
using Microsoft.EntityFrameworkCore;

namespace FarmlyCore.Application.Queries.Customers
{
    public class CreateCustomerAddressQueryHandler : IQueryHandler<CreateCustomerAddressRequest, CustomerAddressDto>
    {
        private readonly FarmlyEntityDbContext _farmlyEntityDataContext;
        private readonly IMapper _mapper;

        public CreateCustomerAddressQueryHandler(IMapper mapper, FarmlyEntityDbContext farmlyEntityDbContext)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _farmlyEntityDataContext = farmlyEntityDbContext ?? throw new ArgumentNullException(nameof(farmlyEntityDbContext));
        }

        public async Task<CustomerAddressDto> HandleAsync(CreateCustomerAddressRequest request, CancellationToken cancellationToken = default)
        {
            var customer = await _farmlyEntityDataContext.Customers.Where(e => e.Id == request.CustomerId).FirstOrDefaultAsync();

            if (customer == null)
            {
                return null;
            }

            var customerAddress = new CustomerAddress
            {
                Street = request.CustomerAddress.Street,
                City = request.CustomerAddress.City,
                Zip = request.CustomerAddress.Zip,
                State = request.CustomerAddress.State,
                FkCustomerId = customer.Id
            };

            await _farmlyEntityDataContext.AddAsync(customerAddress);

            await _farmlyEntityDataContext.SaveChangesAsync(cancellationToken);

            var customerAddressDto = _mapper.Map<CustomerAddressDto>(customerAddress);

            return customerAddressDto;
        }
    }
}