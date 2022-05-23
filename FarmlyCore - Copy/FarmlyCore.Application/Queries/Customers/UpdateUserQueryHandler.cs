using AutoMapper;
using FarmlyCore.Application.DTOs.Customer;
using FarmlyCore.Application.Requests.Customers;
using FarmlyCore.Infrastructure.FarmlyDbContext;
using FarmlyCore.Infrastructure.Queries;
using Microsoft.EntityFrameworkCore;

namespace FarmlyCore.Application.Queries.Customers
{
    public class UpdateUserQueryHandler : IQueryHandler<UpdateUserRequest, UserDto>
    {
        private readonly FarmlyEntityDbContext _farmlyEntityDataContext;
        private readonly IMapper _mapper;

        public UpdateUserQueryHandler(IMapper mapper, FarmlyEntityDbContext farmlyEntityDbContext)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _farmlyEntityDataContext = farmlyEntityDbContext ?? throw new ArgumentNullException(nameof(farmlyEntityDbContext));
        }

        public async Task<UserDto> HandleAsync(UpdateUserRequest request, CancellationToken cancellationToken = default)
        {
            var customerExists = await _farmlyEntityDataContext.Users.AnyAsync(e => e.Id.Equals(request.CustomerId));

            if (customerExists == null)
            {
                return null;
            }

            var user = await _farmlyEntityDataContext.Users
                .Where(e => e.Id == request.UserId && e.FkCustomerId == request.CustomerId)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return null;
            }

            var userDto = _mapper.Map<UserDto>(user);

            request.User.ApplyTo(userDto);

            _mapper.Map(userDto, user);

            await _farmlyEntityDataContext.SaveChangesAsync(cancellationToken);

            return userDto;
        }
    }
}