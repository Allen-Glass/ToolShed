using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Toolshed.Models.Dispensers;
using ToolShed.IotHub.Interfaces;
using ToolShed.Models.API;
using ToolShed.Repository.Interfaces;

namespace ToolShed.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DispenserController : ControllerBase
    {
        private readonly IDispenserSQLService dispenserSQLService;
        private readonly IIotActionServices iotActionServices;

        public DispenserController(IDispenserSQLService dispenserSQLService
            , IIotActionServices iotActionServices)
        {
            this.dispenserSQLService = dispenserSQLService;
            this.iotActionServices = iotActionServices;
        }

        [HttpGet]
        [EnableCors("Dispenser")]
        public async Task<IActionResult> stuff()
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

        [HttpPost("register")]
        [EnableCors("Dispenser")]
        public async Task<IActionResult> RegisterNewDispenser([FromBody] Dispenser dispenser)
        {
            if (!ModelState.IsValid)
                return BadRequest("The user information is incomplete");

            try
            {
                await dispenserSQLService.RegisterNewDispenserAsync(dispenser);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPost("sendaction")]
        [EnableCors("Dispenser")]
        public async Task<IActionResult> SendDispenserMessage([FromBody] UserMessage message)
        {
            if (!ModelState.IsValid)
                return BadRequest("The user information is incomplete");

            try
            {
                var combinedString = message.FirstName + message.LastName;
                await iotActionServices.SendMessageToDispenser("MAH_PIE", combinedString);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpDelete("delete")]
        [EnableCors("Dispenser")]
        public async Task<IActionResult> GetAllDispensers()
        {
            if (!ModelState.IsValid)
                return BadRequest("The user information is incomplete");

            try
            {
                await dispenserSQLService.GetAllDispensers();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}
