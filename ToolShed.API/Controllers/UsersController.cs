﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Toolshed.Models.User;
using ToolShed.Repository.Interfaces;

namespace ToolShed.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserSQLService userSQLService;

        public UsersController(IUserSQLService userSQLService)
        {
            this.userSQLService = userSQLService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUserAsync([FromBody] User user)
        {
            if (!ModelState.IsValid)
                return BadRequest("The user information is incomplete");

            try
            {
                await userSQLService.StoreUserInformationAsync(user);
                return Ok();
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
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateUserAccountAsync([FromBody] User userInformation)
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
