﻿using Microsoft.AspNetCore.Mvc;

namespace MyBooks.Controllers.v2
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet("get-test-data")]
        public IActionResult Get()
        {
            return Ok("This is TestController V1");
        }
    }
}