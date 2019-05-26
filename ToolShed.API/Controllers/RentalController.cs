using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToolShed.Models.API;
using ToolShed.Services.Interfaces;

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

        [HttpPut("return")]
        public async Task<IActionResult> ReturnRentalAsync(Rental rental)
        {
            if (rental == null)
                throw new ArgumentNullException();

            await rentingService.ReturnRentalItemAsync(rental);
            return Ok();
        }

        [HttpPut("complete/{rentalId}")]
        public async Task<IActionResult> CompleteRentalAsync(Guid rentalId)
        {
            if (!ModelState.IsValid)
                return BadRequest("The rental information is incomplete");

            await rentingService.CompleteRentalAsync(rentalId);
            return Ok();
        }
    }
}