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
using Microsoft.Extensions.Logging;
using wleepay.Communication.Templates;
using System.Text.RegularExpressions;


namespace wleepay.Services
{
    public class AuthorizeNetService : IAuthorizeNetService
    {
        private readonly AuthorizeNetSettings _settings;
        private readonly IExternalApiService _externalApiService;
        private readonly IMapper _mapper;
        private readonly ISochitelTransactionService _sochitelTransactionService;
        private readonly ISMSService _smsService;
        private readonly IEmailSender _emailSender;           // Added
        private readonly IUserDetailService _userDetailsService;
        private readonly IPaymentTransactionService _transactionService;
        private readonly ILogger<AuthorizeNetService> _logger;
        private readonly ISystemActivityLogsService _activityLogService;

        public AuthorizeNetService(
            IOptions<AuthorizeNetSettings> settings,
            IExternalApiService externalApiService,
            IMapper mapper,
            ISochitelTransactionService sochitelTransactionService,
            IPaymentTransactionService transactionService,
            ISMSService smsService,
            ISystemActivityLogsService activityLogService,
            ILogger<AuthorizeNetService> logger,
            IEmailSender emailSender,
            IUserDetailService userDetailsService)
        {
            _settings = settings.Value;
            _externalApiService = externalApiService;
            _mapper = mapper;
            _sochitelTransactionService = sochitelTransactionService;
            _transactionService = transactionService;
            _smsService = smsService;
            _logger = logger;
            _activityLogService = activityLogService;
            _emailSender = emailSender;
            _userDetailsService = userDetailsService;
        }

        private void ConfigureAuthorizeNet()
        {
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment =
                _settings.Environment.Equals("SANDBOX", StringComparison.OrdinalIgnoreCase)
                    ? AuthorizeNet.Environment.SANDBOX
                    : AuthorizeNet.Environment.PRODUCTION;

            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication =
                new merchantAuthenticationType
                {
                    name = _settings.ApiLoginId?.Trim(),
                    ItemElementName = ItemChoiceType.transactionKey,
                    Item = _settings.TransactionKey?.Trim()
                };
        }

        public async Task<PaymentTransaction> AuthorizePaymentAsync(PaymentRequests request, string userId)
        {
            ConfigureAuthorizeNet();

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
            var controller = new createTransactionController(apiRequest);
            controller.Execute();

            var apiResponse = controller.GetApiResponse();

            if (apiResponse != null && apiResponse.messages.resultCode == messageTypeEnum.Ok)
            {
                var response = apiResponse.transactionResponse;
                if (response?.responseCode == "1")
                {
                    transactionDto.TransactionId = response.transId;
                    transactionDto.Status = "Pending";
                }
                else
                {
                    transactionDto.Status = "Failed";
                    transactionDto.ErrorMessage = response?.errors?[0]?.errorText ?? "Card declined";
                }
            }
            else
            {
                transactionDto.Status = "Failed";
                transactionDto.ErrorMessage =
                    apiResponse?.transactionResponse?.errors?[0]?.errorText ??
                    apiResponse?.messages?.message?[0]?.text;
            }

            if (!string.IsNullOrEmpty(transactionDto.TransactionId))
                await _transactionService.SaveTransactionAsync(transactionDto);

            return _mapper.Map<PaymentTransaction>(transactionDto);
        }

        public async Task<PaymentTransaction> CapturePaymentAsync(string transactionId)
        {
            ConfigureAuthorizeNet();

            var request = new transactionRequestType
            {
                transactionType = transactionTypeEnum.priorAuthCaptureTransaction.ToString(),
                refTransId = transactionId
            };

            var apiRequest = new createTransactionRequest { transactionRequest = request };
            var controller = new createTransactionController(apiRequest);
            controller.Execute();

            var response = controller.GetApiResponse();
            var existing = await _transactionService.GetByTransactionIdAsync(transactionId);

            if (response != null && response.messages.resultCode == messageTypeEnum.Ok)
            {
                if (response.transactionResponse?.responseCode == "1")
                {
                    existing.Status = "Success";
                    existing.ErrorMessage = null;
                }
                else
                {
                    existing.Status = "Failed";
                    existing.ErrorMessage = response.transactionResponse?.errors?[0]?.errorText;
                }
            }
            else
            {
                existing.Status = "Failed";
                existing.ErrorMessage =
                    response?.transactionResponse?.errors?[0]?.errorText ??
                    response?.messages?.message?[0]?.text;
            }

            await _transactionService.SaveTransactionAsync(_mapper.Map<PaymentTransactionDto>(existing));
            return existing;
        }

        public async Task<PaymentTransaction> VoidPaymentAsync(string transactionId)
        {
            ConfigureAuthorizeNet();

            var request = new transactionRequestType
            {
                transactionType = transactionTypeEnum.voidTransaction.ToString(),
                refTransId = transactionId
            };

            var apiRequest = new createTransactionRequest { transactionRequest = request };
            var controller = new createTransactionController(apiRequest);
            controller.Execute();

            var response = controller.GetApiResponse();
            var existing = await _transactionService.GetByTransactionIdAsync(transactionId);

            existing.Status = "Failed";
            existing.ErrorMessage =
                response?.transactionResponse?.errors?[0]?.errorText ??
                response?.messages?.message?[0]?.text;

            await _transactionService.SaveTransactionAsync(_mapper.Map<PaymentTransactionDto>(existing));
            return existing;
        }

        public async Task<PaymentTransaction> ProcessMobileRechargeAsync(PaymentRequests request, string userId)
        {
            // Development Purpose
            //ApiOperationBase<AuthorizeNet.Api.Contracts.V1.ANetApiRequest, AuthorizeNet.Api.Contracts.V1.ANetApiResponse>.RunEnvironment 
            //    = AuthorizeNet.Environment.SANDBOX;
            //ApiOperationBase<AuthorizeNet.Api.Contracts.V1.ANetApiRequest, AuthorizeNet.Api.Contracts.V1.ANetApiResponse>.MerchantAuthentication 
            //    = new AuthorizeNet.Api.Contracts.V1.merchantAuthenticationType { 
            //        name = _settings.ApiLoginId, 
            //        Item = _settings.TransactionKey, 
            //        ItemElementName = AuthorizeNet.Api.Contracts.V1.ItemChoiceType.transactionKey 
            //    };

            //Production Purpose
            //ApiOperationBase<AuthorizeNet.Api.Contracts.V1.ANetApiRequest, AuthorizeNet.Api.Contracts.V1.ANetApiResponse>.MerchantAuthentication = new AuthorizeNet.Api.Contracts.V1.merchantAuthenticationType { name = _settings.ApiLoginId, Item = _settings.TransactionKey, ItemElementName = AuthorizeNet.Api.Contracts.V1.ItemChoiceType.transactionKey };
            // ================================
            // 1. Authorize.Net Configuration
            // ================================

            ConfigureAuthorizeNet();

            // Plus Transaction fees
            request.Amount = Math.Round(request.Amount * 1.12m, 2);

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

                string jsonPayload = JsonConvert.SerializeObject(payload);
                _logger.LogInformation("Sending Recharge Payload: {Payload}", jsonPayload);

                string json = await _externalApiService.PostDataAsync<MobileAirtimeRechargePayload>(payload);
                _logger.LogInformation("Recharge Provider Response: {Response}", json);
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

                        try
                        {
                            // Get User Details to retrieve Email and Name
                            var userDetailObj = await _userDetailsService.GetApplicationUserById(userId);
                            if (userDetailObj != null && !string.IsNullOrEmpty(userDetailObj.Email))
                            {
                                var (airtimeToken, airtimeValidUntil) = ExtractAirtimeTokenAndValidity(rechargeResponse?.Result?.Instructions);
                                var operatorName = rechargeResponse?.Result?.Operator?.Name ?? "Airtime";
                                var productType = rechargeResponse?.Result?.ProductType ?? "Airtime";

                                string emailBody = MessagesTemplates.MobileRechargeReceiptEmail(
                                    userDetailObj.UserDetail.FirstName,
                                    request.Amount.ToString("F2"),
                                    request.RechargeData.Amount.ToString("F2"),
                                    request.RechargeData.Msisdn,
                                    rechargeResponse?.Result?.Id ?? "0",
                                    rechargeResponse?.Reference,
                                    DateTime.UtcNow.ToString("g"),
                                    operatorName,
                                    productType,
                                    airtimeToken,
                                    airtimeValidUntil
                                );

                                await _emailSender.SendEmailAsync(
                                    userDetailObj.Email,
                                    emailBody,
                                    userDetailObj.UserDetail.FirstName,
                                    "WleeSend.com Airtime Credit Receipt"
                                );
                            }
                        }
                        catch (Exception emailEx)
                        {
                            // Log email failure but do not fail the transaction
                            _logger.LogError(emailEx, "Failed to send email receipt for User {UserId}", userId);
                        }

                        var phone = request.RechargeData.Msisdn;
                        var smsOperatorName = rechargeResponse?.Result?.Operator?.Name ?? "Airtime";
                        var smsProductType = rechargeResponse?.Result?.ProductType ?? "Airtime";
                        var (smsAirtimeToken, smsAirtimeValidUntil) = ExtractAirtimeTokenAndValidity(rechargeResponse?.Result?.Instructions);
                        var message = $"You have received {smsProductType} {smsOperatorName} ${request.RechargeData.Amount:F2} from WleeSend.com. Your Airtime token is {smsAirtimeToken} and valid until {smsAirtimeValidUntil}. Thank you.";
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

                await _activityLogService.AddSystemLogs(new SystemActivityLog
                {
                    UserId = userId,
                    Action = "payment",
                    Description = $"Successfully processed recharge for {request.RechargeData.Msisdn}. Amount: {request.RechargeData.Amount}",
                    CreatedDate = DateTime.UtcNow
                });
            }
            catch (Exception ex)
            {
                await _activityLogService.AddSystemLogs(new SystemActivityLog
                {
                    UserId = userId,
                    //ActivityType = "Error",
                    Action="payment",
                    Description = $"Payment Failed: {ex.Message}",
                    CreatedDate = DateTime.UtcNow
                });
                // Log the exception details and the stack trace
                _logger.LogError(ex, "An error occurred during recharge process for User {UserId}", userId);
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
            // Development Purpose
            //ApiOperationBase<AuthorizeNet.Api.Contracts.V1.ANetApiRequest, AuthorizeNet.Api.Contracts.V1.ANetApiResponse>.RunEnvironment
            //    = AuthorizeNet.Environment.SANDBOX;
            //ApiOperationBase<AuthorizeNet.Api.Contracts.V1.ANetApiRequest, AuthorizeNet.Api.Contracts.V1.ANetApiResponse>.MerchantAuthentication
            //    = new AuthorizeNet.Api.Contracts.V1.merchantAuthenticationType
            //    {
            //        name = _settings.ApiLoginId,
            //        Item = _settings.TransactionKey,
            //        ItemElementName = AuthorizeNet.Api.Contracts.V1.ItemChoiceType.transactionKey
            //    };

            // Production Purpose
            ApiOperationBase<AuthorizeNet.Api.Contracts.V1.ANetApiRequest, AuthorizeNet.Api.Contracts.V1.ANetApiResponse>.MerchantAuthentication = new AuthorizeNet.Api.Contracts.V1.merchantAuthenticationType { name = _settings.ApiLoginId, Item = _settings.TransactionKey, ItemElementName = AuthorizeNet.Api.Contracts.V1.ItemChoiceType.transactionKey };

            // Plus Transaction fees
            request.Payment.Amount = Math.Round(request.Payment.Amount * 1.12m, 2);
            
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

                        try
                        {
                            var userDetailObj = await _userDetailsService.GetApplicationUserById(userId);
                            if (userDetailObj != null && !string.IsNullOrEmpty(userDetailObj.Email))
                            {
                                // Extract Token from instructions if available, or use Reference
                                var instructionDetails = ParseInstructions(response?.Result?.Instructions);
                                string token = !string.IsNullOrWhiteSpace(instructionDetails.Token) ? instructionDetails.Token : "N/A";

                                string emailBody = MessagesTemplates.LecCreditReceiptEmail(
                                    userDetailObj.UserDetail.FirstName,
                                    request.Payment.Amount.ToString("F2"),
                                    request.AmountOperator.ToString("F2"),
                                    request.Payment.ReceiverPhoneNumber,
                                    token,
                                    request.AccountId,
                                    instructionDetails.Units.HasValue ? instructionDetails.Units.Value.ToString("F2") : "N/A",
                                    response?.Result?.Id ?? "0",
                                    DateTime.UtcNow.ToString("g")
                                );

                                await _emailSender.SendEmailAsync(
                                    userDetailObj.Email,
                                    emailBody,
                                    userDetailObj.UserDetail.FirstName,
                                    "WleeSend.com LEC Credit Receipt"
                                );
                            }
                        }
                        catch (Exception emailEx)
                        {
                            _logger.LogError(emailEx, "Failed to send LEC email receipt for User {UserId}", userId);
                        }

                        var instructionDetailsForSms = ParseInstructions(response?.Result?.Instructions);
                        var tokenNumber = !string.IsNullOrWhiteSpace(instructionDetailsForSms.Token) ? instructionDetailsForSms.Token : "N/A";
                        var phone = request.Payment.ReceiverPhoneNumber;
                        var message = $"WleeSend: LEC credit ${request.AmountOperator:F2} Meter {request.AccountId}. Token {tokenNumber}. Thank you.";
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

        private static InstructionDetails ParseInstructions(string instructions)
        {
            var instructionDetails = new InstructionDetails();
            if (string.IsNullOrWhiteSpace(instructions))
                return instructionDetails;

            var lines = instructions.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in lines)
            {
                if (line.StartsWith("Amount:", StringComparison.OrdinalIgnoreCase))
                    instructionDetails.Amount = decimal.Parse(line.Split(':')[1].Trim());
                else if (line.StartsWith("Date:", StringComparison.OrdinalIgnoreCase))
                    instructionDetails.Date = DateTime.Parse(line.Split(':')[1].Trim());
                else if (line.StartsWith("Description:", StringComparison.OrdinalIgnoreCase))
                    instructionDetails.Description = line;
                else if (line.StartsWith("Units:", StringComparison.OrdinalIgnoreCase))
                    instructionDetails.Units = decimal.Parse(line.Split(':')[1].Trim());
                else if (line.StartsWith("Tax:", StringComparison.OrdinalIgnoreCase))
                    instructionDetails.Tax = decimal.Parse(line.Split(':')[1].Trim());
                else if (line.StartsWith("Receipt Number:", StringComparison.OrdinalIgnoreCase))
                    instructionDetails.ReceiptNumber = line.Split(':')[1].Trim();
                else if (line.StartsWith("Account Number:", StringComparison.OrdinalIgnoreCase))
                    instructionDetails.AccountNumber = line.Split(':')[1].Trim();
                else if (line.StartsWith("Customer Name:", StringComparison.OrdinalIgnoreCase))
                    instructionDetails.CustomerName = line.Split(':')[1].Trim();
                else if (line.StartsWith("Token Type:", StringComparison.OrdinalIgnoreCase))
                    instructionDetails.TokenType = line.Split(':')[1].Trim();
                else if (line.StartsWith("Token:", StringComparison.OrdinalIgnoreCase))
                    instructionDetails.Token = line.Split(':')[1].Trim();
            }

            return instructionDetails;
        }

        private static (string Token, string ValidUntil) ExtractAirtimeTokenAndValidity(string instructions)
        {
            if (string.IsNullOrWhiteSpace(instructions))
                return ("N/A", "N/A");

            string token = string.Empty;
            string validUntil = string.Empty;

            var lines = instructions.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines)
            {
                if (line.StartsWith("Token:", StringComparison.OrdinalIgnoreCase))
                    token = line.Split(':')[1].Trim();
                else if (line.StartsWith("Valid until", StringComparison.OrdinalIgnoreCase) ||
                         line.StartsWith("Valid Till", StringComparison.OrdinalIgnoreCase) ||
                         line.StartsWith("Validity", StringComparison.OrdinalIgnoreCase) ||
                         line.StartsWith("Expires", StringComparison.OrdinalIgnoreCase))
                    validUntil = line.Split(':').Length > 1 ? line.Split(':')[1].Trim() : line.Trim();
            }

            if (string.IsNullOrWhiteSpace(token))
            {
                var match = Regex.Match(instructions, @"\b\d{20}\b");
                if (match.Success)
                    token = match.Value;
            }

            if (string.IsNullOrWhiteSpace(token))
                token = "N/A";
            if (string.IsNullOrWhiteSpace(validUntil))
                validUntil = "N/A";

            return (token, validUntil);
        }

    }
}

