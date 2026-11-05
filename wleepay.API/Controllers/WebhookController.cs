using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Stripe;
using wleepay.Core.Services;
using wleepay.Services;

namespace wleepay.Controllers
{
    [ApiController]
    [Route("api/webhook")]
    public class StripeWebhookController : ControllerBase
    {
        // Only these two events are processed.
        private static readonly HashSet<string> HandledEvents = new HashSet<string>
        {
            "checkout.session.completed",
            "payment_intent.amount_capturable_updated",
            "payment_intent.payment_failed"
        };

        private readonly IWebhookQueue _queue;
        private readonly ILogger<StripeWebhookController> _logger;
        private readonly string _webhookSecret;

        public StripeWebhookController(
            IWebhookQueue queue,
            IConfiguration configuration,
            ILogger<StripeWebhookController> logger)
        {
            _queue = queue;
            _logger = logger;
            _webhookSecret = configuration["Stripe:WebhookSecret"];
        }

        [HttpPost("stripe")]
        public async Task<IActionResult> Stripe()
        {
            string json;
            using (var reader = new StreamReader(Request.Body))
                json = await reader.ReadToEndAsync();

            var signature = Request.Headers["Stripe-Signature"].ToString();

            if (string.IsNullOrEmpty(json) || string.IsNullOrEmpty(signature))
            {
                _logger.LogWarning("Stripe webhook received with missing body or signature.");
                return BadRequest("Missing body or signature.");
            }

            Event stripeEvent;
            try
            {
                stripeEvent = EventUtility.ConstructEvent(
                    json, signature, _webhookSecret, throwOnApiVersionMismatch: false);
            }
            catch (StripeException ex)
            {
                _logger.LogWarning(ex, "Stripe webhook signature verification failed.");
                return BadRequest("Invalid signature.");
            }

            if (!HandledEvents.Contains(stripeEvent.Type))
            {
                _logger.LogInformation("Ignored Stripe event {Type} ({Id}).", stripeEvent.Type, stripeEvent.Id);
                return Ok();
            }

            try
            {
                _queue.Enqueue(new WebhookJob(json, stripeEvent.Id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Could not enqueue Stripe event {Id}.", stripeEvent.Id);
                return StatusCode(500); // Stripe will retry
            }

            return Ok();
        }
    }
}
