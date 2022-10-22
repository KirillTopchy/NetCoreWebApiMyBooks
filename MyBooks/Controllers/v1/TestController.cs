using Microsoft.AspNetCore.Mvc;

namespace MyBooks.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet("get-test-dataV1")]
        public IActionResult Get()
        {
            return Ok("This is TestController V1");
        }
    }
}
