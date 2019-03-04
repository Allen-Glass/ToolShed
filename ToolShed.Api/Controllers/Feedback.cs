using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;
using Toolshed.Models.Scheduler;

namespace ToolShed.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Feedback : ControllerBase
    {
        //[HttpPost("add")]
        //public async Task<IActionResult> AddFeebackAsync([FromBody] UserFeedback userFeedback)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest("The user Feedback is incomplete");

        //    try
        //    {
        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}
