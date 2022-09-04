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

            userDto.Password = GetHashString(userDto.Password);

            _mapper.Map(userDto, user);

            await _farmlyEntityDataContext.SaveChangesAsync(cancellationToken);

            return userDto;
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