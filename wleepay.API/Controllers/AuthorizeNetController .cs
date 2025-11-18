using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using wleepay.API.Resources;
using wleepay.Core.Services;
using wleepay.Core.Models;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Security.Claims;
using wleepay.Services;
using Newtonsoft.Json;
using AutoMapper;
using System.Runtime.CompilerServices;
using DevExpress.Xpo.Logger;
using System.Diagnostics;
using System.IO;
using wleepay.Core.DTO.Response;
using wleepay.Core.DTO.Request;
using wleepay.Core.Enums;
using wleepay.Services.Auth;

namespace wleepay.API.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
 
    public class AuthorizeNetController : ControllerBase
    {
        private readonly IAuthorizeNetService _authorizeService;
        private readonly IUserService _userService;
        public AuthorizeNetController(IAuthorizeNetService authorizeService, IUserService userService)
        {
            _authorizeService = authorizeService;
            _userService = userService;

        }

        //[Authorize]
        //[HttpPost("mobile-airtime-payment")]
        //public async Task<IActionResult> ProcessMobileAirtimePayment([FromBody] PaymentRequests request)
        //{
        //    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        //    if (string.IsNullOrEmpty(userId)) return Unauthorized("User ID not found");
        //    if (request.RechargeData == null) return BadRequest("Missing recharge data");
        //    var transaction = await _authorizeService.AuthorizePaymentAsync(request, userId);
        //    if (transaction == null || string.IsNullOrEmpty(transaction.TransactionId))
        //        return BadRequest(transaction?.ErrorMessage ?? "Card authorization failed");

        //    var payload = new MobileAirtimeRechargePayload
        //    {
        //        Auth = new SochitelAuth
        //        {
        //            username = request.RechargeData.username,
        //            salt = request.RechargeData.salt,
        //            password = request.RechargeData.password
        //        },
        //        Command = request.RechargeData.Command,
        //        Operator = request.RechargeData.Operator,
        //        Msisdn = request.RechargeData.Msisdn.Trim(),
        //        Simulate = request.RechargeData.Simulate,
        //        UserReference = $"wlee{Guid.NewGuid().ToString("N").Substring(0, 10)}",
        //        ProductId = request.RechargeData.ProductId,
        //        Version = 5,
        //        Amount = request.RechargeData.Amount.ToString("F2")
        //    };

        //    try
        //    {
        //        var json = await _externalApiService.PostDataAsync<MobileAirtimeRechargePayload>(payload);
        //        var rechargeResponse = JsonConvert.DeserializeObject<SochitelTransactionResponse>(json);

        //        if (rechargeResponse?.Status?.Name == "Successful")
        //        {
        //            var captured = await _authorizeService.CapturePaymentAsync(transaction.TransactionId);
        //            if (captured?.Status != "Success")
        //                return StatusCode(500, captured?.ErrorMessage ?? "Recharge succeeded but payment capture failed");

        //            try
        //            {
        //                var save = new SochitelTransactions
        //                {
        //                    TransactionId = rechargeResponse?.Result?.Id ?? "0",
        //                    TransactionType = "Mobile Recharge Transfer",
        //                    TransactionAmount = request.RechargeData.Amount.ToString("F2"),
        //                    CommissionRate = rechargeResponse?.Result?.Balance?.Commission ?? "0",
        //                    OperatorId = request.RechargeData.Operator,
        //                    ProductId = request.RechargeData.ProductId,
        //                    PhoneNumber = request.RechargeData.Msisdn,
        //                    UserReference = rechargeResponse?.Reference,
        //                    CreatedDate = DateTime.UtcNow,
        //                    ExtraInfo = rechargeResponse?.Result?.Instructions ?? "",
        //                    CurrencyId = (int)Currencies.USD,
        //                    MeterNumber = null
        //                };
        //                await _sochitelTransactionService.AddSochitelTransactions(save);
        //            }
        //            catch (Exception logEx)
        //            {
        //                return StatusCode(500, "Failed to save Transaction: " + logEx.Message);
        //            }

        //            return Ok(transaction);
        //        }
        //        else
        //        {
        //            var voided = await _authorizeService.VoidPaymentAsync(transaction.TransactionId);
        //            return BadRequest(voided?.ErrorMessage ?? "Recharge failed. Payment cancelled.");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //await _authorizeService.VoidPaymentAsync(transaction.TransactionId);
        //        return StatusCode(500, "Recharge or API error: " + ex.Message);
        //    }
        //}


        //[Authorize]
        //[HttpPost("lec-credit-payment")]
        //public async Task<IActionResult> ProcessLecCreditPayment([FromBody] LecCreditPaymentRequest request)
        //{
        //    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        //    if (string.IsNullOrEmpty(userId)) return Unauthorized("User ID not found");

        //    var paymentRequest = new PaymentRequests
        //    {
        //        CardNumber = request.Payment.CardNumber,
        //        CVV = request.Payment.CVV,
        //        Expiration = request.Payment.Expiration,
        //        NameOnCard = request.Payment.NameOnCard,
        //        StreetAddress = request.Payment.StreetAddress,
        //        City = request.Payment.City,
        //        ZipCode = request.Payment.ZipCode,
        //        Amount = request.Payment.Amount,
        //        ReceiverPhoneNumber = request.Payment.ReceiverPhoneNumber,
        //        TransferTypeId = request.Payment.TransferTypeId
        //    };

        //    var transaction = await _authorizeService.AuthorizePaymentAsync(paymentRequest, userId);
        //    if (transaction == null || string.IsNullOrEmpty(transaction.TransactionId))
        //        return BadRequest(transaction?.ErrorMessage ?? "Card authorization failed");

        //    var payload = new LecBillRechargePayload
        //    {
        //        Auth = new SochitelAuth
        //        {
        //            username = request.Payment.RechargeData.username,
        //            salt = request.Payment.RechargeData.salt,
        //            password = request.Payment.RechargeData.password
        //        },
        //        AmountOperator = request.AmountOperator.ToString("F2"),
        //        AccountId = request.AccountId,
        //        Version = 5,
        //        Command = request.Payment.RechargeData.Command,
        //        Operator = request.Payment.RechargeData.Operator,
        //        Simulate = request.Payment.RechargeData.Simulate,
        //        ExtraParameters = new { accountType = "OFFLINE_PREPAID" }
        //    };

        //    try
        //    {
        //        var json = await _externalApiService.PostDataAsync<LecBillRechargePayload>(payload);
        //        var response = JsonConvert.DeserializeObject<SochitelTransactionResponse>(json);

        //        if (response?.Status?.Name == "Successful")
        //        {
        //            var captured = await _authorizeService.CapturePaymentAsync(transaction.TransactionId);
        //            if (captured?.Status != "Success")
        //                return StatusCode(500, captured?.ErrorMessage ?? "Recharge succeeded but payment capture failed");

        //            try
        //            {
        //                var save = new SochitelTransactions
        //                {
        //                    TransactionId = response?.Result?.Id ?? "0",
        //                    TransactionType = "Bill Payment Transfer",
        //                    TransactionAmount = request.Payment.Amount.ToString("F2"),
        //                    CommissionRate = response?.Result?.Balance?.Commission ?? "0",
        //                    OperatorId = request.Payment.RechargeData.Operator,
        //                    ProductId = request.Payment.RechargeData.ProductId,
        //                    PhoneNumber = request.Payment.ReceiverPhoneNumber,
        //                    UserReference = response?.Reference,
        //                    CreatedDate = DateTime.UtcNow,
        //                    ExtraInfo = response?.Result?.Instructions ?? "",
        //                    CurrencyId = (int)Currencies.USD,
        //                    MeterNumber = request.AccountId
        //                };
        //                await _sochitelTransactionService.AddSochitelTransactions(save);
        //            }
        //            catch (Exception logEx)
        //            {
        //                return StatusCode(500, "Failed to save Transaction: " + logEx.Message);
        //            }

        //            return Ok(transaction);
        //        }
        //        else
        //        {
        //            var voided = await _authorizeService.VoidPaymentAsync(transaction.TransactionId);
        //            return BadRequest(voided?.ErrorMessage ?? "Recharge failed. Payment cancelled.");

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ex.Message);
        //    }
        //}

        ////////////////////////////////////////////////////////////////

        [Authorize]
        [HttpPost("mobile-airtime-recharges")]
        public async Task<IActionResult> ProcessMobileAirtimePayment([FromBody] PaymentRequests request)
        {
            var userId = _userService.GetCurrentUserId();
      
            if (request.RechargeData == null)
                return BadRequest("Missing recharge data");

            var transaction = await _authorizeService.ProcessMobileRechargeAsync(request, userId);

            if (transaction.Status == "Success")
               return Ok(transaction);
            
            return StatusCode(500, transaction.ErrorMessage ?? "Recharge failed");
        }

        [Authorize]
        [HttpPost("lec-credit-recharges")]
   
        
        
        public async Task<IActionResult> ProcessLecCreditPayment([FromBody] LecCreditPaymentRequest request)
        {
            var userId = _userService.GetCurrentUserId();
            var transaction = await _authorizeService.ProcessLecRechargeAsync(request, userId);

            if (transaction.Status == "Success")
                return Ok(transaction);

            return StatusCode(500, transaction.ErrorMessage ?? "Recharge failed");
        }

    }
}
