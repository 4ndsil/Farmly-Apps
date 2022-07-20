using FarmlyCore.Application.DTOs.Customer;

namespace FarmlyCore.Application.DTOs.Adverts
{
    public class AdvertDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal? Price { get; set; }
        public AdvertPriceTypeDto PriceType { get; set; }
        public string Description { get; set; }
        public bool Available { get; set; }
        public bool IsBulk { get; set; }
        public string ImageUrl { get; set; }
        public int SellerId { get; set; }
        public CustomerDto Seller { get; set; }
        public IEnumerable<AdvertItemDto> AdvertItems { get; set; } = Array.Empty<AdvertItemDto>();
        public int PickupPointId { get; set; }
        public CustomerAddressDto PickupPoint { get; set; }
        public int CategoryId { get; set; }
        public CategoryDto Category { get; set; }
    }

    public enum AdvertPriceTypeDto
    {
        kg,
        hg,
        g,        
        l,
        dl,
        cl,
        ml,
        st,
    }
}