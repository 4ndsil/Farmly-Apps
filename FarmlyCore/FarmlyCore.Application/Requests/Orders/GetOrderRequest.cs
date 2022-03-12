namespace FarmlyCore.Application.Queries.Requests.Customer
{
    public class GetOrderRequest
    {
        public GetOrderRequest(int customerId)
        {
            CustomerId = customerId;
        }

        public int CustomerId { get; set; }
    }
}
