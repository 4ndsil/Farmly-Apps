using FarmlyCore.Application.DTOs.Customer;
using FarmlyCore.Application.Queries.Requests.Advert;
using FarmlyCore.Application.Queries.Requests.Customer;
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
    [Route("customer-access/customer")]
    public class CustomerController : Controller
    {
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(ILogger<CustomerController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("{customerId:int:min(1)}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(CustomerDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCustomer(
            [FromServices] IQueryHandler<GetOrderRequest, CustomerDto> handler,
            [Range(1, int.MaxValue), FromRoute] int customerId,
            CancellationToken cancellationToken)
        {
            var response = await handler.HandleAsync(new GetOrderRequest(customerId), cancellationToken);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpGet("find")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(CustomerDto[]), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FindCustomers(
            [FromServices] IQueryHandler<FindOrdersRequest, CustomerDto[]> handler,
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
        [ProducesResponseType(typeof(CustomerDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateCustomers(
            [FromServices] IQueryHandler<CreateAdvertRequest, CustomerDto> handler,
            [FromBody] CustomerDto customer,
            CancellationToken cancellationToken)
        {
            var response = await handler.HandleAsync(new CreateCustomerRequest(customer), cancellationToken);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpGet("{customerId:int:min(1)}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(CustomerDto), StatusCodes.Status200OK)]
        [Consumes("application/json-patch+json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status415UnsupportedMediaType)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> UpdateCustomer(
            [FromServices] IQueryHandler<UpdateAdvertRequest, CustomerDto> handler,
            [Range(1, int.MaxValue), FromRoute] int customerId,
            [FromBody] JsonPatchDocument<CustomerDto> patchDocument,
            CancellationToken cancellationToken)
        {
            try
            {
                var response = await handler.HandleAsync(new UpdateCustomerRequest(customerId, patchDocument), cancellationToken);

                return Ok(response);
            }

            catch (JsonPatchException ex)
            {
                _logger.LogError($"Unable to process customerId: {customerId}", ex);

                return UnprocessableEntity();
            }
        }
    }
}
