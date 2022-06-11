using FarmlyCore.Application.DTOs;
using FarmlyCore.Application.Requests.Categories;
using FarmlyCore.Infrastructure.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;

namespace FarmlyCore.Api.ControllerEndpoints
{

    [ApiController]
    [Route("category-access/category")]
    [OpenApiTag("category")]
    public class CategoryController : Controller
    {
        [HttpGet("{categoryId:int:min(1)}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCategory(
        [FromServices] IQueryHandler<GetCategoryRequest, CategoryDto> handler,
        [Range(1, int.MaxValue), FromRoute] int categoryId,
        CancellationToken cancellationToken)
        {
            var response = await handler.HandleAsync(new GetCategoryRequest(categoryId), cancellationToken);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(CategoryDto[]), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCategories(
        [FromServices] IQueryHandler<CategoryDto[]> handler, 
        CancellationToken cancellationToken)
        {
            var response = await handler.HandleAsync(cancellationToken);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }
    }
}