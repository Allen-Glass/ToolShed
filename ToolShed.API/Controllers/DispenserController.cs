using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Toolshed.Models.Dispensers;
using Toolshed.Models.Tools;
using ToolShed.Repository.Interfaces;

namespace ToolShed.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DispenserController : ControllerBase
    {
        private readonly IDispenserSQLService dispenserSQLService;

        public DispenserController(IDispenserSQLService dispenserSQLService)
        {
            this.dispenserSQLService = dispenserSQLService;
        }

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
