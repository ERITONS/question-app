using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace question.api.Controllers
{
    [Route("/health")]
    [ApiController]
    public class HealthController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            Random random = new Random();
            int randomNumber = random.Next(0, 10);

            // Will return ok in 90% off calls 
            if (Enumerable.Range(0, 9).Contains(randomNumber))
                return Ok(new { status = "OK" });
            else
                return StatusCode(503, new { status = "Bad Request. Either destination_email not valid or empty content_url" });
        }
    }
}