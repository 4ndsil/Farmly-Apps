using FarmlyCore.Application.DTOs.Customer;
using FarmlyCore.Application.Requests.Customers;
using FarmlyCore.Infrastructure.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Exceptions;
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
            [FromServices] IQueryHandler<GetCustomerRequest, CustomerDto> handler,
            [Range(1, int.MaxValue), FromRoute] int customerId,
            CancellationToken cancellationToken)
        {
            var response = await handler.HandleAsync(new GetCustomerRequest(customerId), cancellationToken);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpPost]
        [Route("find")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(CustomerDto[]), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> FindCustomers(
            [FromServices] IQueryHandler<FindCustomersRequest, CustomerDto[]> handler,
            [FromBody] FindCustomersRequest request,
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
        public async Task<IActionResult> CreateCustomer(
            [FromServices] IQueryHandler<CreateCustomerRequest, CustomerDto> handler,
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

        [HttpPatch]
        [Route("{customerId:int:min(1)}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(CustomerDto), StatusCodes.Status200OK)]
        [Consumes("application/json-patch+json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status415UnsupportedMediaType)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> UpdateCustomer(
            [FromServices] IQueryHandler<UpdateCustomerRequest, CustomerDto> handler,
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

        [HttpGet("{customerId:int:min(1)}/addresses")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(CustomerDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCustomerAddresses(
            [FromServices] IQueryHandler<GetCustomerAddressesRequest, IReadOnlyList<CustomerAddressDto>> handler,
            [Range(1, int.MaxValue), FromRoute] int customerId,
            CancellationToken cancellationToken)
        {
            var response = await handler.HandleAsync(new GetCustomerAddressesRequest(customerId), cancellationToken);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpPost]
        [Route("{customerId:int:min(1)}/addresses")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(CustomerAddressDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateCustomerAddress(
            [FromServices] IQueryHandler<CreateCustomerAddressRequest, CustomerAddressDto> handler,
            [FromBody] CustomerAddressDto customerAddress,
            [Range(1, int.MaxValue), FromRoute] int customerId,
            CancellationToken cancellationToken)
        {
            var response = await handler.HandleAsync(new CreateCustomerAddressRequest(customerAddress, customerId), cancellationToken);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpPatch]
        [Route("{customerId:int:min(1)}/addresses/{customerAddressId}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(CustomerAddressDto), StatusCodes.Status200OK)]
        [Consumes("application/json-patch+json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status415UnsupportedMediaType)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> UpdateCustomerAddress(
        [FromServices] IQueryHandler<UpdateCustomerAddressRequest, CustomerAddressDto> handler,
        [Range(1, int.MaxValue), FromRoute] int customerId,
        [Range(1, int.MaxValue), FromRoute] int customerAddressId,
        [FromBody] JsonPatchDocument<CustomerAddressDto> patchDocument,
         CancellationToken cancellationToken)
        {
            var response = await handler.HandleAsync(new UpdateCustomerAddressRequest(customerId, customerAddressId, patchDocument), cancellationToken);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpGet("{customerId:int:min(1)}/users")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUser(
        [FromServices] IQueryHandler<GetUserRequest, UserDto> handler,
        string password,
        CancellationToken cancellationToken)
        {
            var response = await handler.HandleAsync(new GetUserRequest(password), cancellationToken);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpPatch]
        [Route("{customerId:int:min(1)}/users/{userId}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [Consumes("application/json-patch+json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status415UnsupportedMediaType)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> UpdateUser(
        [FromServices] IQueryHandler<UpdateUserRequest, UserDto> handler,
        [Range(1, int.MaxValue), FromRoute] int customerId,
        [Range(1, int.MaxValue), FromRoute] int customerAddressId,
        [FromBody] JsonPatchDocument<UserDto> patchDocument,
        CancellationToken cancellationToken)
        {
            var response = await handler.HandleAsync(new UpdateUserRequest(customerId, customerAddressId, patchDocument), cancellationToken);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }
    }
}