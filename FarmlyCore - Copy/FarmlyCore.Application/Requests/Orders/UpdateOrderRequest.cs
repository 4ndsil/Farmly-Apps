using FarmlyCore.Application.DTOs.Order;
using Microsoft.AspNetCore.JsonPatch;

namespace FarmlyCore.Application.Requests.Orders
{
    public class UpdateOrderRequest
    {
        public UpdateOrderRequest(int orderId, JsonPatchDocument<OrderDto> order)
        {
            OrderId = orderId;
            Order = order ?? throw new ArgumentNullException(nameof(order));
        }

        public int OrderId { get; set; }
        public JsonPatchDocument<OrderDto> Order { get; set; }
    }
}
