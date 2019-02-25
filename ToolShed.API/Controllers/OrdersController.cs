using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;
using Toolshed.Models.Scheduler;

namespace ToolShed.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> PlaceOrderAsync(Reservation reservation, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("The information submitted is not in the appropriate format");
            }

            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}