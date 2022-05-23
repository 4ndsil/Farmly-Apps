using FarmlyCore;
using Microsoft.AspNetCore.Mvc;

namespace FarmlyOrderManagement.Controllers
{
    [ApiController]
    [Route("[order-management]")]
    public class OrderManagementController : ControllerBase
    {
        private readonly ILogger<OrderManagementController> _logger;

        public OrderManagementController(ILogger<OrderManagementController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(typeof(OrderDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ManageOrder(OrderDto order)
        {
           
        }
    }
}