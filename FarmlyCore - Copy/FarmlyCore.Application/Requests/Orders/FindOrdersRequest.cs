namespace FarmlyCore.Application.Requests.Orders
{
    public class FindOrdersRequest
    {
        public FindOrdersRequest() { }

        public int? CustomerId { get; set; }

        public bool? Delivered { get; set; }

        public DateTime? PlacementDate { get; set; }

        public DateTime? EstimatedDeliveryDate { get; set; }        
    }
}
