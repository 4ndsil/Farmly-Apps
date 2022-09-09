using FarmlyCore.Application.DTOs.Adverts;
using FarmlyCore.Application.Requests.AdvertItems;
using FarmlyCore.Infrastructure.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Exceptions;
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
    [Route("advertItem-access/advertItem")]
    [OpenApiTag("advertItem")]
    public class AdvertItemController : Controller
    {
        private readonly ILogger<AdvertController> _logger;

        public AdvertItemController(ILogger<AdvertController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost]
        [Route("find")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(AdvertItemDto[]), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FindAdvertItems(
        [FromServices] IQueryHandler<FindAdvertItemsRequest, AdvertItemDto[]> handler,
        [FromBody] FindAdvertItemsRequest request,
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
        [Route("{advertItemId:int:min(1)}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(AdvertItemDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status415UnsupportedMediaType)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> UpdateAdvert(
        [FromServices] IQueryHandler<UpdateAdvertItemRequest, AdvertItemDto> handler,
        [Range(1, int.MaxValue), FromRoute] int advertItemId,
        [FromBody] JsonPatchDocument<AdvertItemDto> patchDocument,
        CancellationToken cancellationToken)
        {
            try
            {
                var response = await handler.HandleAsync(new UpdateAdvertItemRequest(advertItemId, patchDocument), cancellationToken);

                return Ok(response);
            }

            catch (JsonPatchException ex)
            {
                _logger.LogError($"Unable to process advertId: {advertItemId}", ex);

                return UnprocessableEntity();
            }
        }
    }
}
