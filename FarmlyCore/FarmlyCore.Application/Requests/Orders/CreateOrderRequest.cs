﻿using FarmlyCore.Application.DTOs.Order;

namespace FarmlyCore.Application.Requests.Orders
{
    public class CreateOrderRequest
    {
        public OrderDto Order { get; }

        public CreateOrderRequest(OrderDto order)
        {
            Order = order ?? throw new ArgumentNullException(nameof(order));
        }
    }
}
