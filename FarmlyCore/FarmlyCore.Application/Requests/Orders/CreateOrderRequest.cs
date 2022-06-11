using FarmlyCore.Application.DTOs.Order;

namespace FarmlyCore.Application.Requests.Orders
{
    public class CreateOrderRequest
    {
        public CreateOrderRequest(CreateOrderDto order)
        {
            Order = order ?? throw new ArgumentNullException(nameof(order)); ;
        }

        public CreateOrderDto Order { get; }
    }
}