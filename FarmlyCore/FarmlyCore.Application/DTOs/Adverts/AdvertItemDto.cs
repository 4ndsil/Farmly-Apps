namespace FarmlyCore.Application.DTOs.Adverts
{
    public class AdvertItemDto
    {
        public int Id { get; set; }
        public decimal Quantity { get; set; }
        public int? Amount { get; set; }
        public decimal Price { get; set; }
        public int AdvertId { get; set; }
    }
}