using FarmlyCore.Application.DTOs.Customer;

namespace FarmlyCore.Application.Requests.Customers
{
    public class CreateCustomerRequest
    {
        public CustomerDto Customer { get; }

        public CreateCustomerRequest(CustomerDto customer)
        {
            Customer = customer ?? throw new ArgumentNullException(nameof(customer));
        }
    }
}
