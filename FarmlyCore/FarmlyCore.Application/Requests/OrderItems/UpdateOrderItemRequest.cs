using FarmlyCore.Application.DTOs.Order;
using Microsoft.AspNetCore.JsonPatch;

namespace FarmlyCore.Application.Requests.OrderItems
{
    public class UpdateOrderItemRequest
    {
        public UpdateOrderItemRequest(int orderItemId, JsonPatchDocument<OrderItemDto> orderItem)
        {
            OrderItemId = orderItemId;
            OrderItem = orderItem ?? throw new ArgumentNullException(nameof(orderItem));
        }

        public int OrderItemId { get; set; }
        public JsonPatchDocument<OrderItemDto> OrderItem { get; set; }
    }
}
