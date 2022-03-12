namespace FarmlyCore.Infrastructure.Entities
{
    public class Advert
    {
        public Advert() { }

        public int Id { get; set; }        
        public string ProductName { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }
        public int SellerId { get; set; }
        public int CategoryId { get; set; }
        public int PickupPointId { get; set; }
        public PriceType PriceType { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }
        public Customer Seller { get; set; }        
        public CustomerAddress PickupPoint { get; set; }
    }

    public enum PriceType
    {
        Kg,
        Gram,
        Styck
    }
}
