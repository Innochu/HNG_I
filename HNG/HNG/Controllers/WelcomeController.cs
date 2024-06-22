using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.NetworkInformation;

namespace HNG.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WelcomeController : ControllerBase
    {


        [HttpGet]
        public IActionResult Intro([FromQuery] Model model)
        {
            string name = model?.Name ?? "Guest";
            string ipAddress = GetServerPrivateIP();

            // Get the current hour (UTC)
            int hour = DateTime.UtcNow.Hour;

            // Choose greeting based on time
            string greeting = hour switch
            {
                < 12 => "Good morning",
                < 17 => "Good afternoon",
                _ => "Good evening"
            };

            // Return a JSON object with greeting and IP address
            return Ok(new
            {
                message = $"{greeting}, {name}!",
                ipAddress = $"Your Private IP Address is {ipAddress}"
            });
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