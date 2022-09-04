using AutoMapper;
using FarmlyCore.Application.DTOs.Customer;
using FarmlyCore.Application.Requests.Users;
using FarmlyCore.Infrastructure.FarmlyDbContext;
using FarmlyCore.Infrastructure.Queries;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace FarmlyCore.Application.Queries.Users
{
    public class UserAuthenticationResponse
    {
        private UserAuthenticationResponse(UserDto user) { User = user; }

        private UserAuthenticationResponse(AuthenticationDetail detail) { Detail = detail; }

        public static UserAuthenticationResponse WithSuccess(UserDto user) => new UserAuthenticationResponse(user);

        public static UserAuthenticationResponse WithProblem(AuthenticationDetail detail) => new UserAuthenticationResponse(detail);

        public UserDto? User { get; set; }

        public AuthenticationDetail? Detail { get; set; }
    }

    public enum AuthenticationDetail
    {
        UserNotFound,
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
                return UserAuthenticationResponse.WithProblem(AuthenticationDetail.UserNotFound);
            }

            var userPassword = user.Password;

            var inputPassword = GetHashString(request.Password);

            if (!userPassword.Equals(inputPassword))
            {
                return UserAuthenticationResponse.WithProblem(AuthenticationDetail.InvalidCredentials);
            }

            var userDto = _mapper.Map<UserDto>(user);

            return UserAuthenticationResponse.WithSuccess(userDto);
        }

        private static byte[] GetHash(string credentials)
        {
            using (HashAlgorithm algorithm = SHA256.Create())
            {
                return algorithm.ComputeHash(Encoding.UTF8.GetBytes(credentials));
            }             
        }

        private static string GetHashString(string credentials)
        {
            StringBuilder sb = new StringBuilder();

            foreach (byte b in GetHash(credentials))
            {
                sb.Append(b.ToString("X2"));
            }                

            return sb.ToString();
        }
    }
}