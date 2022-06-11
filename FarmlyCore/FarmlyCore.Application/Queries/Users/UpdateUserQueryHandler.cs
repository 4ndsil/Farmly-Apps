using AutoMapper;
using FarmlyCore.Application.DTOs.Customer;
using FarmlyCore.Application.Requests.Users;
using FarmlyCore.Infrastructure.FarmlyDbContext;
using FarmlyCore.Infrastructure.Queries;
using Microsoft.EntityFrameworkCore;

namespace FarmlyCore.Application.Queries.Users
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
            var user = await _farmlyEntityDataContext.Users
                .Where(e => e.Id == request.UserId)
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