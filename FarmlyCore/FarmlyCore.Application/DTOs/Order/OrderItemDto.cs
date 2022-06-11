namespace FarmlyCore.Application.DTOs.Order
{
    public class OrderItemDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal? Quantity { get; set; }
        public int? Amount { get; set; }
        public decimal Price { get; set; }
        public decimal? AdvertItemPrice { get; set; }
        public OrderPriceTypeDto PriceType { get; set; }
        public int AdvertItemId { get; set; }
        public int CategoryId { get; set; }
        public int SellerId { get; set; }
        public int PickupPointId { get; set; }
        public bool HasResponse { get; set; }
        public int OrderId { get; set; }
    }

    public enum OrderPriceTypeDto
    {
        Kg,
        Gram,
        Styck
    }
}