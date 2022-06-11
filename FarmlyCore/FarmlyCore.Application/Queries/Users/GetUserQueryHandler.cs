using AutoMapper;
using FarmlyCore.Application.DTOs.Customer;
using FarmlyCore.Application.Requests.Users;
using FarmlyCore.Infrastructure.FarmlyDbContext;
using FarmlyCore.Infrastructure.Queries;
using Microsoft.EntityFrameworkCore;

namespace FarmlyCore.Application.Queries.Users
{
    public class GetUserQueryHandler : IQueryHandler<GetUserRequest, UserDto>
    {
        private readonly FarmlyEntityDbContext _farmlyEntityDataContext;
        private readonly IMapper _mapper;

        public GetUserQueryHandler(IMapper mapper, FarmlyEntityDbContext farmlyEntityDbContext)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _farmlyEntityDataContext = farmlyEntityDbContext ?? throw new ArgumentNullException(nameof(farmlyEntityDbContext));
        }

        public async Task<UserDto> HandleAsync(GetUserRequest request, CancellationToken cancellationToken = default)
        {
            var user = await _farmlyEntityDataContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == request.UserId, cancellationToken);

            if (user == null)
            {
                return null;
            }

            return _mapper.Map<UserDto>(user);
        }
    }
}