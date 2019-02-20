using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Toolshed.Models.Scheduler;

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

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("{reservationId})")]
        public async Task<IActionResult> GetReservationsAsync(string reservationId)
        {
            if (!ModelState.IsValid)
                return BadRequest("The user information is incomplete");

            try
            {

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("{userId})")]
        public async Task<IActionResult> GetReservationAsync(string userId)
        {
            if (!ModelState.IsValid)
                return BadRequest("The user information is incomplete");

            try
            {

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateReservationAsync([FromBody] Reservation reservation)
        {
            if (!ModelState.IsValid)
                return BadRequest("The user information is incomplete");

            try
            {

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete("{reservationId}")]
        public async Task<IActionResult> CancelReservationAsync(string reservationId)
        {
            if (!ModelState.IsValid)
                return BadRequest("The user information is incomplete");

            try
            {

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
