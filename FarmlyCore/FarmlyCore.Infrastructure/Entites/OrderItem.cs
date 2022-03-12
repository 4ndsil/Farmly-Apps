namespace FarmlyCore.Infrastructure.Entities
{
    public class OrderItem
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }
        public PriceType PriceType { get; set; }        
        public int AdvertId { get; set; }
        public int OrderId { get; set; }
    }
}
