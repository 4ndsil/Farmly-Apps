using FarmlyCore.Application.DTOs.Adverts;
using System.ComponentModel.DataAnnotations;

namespace FarmlyCore.Application.Requests.Adverts
{
    public class FindAdvertsRequest
    {
        public FindAdvertsRequest()
        {
            PageNumber = 1;
            PageSize = 8;
        }

        public FindAdvertsRequest(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize > 8 ? 8 : pageSize;
        }

        [Range(1, int.MaxValue / 1000)]
        public int PageNumber { get; set; }

        [Range(1, 1000)]
        public int PageSize { get; set; }

        public int? CustomerId { get; set; }

        public int? CategoryId { get; set; }

        public string? ProductName { get; set; }        

        public AdvertPriceTypeDto? PriceType { get; set; }

        public decimal? Price { get; set; }
    }
}