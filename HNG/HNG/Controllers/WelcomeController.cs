using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HNG.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GreetingsController : ControllerBase
    {
      

        [HttpPost]
        public IActionResult Intro(Model model)
        {
            string greeting;
            // Get the client's IP address
            string ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();


            // Get the current hour
            int hour = DateTime.Now.Hour;

            // Choose greeting based on time
            if (hour < 12)
            {
                greeting = "Good morning!";
            }
            else if (hour < 17) 
            {
                greeting = "Good afternoon!";
            }
            else
            {
                greeting = "Good evening!";
            }

            // Return a JSON object with greeting and IP address
            return Ok(new { message = $"{greeting}, {model.Name}", ipAddress = $"Your Public IP Address is {ipAddress}" });
        }
    }
}
