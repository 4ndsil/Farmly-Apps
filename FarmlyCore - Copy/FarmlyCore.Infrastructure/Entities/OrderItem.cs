
namespace FarmlyCore.Infrastructure.Entities
{
    public class OrderItem
    {
        public OrderItem() { }

        public OrderItem(Order order)
        {
            Order = order;
        }

        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public OrderPriceType PriceType { get; set; }
        public int FkAdvertItemId { get; set; }
        public int FkOrderId { get; set; }
        public AdvertItem AdvertItem { get; set; }
        public Order Order { get; set; }
    }

    public enum OrderPriceType
    {
        Kg,
        Gram,
        Styck
    }
}