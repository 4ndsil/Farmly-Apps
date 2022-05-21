using FarmlyCore.Application.DTOs.Adverts;

namespace FarmlyCore.Application.Requests.Adverts
{
    public class FindAdvertsRequest
    {
        public FindAdvertsRequest() { }

        public int? CustomerId { get; set; }

        public int? CategoryId { get; set; }

        public string? ProductName { get; set; }

        public AdvertPriceTypeDto PriceType { get; set; }

        public decimal? Price { get; set; }
    }
}