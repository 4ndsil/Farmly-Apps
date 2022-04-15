using FarmlyCore.Application.DTOs.Adverts;
using FarmlyCore.Application.Requests.Adverts;
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
    [Route("advert-access/advert")]
    public class AdvertController : Controller
    {
        private readonly ILogger<CustomerController> _logger;

        public AdvertController(ILogger<CustomerController> logger)
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
            var customer = await handler.HandleAsync(new GetAdvertRequest(advertId), cancellationToken);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        [HttpPost]
        [Route("find")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(AdvertDto[]), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FindAdverts(
        [FromServices] IQueryHandler<FindAdvertsRequest, AdvertDto[]> handler,
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
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(AdvertDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAdvert(
            [FromServices] IQueryHandler<CreateAdvertRequest, AdvertDto> handler,
            [FromBody] AdvertDto advert,
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
        [Consumes("application/json-patch+json")]
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
    }
}
