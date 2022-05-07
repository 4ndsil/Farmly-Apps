using FarmlyCore.Application.DTOs.Customer;

namespace FarmlyCore.Application.DTOs.Adverts
{
    public class AdvertDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public double TotalQuantity { get; set; }
        public double TotalPrice { get; set; }
        public AdvertPriceTypeDto PriceType { get; set; }
        public string Description { get; set; }
        public CategoryDto Category { get; set; }
        public CustomerDto Seller { get; set; }
        public ICollection<AdvertItemDto> AdvertItems { get; set; } = Array.Empty<AdvertItemDto>();
        public CustomerAddressDto PickupPoint { get; set; }
    }

    public class CreateAdvertDto
    {
        public string ProductName { get; set; }
        public double TotalQuantity { get; set; }
        public double TotalPrice { get; set; }
        public AdvertPriceTypeDto PriceType { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public int SellerId { get; set; }
        public int PickupPointId { get; set; }
        public ICollection<CreateAdvertItemDto> AdvertItems { get; set; } = Array.Empty<CreateAdvertItemDto>();
    }
    public class CreateAdvertItemDto
    {
        public double Quantity { get; set; }
        public double Price { get; set; }
    }

    public enum AdvertPriceTypeDto
    {
        Kg,
        Gram,
        Styck
    }
}
