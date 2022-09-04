using FarmlyCore.Application.DTOs.Customer;
using FarmlyCore.Application.DTOs.Order;
using FarmlyCore.Application.Queries.Orders;
using FarmlyCore.Application.Requests.Orders;
using FarmlyCore.Infrastructure.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSwag.Annotations;
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
    [OpenApiTag("order")]
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

        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(OrderDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateOrder(
        [FromServices] IQueryHandler<CreateOrderRequest, CreateOrderResponse> handler,
        [FromBody] CreateOrderDto order,
        CancellationToken cancellationToken)
        {
            var response = await handler.HandleAsync(new CreateOrderRequest(order), cancellationToken);

            if (response.Detail.HasValue)
            {
                return response.Detail.Value switch
                {
                    CreateOrderProblemDetail.ConcurrencyConflict => Forbid(),
                    CreateOrderProblemDetail.ConcurrecyFailure => Forbid(),
                    CreateOrderProblemDetail.AddressNotFound => NotFound(),
                    CreateOrderProblemDetail.AdvertItemsNotFound => NotFound(),
                    _ => StatusCode(StatusCodes.Status500InternalServerError)
                };
            }

            return Ok(response.Order);
        }
    }
}