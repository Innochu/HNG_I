using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.NetworkInformation;

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
            string ipAddress = GetServerPrivateIP();


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
            return Ok(new { message = $"{greeting}, {model.Name}", ipAddress = $"Your IP Address is {ipAddress}" });
        }

        public static string GetServerPrivateIP()
        {
            string privateIP = "";
            NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();

            foreach (NetworkInterface network in networkInterfaces)
            {
                if (network.OperationalStatus == OperationalStatus.Up &&
                    network.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                {
                    IPInterfaceProperties properties = network.GetIPProperties();

                    foreach (UnicastIPAddressInformation address in properties.UnicastAddresses)
                    {
                        if (address.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork &&
                            !IPAddress.IsLoopback(address.Address))
                        {
                            privateIP = address.Address.ToString();
                            return privateIP;
                        }
                    }
                }
            }

            return privateIP;
        }
    }
}