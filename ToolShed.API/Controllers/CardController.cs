using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToolShed.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        [HttpPost("new")]
        public async Task<IActionResult> AddNewCreditCardAsync(Card card)
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

        [HttpGet("card/{cardId}")]
        public async Task<IActionResult> GetCurrentCardInformationAsync(string cardId)
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

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCurrentCardInformationAsync(string userId)
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

        [HttpPut]
        public async Task<IActionResult> UpdateCardInformationAsync(Card card)
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

        [HttpDelete("remove")]
        public async Task<IActionResult> RemoveNewCardAsync(Card card)
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
