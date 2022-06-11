namespace FarmlyCore.Application.DTOs.Adverts
{
    public class CreateAdvertDto
    {
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public AdvertPriceTypeDto PriceType { get; set; }
        public string Description { get; set; }
        public bool Available { get; set; }
        public bool? IsBulk { get; set; }
        public int CategoryId { get; set; }
        public int SellerId { get; set; }
        public int PickupPointId { get; set; }
        public IEnumerable<CreateAdvertItemDto> AdvertItems { get; set; } = Array.Empty<CreateAdvertItemDto>();
    }

    public class CreateAdvertItemDto
    {
        public decimal Weight { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}