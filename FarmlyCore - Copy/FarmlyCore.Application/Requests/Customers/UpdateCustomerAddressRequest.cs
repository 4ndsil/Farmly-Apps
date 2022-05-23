using FarmlyCore.Application.DTOs.Customer;
using Microsoft.AspNetCore.JsonPatch;

namespace FarmlyCore.Application.Requests.Customers
{
    public class UpdateCustomerAddressRequest
    {
        public UpdateCustomerAddressRequest(int customerId, int customerAddressId, JsonPatchDocument<CustomerAddressDto> customerAddress)
        {
            CustomerId = customerId;
            CustomerAddressId = customerAddressId;
            CustomerAddress = customerAddress ?? throw new ArgumentNullException(nameof(customerAddress));
        }

        public int CustomerId { get; set; }

        public int CustomerAddressId { get; set; }

        public JsonPatchDocument<CustomerAddressDto> CustomerAddress { get; set; }
    }
}
