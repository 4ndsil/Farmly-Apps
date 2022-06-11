namespace FarmlyCore.Application.DTOs.Adverts
{
    public class AdvertItemDto
    {
        public int Id { get; set; }
        public decimal Weight { get; set; }
        public int? Quantity { get; set; }
        public decimal Price { get; set; }
        public int AdvertId { get; set; }
    }
}