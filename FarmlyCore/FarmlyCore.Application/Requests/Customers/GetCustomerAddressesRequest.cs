namespace FarmlyCore.Application.Requests.Customers
{
    public class GetCustomerAddressesRequest
    {
        public GetCustomerAddressesRequest(int customerId)
        {
            CustomerId = customerId;            
        }

        public int CustomerId { get; set; }        
    }
}
