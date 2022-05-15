namespace FarmlyCore.Application.Requests.Orders
{
    public class FindOrdersRequest
    {
        public FindOrdersRequest() { }

        public int? CustomerId { get; set; }

        public bool? Delivered { get; set; }
    }
}
