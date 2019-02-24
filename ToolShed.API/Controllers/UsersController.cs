using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Toolshed.Models.User;

namespace ToolShed.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUserAsync([FromBody] User userInformation)
        {
            if (!ModelState.IsValid)
                return BadRequest("The user information is incomplete");

            try
            {
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserDetailsAsync(string userId)
        {
            if (string.IsNullOrEmpty("userId"))
                return BadRequest("The user information is incomplete");

            try
            {
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateUserAccountAsync([FromBody] User userInformation)
        {
            if (!ModelState.IsValid)
                return BadRequest("The user information is incomplete");

            try
            {
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete("unregister/{userId}")]
        public async Task<IActionResult> DeleteUserAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return BadRequest("The user information is incomplete");

            try
            {
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
