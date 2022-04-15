namespace FarmlyCore.Application.DTOs.Order
{
    public class OrderItemDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }
        public OrderPriceTypeDto PriceType { get; set; }
        public int AdvertId { get; set; }
        public int OrderId { get; set; }
    }

    public enum OrderPriceTypeDto
    {
        Kg,
        Gram,
        Styck
    }
}