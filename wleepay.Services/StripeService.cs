using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Stripe;
using Stripe.Checkout;
using wleepay.Communication.Interface;
using wleepay.Communication.Templates;
using wleepay.Core.Configurations;
using wleepay.Core.DTO.Request;
using wleepay.Core.DTO.Response;
using wleepay.Core.Enums;
using wleepay.Core.Models;
using wleepay.Core.Repositories;
using wleepay.Core.Services;

namespace wleepay.Services
{
    public class StripeService : IStripeService
    {
        #region Constants

        private const string GenericFailureMessage = "We couldn't complete this purchase. You have not been charged. Please try again.";
        private const string VoidFailedMessage = "We couldn't complete this purchase. Please contact support so we can release the hold on your card.";
        private const string DeliveredUnconfirmedMessage = "Your purchase was delivered but we had a problem confirming payment. Please contact support.";
        private const string ProviderUnavailableMessage = "We're unable to process this right now. Please try again shortly.";
        private const string InsufficientProviderBalanceMessage = "We're unable to process this right now. Please try again later.";
        private const string PaymentFailedPrefix = "Payment failed: ";

        // Metadata keys stored on the Checkout Session and its PaymentIntent.
        private const string MetaFlow = "flow";                      // mobile | lec | voucher
        private const string MetaUserId = "userId";                  // optional (anonymous checkout)
        private const string MetaPhone = "receiverPhone";
        private const string MetaTransferType = "transferTypeId";
        private const string MetaRecharge = "recharge";              // JSON of RechargeRequestDetails
        private const string MetaProductName = "productName";
        private const string MetaAccountId = "accountId";            // LEC only
        private const string MetaAmountOperator = "amountOperator";  // LEC only
        private const string MetaOperatorId = "operatorId";
        private const string MetaProductId = "productId";
        private static readonly string[] LineSeparators = { "\r\n", "\n" };

        #endregion

        #region Fields and Constructor

        private readonly IConfiguration _configuration;
        private readonly ICouponCodeRepository _couponCodeRepository;
        private readonly SochitelSettings _sochitelSettings;
        private readonly IExternalApiService _externalApiService;
        private readonly IMapper _mapper;
        private readonly ISochitelTransactionService _sochitelTransactionService;
        private readonly ISMSService _smsService;
        private readonly IEmailSender _emailSender;
        private readonly IUserDetailService _userDetailsService;
        private readonly IUserDeviceService _userDeviceService;
        private readonly IFirebaseNotification _firebaseNotification;
        private readonly IPaymentTransactionService _transactionService;
        private readonly ILogger<StripeService> _logger;
        private readonly ISystemActivityLogsService _activityLogService;

        private readonly SessionService _sessions = new SessionService();
        private readonly PaymentIntentService _paymentIntents = new PaymentIntentService();
        private readonly string _defaultApiKey;

        public StripeService(
            IConfiguration configuration,
            ICouponCodeRepository couponCodeRepository,
            ILogger<StripeService> logger,
            IExternalApiService externalApiService,
            IMapper mapper,
            ISochitelTransactionService sochitelTransactionService,
            IPaymentTransactionService transactionService,
            ISMSService smsService,
            ISystemActivityLogsService activityLogService,
            IEmailSender emailSender,
            IUserDetailService userDetailsService,
            IUserDeviceService userDeviceService,
            IFirebaseNotification firebaseNotification,
            IOptions<SochitelSettings> sochitelSettings)
        {
            _configuration = configuration;
            _couponCodeRepository = couponCodeRepository;
            _logger = logger;
            _externalApiService = externalApiService;
            _mapper = mapper;
            _sochitelTransactionService = sochitelTransactionService;
            _transactionService = transactionService;
            _smsService = smsService;
            _activityLogService = activityLogService;
            _emailSender = emailSender;
            _userDetailsService = userDetailsService;
            _userDeviceService = userDeviceService;
            _firebaseNotification = firebaseNotification;
            _sochitelSettings = sochitelSettings.Value;

            _defaultApiKey = configuration["Stripe:SecretKey"];
            if (string.IsNullOrWhiteSpace(_defaultApiKey))
                throw new InvalidOperationException("Stripe:SecretKey is not configured.");
        }

        // The key travels with each request, so the global StripeConfiguration.ApiKey is never touched.
        private RequestOptions Opts(string apiKey = null, string idempotencyKey = null) => new RequestOptions
        {
            ApiKey = string.IsNullOrWhiteSpace(apiKey) ? _defaultApiKey : apiKey,
            IdempotencyKey = idempotencyKey
        };

        // Who receives the receipt. Works for logged-in users and anonymous checkouts.
        private class Recipient
        {
            public string Email { get; set; }
            public string FirstName { get; set; }
        }

        #endregion

        #region Checkout Session Creation (one builder for mobile, LEC, voucher and plain payments)

        // `recharge` == null -> plain payment (automatic capture, no metadata).
        // `recharge` != null -> card hold (manual capture) + metadata for the webhook.
        // The caller supplies the final amount (service fee already included).
        public async Task<CheckoutSessionResponse> CreateEmbeddedCheckoutSessionAsync(CreateCheckoutSessionRequest request, string apiKey = null, RechargeCheckoutContext recharge = null)
        {
            var isRecharge = recharge != null;

            try
            {
                ValidateCheckoutRequest(request);

                var key = string.IsNullOrWhiteSpace(apiKey) ? _defaultApiKey : apiKey;
                ValidateApiKey(key);

                Dictionary<string, string> metadata = null;

                if (isRecharge)
                {
                    ValidateRecharge(recharge);

                    var requested = recharge.Flow == RechargeFlows.Lec ? recharge.AmountOperator : recharge.Recharge.Amount;
                    var balanceError = await ValidateSochitelBalanceAsync(recharge.Recharge, requested);
                    if (!string.IsNullOrEmpty(balanceError))
                        return Fail(balanceError);

                    metadata = BuildMetadata(recharge);
                }

                var baseUrl = _configuration["AppSettings:BaseUrl"] ?? "http://localhost:4200";

                var options = new SessionCreateOptions
                {
                    UiMode = request.HostedCheckout ? null : "embedded_page",
                    Mode = "payment",
                    BillingAddressCollection = isRecharge && recharge.Flow == RechargeFlows.Voucher ? "auto" : "required",
                    ReturnUrl = request.HostedCheckout ? null : $"{baseUrl}/{request.ReturnUrl}",
                    SuccessUrl = request.HostedCheckout ? request.SuccessUrl : null,
                    CancelUrl = request.HostedCheckout ? request.CancelUrl : null,
                    LineItems = new List<SessionLineItemOptions>
                    {
                        new SessionLineItemOptions
                        {
                            Quantity = 1,
                            PriceData = new SessionLineItemPriceDataOptions
                            {
                                Currency = request.Currency.ToLowerInvariant(),
                                UnitAmount = ToCents(request.Amount),
                                ProductData = new SessionLineItemPriceDataProductDataOptions
                                {
                                    Name = request.ProductName,
                                    TaxCode = string.IsNullOrWhiteSpace(request.TaxCode) ? null : request.TaxCode
                                }
                            }
                        }
                    }
                };

                if (isRecharge)
                {
                    // Older production JWTs contain an email claim but no user ID.
                    // Use that signed claim so Stripe can prefill email even when
                    // the optional user lookup cannot be performed.
                    if (!string.IsNullOrWhiteSpace(recharge.CustomerEmail))
                    {
                        options.CustomerEmail = recharge.CustomerEmail;
                    }
                    else if (!string.IsNullOrWhiteSpace(recharge.UserId))
                    {
                        var user = await _userDetailsService.GetApplicationUserById(recharge.UserId);
                        if (!string.IsNullOrWhiteSpace(user?.Email))
                            options.CustomerEmail = user.Email;
                    }
                }

                if (isRecharge)
                {
                    options.Metadata = metadata;
                    if (metadata.TryGetValue(MetaUserId, out var userId))
                        options.ClientReferenceId = userId;

                    options.PaymentIntentData = new SessionPaymentIntentDataOptions
                    {
                        CaptureMethod = "manual",   
                        Metadata = metadata        
                    };
                }

                if (!string.IsNullOrWhiteSpace(request.PromotionCodeId))
                {
                    var coupon = await _couponCodeRepository.GetByCodeAsync(request.PromotionCodeId);
                    if (coupon == null || string.IsNullOrWhiteSpace(coupon.StripePromoCodeId))
                        throw new ArgumentException("Invalid promotion code");

                    options.Discounts = new List<SessionDiscountOptions>
                    {
                        new SessionDiscountOptions { PromotionCode = coupon.StripePromoCodeId }
                    };
                }

                var session = await _sessions.CreateAsync(options, Opts(key));

                return new CheckoutSessionResponse
                {
                    Success = true,
                    ClientSecret = session.ClientSecret,
                    SessionId = session.Id,
                    Url = session.Url,
                    Message = "Checkout session created successfully"
                };
            }
            catch (StripeException ex)
            {
                _logger.LogError(ex, "Stripe error while creating checkout session.");
                return Fail(isRecharge ? GenericFailureMessage : $"Stripe error: {ex.Message}");
            }
            catch (ArgumentException ex)
            {
                return Fail(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while creating checkout session.");
                return Fail(isRecharge ? GenericFailureMessage : $"An error occurred: {ex.Message}");
            }
        }

        public async Task<MobilePaymentIntentResponse> CreateMobilePaymentIntentAsync(decimal amount, RechargeCheckoutContext recharge, string apiKey = null)
        {
            ValidateRecharge(recharge);
            if (amount <= 0) throw new ArgumentException("Payment amount must be greater than zero.");
            var key = string.IsNullOrWhiteSpace(apiKey) ? _defaultApiKey : apiKey;
            ValidateApiKey(key);
            var requested = recharge.Flow == RechargeFlows.Lec ? recharge.AmountOperator : recharge.Recharge.Amount;
            var balanceError = await ValidateSochitelBalanceAsync(recharge.Recharge, requested);
            if (!string.IsNullOrEmpty(balanceError)) return new MobilePaymentIntentResponse { Success = false, Message = balanceError };

            var metadata = BuildMetadata(recharge);
            var intent = await _paymentIntents.CreateAsync(new PaymentIntentCreateOptions
            {
                Amount = ToCents(amount),
                Currency = "usd",
                CaptureMethod = "manual",
                Description = recharge.ProductName ?? "Wleesend mobile recharge",
                ReceiptEmail = recharge.CustomerEmail,
                Metadata = metadata,
                AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
                {
                    Enabled = true,
                    AllowRedirects = "never"
                }
            }, Opts(key));

            return new MobilePaymentIntentResponse
            {
                Success = true,
                PaymentIntentClientSecret = intent.ClientSecret,
                PaymentIntentId = intent.Id,
                PublishableKey = _configuration["Stripe:Publishablekey"]
            };
        }

        public Task<Session> GetCheckoutSessionAsync(string sessionId)
        {
            if (string.IsNullOrWhiteSpace(sessionId) || !sessionId.StartsWith("cs_", StringComparison.Ordinal))
                throw new ArgumentException("Invalid checkout session ID.", nameof(sessionId));

            ValidateApiKey(_defaultApiKey);
            return _sessions.GetAsync(sessionId, null, Opts());
        }

        public async Task<(Session Session, bool PaymentConfirmed)> GetCheckoutSessionStatusAsync(string sessionId)
        {
            var session = await GetCheckoutSessionAsync(sessionId);
            if (!string.Equals(session.Status, "complete", StringComparison.OrdinalIgnoreCase) ||
                string.IsNullOrWhiteSpace(session.PaymentIntentId) ||
                session.Metadata == null ||
                !session.Metadata.TryGetValue(MetaFlow, out var flow) ||
                !IsKnownFlow(flow))
                return (session, false);

            var paymentIntent = await _paymentIntents.GetAsync(session.PaymentIntentId, null, Opts());
            var confirmed = paymentIntent.Status == "requires_capture" || paymentIntent.Status == "succeeded";
            return (session, confirmed);
        }

        private static CheckoutSessionResponse Fail(string message)
            => new CheckoutSessionResponse { Success = false, Message = message };

        private static long ToCents(decimal amount)
            => (long)Math.Round(amount * 100m, MidpointRounding.AwayFromZero);

        private static void ValidateCheckoutRequest(CreateCheckoutSessionRequest request)
        {
            if (request == null)
                throw new ArgumentException("Checkout request is required");
            if (request.Amount <= 0)
                throw new ArgumentException("Amount must be greater than 0");
            if (string.IsNullOrWhiteSpace(request.ReturnUrl))
                throw new ArgumentException("Return URL is required");
            if (string.IsNullOrWhiteSpace(request.Currency))
                throw new ArgumentException("Currency is required");
            if (string.IsNullOrWhiteSpace(request.ProductName))
                throw new ArgumentException("Product name is required");
            if (string.IsNullOrWhiteSpace(request.TaxCode))
                throw new ArgumentException("Tax code is required");
            if (request.HostedCheckout &&
                (string.IsNullOrWhiteSpace(request.SuccessUrl) || string.IsNullOrWhiteSpace(request.CancelUrl) ||
                 !Uri.TryCreate(request.SuccessUrl, UriKind.Absolute, out var successUrl) || successUrl.Scheme != Uri.UriSchemeHttps ||
                 !Uri.TryCreate(request.CancelUrl, UriKind.Absolute, out var cancelUrl) || cancelUrl.Scheme != Uri.UriSchemeHttps))
                throw new ArgumentException("Hosted checkout requires valid HTTPS return URLs.");
        }

        private static void ValidateApiKey(string apiKey)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
                throw new ArgumentException("Stripe API key is not configured");
        }

        private static bool IsKnownFlow(string flow)
            => flow == RechargeFlows.Mobile || flow == RechargeFlows.Lec || flow == RechargeFlows.Voucher;

        private static void ValidateRecharge(RechargeCheckoutContext c)
        {
            if (!IsKnownFlow(c.Flow))
                throw new ArgumentException("Unknown purchase type.");
            if (c.Recharge == null)
                throw new ArgumentException("Missing recharge data");
            if (c.Flow == RechargeFlows.Lec && (string.IsNullOrWhiteSpace(c.AccountId) || c.AmountOperator <= 0))
                throw new ArgumentException("Meter number and amount are required.");
            if (c.Flow == RechargeFlows.Mobile && string.IsNullOrWhiteSpace(c.Recharge.Msisdn))
                throw new ArgumentException("Phone number is required.");
        }

        // Builds the metadata Stripe stores. Empty values are dropped; Stripe allows 500 characters per value.
        private static Dictionary<string, string> BuildMetadata(RechargeCheckoutContext c)
        {
            var m = new Dictionary<string, string>
            {
                [MetaFlow] = c.Flow,
                [MetaUserId] = c.UserId,
                [MetaPhone] = c.ReceiverPhoneNumber,
                [MetaTransferType] = c.TransferTypeId.ToString(CultureInfo.InvariantCulture),
                [MetaRecharge] = JsonConvert.SerializeObject(c.Recharge,
                    new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
                [MetaProductName] = c.ProductName,
                [MetaAccountId] = c.AccountId,
                [MetaProductId] = c.Recharge.ProductId.ToString(),
                [MetaOperatorId] = c.Flow == RechargeFlows.Lec
                    ? c.Recharge.OperatorId.ToString(CultureInfo.InvariantCulture)
                    : c.Recharge.Operator,
                [MetaAmountOperator] = c.Flow == RechargeFlows.Lec ? Money(c.AmountOperator) : null
            };
            if (c.NativePaymentSheet) m["paymentSource"] = "mobile_payment_sheet";

            foreach (var emptyKey in m.Where(kv => string.IsNullOrEmpty(kv.Value)).Select(kv => kv.Key).ToList())
                m.Remove(emptyKey);

            if (m.Values.Any(v => v.Length > 500))
                throw new ArgumentException("Order details are too large to process.");

            return m;
        }

        #endregion

        #region Webhook Handlers

        // checkout.session.completed: for recharge sessions the card hold exists. Deliver, then capture or cancel.
        public async Task HandleCheckoutCompletedAsync(Session session)
        {
            var meta = session.Metadata ?? new Dictionary<string, string>();

            // Plain payments (no recharge metadata) hit the same webhook. They are not ours to process.
            if (!meta.TryGetValue(MetaFlow, out var flow) || !IsKnownFlow(flow))
            {
                _logger.LogInformation("Checkout session {Id} is not a recharge session. Ignored.", session.Id);
                return;
            }

            if (string.IsNullOrEmpty(session.PaymentIntentId))
            {
                _logger.LogWarning("Checkout session {Id} has no PaymentIntent.", session.Id);
                return;
            }

            // Re-read from Stripe: the event payload can be stale.
            var pi = await _paymentIntents.GetAsync(session.PaymentIntentId, null, Opts());

            if (pi.Status != "requires_capture")
            {
                _logger.LogInformation("PaymentIntent {Id} is {Status}, nothing to do.", pi.Id, pi.Status);
                return;
            }

            var existing = await _transactionService.GetByTransactionIdAsync(pi.Id);
            var failedBeforeSuccess = existing != null
                && existing.Status == "Failed"
                && (existing.ErrorMessage ?? string.Empty).StartsWith(PaymentFailedPrefix, StringComparison.Ordinal);

            if (existing != null && !failedBeforeSuccess)
            {
                _logger.LogInformation("PaymentIntent {Id} already handled (status {Status}). Skipping.", pi.Id, existing.Status);
                return;
            }

            meta.TryGetValue(MetaUserId, out var userId);       
            meta.TryGetValue(MetaPhone, out var phone);
            meta.TryGetValue(MetaTransferType, out var transferTypeRaw);
            meta.TryGetValue(MetaRecharge, out var rechargeJson);
            meta.TryGetValue(MetaProductName, out var productName);
            meta.TryGetValue(MetaOperatorId, out var operatorId);
            int.TryParse(transferTypeRaw, out var transferTypeId);

            var transaction = new PaymentTransaction
            {
                UserId = userId,
                Amount = pi.Amount / 100m,
                ReceiverPhoneNumber = phone,
                TransferTypeId = transferTypeId,
                CurrencyId = (int)Currencies.USD,
                TransactionId = pi.Id,
                Status = "Pending",
                CreatedAt = DateTime.UtcNow
            };

            // Saved first so a redelivered event is skipped.

            try { await _transactionService.SaveTransactionAsync(_mapper.Map<PaymentTransactionDto>(transaction)); } 
            catch(Exception ex) {
                throw new Exception(ex.Message);
            }

            RechargeRequestDetails recharge = null;
            try { recharge = JsonConvert.DeserializeObject<RechargeRequestDetails>(rechargeJson ?? ""); }
            catch (Exception ex) { _logger.LogError(ex, "Bad recharge metadata on {Id}", pi.Id); }

            if (recharge == null)
            {
                _logger.LogError("Missing or invalid recharge metadata on PaymentIntent {Id}. Releasing hold.", pi.Id);
                await VoidAndFailAsync(transaction);
                await SaveFinalAndReloadAsync(transaction);
                return;
            }

            var recipient = await ResolveRecipientAsync(userId, session);

            if (flow == RechargeFlows.Lec)
            {
                meta.TryGetValue(MetaAccountId, out var accountId);
                meta.TryGetValue(MetaAmountOperator, out var amountOperatorRaw);
                decimal.TryParse(amountOperatorRaw, NumberStyles.Number, CultureInfo.InvariantCulture, out var amountOperator);

                await RunLecRechargeAsync(transaction, userId, recipient, new LecCreditPaymentRequest
                {
                    AccountId = accountId,
                    AmountOperator = amountOperator,
                    OperatorId = operatorId,
                    Payment = new PaymentRequests
                    {
                        Amount = transaction.Amount,
                        ReceiverPhoneNumber = phone,
                        TransferTypeId = transferTypeId,
                        RechargeData = recharge
                    }
                });
            }
            else
            {
                await RunTopupAsync(transaction, userId, recipient, productName, flow == RechargeFlows.Voucher,
                    new PaymentRequests
                    {
                        Amount = transaction.Amount,
                        ReceiverPhoneNumber = phone,
                        TransferTypeId = transferTypeId,
                        RechargeData = recharge
                    });
            }
        }

        public Task HandlePaymentIntentAuthorizedAsync(PaymentIntent paymentIntent)
        {
            if (paymentIntent == null || paymentIntent.Status != "requires_capture") return Task.CompletedTask;
            var metadata = paymentIntent.Metadata ?? new Dictionary<string, string>();
            if (!metadata.TryGetValue("paymentSource", out var source) || source != "mobile_payment_sheet") return Task.CompletedTask;
            if (!metadata.TryGetValue(MetaFlow, out var flow) || !IsKnownFlow(flow)) return Task.CompletedTask;
            return HandleCheckoutCompletedAsync(new Session
            {
                Id = paymentIntent.Id,
                PaymentIntentId = paymentIntent.Id,
                Metadata = metadata
            });
        }

        // payment_intent.payment_failed: card declined or authentication failed.
        // No money moved, so this only records it. A Checkout customer can retry in the same session,
        // and a later success arrives as checkout.session.completed.
        public async Task HandlePaymentFailedAsync(PaymentIntent pi)
        {
            var reason = pi.LastPaymentError?.Message ?? "Payment failed";
            _logger.LogWarning("PaymentIntent {Id} failed: {Reason}", pi.Id, reason);

            var meta = pi.Metadata ?? new Dictionary<string, string>();
            meta.TryGetValue(MetaUserId, out var userId);

            if (meta.TryGetValue(MetaFlow, out var flow) && IsKnownFlow(flow))
            {
                var transaction = await _transactionService.GetByTransactionIdAsync(pi.Id);

                if (transaction != null && transaction.Status != "Failed")
                {
                    _logger.LogInformation("PaymentIntent {Id} already {Status}. Failed event ignored.", pi.Id, transaction.Status);
                    return;
                }

                if (transaction == null)
                {
                    meta.TryGetValue(MetaPhone, out var phone);
                    meta.TryGetValue(MetaTransferType, out var transferTypeRaw);
                    int.TryParse(transferTypeRaw, out var transferTypeId);

                    transaction = new PaymentTransaction
                    {
                        UserId = userId,
                        Amount = pi.Amount / 100m,
                        ReceiverPhoneNumber = phone,
                        TransferTypeId = transferTypeId,
                        CurrencyId = (int)Currencies.USD,
                        TransactionId = pi.Id,
                        CreatedAt = DateTime.UtcNow
                    };
                }

                transaction.Status = "Failed";
                transaction.ErrorMessage = PaymentFailedPrefix + reason;

                await _transactionService.SaveTransactionAsync(_mapper.Map<PaymentTransactionDto>(transaction));
            }

            if (!string.IsNullOrEmpty(userId))
                await SafeLogActivityAsync(userId, $"Card payment failed. PaymentIntent {pi.Id}. Reason: {reason}");
        }

        // Logged-in user's details if available, otherwise the email and name Stripe collected.
        private async Task<Recipient> ResolveRecipientAsync(string userId, Session session)
        {
            var stripeEmail = session.CustomerDetails?.Email;
            var stripeName = session.CustomerDetails?.Name;
            var fallbackName = string.IsNullOrWhiteSpace(stripeName) ? "Customer" : stripeName;

            if (!string.IsNullOrEmpty(userId))
            {
                try
                {
                    var user = await _userDetailsService.GetApplicationUserById(userId);
                    if (user != null && !string.IsNullOrEmpty(user.Email))
                        return new Recipient { Email = user.Email, FirstName = user.UserDetail?.FirstName ?? fallbackName };
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Could not load user {UserId} for receipt. Using Stripe details.", userId);
                }
            }

            return new Recipient { Email = stripeEmail, FirstName = fallbackName };
        }

        #endregion

        #region Stripe Operations (capture / void)

        // Captures a held PaymentIntent. The idempotency key makes retries safe.
        private async Task<PaymentTransaction> CapturePaymentAsync(string paymentIntentId)
        {
            var existing = await _transactionService.GetByTransactionIdAsync(paymentIntentId)
                           ?? new PaymentTransaction { TransactionId = paymentIntentId };

            try
            {
                var pi = await _paymentIntents.CaptureAsync(
                    paymentIntentId, new PaymentIntentCaptureOptions(), Opts(null, "capture-" + paymentIntentId));

                if (pi.Status == "succeeded")
                {
                    existing.Status = "Success";
                    existing.ErrorMessage = null;
                }
                else
                {
                    existing.Status = "Failed";
                    existing.ErrorMessage = "Unexpected PaymentIntent status after capture: " + pi.Status;
                }
            }
            catch (StripeException ex)
            {
                existing.Status = "Failed";
                existing.ErrorMessage = ex.StripeError?.Message ?? ex.Message;
            }

            await _transactionService.SaveTransactionAsync(_mapper.Map<PaymentTransactionDto>(existing));
            return existing;
        }

        // Cancels the hold and records whether the cancel really succeeded.
        private async Task<PaymentTransaction> VoidPaymentAsync(string paymentIntentId)
        {
            var existing = await _transactionService.GetByTransactionIdAsync(paymentIntentId);

            var voidOk = false;
            string error = null;

            try
            {
                var pi = await _paymentIntents.CancelAsync(
                    paymentIntentId, new PaymentIntentCancelOptions(), Opts(null, "cancel-" + paymentIntentId));

                voidOk = pi.Status == "canceled";
                if (!voidOk) error = "Unexpected PaymentIntent status after cancel: " + pi.Status;
            }
            catch (StripeException ex)
            {
                error = ex.StripeError?.Message ?? ex.Message;
            }

            if (existing != null)
            {
                existing.Status = voidOk ? "Voided" : "VoidFailed";
                existing.ErrorMessage = voidOk ? null : (error ?? "Void failed");
                await _transactionService.SaveTransactionAsync(_mapper.Map<PaymentTransactionDto>(existing));
            }

            if (!voidOk)
                _logger.LogCritical("VOID FAILED for Stripe PaymentIntent {Id}. Manual action required.", paymentIntentId);

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
                _logger.LogCritical(ex, "VOID THREW for Stripe PaymentIntent {Id}. Manual action required.", transactionId);
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
                "{Flow}: CREDIT DELIVERED BUT CAPTURE FAILED. PaymentIntent {Id}, Error {Err}",
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
                    "Exception AFTER credit delivered. Do NOT void. PaymentIntent {Id}, Ref {Ref}",
                    transaction.TransactionId, reference);
                transaction.Status = "Failed";
                transaction.ErrorMessage = DeliveredUnconfirmedMessage;
                return;
            }

            _logger.LogWarning("Voiding after exception. Verify Ref {Ref} in Sochitel if this was a timeout.", reference);
            await VoidAndFailAsync(transaction);
        }

        // Saves the final transaction state and returns the stored copy.
        private async Task<PaymentTransaction> SaveFinalAndReloadAsync(PaymentTransaction transaction)
        {
            try
            {
            await _transactionService.SaveTransactionAsync(_mapper.Map<PaymentTransactionDto>(transaction));
            var reloaded = await _transactionService.GetByTransactionIdAsync(transaction.TransactionId);
            return reloaded ?? transaction;

            }catch(Exception ex)
            {
                throw ex;
            }
        }

        // Writes a payment activity log entry without ever throwing. Anonymous purchases are only logged to the logger.
        private async Task SafeLogActivityAsync(string userId, string description)
        {
            if (string.IsNullOrEmpty(userId))
            {
                _logger.LogInformation("Anonymous payment activity: {Description}", description);
                return;
            }

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

        // Same Sochitel reference for the same PaymentIntent, so a retry can never deliver credit twice.
        private static string BuildUserReference(string paymentIntentId)
            => "wlee" + Sha1Hex(paymentIntentId).Substring(0, 12);

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

        private static string GetStatusId(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
                return null;

            try { return JObject.Parse(json)["status"]?["id"]?.ToString(); }
            catch (JsonReaderException) { return null; }
        }

        // Maps a successful Sochitel response to the DTO, returning null if mapping fails.
        private SochitelTransactionResponse TryMapResponse(string json, string reference)
        {
            try
            {
                var responseJson = JObject.Parse(json);
                var providerOperator = responseJson["result"]?["operator"] as JObject;
                if (providerOperator != null)
                {
                    var providerOperatorId = providerOperator["id"]?.ToString();
                    if (!Guid.TryParse(providerOperatorId, out _))
                    {
                        // The Sochitel API uses string IDs; the shared Operator model has a Guid PK.
                        // Preserve the provider ID in SochitelId and keep its DB identity unset.
                        providerOperator["sochitelId"] = providerOperatorId;
                        providerOperator["id"] = Guid.Empty;
                    }
                }

                return responseJson.ToObject<SochitelTransactionResponse>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Could not map Sochitel success response for {Ref}.", reference);
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

        #region Mobile Recharge and Voucher (same Sochitel call, different receipt)

        // Runs from the webhook: deliver credit, then capture or void, notify, log.
        private async Task RunTopupAsync(
            PaymentTransaction transaction, string userId, Recipient recipient,
            string productName, bool isVoucher, PaymentRequests request)
        {
            var flowName = isVoucher ? "Voucher" : "Mobile";
            var userRef = BuildUserReference(transaction.TransactionId);
            var target = isVoucher ? $"voucher '{productName}'" : request.RechargeData.Msisdn;

            SochitelTransactionResponse rechargeResponse = null;
            bool rechargeDelivered = false;
            bool captured = false;

            try
            {
                var payload = BuildTopupPayload(request.RechargeData, userRef);

                _logger.LogInformation(
                    "Sending {Flow}. UserRef={UserRef} Operator={Operator} Msisdn={Msisdn} Product={ProductId} Amount={Amount} Simulate={Simulate}",
                    flowName, userRef, payload.Operator, payload.Msisdn, payload.ProductId, payload.Amount, payload.Simulate);

                var rechargeResponseJson = BuildSimulatedTopupResponseJson(payload, isVoucher, transaction.TransactionId);
                //var rechargeResponseJson = await _externalApiService.PostDataAsync(payload);

                var statusName = GetStatusName(rechargeResponseJson);
                var providerStatusId = GetStatusId(rechargeResponseJson);
                _logger.LogInformation(
                    "{Flow} provider response received. UserRef={UserRef} Status={Status} StatusId={StatusId}",
                    flowName, userRef, statusName, providerStatusId);

                if (string.Equals(statusName, "Successful", StringComparison.OrdinalIgnoreCase))
                {
                    rechargeDelivered = true;
                    rechargeResponse = TryMapResponse(rechargeResponseJson, userRef);
                    captured = await CaptureAfterDeliveryAsync(transaction, flowName);
                }
                else
                {
                    _logger.LogWarning(
                        "Sochitel rejected {Flow}. PaymentIntent={PaymentIntentId} UserRef={UserRef} Status={Status} StatusId={StatusId}",
                        flowName, transaction.TransactionId, userRef, statusName, providerStatusId);
                    await VoidAndFailAsync(transaction);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during {Flow} for User {UserId}, PaymentIntent {Id}, UserRef {UserRef}",
                    flowName, userId, transaction.TransactionId, userRef);
                await HandleRechargeExceptionAsync(transaction, rechargeDelivered, userRef);
            }

            if (rechargeDelivered)
                await SaveSochitelRecordAsync(BuildTopupSochitelRecord(
                    request, rechargeResponse, userRef, isVoucher ? "Gift Voucher Transfer" : "Mobile Recharge Transfer"));

            if (captured)
            {
                if (isVoucher)
                    await NotifyVoucherAsync(request, recipient, productName, rechargeResponse, userRef);
                else
                    await NotifyMobileRechargeAsync(request, recipient, rechargeResponse, userRef, userId);
            }

            await SafeLogActivityAsync(userId, captured
                ? $"Successfully processed {flowName.ToLowerInvariant()} purchase for {target}. Amount: {request.RechargeData.Amount}. UserRef {userRef}"
                : $"{flowName} purchase FAILED for {target}. Amount: {request.RechargeData.Amount}. UserRef {userRef}. Reason: {transaction.ErrorMessage}");

            await SaveFinalAndReloadAsync(transaction);
        }

        private MobileAirtimeRechargePayload BuildTopupPayload(RechargeRequestDetails data, string userRef)
        {
            return new MobileAirtimeRechargePayload
            {
                Auth = BuildFreshAuth(),
                Command = "execTransaction",
                Operator = data.Operator,
                Msisdn = data.Msisdn?.Trim(),
                Simulate = data.Simulate,
                UserReference = userRef,
                ProductId = data.ProductId.ToString(),
                Version = 5,
                Amount = Money(data.Amount)
            };
        }

        private static string BuildSimulatedTopupResponseJson(MobileAirtimeRechargePayload payload, bool isVoucher, string transactionId)
        {
            var reference = payload.UserReference ?? transactionId ?? string.Empty;
            var code = BuildTestCode(isVoucher ? "TEST-VOUCHER" : "TEST-AIRTIME", transactionId);
            var amount = payload.Amount ?? "0.00";

            return JsonConvert.SerializeObject(new
            {
                status = new { id = 0, name = "Successful", type = 0, typeName = "Success" },
                command = payload.Command,
                timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                reference,
                result = new
                {
                    id = "TEST-" + reference,
                    @operator = new
                    {
                        id = "00000000-0000-0000-0000-000000000000",
                        sochitelId = payload.Operator,
                        name = payload.Operator
                    },
                    country = (object)null,
                    amount = new { @operator = amount, user = amount },
                    currency = new { name = "USD", symbol = "$" },
                    productId = payload.ProductId,
                    productType = isVoucher ? "2" : "1",
                    simulation = true,
                    userReference = reference,
                    msisdn = payload.Msisdn,
                    balance = new
                    {
                        initial = "0.00",
                        transaction = amount,
                        commission = "0.00",
                        commissionPercentage = "0.00",
                        final = "0.00",
                        currency = "USD"
                    },
                    instructions = $"Code: {code}\nValid until: 2099-12-31"
                }
            });
        }

        private static string BuildTestCode(string prefix, string transactionId)
        {
            var seed = string.IsNullOrWhiteSpace(transactionId) ? Guid.NewGuid().ToString("N") : transactionId;
            return prefix + "-" + Sha1Hex(seed).Substring(0, 12).ToUpperInvariant();
        }

        // Builds the Sochitel transaction record for a mobile recharge or voucher.
        private static SochitelTransactions BuildTopupSochitelRecord(
            PaymentRequests request, SochitelTransactionResponse response, string userRef, string transactionType)
        {
            return new SochitelTransactions
            {
                TransactionId = response?.Result?.Id ?? "0",
                TransactionType = transactionType,
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
        private async Task NotifyMobileRechargeAsync(PaymentRequests request, Recipient recipient, SochitelTransactionResponse response, string userRef, string userId)
        {
            var (token, validUntil) = ExtractAirtimeTokenAndValidity(response?.Result?.Instructions);
            var operatorName = response?.Result?.Operator?.Name ?? "Airtime";
            var productType = response?.Result?.ProductType ?? "Airtime";

            try
            {
                if (!string.IsNullOrEmpty(recipient.Email))
                {
                    var body = MessagesTemplates.MobileRechargeReceiptEmail(
                        recipient.FirstName,
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

                    await _emailSender.SendEmailAsync(recipient.Email, body, recipient.FirstName, "WleeSend.com Airtime Credit Receipt");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send email receipt for UserRef {UserRef}", userRef);
            }

            try
            {
                var message = $"You have received {productType} {operatorName} ${Money(request.RechargeData.Amount)} from WleeSend.com. Your Airtime token is {token} and valid until {validUntil}. Thank you.";
                _smsService.sendMessage(message, request.RechargeData.Msisdn);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send SMS for UserRef {UserRef}", userRef);
            }

            await SendRechargePushNotificationAsync(
                userId,
                "Mobile Recharge",
                $"Mobile recharge approved for ${Money(request.RechargeData.Amount)}",
                userRef);
        }

        // Emails the voucher code. If the provider text has no recognisable code, the full instructions are included.
        private async Task NotifyVoucherAsync(
            PaymentRequests request, Recipient recipient, string productName,
            SochitelTransactionResponse response, string userRef)
        {
            if (string.IsNullOrEmpty(recipient.Email))
            {
                _logger.LogWarning("No email available for voucher receipt. UserRef {UserRef}", userRef);
                return;
            }

            try
            {
                var instructions = response?.Result?.Instructions;
                var (code, validUntil) = ExtractAirtimeTokenAndValidity(instructions);

                var operatorName = string.Equals(request?.RechargeData?.Operator, "2482", StringComparison.Ordinal)
                    ? "Stop & Shop Supermarket - Liberia"
                    : response?.Result?.Operator?.Name ?? "Gift Voucher";
                var body = GiftVoucherReceiptTemplate.BuildHtml(
                    recipient.FirstName,
                    operatorName,
                    productName,
                    Money(request.Amount),
                    Money(request.RechargeData.Amount),
                    code,
                    validUntil,
                    response?.Reference ?? userRef,
                    DateTime.UtcNow.ToString("g", CultureInfo.InvariantCulture),
                    instructions);

                if (!string.IsNullOrWhiteSpace(code) && !string.Equals(code, "N/A", StringComparison.OrdinalIgnoreCase))
                {
                    var qrCode = GiftVoucherReceiptTemplate.CreateQrCode(code);
                    await _emailSender.SendEmailWithInlineImageAsync(
                        recipient.Email, body, recipient.FirstName, "WleeSend.com Gift Voucher Receipt",
                        "voucher-qr.png", qrCode, GiftVoucherReceiptTemplate.QrContentId);
                }
                else
                {
                    await _emailSender.SendEmailAsync(recipient.Email, body, recipient.FirstName, "WleeSend.com Gift Voucher Receipt");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send voucher email for UserRef {UserRef}", userRef);
            }
        }

        #endregion

        #region LEC Recharge

        // Runs from the webhook: deliver credit, then capture or void, notify, log.
        private async Task RunLecRechargeAsync(PaymentTransaction transaction, string userId, Recipient recipient, LecCreditPaymentRequest request)
        {
            SochitelTransactionResponse response = null;
            bool rechargeDelivered = false;
            bool captured = false;

            try
            {
                var payload = BuildLecPayload(request);

                _logger.LogInformation(
                    "Sending LEC recharge. Operator={Operator} Account={AccountId} Amount={Amount} Simulate={Simulate}",
                    payload.Operator, payload.AccountId, payload.AmountOperator, payload.Simulate);

                // Test response: creates a test token and echoes the requested meter and amount.
                var json = BuildSimulatedLecResponseJson(payload, transaction.TransactionId);
                // Actual Sochitel call; uncomment this line to test against the provider instead.
                 //var json = await _externalApiService.PostDataAsync<LecBillRechargePayload>(payload);

                _logger.LogInformation("LEC Provider Response (PaymentIntent {Id}): {Response}", transaction.TransactionId, json);

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
                        "Sochitel rejected LEC recharge. PaymentIntent {Id}, Status {Status}. Raw: {Json}",
                        transaction.TransactionId, statusName, json);
                    await VoidAndFailAsync(transaction);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during LEC recharge for User {UserId}, PaymentIntent {Id}", userId, transaction.TransactionId);
                await HandleRechargeExceptionAsync(transaction, rechargeDelivered, transaction.TransactionId);
            }

            if (rechargeDelivered)
                await SaveSochitelRecordAsync(BuildLecSochitelRecord(request, response));

            if (captured)
                await NotifyLecRechargeAsync(request, recipient, response, userId);

            await SafeLogActivityAsync(userId, captured
                ? $"Successfully processed LEC recharge for meter {request.AccountId}. Amount: {request.AmountOperator}"
                : $"LEC recharge FAILED for meter {request.AccountId}. Amount: {request.AmountOperator}. Reason: {transaction.ErrorMessage}");

            await SaveFinalAndReloadAsync(transaction);
        }

        // Builds the Sochitel payload for an LEC bill payment.
        private LecBillRechargePayload BuildLecPayload(LecCreditPaymentRequest request)
        {
            var data = request.Payment.RechargeData;
            return new LecBillRechargePayload
            {
                Auth = BuildFreshAuth(),
                Command = "execTransaction",
                Operator = data.OperatorId.ToString(),
                Simulate = data.Simulate,
                Version = 5,
                AccountId = request.AccountId,
                AmountOperator = Money(request.AmountOperator),
                ExtraParameters = new { accountType = "OFFLINE_PREPAID" }
            };
        }

        private static string BuildSimulatedLecResponseJson(LecBillRechargePayload payload, string transactionId)
        {
            var amount = payload.AmountOperator ?? "0.00";
            var token = BuildTestCode("TEST-LEC", transactionId);

            return JsonConvert.SerializeObject(new
            {
                status = new { id = 0, name = "Successful", type = 0, typeName = "Success" },
                command = payload.Command,
                timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                reference = transactionId,
                result = new
                {
                    id = "TEST-" + transactionId,
                    @operator = new
                    {
                        id = "00000000-0000-0000-0000-000000000000",
                        sochitelId = payload.Operator,
                        name = payload.Operator
                    },
                    country = (object)null,
                    amount = new { @operator = amount, user = amount },
                    currency = new { name = "USD", symbol = "$" },
                    productId = "LEC",
                    productType = "LEC",
                    simulation = true,
                    accountId = payload.AccountId,
                    balance = new
                    {
                        initial = "0.00",
                        transaction = amount,
                        commission = "0.00",
                        commissionPercentage = "0.00",
                        final = "0.00",
                        currency = "USD"
                    },
                    instructions = $"Amount: {amount}\nUnits: {amount}\nAccount Number: {payload.AccountId}\nToken: {token}"
                }
            });
        }

        // Builds the Sochitel transaction record for an LEC payment.
        private static SochitelTransactions BuildLecSochitelRecord(
            LecCreditPaymentRequest request, SochitelTransactionResponse response)
        {
            return new SochitelTransactions
            {
                TransactionId = response?.Result?.Id ?? "0",
                // This value is used by the dashboard and LEC transaction queries.
                TransactionType = "Bill Payment",
                TransactionAmount = Money(request.Payment.Amount),
                CommissionRate = response?.Result?.Balance?.Commission ?? "0",
                OperatorId = request.Payment.RechargeData.Operator,
                ProductId = request.Payment.RechargeData.ProductId.ToString(),
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
            LecCreditPaymentRequest request, Recipient recipient, SochitelTransactionResponse response, string userId)
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
                if (!string.IsNullOrEmpty(recipient.Email))
                {
                    var body = MessagesTemplates.LecCreditReceiptEmail(
                        recipient.FirstName,
                        Money(request.Payment.Amount),
                        Money(request.AmountOperator),
                        request.Payment.ReceiverPhoneNumber,
                        token,
                        request.AccountId,
                        details.Units.HasValue ? Money(details.Units.Value) : "N/A",
                        response?.Result?.Id ?? "0",
                        DateTime.UtcNow.ToString("g"));

                    await _emailSender.SendEmailAsync(
                        recipient.Email, body, recipient.FirstName, "WleeSend.com LEC Credit Receipt");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send LEC email receipt for meter {Meter}", request.AccountId);
            }

            try
            {
                if (!string.IsNullOrWhiteSpace(request.Payment.ReceiverPhoneNumber))
                {
                    var message = $"WleeSend: LEC credit ${Money(request.AmountOperator)} Meter {request.AccountId}. Token {token}. Thank you.";
                    _smsService.sendMessage(message, request.Payment.ReceiverPhoneNumber);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send LEC SMS for meter {Meter}", request.AccountId);
            }

            await SendRechargePushNotificationAsync(
                userId,
                "LEC Recharge",
                $"LEC credit approved for ${Money(request.AmountOperator)}. Meter {request.AccountId}",
                request.AccountId);
        }

        // Sends the same per-device Firebase notification used by SochitelController.
        private async Task SendRechargePushNotificationAsync(string userId, string title, string body, string reference)
        {
            if (string.IsNullOrWhiteSpace(userId))
                return; // Anonymous Stripe checkouts have no registered devices.

            try
            {
                var devices = await _userDeviceService.GetUserDeviceByUserId(userId);
                if (devices == null)
                    return;

                foreach (var device in devices)
                {
                    if (string.IsNullOrWhiteSpace(device?.FcmToken))
                        continue;

                    try
                    {
                        _firebaseNotification.SendFirebaseMessageRequest(new FirebaseMessageRequest
                        {
                            UserId = userId,
                            DeviceId = device.DeviceId,
                            DeviceToken = device.FcmToken,
                            Title = title,
                            Body = body
                        });
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Failed to send Firebase recharge notification for {Reference} to device {DeviceId}", reference, device.DeviceId);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to load devices for Firebase recharge notification {Reference}, User {UserId}", reference, userId);
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

        // Extracts the token / voucher code and validity text, falling back to a 20-digit match.
        private static (string Token, string ValidUntil) ExtractAirtimeTokenAndValidity(string instructions)
        {
            if (string.IsNullOrWhiteSpace(instructions))
                return ("N/A", "N/A");

            var token = string.Empty;
            var validUntil = string.Empty;

            foreach (var raw in instructions.Split(LineSeparators, StringSplitOptions.RemoveEmptyEntries))
            {
                var line = raw.Trim();

                if (line.StartsWith("Token:", StringComparison.OrdinalIgnoreCase) ||
                    line.StartsWith("PIN:", StringComparison.OrdinalIgnoreCase) ||
                    line.StartsWith("Voucher Code:", StringComparison.OrdinalIgnoreCase) ||
                    line.StartsWith("Voucher:", StringComparison.OrdinalIgnoreCase) ||
                    line.StartsWith("Code:", StringComparison.OrdinalIgnoreCase))
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
