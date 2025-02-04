﻿using FarmlyCore.Application.DTOs.Customer;
using System;
using System.Collections.Generic;

namespace FarmlyCore.Application.DTOs.Order
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal TotalWeight { get; set; }
        public DateTime PlacementDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public DateTime EstimatedDeliveryDate { get; set; }
        public int DeliveryPointId { get; set; }
        public int BuyerId { get; set; }
        public string BuyerName { get; set; }
        public IEnumerable<OrderItemDto> OrderItems { get; set; } = Array.Empty<OrderItemDto>();
    }
}
