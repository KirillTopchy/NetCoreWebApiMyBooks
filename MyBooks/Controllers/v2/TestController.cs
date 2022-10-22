﻿using Microsoft.AspNetCore.Mvc;

namespace MyBooks.Controllers.v2
{
    [ApiVersion("2.0")]
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet("get-test-dataV2")]
        public IActionResult Get()
        {
            return Ok("This is TestController V2");
        }
    }
}
