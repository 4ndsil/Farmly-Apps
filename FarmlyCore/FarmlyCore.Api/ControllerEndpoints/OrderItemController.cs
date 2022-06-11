using FarmlyCore.Application.DTOs.Order;
using FarmlyCore.Application.Requests.OrderItems;
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
    [Route("orderItem-access/orderItem")]
    [OpenApiTag("orderItem")]
    public class OrderItemController : Controller
    {
        private readonly ILogger<OrderController> _logger;

        public OrderItemController(ILogger<OrderController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost]
        [Route("find")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(OrderDto[]), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FindOrderItems(
        [FromServices] IQueryHandler<FindOrderItemsRequest, IReadOnlyList<OrderItemDto>> handler,
        [FromBody] FindOrderItemsRequest request,
        CancellationToken cancellationToken)
        {
            var response = await handler.HandleAsync(request, cancellationToken);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpPatch]
        [Route("{orderItemId:int:min(1)}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(OrderItemDto), StatusCodes.Status200OK)]
        [Consumes("application/json-patch+json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status415UnsupportedMediaType)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> RespondToOrderItem(
        [FromServices] IQueryHandler<RespondToOrderItemRequest, OrderItemDto> handler,
        [Range(1, int.MaxValue), FromRoute] int orderItemId,
        [FromBody] JsonPatchDocument<OrderItemDto> patchDocument,
        CancellationToken cancellationToken)
        {
            var response = await handler.HandleAsync(new RespondToOrderItemRequest(orderItemId, patchDocument), cancellationToken);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }
    }
}
