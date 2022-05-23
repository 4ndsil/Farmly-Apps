using FarmlyCore.Application.DTOs.Customer;
using FarmlyCore.Application.DTOs.Order;
using FarmlyCore.Application.Requests.Orders;
using FarmlyCore.Infrastructure.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;

namespace FarmlyCore.Api.ControllerEndpoints
{
    [ApiController]
    [Route("order-access/order")]
    public class OrderController : Controller
    {
        private readonly ILogger<OrderController> _logger;

        public OrderController(ILogger<OrderController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("{orderId:int:min(1)}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(OrderDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOrder(
        [FromServices] IQueryHandler<GetOrderRequest, OrderDto> handler,
        [Range(1, int.MaxValue), FromRoute] int orderId,
        CancellationToken cancellationToken)
        {
            var customer = await handler.HandleAsync(new GetOrderRequest(orderId), cancellationToken);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        [HttpPost]
        [Route("find")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(OrderDto[]), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FindOrders(
        [FromServices] IQueryHandler<FindOrdersRequest, IReadOnlyList<OrderDto>> handler,
        [FromBody] FindOrdersRequest request,
        CancellationToken cancellationToken)
        {
            var response = await handler.HandleAsync(request, cancellationToken);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }
    }
}