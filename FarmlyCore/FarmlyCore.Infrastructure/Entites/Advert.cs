using FarmlyCore.Infrastructure.Entites;

namespace FarmlyCore.Infrastructure.Entities
{
    public class Advert
    {
        public Advert() { }

        public int Id { get; set; }
        public string ProductName { get; set; }
        public double TotalQuantity { get; set; }
        public AdvertPriceType PriceType { get; set; }
        public string Description { get; set; }
        public int FkSellerId { get; set; }
        public int FkCategoryId { get; set; }
        public int FkPickupPointId { get; set; }        
        public Category Category { get; set; }
        public Customer Seller { get; set; }       
        public CustomerAddress PickupPoint { get; set; }
        public ICollection<AdvertItem> AdvertItems { get; set; } = Array.Empty<AdvertItem>();
    }

    public enum AdvertPriceType
    {
        Kg,
        Gram,
        Styck
    }
}
