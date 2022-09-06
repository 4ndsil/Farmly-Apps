using AutoMapper;
using AutoMapper.QueryableExtensions;
using FarmlyCore.Application.DTOs.Customer;
using FarmlyCore.Application.Requests.Users;
using FarmlyCore.Infrastructure.FarmlyDbContext;
using FarmlyCore.Infrastructure.Queries;
using Microsoft.EntityFrameworkCore;

namespace FarmlyCore.Application.Queries.Users
{
    public class GetUserByEmailQueryHandler : IQueryHandler<GetUserByEmailRequest, UserDto>
    {
        private readonly FarmlyEntityDbContext _farmlyEntityDataContext;
        private readonly IMapper _mapper;

        public GetUserByEmailQueryHandler(IMapper mapper, FarmlyEntityDbContext farmlyEntityDbContext)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _farmlyEntityDataContext = farmlyEntityDbContext ?? throw new ArgumentNullException(nameof(farmlyEntityDbContext));
        }

        public async Task<UserDto> HandleAsync(GetUserByEmailRequest request, CancellationToken cancellationToken = default)
        {
            var userDto = await _farmlyEntityDataContext.Users
                .AsNoTracking()
                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .AsNoTracking()
                .FirstOrDefaultAsync(e => request.Email.Equals(e.Email), cancellationToken);

            if (userDto == null)
            {
                return null;
            }

            return userDto;
        }
    }
}