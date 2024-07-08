using FirebaseAdmin.Messaging;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using wleepay.API.Resources;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using wleepay.Core.DTO.Request;


namespace wleepay.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]

    public class FirebaseMessageController : Controller
    {

        public FirebaseMessageController() { }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> SendMessageAsync([FromBody] FirebaseMessageRequest request)
        {
            var message = new Message()
            {
                Notification = new Notification
                {
                    Title = request.Title,
                    Body = request.Body,
                },
                Data = new Dictionary<string, string>()
                {
                    ["FirstName"] = "John",
                    ["LastName"] = "Doe"
                },
                Token = request.DeviceToken
            };

            var messaging = FirebaseMessaging.DefaultInstance;
            var result = await messaging.SendAsync(message);

            if (!string.IsNullOrEmpty(result))
            {
                // Message was sent successfully
                return Ok("Message sent successfully!");
            }
            else
            {
                // There was an error sending the message
                throw new Exception("Error sending the message.");
            }
        }
    }
}
