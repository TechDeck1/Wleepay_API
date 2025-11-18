using System;
using System.Threading.Tasks;
using wleepay.Core.Models;
using wleepay.Core.Services;
using AuthorizeNet.Api.Contracts.V1;
using AuthorizeNet.Api.Controllers;
using AuthorizeNet.Api.Controllers.Bases;
using wleepay.Core.Configurations;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using wleepay.Core.Enums;
using wleepay.Core.DTO.Request;
using wleepay.Core.DTO.Response;
using AutoMapper;
using wleepay.Communication.Interface;

namespace wleepay.Services
{
    public class AuthorizeNetService : IAuthorizeNetService
    {
        private readonly AuthorizeNetSettings _settings;
        private readonly IExternalApiService _externalApiService;
        private readonly IMapper _mapper;
        private readonly ISochitelTransactionService _sochitelTransactionService;
        private readonly ISMSService _smsService;
        private readonly IPaymentTransactionService _transactionService;
        public AuthorizeNetService(IOptions<AuthorizeNetSettings> settings, IExternalApiService externalApiService,
         IMapper mapper, ISochitelTransactionService sochitelTransactionService, IPaymentTransactionService transactionService, ISMSService smsService)
        {
            _settings = settings.Value;
            _externalApiService = externalApiService;
            _mapper = mapper;
            _sochitelTransactionService = sochitelTransactionService;
            _transactionService = transactionService;
            _smsService = smsService;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment =
                _settings.Environment == "SANDBOX" ? AuthorizeNet.Environment.SANDBOX : AuthorizeNet.Environment.PRODUCTION;

            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = _settings.ApiLoginId,
                ItemElementName = ItemChoiceType.transactionKey,
                Item = _settings.TransactionKey
            };
        }


        

        public async Task<PaymentTransaction> AuthorizePaymentAsync(PaymentRequests request, string userId)
        {
            var transactionDto = new PaymentTransactionDto
            {
                UserId = userId,
                Amount = request.Amount,
                CreatedAt = DateTime.UtcNow.Date,
                ReceiverPhoneNumber = request.ReceiverPhoneNumber,
                TransferTypeId = request.TransferTypeId,
                Street = request.StreetAddress,
                City = request.City,
                ZipCode = request.ZipCode,
                CurrencyId = (int)Currencies.USD
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
                transactionType = transactionTypeEnum.authOnlyTransaction.ToString(),
                amount = request.Amount,
                payment = paymentType,
                billTo = billingAddress
            };

            var apiRequest = new createTransactionRequest { transactionRequest = transactionRequest };
            var transactionHandler = new createTransactionController(apiRequest);
            transactionHandler.Execute();

            var apiResponse = transactionHandler.GetApiResponse();
            string fullResponseJson = JsonConvert.SerializeObject(apiResponse, Formatting.Indented);
            Console.WriteLine("API Response:\n" + fullResponseJson);

            if (apiResponse != null && apiResponse.messages.resultCode == messageTypeEnum.Ok)
            {
                var transactionResponse = apiResponse.transactionResponse;
                if (transactionResponse?.responseCode == "1")
                {
                    transactionDto.TransactionId = transactionResponse.transId;
                    transactionDto.Status = "Pending";
                }
                else
                {
                    transactionDto.Status = "Failed";
                    transactionDto.ErrorMessage = transactionResponse?.errors?[0]?.errorText ?? "Card declined";
                }
            }
            else
            {
                transactionDto.Status = "Failed";
                if (apiResponse?.transactionResponse?.errors != null)
                    transactionDto.ErrorMessage = apiResponse.transactionResponse.errors[0].errorText;
                else if (apiResponse?.messages?.message != null)
                    transactionDto.ErrorMessage = apiResponse.messages.message[0].text;
            }

            if (!string.IsNullOrEmpty(transactionDto.TransactionId))
               await _transactionService.SaveTransactionAsync(transactionDto);

            return _mapper.Map<PaymentTransaction>(transactionDto);
        }

        public async Task<PaymentTransaction> CapturePaymentAsync(string transactionId)
        {
            var transactionRequest = new transactionRequestType
            {
                transactionType = transactionTypeEnum.priorAuthCaptureTransaction.ToString(),
                refTransId = transactionId
            };

            var apiRequest = new createTransactionRequest { transactionRequest = transactionRequest };
            var controller = new createTransactionController(apiRequest);
            controller.Execute();

            var response = controller.GetApiResponse();
            var existingTransaction = await _transactionService.GetByTransactionIdAsync(transactionId);


            if (response != null && response.messages.resultCode == messageTypeEnum.Ok)
            {
                if (response.transactionResponse?.responseCode == "1")
                {
                    existingTransaction.Status = "Success";
                    existingTransaction.ErrorMessage = null;
                }
                else
                {
                    existingTransaction.Status = "Failed";
                    existingTransaction.ErrorMessage = response.transactionResponse?.errors?[0]?.errorText ?? "Capture failed";
                }
            }
            else
            {
                existingTransaction.Status = "Failed";
                existingTransaction.ErrorMessage =
                    response?.transactionResponse?.errors?[0]?.errorText ??
                    response?.messages?.message?[0]?.text ??
                    "Capture failed with unknown error.";
            }

            var updatedDto = _mapper.Map<PaymentTransactionDto>(existingTransaction);
            await _transactionService.SaveTransactionAsync(updatedDto);
            return existingTransaction;
        }

        public async Task<PaymentTransaction> VoidPaymentAsync(string transactionId)
        {
            var request = new transactionRequestType
            {
                transactionType = transactionTypeEnum.voidTransaction.ToString(),
                refTransId = transactionId
            };

            var apiRequest = new createTransactionRequest { transactionRequest = request };
            var controller = new createTransactionController(apiRequest);
            controller.Execute();

            var response = controller.GetApiResponse();
            var transaction = await _transactionService.GetByTransactionIdAsync(transactionId);

            if (response != null && response.messages.resultCode == messageTypeEnum.Ok)
            {
                if (response.transactionResponse?.responseCode == "1")
                {
                    transaction.Status = "Failed";
                    transaction.ErrorMessage = null;
                }
                else
                {
                    transaction.Status = "VoidFailed";
                    transaction.ErrorMessage = response.transactionResponse?.errors?[0]?.errorText ?? "Void failed";
                }
            }
            else
            {
                transaction.Status = "VoidFailed";
                transaction.ErrorMessage = response?.transactionResponse?.errors?[0]?.errorText ?? response?.messages?.message?[0]?.text ??
                    "Void failed with unknown error.";
            }
            var voidedDto = _mapper.Map<PaymentTransactionDto>(transaction);
            await _transactionService.SaveTransactionAsync(voidedDto);
            return transaction;
        }

        public async Task<PaymentTransaction> ProcessMobileRechargeAsync(PaymentRequests request, string userId)
        {
            var transaction = await AuthorizePaymentAsync(request, userId);
            if (string.IsNullOrEmpty(transaction?.TransactionId))
                return transaction;
            try
            {
                var payload = new MobileAirtimeRechargePayload
                {
                    Auth = new SochitelAuth
                    {
                        username = request.RechargeData.username,
                        salt = request.RechargeData.salt,
                        password = request.RechargeData.password
                    },
                    Command = request.RechargeData.Command,
                    Operator = request.RechargeData.Operator,
                    Msisdn = request.RechargeData.Msisdn.Trim(),
                    Simulate = request.RechargeData.Simulate,
                    UserReference = $"wlee{Guid.NewGuid():N}".Substring(0, 10),
                    ProductId = request.RechargeData.ProductId,
                    Version = 5,
                    Amount = request.RechargeData.Amount.ToString("F2")
                };

                string json = await _externalApiService.PostDataAsync<MobileAirtimeRechargePayload>(payload);
                var rechargeResponse = JsonConvert.DeserializeObject<SochitelTransactionResponse>(json);

                if (rechargeResponse?.Status?.Name == "Successful")
                {
                    var captured = await CapturePaymentAsync(transaction.TransactionId);
                    if (captured.Status == "Success")
                    {
                    
                        var save = new SochitelTransactions
                        {
                            TransactionId = rechargeResponse?.Result?.Id ?? "0",
                            TransactionType = "Mobile Recharge Transfer",
                            TransactionAmount = request.RechargeData.Amount.ToString("F2"),
                            CommissionRate = rechargeResponse?.Result?.Balance?.Commission ?? "0",
                            OperatorId = request.RechargeData.Operator,
                            ProductId = request.RechargeData.ProductId,
                            PhoneNumber = request.RechargeData.Msisdn,
                            UserReference = rechargeResponse?.Reference,
                            CreatedDate = DateTime.UtcNow,
                            ExtraInfo = rechargeResponse?.Result?.Instructions ?? "",
                            CurrencyId = (int)Currencies.USD
                        };
                        await _sochitelTransactionService.AddSochitelTransactions(save);
                        var phone = request.RechargeData.Msisdn;
                        var message = $"Dear customer, your mobile airtime recharge of ${request.RechargeData.Amount} was successful. Thank you for using Wleepay.";
                        _smsService.sendMessage(message, phone);

                    }
                    else
                    {
                        transaction.Status = "Failed";
                        transaction.ErrorMessage = captured.ErrorMessage ?? "Payment capture failed";
                    }
                }
                else
                {
                    var voided = await VoidPaymentAsync(transaction.TransactionId);
                    transaction.Status = "Failed";
                    transaction.ErrorMessage = voided?.ErrorMessage ?? "Recharge failed. Payment voided.";
                }
            }
            catch (Exception ex)
            {
                var voided = await VoidPaymentAsync(transaction.TransactionId);
                transaction.Status = "Failed";
                transaction.ErrorMessage = "Exception occurred: " + ex.Message;
            }

            var updatedDto = _mapper.Map<PaymentTransactionDto>(transaction);
            await _transactionService.SaveTransactionAsync(updatedDto);
            var updatedTransaction = await _transactionService.GetByTransactionIdAsync(transaction.TransactionId);
            return updatedTransaction;
        }

        public async Task<PaymentTransaction> ProcessLecRechargeAsync(LecCreditPaymentRequest request, string userId)
        {
            var paymentRequest = new PaymentRequests
            {
                CardNumber = request.Payment.CardNumber,
                CVV = request.Payment.CVV,
                Expiration = request.Payment.Expiration,
                NameOnCard = request.Payment.NameOnCard,
                StreetAddress = request.Payment.StreetAddress,
                City = request.Payment.City,
                ZipCode = request.Payment.ZipCode,
                Amount = request.Payment.Amount,
                ReceiverPhoneNumber = request.Payment.ReceiverPhoneNumber,
                TransferTypeId = request.Payment.TransferTypeId
            };

            var transaction = await AuthorizePaymentAsync(paymentRequest, userId);
            if (string.IsNullOrEmpty(transaction?.TransactionId))
                return transaction;
            try
            {
                var payload = new LecBillRechargePayload
                {
                    Auth = new SochitelAuth
                    {
                        username = request.Payment.RechargeData.username,
                        salt = request.Payment.RechargeData.salt,
                        password = request.Payment.RechargeData.password
                    },
                    Command = request.Payment.RechargeData.Command,
                    Operator = request.Payment.RechargeData.Operator,
                    Simulate = request.Payment.RechargeData.Simulate,
                    Version = 5,
                    AccountId = request.AccountId,
                    AmountOperator = request.AmountOperator.ToString("F2"),
                    ExtraParameters = new { accountType = "OFFLINE_PREPAID" }
                };

                string json = await _externalApiService.PostDataAsync<LecBillRechargePayload>(payload);
                var response = JsonConvert.DeserializeObject<SochitelTransactionResponse>(json);

                if (response?.Status?.Name == "Successful")
                {
                    var captured = await CapturePaymentAsync(transaction.TransactionId);
                    if (captured.Status == "Success")
                    {
                        var save = new SochitelTransactions
                        {
                            TransactionId = response?.Result?.Id ?? "0",
                            TransactionType = "Bill Payment Transfer",
                            TransactionAmount = request.Payment.Amount.ToString("F2"),
                            CommissionRate = response?.Result?.Balance?.Commission ?? "0",
                            OperatorId = request.Payment.RechargeData.Operator,
                            ProductId = request.Payment.RechargeData.ProductId,
                            PhoneNumber = request.Payment.ReceiverPhoneNumber,
                            UserReference = response?.Reference,
                            CreatedDate = DateTime.UtcNow,
                            ExtraInfo = response?.Result?.Instructions ?? "",
                            CurrencyId = (int)Currencies.USD,
                            MeterNumber = request.AccountId
                        };
                        await _sochitelTransactionService.AddSochitelTransactions(save);

                        var Phone = request.Payment.ReceiverPhoneNumber;
                        var message = $"Dear customer, your LEC bill payment of ${request.Payment.Amount} for meter {request.AccountId} was successful. Thank you for using Wleepay.";
                        _smsService.sendMessage(message, Phone);
                    }
                    else
                    {
                        transaction.Status = "Failed";
                        transaction.ErrorMessage = captured.ErrorMessage ?? "Payment capture failed";
                    }
                }
                else
                {
                    var voided = await VoidPaymentAsync(transaction.TransactionId);
                    transaction.Status = "Failed";
                    transaction.ErrorMessage = voided?.ErrorMessage ?? "Recharge failed. Payment voided.";
                }
            }
            catch (Exception ex)
            {
                var voided = await VoidPaymentAsync(transaction.TransactionId);
                transaction.Status = "Failed";
                transaction.ErrorMessage = "Exception occurred: " + ex.Message;
            }
            var updatedDto = _mapper.Map<PaymentTransactionDto>(transaction);
            await _transactionService.SaveTransactionAsync(updatedDto);

            var updatedTransaction = await _transactionService.GetByTransactionIdAsync(transaction.TransactionId);
            return updatedTransaction;
        }

    }
}

