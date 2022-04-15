using FarmlyCore.Application.DTOs;
using FarmlyCore.Application.DTOs.Customer;
using Microsoft.AspNetCore.JsonPatch;

namespace FarmlyCore.Application.Requests.Customers
{
    public class UpdateCustomerRequest
    {
        public UpdateCustomerRequest(int customerId, JsonPatchDocument<CustomerDto> customer)
        {
            CustomerId = customerId;
            Customer = customer ?? throw new ArgumentNullException(nameof(customer));
        }

        public int CustomerId { get; set; }
        public JsonPatchDocument<CustomerDto> Customer { get; set; }
    }
}
