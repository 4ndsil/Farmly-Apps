using FarmlyCore.Application.DTOs.Order;
using FarmlyCore.Application.Requests.Orders;
using FarmlyCore.Infrastructure.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
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
         [Range(1, int.MaxValue), FromRoute] int advertId,
         CancellationToken cancellationToken)
        {
            var customer = await handler.HandleAsync(new GetOrderRequest(advertId), cancellationToken);

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
        public async Task<IActionResult> FindOrder(
        [FromServices] IQueryHandler<FindOrdersRequest, OrderDto[]> handler,
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
            [FromServices] IQueryHandler<CreateOrderRequest, OrderDto> handler,
            [FromBody] OrderDto advert,
            CancellationToken cancellationToken)
        {
            var response = await handler.HandleAsync(new CreateOrderRequest(advert), cancellationToken);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpPatch]
        [Route("{orderId:int:min(1)}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(OrderDto), StatusCodes.Status200OK)]
        [Consumes("application/json-patch+json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status415UnsupportedMediaType)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> UpdateOrder(
            [FromServices] IQueryHandler<UpdateOrderRequest, OrderDto> handler,
            [Range(1, int.MaxValue), FromRoute] int advertId,
            [FromBody] JsonPatchDocument<OrderDto> patchDocument,
            CancellationToken cancellationToken)
        {
            try
            {
                var response = await handler.HandleAsync(new UpdateOrderRequest(advertId, patchDocument), cancellationToken);

                return Ok(response);
            }

            catch (JsonPatchException ex)
            {
                _logger.LogError($"Unable to process advertId: {advertId}", ex);

                return UnprocessableEntity();
            }
        }
    }
}
