namespace FarmlyCore.Infrastructure.Entities
{
    public class OrderItem
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }
        public OrderPriceType PriceType { get; set; }
        public int FkAdvertId { get; set; }
        public int FkOrderId { get; set; }
        public Advert Advert { get; set; }
        public Order Order { get; set; }
    }

    public enum OrderPriceType
    {
        Kg,
        Gram,
        Styck
    }
}