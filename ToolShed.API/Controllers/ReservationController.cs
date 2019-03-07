using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Toolshed.Models.API;

namespace ToolShed.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        [HttpPost("new")]
        public async Task<IActionResult> CreateReservationAsync(Reservation reservation)
        {
            if (!ModelState.IsValid)
                return BadRequest("The user information is incomplete");

            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet("reservation/{reservationId}")]
        public async Task<IActionResult> GetReservationAsync(string reservationId)
        {
            if (!ModelState.IsValid)
                return BadRequest("The user information is incomplete");

            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet("{userId})")]
        public async Task<IActionResult> GetReservationsAsync(string userId)
        {
            if (!ModelState.IsValid)
                return BadRequest("The user information is incomplete");

            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateReservationAsync([FromBody] Reservation reservation)
        {
            if (!ModelState.IsValid)
                return BadRequest("The user information is incomplete");

            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpDelete("{reservationId}")]
        public async Task<IActionResult> CancelReservationAsync(string reservationId)
        {
            if (!ModelState.IsValid)
                return BadRequest("The user information is incomplete");

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
