namespace FarmlyCore.Infrastructure.Entities
{
    public class Order
    {
        public Order() { }

        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public DateTime PlacementDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public DateTime EstimatedDeliveryDate { get; set; }
        public bool? Delivered { get; set; }
        public decimal? TotalPrice { get; set; }
        public decimal? TotalQuantity { get; set; }
        public int FkBuyerId { get; set; }        
        public int FkDeliveryPointId { get; set; }
        public Customer Buyer { get; set; }
        public CustomerAddress DeliveryPoint { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}