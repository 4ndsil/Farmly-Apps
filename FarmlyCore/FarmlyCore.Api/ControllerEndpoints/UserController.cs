using FarmlyCore.Application.DTOs.Customer;
using FarmlyCore.Application.Requests.Users;
using FarmlyCore.Infrastructure.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSwag.Annotations;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;

namespace FarmlyCore.Api.ControllerEndpoints
{
    [ApiController]
    [Route("user-access/user")]
    [OpenApiTag("user")]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
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

        [HttpPost]
        [Route("find")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(UserDto[]), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FindUsers(
        [FromServices] IQueryHandler<FindUsersRequest, UserDto[]> handler,
        [FromBody] FindUsersRequest request,
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
        [Route("{userId:int:min(1)}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [Consumes("application/json-patch+json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status415UnsupportedMediaType)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> UpdateUser(
        [FromServices] IQueryHandler<UpdateUserRequest, UserDto> handler,
        [Range(1, int.MaxValue), FromRoute] int userId,        
        [FromBody] JsonPatchDocument<UserDto> patchDocument,
        CancellationToken cancellationToken)
        {
            var response = await handler.HandleAsync(new UpdateUserRequest(userId, patchDocument), cancellationToken);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }
    }
}