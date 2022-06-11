using FarmlyCore.Infrastructure.Entities;

namespace FarmlyCore.Application.Requests.OrderItems
{
    public class FindOrderItemsRequest
    {
        public FindOrderItemsRequest() { }

        public int? SellerId { get; set; }

        public ResponseStatus? ResponseStatus { get; set; }
    }
}