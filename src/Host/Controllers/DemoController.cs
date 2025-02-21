using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers
{
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    public class DemoController : VersionedApiController
    {
        public DemoController()
        {
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Hello World!");
        }
    }
}