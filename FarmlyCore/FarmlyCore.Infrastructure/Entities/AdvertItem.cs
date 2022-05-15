using FarmlyCore.Infrastructure.Entities;

namespace FarmlyCore.Infrastructure.Entities
{
    public class AdvertItem
    {
        public AdvertItem() { }
        public AdvertItem(Advert advert)
        {
            Advert = advert;
        }

        public int Id { get; set; }
        public decimal Quantity { get; set; }
        public int? Amount { get; set; }
        public decimal Price { get; set; }
        public int FkAdvertId { get; set; }
        public Advert Advert { get; set; }
    }
}