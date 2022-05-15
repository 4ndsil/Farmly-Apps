using FarmlyCore.Application.DTOs.Customer;
using Microsoft.AspNetCore.JsonPatch;

namespace FarmlyCore.Application.Requests.Customers
{
    public class UpdateUserRequest
    {
        public UpdateUserRequest(int customerId, int customerAddressId, JsonPatchDocument<UserDto> user)
        {
            CustomerId = customerId;
            UserId = customerAddressId;
            User = user ?? throw new ArgumentNullException(nameof(user));
        }

        public int CustomerId { get; set; }

        public int UserId { get; set; }

        public JsonPatchDocument<UserDto> User { get; set; }
    }
}