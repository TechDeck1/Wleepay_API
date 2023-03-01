using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using wleepay.API.Resources;

namespace wleepay.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class USSDController : Controller
    {
        [HttpPost]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<IActionResult> Index([FromForm] USSDResponse ussdResponse)
        {
            string response = string.Empty;
            string texReceived = ussdResponse.text == null ? string.Empty : ussdResponse.text.Trim();
            switch (texReceived)
            {
                case "":
                case "7":
                    response = "CON Welcome to Wlee USSD Serive. Please select option \n";
                    response += "1. Check Account balance \n";
                    response += "2. Send Money \n";
                    response += "3. Send Cashout request \n";
                    break;

                case "1":
                    response = "CON Your Account balance is $34US \n";
                    response += "7. Main\n";
                    response += "8. Exit\n";
                    break;

                case "2":
                    response = "CON Enter Phone number you want to send money \n";
                    response += "7. Main\n";
                    response += "8. Exit\n";
                    break;

                case "3":
                    response = $"CON Enter amount for your Cashout. \n";
                    response += "7. Main\n";
                    response += "8. Exit\n";
                    break;

                case "8":
                    response = "END Thanks for using Wlee Pay. \n";
                    break;

                default:
                    response = "END Invalid option \n";
                    break;

            }

            Response.Headers["ContentType"] = "text/plain";
            return Ok(response);
        }
    }
}
