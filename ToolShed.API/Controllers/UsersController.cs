using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ToolShed.Models.API;
using ToolShed.Repository.Interfaces;
using ToolShed.Services.Interfaces;

namespace ToolShed.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILoginService loginService;

        public UsersController(ILoginService loginService)
        {
            this.loginService = loginService;
        }

        [HttpPost("create/account")]
        public async Task<IActionResult> CreateUserAsync([FromBody] User user)
        {
            if (!ModelState.IsValid)
                return BadRequest("The user information is incomplete");

            if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
                return BadRequest("The username or password is missing");

            try
            {
                await loginService.CreateNewAccountAsync(user);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> LogIntoUserAccountAsync(User user)
        {
            if (!ModelState.IsValid)
                return BadRequest("The user information is incomplete");

            try
            {
                await loginService.LogIntoAccountAsync(user);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserDetailsAsync(string userId)
        {
            if (string.IsNullOrEmpty("userId"))
                return BadRequest("The user information is incomplete");

            try
            {
                var user = await loginService.GetUserInformationAsync(new Guid(userId));
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet("password/reset/{email}")]
        public async Task<IActionResult> SendPasswordResetAsync(string email)
        {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentNullException();

            await loginService.SendPasswordResetEmailAsync(email);
            return Ok();
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdatePasswordAsync([FromBody] User user)
        {
            if (!ModelState.IsValid)
                return BadRequest("The user information is incomplete");

            try
            {
                await loginService.UpdateUserPasswordAsync(user.UserId, user.Password);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpDelete("unregister/{userId}")]
        public async Task<IActionResult> DeleteUserAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
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
