namespace FarmlyCore.Application.DTOs.Adverts
{
    public class CreateAdvertDto
    {
        public string ProductName { get; set; }
        public decimal TotalQuantity { get; set; }
        public decimal Price { get; set; }
        public AdvertPriceTypeDto PriceType { get; set; }
        public string Description { get; set; }
        public bool Available { get; set; }
        public int CategoryId { get; set; }
        public int SellerId { get; set; }
        public int PickupPointId { get; set; }
        public ICollection<CreateAdvertItemDto> AdvertItems { get; set; } = Array.Empty<CreateAdvertItemDto>();
    }

    public class CreateAdvertItemDto
    {
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
    }
}