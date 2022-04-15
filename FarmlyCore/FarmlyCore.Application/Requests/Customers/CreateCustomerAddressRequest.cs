using FarmlyCore.Application.DTOs.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmlyCore.Application.Requests.Customers
{
    public class CreateCustomerAddressRequest
    {
        public int CustomerId {get;}
        public CustomerAddressDto CustomerAddress { get; }

        public CreateCustomerAddressRequest(CustomerAddressDto customerAddress, int customerId)
        {
            CustomerAddress = customerAddress ?? throw new ArgumentNullException(nameof(customerAddress));

            CustomerId = customerId;
        }
    }
}
