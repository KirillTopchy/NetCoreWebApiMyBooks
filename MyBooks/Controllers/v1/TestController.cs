using Microsoft.AspNetCore.Mvc;

namespace MyBooks.Controllers.v1
{
    [ApiVersion("1.0")]
    [ApiVersion("1.1")]
    [ApiVersion("1.2")]
    [Route("api/[controller]")]
    //[Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet("get-test-dataV1")]
        public IActionResult GetV1()
        {
            return Ok("This is TestController V1");
        }

        [HttpGet("get-test-dataV1"), MapToApiVersion("1.1")]
        public IActionResult GetV11()
        {
            return Ok("This is TestController V1.1");
        }

        [HttpGet("get-test-dataV1"), MapToApiVersion("1.2")]
        public IActionResult GetV12()
        {
            return Ok("This is TestController V1.2");
        }
    }
}
