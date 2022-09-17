using FarmlyCore.Application.DTOs.Adverts;
using FarmlyCore.Application.DTOs.Customer;

namespace FarmlyCore.Application.DTOs.Order
{
    public class OrderItemDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal? Weight { get; set; }
        public decimal? Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal? AdvertItemPrice { get; set; }
        public OrderPriceTypeDto PriceType { get; set; }
        public  int AdvertItemId { get; set; }
        public int CategoryId { get; set; }
        public CategoryDto Category { get; set; }
        public int SellerId { get; set; }
        public string SellerName { get; set; }
        public int PickupPointId { get; set; }
        public ResponseStatusDto ResponseStatus { get; set; }
        public int OrderId { get; set; }
        public DateTime PickupDate { get; set; }
        public DateTime PlacementDate { get; set; }
        public DateTime? ResponseDate { get; set; }
    }

    public enum OrderPriceTypeDto
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

    public enum ResponseStatusDto
    {
        Pending,
        IsAccepted,
        Delivered,
    }
}