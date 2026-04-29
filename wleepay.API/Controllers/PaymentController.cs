using System;
using AuthorizeNet.Api.Contracts.V1;
using AuthorizeNet.Api.Controllers;
using AuthorizeNet.Api.Controllers.Bases;
using Microsoft.AspNetCore.Mvc;
using wleepay.API.Resources;
using wleepay.Core.Models;

[ApiController]
[Route("api/[controller]")]
public class PaymentController : ControllerBase
{
    [HttpPost("process-payment")]
    public IActionResult ProcessPayment([FromBody]  PaymentRequests request)
    {
        ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;

        ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
        {
            name = "73jKNV7Cnr",  
            ItemElementName = ItemChoiceType.transactionKey,
            Item = "4P74mM9VdsQG48ap"
        };

        var creditCard = new creditCardType
        {
            cardNumber = request.CardNumber,
            expirationDate = DateTime.Parse(request.Expiration).ToString("yyyy-MM"),

            cardCode = request.CVV
        };

        var billingAddress = new customerAddressType
        {
            firstName = request.NameOnCard,
            address = request.StreetAddress,
            city = request.City,
            zip = request.ZipCode,
            country = "USA"
        };

        var paymentType = new paymentType { Item = creditCard };

        var transactionRequest = new transactionRequestType
        {
            transactionType = transactionTypeEnum.authCaptureTransaction.ToString(),
            amount = request.Amount,
            payment = paymentType,
            billTo = billingAddress
        };

        var requestObj = new createTransactionRequest { transactionRequest = transactionRequest };
        var controller = new createTransactionController(requestObj);
        controller.Execute();

        var response = controller.GetApiResponse();

        if (response != null && response.messages.resultCode == messageTypeEnum.Ok &&
            response.transactionResponse?.responseCode == "1")
        {
            return Ok(new { success = true, transactionId = response.transactionResponse.transId });
        }
        else
        {
            var errorMessage = response?.transactionResponse?.errors?[0].errorText ??
                               response?.messages?.message[0]?.text ?? "Transaction failed.";
            return BadRequest(new { success = false, message = errorMessage });
        }
    }
}
