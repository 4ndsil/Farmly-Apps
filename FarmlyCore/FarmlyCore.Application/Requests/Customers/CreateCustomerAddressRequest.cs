using FarmlyCore.Application.DTOs.Customer;

namespace FarmlyCore.Application.Requests.Customers
{
    public class CreateCustomerAddressRequest
    {
        public int CustomerId { get; }
        public CustomerAddressDto CustomerAddress { get; }

        public CreateCustomerAddressRequest(CustomerAddressDto customerAddress, int customerId)
        {
            CustomerAddress = customerAddress ?? throw new ArgumentNullException(nameof(customerAddress));

            CustomerId = customerId;
        }
    }
}