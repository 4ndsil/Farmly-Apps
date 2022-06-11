
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
        public decimal? Weight { get; set; }
        public int? Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal? AdvertItemPrice { get; set; }
        public OrderPriceType PriceType { get; set; }
        public ResponseStatus ResponseStatus { get; set; }
        public int FkAdvertItemId { get; set; }
        public int FkOrderId { get; set; }
        public int FkPickupPointId { get; set; }
        public int FkCategoryId { get; set; }
        public int FkSellerId { get; set; }
        public AdvertItem AdvertItem { get; set; }
        public Order Order { get; set; }
        public CustomerAddress PickupPoint { get; set; }
        public Category Category { get; set; }
        public Customer Seller { get; set; }
    }

    public enum OrderPriceType
    {
        Kg,
        Gram,
        Styck
    }

    public enum ResponseStatus
    {
        Pending,
        IsAccepted,
        Delivered,
    }
}