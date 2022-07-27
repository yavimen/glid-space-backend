using Microsoft.AspNetCore.Mvc;

namespace Main.API.Controllers
{
    [ApiController]
    [Route("dev")]
    public class DevController : ControllerBase
    {
        /// <summary>
        /// Test endpoint
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok("Hello World!");
        }
    }
}