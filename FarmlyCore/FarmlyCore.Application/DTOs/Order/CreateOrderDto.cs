using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmlyCore.Application.DTOs.Order
{
    public class CreateOrderDto
    {
        public DateTime PlacementDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public DateTime EstimatedDeliveryDate { get; set; }
        public decimal? TotalPrice { get; set; }
        public decimal? TotalWeight { get; set; }
        public int DeliveryPointId { get; set; }
        public int BuyerId { get; set; }
        public IEnumerable<CreateOrderItemDto> CreateOrderItems { get; set; } = Array.Empty<CreateOrderItemDto>();
    }

    public class CreateOrderItemDto
    {
        public string ProductName { get; set; }
        public decimal? Weight { get; set; }
        public int? Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal? AdvertItemPrice { get; set; }
        public OrderPriceTypeDto PriceType { get; set; }
        public int AdvertItemId { get; set; }
        public int CategoryId { get; set; }
        public int PickupPointId { get; set; }
    }
}
