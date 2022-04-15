namespace FarmlyCore.Application.Requests.Orders
{
    public class GetOrderRequest
    {
        public GetOrderRequest(int orderId)
        {
            OrderId = orderId;
        }

        public int OrderId { get; set; }
    }
}
