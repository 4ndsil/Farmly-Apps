using FarmlyCore.Application.DTOs.Customer;
using System;
using System.Collections.Generic;

namespace FarmlyCore.Application.DTOs.Order
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public double TotalPrice { get; set; }
        public DateTime PlacementDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public bool? Delivered { get; set; }
        public CustomerAddressDto DeliveryPoint { get; set; }
        public CustomerDto Buyer { get; set; }
        public ICollection<OrderItemDto> OrderItems { get; set; } = Array.Empty<OrderItemDto>();
    }
}
