using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToolShed.Models.API;
using ToolShed.Renting.Interfaces;

namespace ToolShed.API.Controllers
{
    [Route("api/[controller]")]
    public class RentalController : Controller
    {
        private readonly IRentingService rentingService;

        public RentalController(IRentingService rentingService)
        {
            this.rentingService = rentingService;
        }

        [HttpPost("place")]
        public async Task<IActionResult> PlaceNewRentalAsync(Rental rental)
        {
            if (!ModelState.IsValid)
                return BadRequest("The rental information is incomplete");

            await rentingService.PlaceRentalAsync(rental);
            return Ok();
        }

        [HttpGet("start")]
        public async Task<IActionResult> StartRentalAsync(Rental rental)
        {
            if (!ModelState.IsValid)
                return BadRequest("The rental information is incomplete");

            await rentingService.StartRentalAsync(rental);
            return Ok();
        }

        [HttpGet("{rentalId}")]
        public async Task<IActionResult> GetCurrentRentalAsync(string rentalId)
        {
            if (!ModelState.IsValid)
                return BadRequest("The rental information is incomplete");

            await rentingService.CheckRentalStatusAsync(new Guid(rentalId));
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> CompleteRentalAsync(Guid rentalId)
        {
            if (!ModelState.IsValid)
                return BadRequest("The rental information is incomplete");

            await rentingService.CompleteRentalAsync(rentalId);
            return Ok();
        }

        [HttpDelete("remove/{cardId}")]
        public async Task<IActionResult> RemoveNewCardAsync(string cardId)
        {
            if (!ModelState.IsValid)
                return BadRequest("The rental information is incomplete");

            return Ok();
        }
    }
}