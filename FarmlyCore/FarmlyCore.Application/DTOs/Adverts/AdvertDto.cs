using FarmlyCore.Application.DTOs.Customer;

namespace FarmlyCore.Application.DTOs.Adverts
{
    public class AdvertDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal TotalQuantity { get; set; }        
        public AdvertPriceTypeDto PriceType { get; set; }
        public string Description { get; set; }
        public bool Available { get; set; }        
        public int SellerId { get; set; }
        public ICollection<AdvertItemDto> AdvertItems { get; set; } = Array.Empty<AdvertItemDto>();
        public CustomerAddressDto PickupPoint { get; set; }
        public CategoryDto Category { get; set; }
    }

    public enum AdvertPriceTypeDto
    {
        Kg,
        Gram,
        Styck
    }
}
