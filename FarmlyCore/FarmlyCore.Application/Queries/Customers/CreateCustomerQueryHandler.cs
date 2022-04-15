using AutoMapper;
using FarmlyCore.Application.DTOs.Customer;
using FarmlyCore.Application.Requests.Customers;
using FarmlyCore.Infrastructure.Entities;
using FarmlyCore.Infrastructure.FarmlyDbContext;
using FarmlyCore.Infrastructure.Queries;

namespace FarmlyCore.Application.Queries.Customers
{
    public class CreateCustomerQueryHandler : IQueryHandler<CreateCustomerRequest, CustomerDto>
    {
        private readonly FarmlyEntityDbContext _farmlyEntityDataContext;
        private readonly IMapper _mapper;

        public CreateCustomerQueryHandler(IMapper mapper, FarmlyEntityDbContext farmlyEntityDbContext)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

            _farmlyEntityDataContext = farmlyEntityDbContext ?? throw new ArgumentNullException(nameof(farmlyEntityDbContext));
        }

        public async Task<CustomerDto> HandleAsync(CreateCustomerRequest request, CancellationToken cancellationToken = default)
        {
            var customer = new Customer
            {
                BankGiro = request.Customer.BankGiro,
                CompanyName = request.Customer.CompanyName,
                Email = request.Customer.Email,
                CustomerType = (CustomerType)request.Customer.CustomerType,
                OrgNumber = request.Customer.OrgNumber,
            };

            await _farmlyEntityDataContext.AddAsync(customer);

            await _farmlyEntityDataContext.SaveChangesAsync();

            var addressList = new List<CustomerAddress>();

            foreach (var address in request.Customer.CustomerAddresses)
            {
                var customerAddress = new CustomerAddress(customer)
                {
                    Street = address.Street,
                    City = address.City,
                    State = address.State,
                    Zip = address.Zip,
                };

                addressList.Add(customerAddress);

                await _farmlyEntityDataContext.CustomerAddresses.AddAsync(customerAddress);
            }

            customer.CustomerAddresses = addressList;        

            await _farmlyEntityDataContext.SaveChangesAsync();

            var customerDto = _mapper.Map<CustomerDto>(customer);

            return customerDto;
        }
    }
}