namespace FarmlyCore.Application.DTOs
{
    public class AdvertDto
    {
        public string Id { get; set; }
        public string ProductName { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }
        public PriceTypeDto PriceType { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public int SellerId { get; set; }
        public int PickupPointId { get; set; }
    }

    public enum PriceTypeDto
    {
        Kg,
        Gram,
        Styck
    }
}
