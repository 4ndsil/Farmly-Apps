
namespace FarmlyCore.Infrastructure.Entities
{
    public class Advert
    {
        public Advert() { }

        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal TotalQuantity { get; set; }
        public decimal? Price { get; set; }
        public AdvertPriceType PriceType { get; set; }
        public string Description { get; set; }
        public bool Available { get; set; }
        public int FkSellerId { get; set; }
        public int FkCategoryId { get; set; }
        public int FkPickupPointId { get; set; }
        public Category Category { get; set; }
        public Customer Seller { get; set; }
        public CustomerAddress PickupPoint { get; set; }
        public IEnumerable<AdvertItem> AdvertItems { get; set; }
    }

    public enum AdvertPriceType
    {
        Kg,
        Gram,
        Styck
    }
}
