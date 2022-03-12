using AutoMapper;
using FarmlyCore.Application.DTOs.Customer;
using FarmlyCore.Application.Queries.Requests.Customers;
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
            List<CustomerAddress> addressList = null;

            if (request.Customer.AddressList != null)
            {
                addressList.AddRange(_mapper.Map<CustomerAddress[]>(request.Customer.AddressList));
            }

            var customer = new Customer
            {
                AddressList = addressList,
                BankGiro = request.Customer.BankGiro,
                CompanyName = request.Customer.CompanyName,
                CustomerType = (CustomerType)request.Customer.CustomerType,
                OrgNumber = request.Customer.OrgNumber,
            };

            await _farmlyEntityDataContext.AddAsync(customer);

            await _farmlyEntityDataContext.SaveChangesAsync();

            var customerDto = _mapper.Map<CustomerDto>(customer);

            return customerDto;
        }
    }
}