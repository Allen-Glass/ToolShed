using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToolShed.Models.API;
using ToolShed.Services.Interfaces.UserCart;

namespace ToolShed.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserCartController : ControllerBase
    {
        private readonly IUserCartService userCartService;

        public UserCartController(IUserCartService userCartService)
        {
            this.userCartService = userCartService;
        }

        [HttpPost("save")]
        public async Task<IActionResult> SaveCartAsync([FromBody] UserCart userCart)
        {
            if (!ModelState.IsValid)
                return BadRequest("Request is missing information");

            await userCartService.SaveUserCartAsync(userCart);
            return Ok();
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserCartAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                throw new ArgumentNullException();

            var userIdGuid = new Guid(userId);
            var userCart = await userCartService.GetUserCartAsync(userIdGuid);

            return Ok(userCart);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserItemCount(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                throw new ArgumentNullException();

            var userIdGuid = new Guid(userId);
            var itemCount = await userCartService.GetUserItemCount(userIdGuid);

            return Ok(itemCount);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateItemsInUserCart([FromBody] UserCart userCart)
        {
            if (!ModelState.IsValid)
                return BadRequest("Request is missing information");

            await userCartService.UpdateItemsInUserCartAsync(userCart);
            return Ok();
        }

        [HttpDelete("userId")]
        public async Task<IActionResult> DeleteUserCartAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                throw new ArgumentNullException();

            await userCartService.DeleteUserCartAsync(new Guid(userId));
            return Ok();
        }
    }
}
