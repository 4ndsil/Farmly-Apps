using FarmlyCore.Application.DTOs.Order;
using Microsoft.AspNetCore.JsonPatch;

namespace FarmlyCore.Application.Requests.Orders
{
    public class RespondToOrderItemRequest
    {
        public RespondToOrderItemRequest(int orderItemId, JsonPatchDocument<OrderItemDto> orderItem)
        {
            OrderItemId = orderItemId;
            OrderItem = orderItem ?? throw new ArgumentNullException(nameof(orderItemId));
        }

        public int OrderItemId { get; set; }
        public JsonPatchDocument<OrderItemDto> OrderItem { get; set; }
    }
}