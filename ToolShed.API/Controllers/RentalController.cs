using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToolShed.Models.API;
using ToolShed.RentingServices.Interfaces;

namespace ToolShed.API.Controllers
{
    public class RentalController : Controller
    {
        private readonly IRentalService rentalService;

        public RentalController(IRentalService rentalService)
        {
            this.rentalService = rentalService;
        }

        [HttpPost("process")]
        public async Task<IActionResult> ProcessNewRentalAsync(Rental rental)
        {
            if (!ModelState.IsValid)
                return BadRequest("The rental information is incomplete");

            await rentalService.PlaceRentalAsync(rental);
            return Ok();
        }

        [HttpGet("start")]
        public async Task<IActionResult> StartRentalAsync(Rental rental)
        {
            if (!ModelState.IsValid)
                return BadRequest("The rental information is incomplete");

            await rentalService.StartRentalAsync(rental);
            return Ok();
        }

        [HttpGet("{rentalId}")]
        public async Task<IActionResult> GetCurrentRentalAsync(string rentalId)
        {
            if (!ModelState.IsValid)
                return BadRequest("The rental information is incomplete");

            await rentalService.CheckRentalStatusAsync(new Guid(rentalId));
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> CompleteRentalAsync(Guid rentalId)
        {
            if (!ModelState.IsValid)
                return BadRequest("The rental information is incomplete");

            await rentalService.CompleteRentalAsync(rentalId);
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