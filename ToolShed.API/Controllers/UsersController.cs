using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToolShed.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUserAsync([FromBody] UserInformation userInformation)
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
        public async Task<IActionResult> GetUserDetailsAsync(string userId)
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
        public async Task<IActionResult> UpdateUserAccountAsync([FromBody] UserInformation userInformation)
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

        [HttpDelete("unregister/{userId}")]
        public async Task<IActionResult> DeleteUserAsync(string userId)
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
