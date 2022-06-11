using AutoMapper;
using FarmlyCore.Application.DTOs.Customer;
using FarmlyCore.Application.Queries.Users.QueryFilters;
using FarmlyCore.Application.Requests.Users;
using FarmlyCore.Infrastructure.FarmlyDbContext;
using FarmlyCore.Infrastructure.Queries;
using Microsoft.EntityFrameworkCore;

namespace FarmlyCore.Application.Queries.Users
{
    public class UserAuthenticationResponse
    {
        private UserAuthenticationResponse(UserDto user) { User = user; }

        private UserAuthenticationResponse(AuthenticationProblemDetail detail) { Detail = detail; }

        public static UserAuthenticationResponse WithSuccess(UserDto user) => new UserAuthenticationResponse(user);

        public static UserAuthenticationResponse WithProblem(AuthenticationProblemDetail detail) => new UserAuthenticationResponse(detail);

        public UserDto? User { get; set; }

        public AuthenticationProblemDetail Detail { get; set; }
    }

    public enum AuthenticationProblemDetail
    {
       InvalidCredentials
    }

    public class UserAuthenticationQueryHandler : IQueryHandler<UserAuthenticationRequest, UserAuthenticationResponse>
    {
        private readonly FarmlyEntityDbContext _farmlyEntityDataContext;
        private readonly IMapper _mapper;

        public UserAuthenticationQueryHandler(IMapper mapper, FarmlyEntityDbContext farmlyEntityDbContext)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _farmlyEntityDataContext = farmlyEntityDbContext ?? throw new ArgumentNullException(nameof(farmlyEntityDbContext));
        }

        public async Task<UserAuthenticationResponse> HandleAsync(UserAuthenticationRequest request, CancellationToken cancellationToken = default)
        {
            var user = await _farmlyEntityDataContext.Users
                 .AsNoTracking()
                 .FirstOrDefaultAsync(e => e.Email != null && e.Email.Equals(request.Email), cancellationToken);            

            if (user == null)
            {
                return null;
            }

            if(!CompareByteArrays(Convert.FromBase64String(user.Password), Convert.FromBase64String(request.Credentials)))
            {
                return UserAuthenticationResponse.WithProblem(AuthenticationProblemDetail.InvalidCredentials);
            }

            var userDto = _mapper.Map<UserDto>(user);

            return UserAuthenticationResponse.WithSuccess(userDto);
        }

        public static bool CompareByteArrays(byte[] array1, byte[] array2)
        {
            if (array1.Length != array2.Length)
            {
                return false;
            }

            for (int i = 0; i < array1.Length; i++)
            {
                if (array1[i] != array2[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}