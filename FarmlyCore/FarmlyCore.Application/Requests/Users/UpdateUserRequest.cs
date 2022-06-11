using FarmlyCore.Application.DTOs.Customer;
using Microsoft.AspNetCore.JsonPatch;

namespace FarmlyCore.Application.Requests.Users
{
    public class UpdateUserRequest
    {
        public UpdateUserRequest(int userId, JsonPatchDocument<UserDto> user)
        {
            UserId = userId;

            User = user ?? throw new ArgumentNullException(nameof(user));
        }

        public int UserId { get; set; }

        public JsonPatchDocument<UserDto> User { get; set; }
    }
}