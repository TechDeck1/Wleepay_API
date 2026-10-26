using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using wleepay.API.Resources;
using wleepay.Core.DTO.Request;
using wleepay.Core.Models;
using wleepay.Core.Services;
using wleepay.Core.Services.wleepay.Services.Interfaces;

namespace wleepay.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StripeController : Controller
    {
        private const decimal FeeMultiplier = 1.08m;
        private const string TaxCode = "txcd_10000000";

        private readonly IConfiguration _configuration;
        private readonly IStripeService _stripeService;
        private readonly IProductService _productService;
        private readonly ISystemActivityLogsService _activityLogService;

        public StripeController(
            IConfiguration configuration,
            IStripeService stripeService,
            IProductService productService,
            ISystemActivityLogsService activityLogService)
        {
            _configuration = configuration;
            _stripeService = stripeService;
            _productService = productService;
            _activityLogService = activityLogService;
        }

        [HttpPost("createCheckoutSession-mobile")]
        [AllowAnonymous]
        public async Task<IActionResult> CheckoutSessionForMobileRecharge(MobileCheckoutPayload payload)
            {
            try
            {
                return await CreateProductCheckoutAsync(payload, RechargeFlows.Mobile);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
            catch (Stripe.StripeException ex)
            {
                await LogFailureAsync("Payment Failed", $"Stripe error while creating mobile recharge checkout session for product {payload?.ProductId}. Error: {ex.Message}");
                return BadRequest(new { success = false, message = $"Stripe error: {ex.Message}" });
            }
            catch (Exception ex)
            {
                await LogFailureAsync("Payment Failed", $"Failed to create mobile recharge checkout session for product {payload?.ProductId}. Error: {ex.Message}");
                return StatusCode(500, new { success = false, message = "An error occurred while creating the checkout session." });
            }
        }

        [HttpPost("createCheckoutSession-voucher")]
        [AllowAnonymous]
        public async Task<IActionResult> CheckoutSessionForGiftVoucher([FromBody] MobileCheckoutPayload payload)
        {
            try
            {
                var validation = ValidateRequest(payload);
                if (validation != null)
                    return validation;

                if (string.IsNullOrWhiteSpace(payload.ProductId.ToString()) || string.IsNullOrWhiteSpace(payload.ReturnUrl) || string.IsNullOrWhiteSpace(payload.Operator))
                    return BadRequest(new { success = false, message = "Product ID, operator, and return URL are required." });

                payload.Command = "execTransaction";
                payload.TransferTypeId = (int)wleepay.Core.Enums.TransferTypes.VoucherCodeTransfer;
                return await CreateProductCheckoutAsync(payload, RechargeFlows.Voucher);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
            catch (Stripe.StripeException ex)
            {
                await LogFailureAsync("Payment Failed", $"Stripe error while creating voucher checkout session for product {payload?.ProductId}. Error: {ex.Message}");
                return BadRequest(new { success = false, message = $"Stripe error: {ex.Message}" });
            }
            catch (Exception ex)
            {
                await LogFailureAsync("Payment Failed", $"Failed to create voucher checkout session for product {payload?.ProductId}. Error: {ex.Message}");
                return StatusCode(500, new { success = false, message = "Could not create voucher checkout session." });
            }
        }

        [HttpPost("createCheckoutSession-billPayment")]
        [AllowAnonymous]
        public async Task<IActionResult> CheckoutSessionForBillPayment(BillPaymentCheckoutPayload payload)
        {
            try
            {
                var validation = ValidateRequest(payload);
                if (validation != null)
                    return validation;

                var billValidation = ValidateBillPaymentRequest(payload);
                if (billValidation != null)
                    return billValidation;

                var apiKey = _configuration["Stripe:SecretKey"];
                if (string.IsNullOrWhiteSpace(apiKey))
                    return StatusCode(500, new { success = false, message = "Stripe configuration is missing." });

                var checkoutRequest = new CreateCheckoutSessionRequest
                {
                    Amount = decimal.Round(payload.Amount * FeeMultiplier, 2, MidpointRounding.AwayFromZero),
                    Currency = "usd",
                    ProductName = $"Electricity Bill Payment - Meter {payload.MeterNumber}",
                    TaxCode = TaxCode,
                    PromotionCodeId = payload.CouponCode,
                    ReturnUrl = payload.ReturnUrl
                };

                var recharge = new RechargeCheckoutContext
                {
                    Flow = RechargeFlows.Lec,
                    UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                    ReceiverPhoneNumber = payload.ReceiverPhoneNumber,
                    TransferTypeId = payload.TransferTypeId,
                    AccountId = payload.MeterNumber,
                    AmountOperator = payload.Amount,
                    Recharge = new RechargeRequestDetails
                    {
                        Command = payload.Command,
                        ProductId = payload.ProductId.ToString(),
                        OperatorId = payload.OperatorId,
                        Amount = payload.Amount,
                        Simulate = GetSimulateFlag()
                    }
                };

                var response = await _stripeService.CreateEmbeddedCheckoutSessionAsync(checkoutRequest, apiKey, recharge);
                return response.Success ? Ok(response) : BadRequest(response);
            }
            catch (Stripe.StripeException ex)
            {
                await LogFailureAsync("Payment Failed", $"Stripe error while creating bill payment checkout session for meter {payload?.MeterNumber}. Error: {ex.Message}");
                return BadRequest(new { success = false, message = $"Stripe error: {ex.Message}" });
            }
            catch (Exception ex)
            {
                await LogFailureAsync("Payment Failed", $"Failed to create bill payment checkout session for meter {payload?.MeterNumber}. Error: {ex.Message}");
                return StatusCode(500, new { success = false, message = "An error occurred while creating the checkout session." });
            }
        }

        #region Shared flow for mobile recharge and voucher

        private async Task<IActionResult> CreateProductCheckoutAsync(MobileCheckoutPayload payload, string flow)
        {
            var validation = ValidateRequest(payload);
            if (validation != null)
                return validation;

            if (string.IsNullOrWhiteSpace(payload.ProductId.ToString()) || string.IsNullOrWhiteSpace(payload.ReturnUrl))
                return BadRequest(new { success = false, message = "Product ID and return URL are required." });

            if (flow == RechargeFlows.Mobile && string.IsNullOrWhiteSpace(payload.PhoneNumber))
                return BadRequest(new { success = false, message = "Phone number is required." });

            var apiKey = _configuration["Stripe:SecretKey"];
            if (string.IsNullOrWhiteSpace(apiKey))
                return StatusCode(500, new { success = false, message = "Stripe configuration is missing." });

            var product = await _productService.GetProductByIdAsync(payload.StripeProductId.ToString());
            if (product == null)
                return NotFound(new { success = false, message = "Product not found." });

            var amounts = ValidateAndGetAmounts(payload, product);
            if (amounts.Error != null)
                return amounts.Error;

            string productName = product.Name;

            var checkoutRequest = new CreateCheckoutSessionRequest
            {
                Amount = amounts.Charge,
                Currency = "usd",
                ProductName = productName,
                TaxCode = TaxCode,
                PromotionCodeId = payload.CouponCode,
                ReturnUrl = payload.ReturnUrl
            };

            var recharge = new RechargeCheckoutContext
            {
                Flow = flow,
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                ReceiverPhoneNumber = payload.PhoneNumber,
                TransferTypeId = payload.TransferTypeId,
                ProductName = productName,
                Recharge = new RechargeRequestDetails
                {
                    Command = payload.Command,
                    Operator = payload.Operator,
                    Msisdn = payload.PhoneNumber,
                    ProductId = payload.ProductId,
                    Amount = amounts.Recharge,
                    Simulate = GetSimulateFlag()
                }
            };

            var response = await _stripeService.CreateEmbeddedCheckoutSessionAsync(checkoutRequest, apiKey, recharge);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        // Both amounts are computed on the server:
        //   Recharge = what the customer receives, Charge = what the customer pays (Recharge + 8% fee).
        private (IActionResult Error, decimal Charge, decimal Recharge) ValidateAndGetAmounts(
            MobileCheckoutPayload payload, dynamic product)
        {
            decimal recharge;

            if (string.Equals((string)product.PriceType, "range", StringComparison.OrdinalIgnoreCase))
            {
                if (payload.Amount <= 0)
                    return (BadRequest(new { success = false, message = "Amount must be greater than zero." }), 0m, 0m);

                recharge = decimal.Round(payload.Amount / FeeMultiplier, 2, MidpointRounding.AwayFromZero);

                decimal min = product.PriceMin;
                decimal max = product.PriceMax;
                if (recharge < min || recharge > max)
                    return (BadRequest(new { success = false, message = $"Amount must be between {min} and {max}." }), 0m, 0m);
            }
            else
            {
                recharge = product.Price;
                if (recharge <= 0)
                    return (BadRequest(new { success = false, message = "Invalid product price." }), 0m, 0m);
            }

            var charge = decimal.Round(recharge * FeeMultiplier, 2, MidpointRounding.AwayFromZero);
            return (null, charge, recharge);
        }

        #endregion

        #region Helper Methods

        // Never taken from the client. Set "Sochitel:Simulate": true in test environments only.
        private bool GetSimulateFlag() => bool.TryParse(_configuration["Sochitel:Simulate"], out var simulate) && simulate;

        private async Task LogFailureAsync(string action, string description)
        {
            try
            {
                await _activityLogService.AddSystemLogs(new SystemActivityLog
                {
                    UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                    Action = action,
                    Description = description,
                    CreatedDate = DateTime.UtcNow
                });
            }
            catch
            {
                // A logging failure must never change the response returned to the caller.
            }
        }

        private IActionResult ValidateRequest<TPayload>(TPayload payload) where TPayload : class
        {
            if (payload == null)
                return BadRequest(new { success = false, message = "Request body is required." });

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return BadRequest(new { success = false, message = "Validation failed", errors });
            }

            return null;
        }

        private IActionResult ValidateBillPaymentRequest(BillPaymentCheckoutPayload payload)
        {
            if (string.IsNullOrWhiteSpace(payload.MeterNumber))
                return BadRequest(new { success = false, message = "Meter number is required." });

            if (payload.Amount <= 0)
                return BadRequest(new { success = false, message = "Amount must be greater than zero." });

            if (string.IsNullOrWhiteSpace(payload.ReceiverPhoneNumber))
                return BadRequest(new { success = false, message = "Receiver phone number is required." });

            if (string.IsNullOrWhiteSpace(payload.ReturnUrl))
                return BadRequest(new { success = false, message = "Return URL is required." });

            return null;
        }

        #endregion
    }
}
