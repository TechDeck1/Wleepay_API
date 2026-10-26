using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AuthorizeNet.Api.Contracts.V1;
using AuthorizeNet.Api.Controllers;
using AuthorizeNet.Api.Controllers.Bases;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using wleepay.Communication.Interface;
using wleepay.Communication.Templates;
using wleepay.Core.Configurations;
using wleepay.Core.DTO.Request;
using wleepay.Core.DTO.Response;
using wleepay.Core.Enums;
using wleepay.Core.Models;
using wleepay.Core.Services;

namespace wleepay.Services
{
    public class AuthorizeNetService : IAuthorizeNetService
    {
        #region Constants

        // Customer-facing messages.
        private const string GenericFailureMessage =  "We couldn't complete this recharge. You have not been charged. Please try again.";
        private const string VoidFailedMessage = "We couldn't complete this recharge. Please contact support so we can release the hold on your card.";
        private const string DeliveredUnconfirmedMessage = "Your recharge was sent but we had a problem confirming payment. Please contact support.";
        private const string ProviderUnavailableMessage = "We're unable to process recharges right now. Please try again shortly.";
        private const string InsufficientProviderBalanceMessage =  "We're unable to process this recharge right now. Please try again later.";

        // Service fee multiplier applied to the customer's amount.
        private const decimal FeeMultiplier = 1.08m;

        private static readonly string[] LineSeparators = { "\r\n", "\n" };

        #endregion

        #region Fields and Constructor

        private readonly AuthorizeNetSettings _settings;
        private readonly SochitelSettings _sochitelSettings;
        private readonly IExternalApiService _externalApiService;
        private readonly IMapper _mapper;
        private readonly ISochitelTransactionService _sochitelTransactionService;
        private readonly ISMSService _smsService;
        private readonly IEmailSender _emailSender;
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
            IUserDetailService userDetailsService,
            IOptions<SochitelSettings> sochitelSettings)
        {
            _settings = settings.Value;
            _sochitelSettings = sochitelSettings.Value;
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

        #endregion

        #region Authorize.Net Operations

        // Sets the Authorize.Net environment and merchant credentials.
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

        // Sends a transaction request to Authorize.Net and returns the raw response.
        private static createTransactionResponse ExecuteTransaction(transactionRequestType request)
        {
            var controller = new createTransactionController(new createTransactionRequest { transactionRequest = request });
            controller.Execute();
            return controller.GetApiResponse();
        }

        // Places an authorization hold on the card without capturing funds.
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

            if (!DateTime.TryParse(request.Expiration, CultureInfo.InvariantCulture, DateTimeStyles.None, out var expiry))
            {
                transactionDto.Status = "Failed";
                transactionDto.ErrorMessage = "Invalid card expiration date.";
                return _mapper.Map<PaymentTransaction>(transactionDto);
            }

            var apiResponse = ExecuteTransaction(new transactionRequestType
            {
                transactionType = transactionTypeEnum.authOnlyTransaction.ToString(),
                amount = request.Amount,
                payment = new paymentType
                {
                    Item = new creditCardType
                    {
                        cardNumber = request.CardNumber,
                        expirationDate = expiry.ToString("yyyy-MM", CultureInfo.InvariantCulture),
                        cardCode = request.CVV
                    }
                },
                billTo = new customerAddressType
                {
                    firstName = request.NameOnCard,
                    address = request.StreetAddress,
                    city = request.City,
                    zip = request.ZipCode,
                    country = "USA"
                }
            });

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
                    apiResponse?.messages?.message?[0]?.text ??
                    "Card authorization failed";
            }

            if (!string.IsNullOrEmpty(transactionDto.TransactionId))
                await _transactionService.SaveTransactionAsync(transactionDto);

            return _mapper.Map<PaymentTransaction>(transactionDto);
        }

        // Captures a previously authorized payment.
        public async Task<PaymentTransaction> CapturePaymentAsync(string transactionId)
        {
            ConfigureAuthorizeNet();

            var response = ExecuteTransaction(new transactionRequestType
            {
                transactionType = transactionTypeEnum.priorAuthCaptureTransaction.ToString(),
                refTransId = transactionId
            });

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

        // Voids an authorization and records whether the void really succeeded.
        public async Task<PaymentTransaction> VoidPaymentAsync(string transactionId)
        {
            ConfigureAuthorizeNet();

            var response = ExecuteTransaction(new transactionRequestType
            {
                transactionType = transactionTypeEnum.voidTransaction.ToString(),
                refTransId = transactionId
            });

            var existing = await _transactionService.GetByTransactionIdAsync(transactionId);

            var voidOk = response != null
                         && response.messages.resultCode == messageTypeEnum.Ok
                         && response.transactionResponse?.responseCode == "1";

            if (existing != null)
            {
                if (voidOk)
                {
                    existing.Status = "Voided";
                    existing.ErrorMessage = null;
                }
                else
                {
                    existing.Status = "VoidFailed";
                    existing.ErrorMessage =
                        response?.transactionResponse?.errors?[0]?.errorText ??
                        response?.messages?.message?[0]?.text ??
                        "Void failed";
                }

                await _transactionService.SaveTransactionAsync(_mapper.Map<PaymentTransactionDto>(existing));
            }

            if (!voidOk)
                _logger.LogCritical("VOID FAILED for Authorize.Net transaction {TransId}. Manual action required.", transactionId);

            return existing;
        }

        #endregion

        #region Payment Flow Helpers

        // Voids without ever throwing; returns true only if the hold was released.
        private async Task<bool> SafeVoidAsync(string transactionId)
        {
            try
            {
                var voided = await VoidPaymentAsync(transactionId);
                return voided?.Status == "Voided";
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "VOID THREW for Authorize.Net transaction {TransId}. Manual action required.", transactionId);
                return false;
            }
        }

        // Voids the hold and marks the transaction failed with the right customer message.
        private async Task VoidAndFailAsync(PaymentTransaction transaction)
        {
            var voided = await SafeVoidAsync(transaction.TransactionId);
            transaction.Status = "Failed";
            transaction.ErrorMessage = voided ? GenericFailureMessage : VoidFailedMessage;
        }

        // Captures payment after credit was delivered; returns true on success.
        private async Task<bool> CaptureAfterDeliveryAsync(PaymentTransaction transaction, string flow)
        {
            var captureResult = await CapturePaymentAsync(transaction.TransactionId);

            if (captureResult.Status == "Success")
            {
                transaction.Status = "Success";
                transaction.ErrorMessage = null;
                return true;
            }

            _logger.LogCritical(
                "{Flow}: CREDIT DELIVERED BUT CAPTURE FAILED. AuthNet {TransId}, Error {Err}",
                flow, transaction.TransactionId, captureResult.ErrorMessage);

            transaction.Status = "Failed";
            transaction.ErrorMessage = DeliveredUnconfirmedMessage;
            return false;
        }

        // Handles unexpected exceptions; never voids if credit was already delivered.
        private async Task HandleRechargeExceptionAsync(PaymentTransaction transaction, bool rechargeDelivered, string reference)
        {
            if (rechargeDelivered)
            {
                _logger.LogCritical(
                    "Exception AFTER credit delivered. Do NOT void. AuthNet {TransId}, Ref {Ref}",
                    transaction.TransactionId, reference);
                transaction.Status = "Failed";
                transaction.ErrorMessage = DeliveredUnconfirmedMessage;
                return;
            }

            _logger.LogWarning("Voiding after exception. Verify Ref {Ref} in Sochitel if this was a timeout.", reference);
            await VoidAndFailAsync(transaction);
        }

        // Builds a failed result for problems that happen before authorization.
        private static PaymentTransaction BuildPreAuthFailure(
            string userId, decimal amount, string phone, int transferTypeId, string error)
        {
            return new PaymentTransaction
            {
                UserId = userId,
                Amount = amount,
                ReceiverPhoneNumber = phone,
                TransferTypeId = transferTypeId,
                CurrencyId = (int)Currencies.USD,
                Status = "Failed",
                ErrorMessage = error,
                CreatedAt = DateTime.UtcNow
            };
        }

        // Saves the final transaction state and returns the stored copy.
        private async Task<PaymentTransaction> SaveFinalAndReloadAsync(PaymentTransaction transaction)
        {
            await _transactionService.SaveTransactionAsync(_mapper.Map<PaymentTransactionDto>(transaction));
            var reloaded = await _transactionService.GetByTransactionIdAsync(transaction.TransactionId);
            return reloaded ?? transaction;
        }

        // Writes a payment activity log entry without ever throwing.
        private async Task SafeLogActivityAsync(string userId, string description)
        {
            try
            {
                await _activityLogService.AddSystemLogs(new SystemActivityLog
                {
                    UserId = userId,
                    Action = "payment",
                    Description = description,
                    CreatedDate = DateTime.UtcNow
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to write activity log for User {UserId}", userId);
            }
        }

        #endregion

        #region Sochitel Auth and API

        // Builds auth with a fresh salt and matching password; call once per request.
        private SochitelAuth BuildFreshAuth()
        {
            var salt = GenerateSalt();
            return new SochitelAuth
            {
                username = _sochitelSettings.Username,
                salt = salt,
                password = Sha1Hex(salt + Sha1Hex(_sochitelSettings.Password ?? string.Empty))
            };
        }

        // Generates a random alphanumeric salt.
        private static string GenerateSalt(int length = 40)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var sb = new StringBuilder(length);
            for (int i = 0; i < length; i++)
                sb.Append(chars[RandomNumberGenerator.GetInt32(chars.Length)]);
            return sb.ToString();
        }

        // Returns the lowercase hex SHA1 of the input.
        private static string Sha1Hex(string input)
        {
            using (var sha1 = SHA1.Create())
            {
                var bytes = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
                return BitConverter.ToString(bytes).Replace("-", "").ToLowerInvariant();
            }
        }

        // Reads status.name from a Sochitel response without strict mapping.
        private static string GetStatusName(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
                return null;

            try { return JObject.Parse(json)["status"]?["name"]?.ToString(); }
            catch (JsonReaderException) { return null; }
        }

        // Maps a successful Sochitel response to the DTO, returning null if mapping fails.
        private SochitelTransactionResponse TryMapResponse(string json, string reference)
        {
            try { return JsonConvert.DeserializeObject<SochitelTransactionResponse>(json); }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Could not map Sochitel success response for {Ref}. Raw: {Json}", reference, json);
                return null;
            }
        }

        // Checks the Sochitel balance covers the requested amount; returns an error message or null.
        private async Task<string> ValidateSochitelBalanceAsync(RechargeRequestDetails rechargeData, decimal requestedAmount)
        {
            if (rechargeData == null)
                return "Missing recharge data";

            try
            {
                var balanceRequest = new
                {
                    auth = BuildFreshAuth(),
                    version = 5,
                    command = "getBalance"
                };

                var balanceJson = await _externalApiService.PostDataAsync(balanceRequest);
                var balanceResponse = JObject.Parse(balanceJson);
                var statusName = balanceResponse["status"]?["name"]?.ToString();

                if (!string.Equals(statusName, "Successful", StringComparison.OrdinalIgnoreCase))
                {
                    _logger.LogWarning("Sochitel getBalance failed. Status {Status}. Raw: {Json}", statusName, balanceJson);
                    return ProviderUnavailableMessage;
                }

                var balanceValue = balanceResponse["result"]?["value"]?.ToString();
                if (!TryParseSochitelBalance(balanceValue, out var currentBalance))
                {
                    _logger.LogWarning("Could not read Sochitel balance. Raw: {Json}", balanceJson);
                    return ProviderUnavailableMessage;
                }

                if (currentBalance < requestedAmount)
                {
                    _logger.LogWarning("Sochitel balance {Balance} is below requested {Requested}", currentBalance, requestedAmount);
                    return InsufficientProviderBalanceMessage;
                }

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Sochitel balance validation threw");
                return ProviderUnavailableMessage;
            }
        }

        // Parses the balance value, tolerating text around the number.
        private static bool TryParseSochitelBalance(string balance, out decimal value)
        {
            value = 0;
            if (string.IsNullOrWhiteSpace(balance))
                return false;

            if (decimal.TryParse(balance, NumberStyles.Number, CultureInfo.InvariantCulture, out value))
                return true;

            var match = Regex.Match(balance, @"-?\d+(\.\d+)?");
            return match.Success && decimal.TryParse(match.Value, NumberStyles.Number, CultureInfo.InvariantCulture, out value);
        }

        // Saves the Sochitel transaction record without ever throwing.
        private async Task SaveSochitelRecordAsync(SochitelTransactions record)
        {
            try { await _sochitelTransactionService.AddSochitelTransactions(record); }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed saving Sochitel transaction record for Ref {Ref}", record.UserReference);
            }
        }

        #endregion

        #region Mobile Recharge

        // Runs the full mobile airtime flow: authorize, recharge, capture or void, notify.
        public async Task<PaymentTransaction> ProcessMobileRechargeAsync(PaymentRequests request, string userId)
        {
            request.Amount = Math.Round(request.Amount * FeeMultiplier, 2);

            var balanceError = await ValidateSochitelBalanceAsync(request.RechargeData, request.RechargeData?.Amount ?? 0m);
            if (!string.IsNullOrEmpty(balanceError))
                return BuildPreAuthFailure(userId, request.Amount, request.ReceiverPhoneNumber, request.TransferTypeId, balanceError);

            var transaction = await AuthorizePaymentAsync(request, userId);
            if (string.IsNullOrEmpty(transaction?.TransactionId))
                return transaction;

            var userRef = "wlee" + Guid.NewGuid().ToString("N").Substring(0, 12);
            SochitelTransactionResponse rechargeResponse = null;
            bool rechargeDelivered = false;
            bool captured = false;

            try
            {
                var payload = BuildMobilePayload(request.RechargeData, userRef);

                _logger.LogInformation(
                    "Sending recharge. UserRef={UserRef} Operator={Operator} Msisdn={Msisdn} Product={ProductId} Amount={Amount} Simulate={Simulate}",
                    userRef, payload.Operator, payload.Msisdn, payload.ProductId, payload.Amount, payload.Simulate);

                var json = await _externalApiService.PostDataAsync<MobileAirtimeRechargePayload>(payload);
                _logger.LogInformation("Recharge Provider Response (UserRef {UserRef}): {Response}", userRef, json);

                var statusName = GetStatusName(json);

                if (string.Equals(statusName, "Successful", StringComparison.OrdinalIgnoreCase))
                {
                    rechargeDelivered = true;
                    rechargeResponse = TryMapResponse(json, userRef);
                    captured = await CaptureAfterDeliveryAsync(transaction, "Mobile");
                }
                else
                {
                    _logger.LogWarning(
                        "Sochitel rejected recharge. AuthNet {TransId}, UserRef {UserRef}, Status {Status}. Raw: {Json}",
                        transaction.TransactionId, userRef, statusName, json);
                    await VoidAndFailAsync(transaction);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during recharge for User {UserId}, AuthNet {TransId}, UserRef {UserRef}",
                    userId, transaction.TransactionId, userRef);
                await HandleRechargeExceptionAsync(transaction, rechargeDelivered, userRef);
            }

            if (rechargeDelivered)
                await SaveSochitelRecordAsync(BuildMobileSochitelRecord(request, rechargeResponse, userRef));

            if (captured)
                await NotifyMobileRechargeAsync(request, userId, rechargeResponse, userRef);

            await SafeLogActivityAsync(userId, captured
                ? $"Successfully processed recharge for {request.RechargeData.Msisdn}. Amount: {request.RechargeData.Amount}. UserRef {userRef}"
                : $"Recharge FAILED for {request.RechargeData.Msisdn}. Amount: {request.RechargeData.Amount}. UserRef {userRef}. Reason: {transaction.ErrorMessage}");

            return await SaveFinalAndReloadAsync(transaction);
        }

        // Builds the Sochitel payload for a mobile airtime recharge.
        private MobileAirtimeRechargePayload BuildMobilePayload(RechargeRequestDetails data, string userRef)
        {
            return new MobileAirtimeRechargePayload
            {
                Auth = BuildFreshAuth(),
                Command = data.Command,
                Operator = data.Operator,
                Msisdn = data.Msisdn.Trim(),
                Simulate = data.Simulate,
                UserReference = userRef,
                ProductId = data.ProductId.ToString(), 
                Version = 5,
                Amount = Money(data.Amount)
            };
        }

        // Builds the Sochitel transaction record for a mobile recharge.
        private static SochitelTransactions BuildMobileSochitelRecord(
            PaymentRequests request, SochitelTransactionResponse response, string userRef)
        {
            return new SochitelTransactions
            {
                TransactionId = response?.Result?.Id ?? "0",
                TransactionType = "Mobile Recharge Transfer",
                TransactionAmount = Money(request.RechargeData.Amount),
                CommissionRate = response?.Result?.Balance?.Commission ?? "0",
                OperatorId = request.RechargeData.Operator,
                ProductId = request.RechargeData.ProductId.ToString(),
                PhoneNumber = request.RechargeData.Msisdn,
                UserReference = response?.Reference ?? userRef,
                CreatedDate = DateTime.UtcNow,
                ExtraInfo = response?.Result?.Instructions ?? "",
                CurrencyId = (int)Currencies.USD
            };
        }

        // Sends the email receipt and SMS for a successful mobile recharge.
        private async Task NotifyMobileRechargeAsync(
            PaymentRequests request, string userId, SochitelTransactionResponse response, string userRef)
        {
            var (token, validUntil) = ExtractAirtimeTokenAndValidity(response?.Result?.Instructions);
            var operatorName = response?.Result?.Operator?.Name ?? "Airtime";
            var productType = response?.Result?.ProductType ?? "Airtime";

            try
            {
                var user = await _userDetailsService.GetApplicationUserById(userId);
                if (user != null && !string.IsNullOrEmpty(user.Email))
                {
                    var body = MessagesTemplates.MobileRechargeReceiptEmail(
                        user.UserDetail.FirstName,
                        Money(request.Amount),
                        Money(request.RechargeData.Amount),
                        request.RechargeData.Msisdn,
                        response?.Result?.Id ?? "0",
                        response?.Reference ?? userRef,
                        DateTime.UtcNow.ToString("g"),
                        operatorName,
                        productType,
                        token,
                        validUntil);

                    await _emailSender.SendEmailAsync(
                        user.Email, body, user.UserDetail.FirstName, "WleeSend.com Airtime Credit Receipt");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send email receipt for User {UserId}", userId);
            }

            try
            {
                var message = $"You have received {productType} {operatorName} ${Money(request.RechargeData.Amount)} from WleeSend.com. Your Airtime token is {token} and valid until {validUntil}. Thank you.";
                _smsService.sendMessage(message, request.RechargeData.Msisdn);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send SMS for User {UserId}", userId);
            }
        }

        #endregion

        #region LEC Recharge

        // Runs the full LEC bill flow: authorize, recharge, capture or void, notify.
        public async Task<PaymentTransaction> ProcessLecRechargeAsync(LecCreditPaymentRequest request, string userId)
        {
            request.Payment.Amount = Math.Round(request.Payment.Amount * FeeMultiplier, 2);

            var balanceError = await ValidateSochitelBalanceAsync(request.Payment.RechargeData, request.AmountOperator);
            if (!string.IsNullOrEmpty(balanceError))
                return BuildPreAuthFailure(
                    userId, request.Payment.Amount, request.Payment.ReceiverPhoneNumber,
                    request.Payment.TransferTypeId, balanceError);

            var transaction = await AuthorizePaymentAsync(BuildCardPaymentRequest(request.Payment), userId);
            if (string.IsNullOrEmpty(transaction?.TransactionId))
                return transaction;

            SochitelTransactionResponse response = null;
            bool rechargeDelivered = false;
            bool captured = false;

            try
            {
                var payload = BuildLecPayload(request);

                _logger.LogInformation(
                    "Sending LEC recharge. Operator={Operator} Account={AccountId} Amount={Amount} Simulate={Simulate}",
                    payload.Operator, payload.AccountId, payload.AmountOperator, payload.Simulate);

                var json = await _externalApiService.PostDataAsync<LecBillRechargePayload>(payload);
                _logger.LogInformation("LEC Provider Response (AuthNet {TransId}): {Response}", transaction.TransactionId, json);

                var statusName = GetStatusName(json);

                if (string.Equals(statusName, "Successful", StringComparison.OrdinalIgnoreCase))
                {
                    rechargeDelivered = true;
                    response = TryMapResponse(json, transaction.TransactionId);
                    captured = await CaptureAfterDeliveryAsync(transaction, "LEC");
                }
                else
                {
                    _logger.LogWarning(
                        "Sochitel rejected LEC recharge. AuthNet {TransId}, Status {Status}. Raw: {Json}",
                        transaction.TransactionId, statusName, json);
                    await VoidAndFailAsync(transaction);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during LEC recharge for User {UserId}, AuthNet {TransId}", userId, transaction.TransactionId);
                await HandleRechargeExceptionAsync(transaction, rechargeDelivered, transaction.TransactionId);
            }

            if (rechargeDelivered)
                await SaveSochitelRecordAsync(BuildLecSochitelRecord(request, response));

            if (captured)
                await NotifyLecRechargeAsync(request, userId, response);

            await SafeLogActivityAsync(userId, captured
                ? $"Successfully processed LEC recharge for meter {request.AccountId}. Amount: {request.AmountOperator}"
                : $"LEC recharge FAILED for meter {request.AccountId}. Amount: {request.AmountOperator}. Reason: {transaction.ErrorMessage}");

            return await SaveFinalAndReloadAsync(transaction);
        }

        // Copies card and billing details into a fresh payment request without recharge data.
        private static PaymentRequests BuildCardPaymentRequest(PaymentRequests payment)
        {
            return new PaymentRequests
            {
                CardNumber = payment.CardNumber,
                CVV = payment.CVV,
                Expiration = payment.Expiration,
                NameOnCard = payment.NameOnCard,
                StreetAddress = payment.StreetAddress,
                City = payment.City,
                ZipCode = payment.ZipCode,
                Amount = payment.Amount,
                ReceiverPhoneNumber = payment.ReceiverPhoneNumber,
                TransferTypeId = payment.TransferTypeId
            };
        }

        // Builds the Sochitel payload for an LEC bill payment.
        private LecBillRechargePayload BuildLecPayload(LecCreditPaymentRequest request)
        {
            var data = request.Payment.RechargeData;
            return new LecBillRechargePayload
            {
                Auth = BuildFreshAuth(),
                Command = data.Command,
                Operator = data.Operator,
                Simulate = data.Simulate,
                Version = 5,
                AccountId = request.AccountId,
                AmountOperator = Money(request.AmountOperator),
                ExtraParameters = new { accountType = "OFFLINE_PREPAID" }
            };
        }

        // Builds the Sochitel transaction record for an LEC payment.
        private static SochitelTransactions BuildLecSochitelRecord(
            LecCreditPaymentRequest request, SochitelTransactionResponse response)
        {
            return new SochitelTransactions
            {
                TransactionId = response?.Result?.Id ?? "0",
                TransactionType = "Bill Payment",
                TransactionAmount = Money(request.Payment.Amount),
                CommissionRate = response?.Result?.Balance?.Commission ?? "0",
                OperatorId = request.Payment.RechargeData.Operator,
                ProductId = request.Payment.RechargeData.ToString(),
                PhoneNumber = request.Payment.ReceiverPhoneNumber,
                UserReference = response?.Reference,
                CreatedDate = DateTime.UtcNow,
                ExtraInfo = response?.Result?.Instructions ?? "",
                CurrencyId = (int)Currencies.USD,
                MeterNumber = request.AccountId
            };
        }

        // Sends the email receipt and SMS for a successful LEC payment.
        private async Task NotifyLecRechargeAsync(
            LecCreditPaymentRequest request, string userId, SochitelTransactionResponse response)
        {
            InstructionDetails details;
            try { details = ParseInstructions(response?.Result?.Instructions); }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed parsing LEC instructions");
                details = new InstructionDetails();
            }

            var token = !string.IsNullOrWhiteSpace(details.Token) ? details.Token : "N/A";

            try
            {
                var user = await _userDetailsService.GetApplicationUserById(userId);
                if (user != null && !string.IsNullOrEmpty(user.Email))
                {
                    var body = MessagesTemplates.LecCreditReceiptEmail(
                        user.UserDetail.FirstName,
                        Money(request.Payment.Amount),
                        Money(request.AmountOperator),
                        request.Payment.ReceiverPhoneNumber,
                        token,
                        request.AccountId,
                        details.Units.HasValue ? Money(details.Units.Value) : "N/A",
                        response?.Result?.Id ?? "0",
                        DateTime.UtcNow.ToString("g"));

                    await _emailSender.SendEmailAsync(
                        user.Email, body, user.UserDetail.FirstName, "WleeSend.com LEC Credit Receipt");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send LEC email receipt for User {UserId}", userId);
            }

            try
            {
                var message = $"WleeSend: LEC credit ${Money(request.AmountOperator)} Meter {request.AccountId}. Token {token}. Thank you.";
                _smsService.sendMessage(message, request.Payment.ReceiverPhoneNumber);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send LEC SMS for User {UserId}", userId);
            }
        }

        #endregion

        #region Instruction Parsing

        // Formats a decimal with two places using invariant culture.
        private static string Money(decimal value) => value.ToString("F2", CultureInfo.InvariantCulture);

        // Returns the text after the first colon, keeping any later colons.
        private static string ValueAfterColon(string line)
        {
            var idx = line.IndexOf(':');
            return idx >= 0 ? line.Substring(idx + 1).Trim() : line.Trim();
        }

        // Parses a decimal safely, returning null on failure.
        private static decimal? TryDecimal(string s) =>
            decimal.TryParse(s, NumberStyles.Number, CultureInfo.InvariantCulture, out var d) ? d : (decimal?)null;

        // Parses the provider's multi-line instructions text into structured details.
        private static InstructionDetails ParseInstructions(string instructions)
        {
            var details = new InstructionDetails();
            if (string.IsNullOrWhiteSpace(instructions))
                return details;

            foreach (var raw in instructions.Split(LineSeparators, StringSplitOptions.RemoveEmptyEntries))
            {
                var line = raw.Trim();

                if (line.StartsWith("Amount:", StringComparison.OrdinalIgnoreCase))
                {
                    var d = TryDecimal(ValueAfterColon(line));
                    if (d.HasValue) details.Amount = d.Value;
                }
                else if (line.StartsWith("Date:", StringComparison.OrdinalIgnoreCase))
                {
                    if (DateTime.TryParse(ValueAfterColon(line), CultureInfo.InvariantCulture, DateTimeStyles.None, out var dt))
                        details.Date = dt;
                }
                else if (line.StartsWith("Description:", StringComparison.OrdinalIgnoreCase))
                    details.Description = line;
                else if (line.StartsWith("Units:", StringComparison.OrdinalIgnoreCase))
                {
                    var d = TryDecimal(ValueAfterColon(line));
                    if (d.HasValue) details.Units = d.Value;
                }
                else if (line.StartsWith("Tax:", StringComparison.OrdinalIgnoreCase))
                {
                    var d = TryDecimal(ValueAfterColon(line));
                    if (d.HasValue) details.Tax = d.Value;
                }
                else if (line.StartsWith("Receipt Number:", StringComparison.OrdinalIgnoreCase))
                    details.ReceiptNumber = ValueAfterColon(line);
                else if (line.StartsWith("Account Number:", StringComparison.OrdinalIgnoreCase))
                    details.AccountNumber = ValueAfterColon(line);
                else if (line.StartsWith("Customer Name:", StringComparison.OrdinalIgnoreCase))
                    details.CustomerName = ValueAfterColon(line);
                else if (line.StartsWith("Token Type:", StringComparison.OrdinalIgnoreCase))
                    details.TokenType = ValueAfterColon(line);
                else if (line.StartsWith("Token:", StringComparison.OrdinalIgnoreCase))
                    details.Token = ValueAfterColon(line);
            }

            return details;
        }

        // Extracts the airtime token and validity text, falling back to a 20-digit match.
        private static (string Token, string ValidUntil) ExtractAirtimeTokenAndValidity(string instructions)
        {
            if (string.IsNullOrWhiteSpace(instructions))
                return ("N/A", "N/A");

            var token = string.Empty;
            var validUntil = string.Empty;

            foreach (var raw in instructions.Split(LineSeparators, StringSplitOptions.RemoveEmptyEntries))
            {
                var line = raw.Trim();

                if (line.StartsWith("Token:", StringComparison.OrdinalIgnoreCase))
                    token = ValueAfterColon(line);
                else if (line.StartsWith("Valid until", StringComparison.OrdinalIgnoreCase) ||
                         line.StartsWith("Valid Till", StringComparison.OrdinalIgnoreCase) ||
                         line.StartsWith("Validity", StringComparison.OrdinalIgnoreCase) ||
                         line.StartsWith("Expires", StringComparison.OrdinalIgnoreCase))
                    validUntil = ValueAfterColon(line);
            }

            if (string.IsNullOrWhiteSpace(token))
            {
                var match = Regex.Match(instructions, @"\b\d{20}\b");
                if (match.Success)
                    token = match.Value;
            }

            return (
                string.IsNullOrWhiteSpace(token) ? "N/A" : token,
                string.IsNullOrWhiteSpace(validUntil) ? "N/A" : validUntil);
        }

        #endregion
    }
}
