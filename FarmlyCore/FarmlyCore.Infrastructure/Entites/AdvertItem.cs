using FarmlyCore.Infrastructure.Entities;

namespace FarmlyCore.Infrastructure.Entites
{
    public class AdvertItem
    {
        public int Id { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }
        public int FkAdvertId { get; set; }
        public Advert Advert { get; set; }
    }
}