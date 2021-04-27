using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace question.api.Controllers
{
    [Route("/share")]
    [ApiController]
    public class ShareController : Controller
    {
        [HttpPost]
        [Produces("application/json")]
        public IActionResult Post([FromQueryAttribute] string destination_email, [FromQueryAttribute] string content_url)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(destination_email);
            if (match.Success)
                return Ok(new { status = "ok" });
            else
            {
                return BadRequest(new { status = "Bad Request. Either destination_email not valid or empty content_url" });
            }
        }
    }
}