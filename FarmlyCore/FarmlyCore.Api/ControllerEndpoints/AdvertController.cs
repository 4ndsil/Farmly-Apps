using FarmlyCore.Application.DTOs.Adverts;
using FarmlyCore.Application.Paging;
using FarmlyCore.Application.Requests.Adverts;
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
    [Route("advert-access/advert")]
    [OpenApiTag("advert")]
    public class AdvertController : Controller
    {
        private readonly ILogger<AdvertController> _logger;

        public AdvertController(ILogger<AdvertController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("{advertId:int:min(1)}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(AdvertDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAdvert(
         [FromServices] IQueryHandler<GetAdvertRequest, AdvertDto> handler,
         [Range(1, int.MaxValue), FromRoute] int advertId,
         CancellationToken cancellationToken)
        {
            var response = await handler.HandleAsync(new GetAdvertRequest(advertId), cancellationToken);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(AdvertDto[]), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllAdverts(
        [FromServices] IQueryHandler<AdvertDto[]> handler,
        CancellationToken cancellationToken)
        {
            var response = await handler.HandleAsync(cancellationToken);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpPost]
        [Route("find")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(PagedResponse<IReadOnlyList<AdvertDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FindAdverts(
        [FromServices] IQueryHandler<FindAdvertsRequest, PagedResponse<IReadOnlyList<AdvertDto>>> handler,
        [FromBody] FindAdvertsRequest request,
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
        [ProducesResponseType(typeof(AdvertDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAdvert(
            [FromServices] IQueryHandler<CreateAdvertRequest, AdvertDto> handler,
            [FromBody] CreateAdvertDto advert,
            CancellationToken cancellationToken)
        {
            var response = await handler.HandleAsync(new CreateAdvertRequest(advert), cancellationToken);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpPatch]
        [Route("{advertId:int:min(1)}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(AdvertDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status415UnsupportedMediaType)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> UpdateAdvert(
            [FromServices] IQueryHandler<UpdateAdvertRequest, AdvertDto> handler,
            [Range(1, int.MaxValue), FromRoute] int advertId,
            [FromBody] JsonPatchDocument<AdvertDto> patchDocument,
            CancellationToken cancellationToken)
        {
            try
            {
                var response = await handler.HandleAsync(new UpdateAdvertRequest(advertId, patchDocument), cancellationToken);

                return Ok(response);
            }

            catch (JsonPatchException ex)
            {
                _logger.LogError($"Unable to process advertId: {advertId}", ex);

                return UnprocessableEntity();
            }
        }

        [HttpPost]
        [Route("{advertId:int:min(1)}/advertItems")]
        [ProducesResponseType(typeof(AdvertItemDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAdvertItem(
            [FromServices] IQueryHandler<CreateAdvertItemRequest, AdvertItemDto> handler,           
            [Range(1, int.MaxValue), FromRoute] int advertId,
            [FromBody] CreateAdvertItemDto advertItem,
            CancellationToken cancellationToken)
        {
            var response = await handler.HandleAsync(new CreateAdvertItemRequest(advertItem, advertId), cancellationToken);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpDelete("{advertId:int:min(1)}/advertItems/{advertItemId:int:min(1)}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAdvertItem(
            [FromServices] IQueryHandler<DeleteAdvertItemRequest, DeleteAdvertItemResult> handler,
            [Range(1, int.MaxValue), FromRoute] int advertItemId,
            CancellationToken cancellationToken)
        {
            var deleteAdvertItemResult = await handler.HandleAsync(new DeleteAdvertItemRequest(advertItemId), cancellationToken);

            return deleteAdvertItemResult.Problem switch
            {
                null => NoContent(),
                DeleteAdvertItemResult.ProblemDetails.AdvertItemNotFound => NotFound(),
                _ => StatusCode(StatusCodes.Status500InternalServerError)
            };
        }
    }
}
