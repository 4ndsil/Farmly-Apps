using AutoMapper;
using FarmlyCore.Application.DTOs.Customer;
using FarmlyCore.Application.Queries.Requests.Customer;
using FarmlyCore.Infrastructure.FarmlyDbContext;
using FarmlyCore.Infrastructure.Queries;
using Microsoft.EntityFrameworkCore;

namespace FarmlyCore.Application.Queries.Customers
{
    public class UpdateAdvertQueryHandler : IQueryHandler<UpdateAdvertRequest, CustomerDto>
    {
        private readonly FarmlyEntityDbContext _farmlyEntityDataContext;
        private readonly IMapper _mapper;

        public UpdateAdvertQueryHandler(IMapper mapper, FarmlyEntityDbContext farmlyEntityDbContext)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _farmlyEntityDataContext = farmlyEntityDbContext ?? throw new ArgumentNullException(nameof(farmlyEntityDbContext));
        }

        public async Task<CustomerDto> HandleAsync(UpdateAdvertRequest request, CancellationToken cancellationToken = default)
        {
            var customer = await _farmlyEntityDataContext.Customers
                .Include(e => e.AddressList)
                .Where(e => e.Id == request.CustomerId)
                .FirstOrDefaultAsync();

            if (customer == null)
            {
                return null;
            }

            var customerDto = _mapper.Map<CustomerDto>(customer);

            request.Customer.ApplyTo(customerDto);

            _mapper.Map(customerDto, customer);

            await _farmlyEntityDataContext.SaveChangesAsync(cancellationToken);

            return customerDto;
        }
    }
}
