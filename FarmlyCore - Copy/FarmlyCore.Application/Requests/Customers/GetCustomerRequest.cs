namespace FarmlyCore.Application.Requests.Customers
{
    public class GetCustomerRequest
    {
        public GetCustomerRequest(int customerId)
        {
            CustomerId = customerId;
        }

        public int CustomerId { get; set; }
    }
}
