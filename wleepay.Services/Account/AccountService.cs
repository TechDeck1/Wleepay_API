using AutoMapper;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using wleepay.Communication.EmailTemplates;
using wleepay.Communication.Interface;
using wleepay.Communication.Templates;
using wleepay.Core.Common;
using wleepay.Core.DTO.Request;
using wleepay.Core.DTO.Response;
using wleepay.Core.Enums;
using wleepay.Core.Models;
using wleepay.Core.Services;

namespace wleepay.Services.Account
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserDetailService _userDetailsService;
        private readonly ISMSService _smsService;
        private readonly IEmailSender _emailSender;
        private readonly IJwtTokenService _tokenService;
        private readonly IValidationService _validationService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AccountService> _logger;
        private readonly IMemoryCache _cache;
        private readonly IMapper _mapper;
        private readonly IDataProtector _emailVerificationProtector;

        // ---- Dual email OTP settings ----
        private const string DualOtpCachePrefix = "DUAL_EMAIL_OTP_";
        private const int DualOtpExpiryMinutes = 10;
        private const int DualOtpMaxAttempts = 5;

        public AccountService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IUserDetailService userDetailsService,
            ISMSService smsService,
            IJwtTokenService tokenService,
                IValidationService validationService,
            IEmailSender emailSender,
            IConfiguration configuration,
            ILogger<AccountService> logger,
                IMapper mapper,
            IMemoryCache cache,
            IDataProtectionProvider dataProtectionProvider)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userDetailsService = userDetailsService;
            _smsService = smsService;
            _tokenService = tokenService;
            _validationService = validationService;
            _emailSender = emailSender;
            _configuration = configuration;
            _logger = logger;
            _mapper = mapper;
            _cache = cache;
            _emailVerificationProtector = dataProtectionProvider.CreateProtector("WleeSend.EmailVerification");
        }
        public async Task<AuthenticationResponse> RegisterAsync(RegisterUserRequest userDetail)
        {
            var validationResult = _validationService.ValidateUserInput(userDetail);
            if (validationResult != null)
                return new AuthenticationResponse
                {
                    Success = false,
                    ErrorMessage = validationResult
                };




            var existingEmailUser = await _userManager.FindByEmailAsync(userDetail.EmailId);
            if (existingEmailUser != null)
                return new AuthenticationResponse
                {
                    Success = false,
                    ErrorMessage = Messages.EmailAlreadyUsed.ToDescriptionString()
                };

            var userExistWithPhone = await _userDetailsService.IsPhoneAlreadyRegistered(userDetail.PhoneNumber);
            if (userExistWithPhone)
                return new AuthenticationResponse
                {
                    Success = false,
                    ErrorMessage = Messages.PhoneAlreadyUsed.ToDescriptionString()
                };

            var newUser = new ApplicationUser
            {
                UserName = userDetail.EmailId,
                Email = userDetail.EmailId,
                Id = Guid.NewGuid().ToString()
            };

            IdentityResult isUserCreated = string.IsNullOrEmpty(userDetail.Pin)
                ? await _userManager.CreateAsync(newUser)
                : await _userManager.CreateAsync(newUser, userDetail.Pin);

            if (!isUserCreated.Succeeded)
                return new AuthenticationResponse
                {
                    Success = false,
                    ErrorMessage = isUserCreated.Errors.FirstOrDefault()?.Description ?? "User creation failed."
                };

            var roleEnum = Role.WleeSendUser;
            await _userManager.AddToRoleAsync(newUser, roleEnum.ToString());

            if (!string.IsNullOrEmpty(userDetail.Pin))
                await _signInManager.PasswordSignInAsync(newUser, userDetail.Pin, false, true);

            var userDetailsToInsert = _mapper.Map<UserDetail>(userDetail);
            userDetailsToInsert.UserId = newUser.Id;
            userDetailsToInsert.Gender = string.IsNullOrWhiteSpace(userDetailsToInsert.Gender) ? "Unknown" : userDetailsToInsert.Gender;
            userDetailsToInsert.CountyId = userDetailsToInsert.CountyId == 0 ? 1 : userDetailsToInsert.CountyId;
            userDetailsToInsert.AccountNumber = await GenerateUniqueAccountNumber();
            userDetailsToInsert.Status = (int)Status.DeActive;

            var updatedDetails = await _userDetailsService.AddOrUpdateUserDetails(userDetailsToInsert);

            if (updatedDetails?.UserId == null)
                return new AuthenticationResponse
                {
                    Success = false,
                    ErrorMessage = "User registration failed at user detail level."
                };

            if (!string.IsNullOrWhiteSpace(newUser.Email))
            {
                var emailToken = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
                var payload = $"{newUser.Id}|{emailToken}|{DateTime.UtcNow:o}";
                var packedToken = _emailVerificationProtector.Protect(payload);
                var encodedToken = HttpUtility.UrlEncode(packedToken);
                var webBaseUrl = _configuration["AppSettings:BaseUrl"];
                var verifyUrl = $"{webBaseUrl}/account/verify?token={encodedToken}";
                var emailTemplate = new EmailTemplates.EmailHtmlTemplates().EmailTemplateForEmailVerification(verifyUrl, updatedDetails.FirstName);
                await _emailSender.SendEmailAsync(newUser.Email, emailTemplate, updatedDetails.FirstName, "Verify your email");
            }

            var token = _tokenService.GenerateToken(updatedDetails);

            var completeSignUpModel = new CompleteSignUpResourceModel
            {
                Email = userDetail.EmailId,
                PhoneNumber = userDetail.PhoneNumber,
                ClientURI = _configuration["AppSettings:BaseUrl"] + "/agent-account-setup"
            };
            await GenerateLinkForComleteSignUp(completeSignUpModel);

            _smsService.sendMessage(MessagesTemplates.RegistrationSuccessMessage(updatedDetails.AccountNumber, updatedDetails?.FirstName), updatedDetails.PhoneNumber);

            return new AuthenticationResponse
            {
                Success = true,
                Token = token,
                UserId = updatedDetails.UserId,
                FirstName = updatedDetails.FirstName,
                LastName = updatedDetails.LastName,
                ProfilePic = updatedDetails.Image,
                PhoneNumber = updatedDetails.PhoneNumber,
                AccountNumber = updatedDetails.AccountNumber
            };
        }

        public async Task<AuthenticationResponse> LoginAsync(EmailLoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
                return new AuthenticationResponse { Success = false, ErrorMessage = "Account does not exist." };

            if (!user.EmailConfirmed)
                return new AuthenticationResponse { Success = false, ErrorMessage = "Please verify your email to activate your account." };

            var result = await _signInManager.PasswordSignInAsync(user, request.Pin, false, true);
            if (!result.Succeeded)
                return new AuthenticationResponse { Success = false, ErrorMessage = "Invalid Password." };

            var userDetails = await _userDetailsService.GetUserDetailsById(user.Id);
            if (userDetails == null)
                return new AuthenticationResponse { Success = false, ErrorMessage = "User details not found." };

            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault();

            var token = _tokenService.GenerateToken(userDetails);

            return new AuthenticationResponse
            {
                Success = true,
                Token = token,
                UserId = userDetails.UserId,
                FirstName = userDetails.FirstName,
                LastName = userDetails.LastName,
                ProfilePic = userDetails.Image,
                PhoneNumber = userDetails.PhoneNumber,
                AccountNumber = userDetails.AccountNumber,
                Email = user.Email,
                Role = role
            };
        }

        public async Task<GenericResponse> ResetPinAsync(ResetPinRequest request, string userId)
        {
            if (string.IsNullOrWhiteSpace(request.OldPin))
                return new GenericResponse(false, "Old PIN is required.");

            if (string.IsNullOrWhiteSpace(request.NewPin) || string.IsNullOrWhiteSpace(request.ConfirmPin))
                return new GenericResponse(false, "New PIN and Confirm PIN are required.");

            if (request.NewPin != request.ConfirmPin)
                return new GenericResponse(false, "New PIN and Confirm PIN do not match.");

            var pinError = _validationService.ValidatePin(request.NewPin);
            if (pinError != null)
                return new GenericResponse(false, pinError);

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return new GenericResponse(false, "User not found.");

            var changeResult = await _userManager.ChangePasswordAsync(user, request.OldPin, request.NewPin);
            if (!changeResult.Succeeded)
                return new GenericResponse(false, "Invalid Old PIN.");

            return new GenericResponse(true, "PIN reset successfully.");
        }
        public async Task<GenericResponse> ForgotPinAsync(ForgotPinRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Email))
                return new GenericResponse(false, "Email is required.");

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
                return new GenericResponse(true, "If the email exists, a code has been sent.");

            string cacheKey = $"PIN_{user.Id}";

            if (!string.IsNullOrEmpty(request.NewPin) && !string.IsNullOrEmpty(request.ConfirmPin))
            {
                if (request.NewPin != request.ConfirmPin)
                    return new GenericResponse(false, "Passwords do not match.");

                var pinError = _validationService.ValidatePin(request.NewPin);
                if (pinError != null)
                    return new GenericResponse(false, pinError);

                if (!_cache.TryGetValue(cacheKey, out string savedCode) || savedCode != request.Code)
                    return new GenericResponse(false, "Invalid or expired code.");

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, token, request.NewPin);

                if (!result.Succeeded)
                    return new GenericResponse(false, "Failed to reset password.");

                _cache.Remove(cacheKey);
                return new GenericResponse(true, "Password reset successful.");
            }

            if (!string.IsNullOrEmpty(request.Code))
            {
                if (!_cache.TryGetValue(cacheKey, out string savedCode) || savedCode != request.Code)
                    return new GenericResponse(false, "Invalid or expired code.");

                return new GenericResponse(true, "Code verified. Proceed to reset password.");
            }

            var code = new Random().Next(100000, 999999).ToString();
            _cache.Set(cacheKey, code, TimeSpan.FromMinutes(10));

            string body = $@"<p>Dear User, your password reset verification code is:</p><h2 style='color:#2d8fdd;'><strong>{code}</strong></h2>
                <p>This code is valid for <strong>10 minutes</strong>. Do not share it with anyone.</p>
                <p>Thank you,<br/>WleeSend Support Team</p>";

            var resultEmail = await _emailSender.SendEmailToUserAsync(request.Email, "Reset Password Code", body);
            if (resultEmail != "Email queued successfully!")
                return new GenericResponse(false, resultEmail);

            return new GenericResponse(true, "Verification code sent.");
        }


        private async Task<string> GenerateUniqueAccountNumber()
        {
            string accountNumber;
            do
            {
                accountNumber = new Random().Next(10000000, 99999999).ToString();
            }
            while (await _userDetailsService.IsAccountAlreadyExists(accountNumber));

            return accountNumber;
        }


        private async Task<bool> GenerateLinkForComleteSignUp(CompleteSignUpResourceModel completeSignUpResourceModel)
        {
            var user = await _userManager.FindByEmailAsync(completeSignUpResourceModel.Email);
            if (user == null)
                return false;

            var userDetails = await _userDetailsService.GetUserDetailsById(user.Id);
            if (userDetails != null)
            {
                if (userDetails.PhoneNumber == completeSignUpResourceModel.PhoneNumber && user.Email == completeSignUpResourceModel.Email)
                {
                    var secretKey = _configuration.GetSection("SecretKeyForEncryptAndDecrypt").Value;

                    string encryptedText = EncryptionHelper.Encrypt(completeSignUpResourceModel.Email, secretKey);
                    var token = HttpUtility.UrlEncode(encryptedText);
                    string callbackUrl = $"{completeSignUpResourceModel.ClientURI}?email={(token)}";
                    string message = "Your account for WleeSend is successfully created. To set up your account, please open the link below and complete registration." + " " + callbackUrl;

                    var sendText = _smsService.sendMessage(message, userDetails.PhoneNumber);
                    if (sendText == "success")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }


                }

            }
            return false;
        }


        public async Task<GenericResponse> SendDualEmailOtpAsync(SendDualEmailOtpRequest request)
        {
            if (request == null)
                return new GenericResponse(false, "Request is required.");

            var currentEmail = NormalizeEmail(request.FirstEmail);
            var newEmail = NormalizeEmail(request.SecondEmail);

            if (string.IsNullOrEmpty(currentEmail) || string.IsNullOrEmpty(newEmail))
                return new GenericResponse(false, "Both emails are required.");

            if (!IsValidEmail(currentEmail))
                return new GenericResponse(false, "First email is not valid.");

            if (!IsValidEmail(newEmail))
                return new GenericResponse(false, "Second email is not valid.");

            if (currentEmail == newEmail)
                return new GenericResponse(false, "Both emails must be different.");

            // Current email must belong to an existing account
            var currentUser = await _userManager.FindByEmailAsync(currentEmail);
            if (currentUser == null)
                return new GenericResponse(false, "No account found with the current email.");

            // New email must NOT already exist in the DB
            var existingNewEmailUser = await _userManager.FindByEmailAsync(newEmail);
            if (existingNewEmailUser != null)
                return new GenericResponse(false, Messages.EmailAlreadyUsed.ToDescriptionString());

            var currentOtp = GenerateSecureOtp();
            var newOtp = GenerateSecureOtp();

            var expiry = TimeSpan.FromMinutes(DualOtpExpiryMinutes);

            // Set (overwrites any previous OTP for the same email and resets attempts)
            _cache.Set(GetDualOtpCacheKey(currentEmail), new DualOtpEntry(currentOtp), expiry);
            _cache.Set(GetDualOtpCacheKey(newEmail), new DualOtpEntry(newOtp), expiry);

            var firstResult = await _emailSender.SendEmailToUserAsync(
                currentEmail, "Your verification code", BuildDualOtpEmailBody(currentOtp));

            var secondResult = await _emailSender.SendEmailToUserAsync(
                newEmail, "Your verification code", BuildDualOtpEmailBody(newOtp));

            const string queuedMessage = "Email queued successfully!";

            if (firstResult != queuedMessage || secondResult != queuedMessage)
            {
                // Don't leave half-usable OTPs behind if either email failed
                _cache.Remove(GetDualOtpCacheKey(currentEmail));
                _cache.Remove(GetDualOtpCacheKey(newEmail));

                _logger.LogWarning(
                    "Dual email OTP sending failed. First: {FirstResult}, Second: {SecondResult}",
                    firstResult, secondResult);

                return new GenericResponse(false, "Failed to send OTP to one or both emails. Please try again.");
            }

            return new GenericResponse(true, $"OTP sent to both emails. It is valid for {DualOtpExpiryMinutes} minutes.");
        }
     
        public async Task<GenericResponse> VerifyDualEmailOtpAsync(VerifyDualEmailOtpRequest request)
        {
            if (request == null)
                return new GenericResponse(false, "Request is required.");

            var currentEmail = NormalizeEmail(request.FirstEmail);
            var newEmail = NormalizeEmail(request.SecondEmail);
            var currentOtp = request.FirstOtp?.Trim();
            var newOtp = request.SecondOtp?.Trim();

            if (string.IsNullOrEmpty(currentEmail) || string.IsNullOrEmpty(newEmail))
                return new GenericResponse(false, "Both emails are required.");

            if (string.IsNullOrEmpty(currentOtp) || string.IsNullOrEmpty(newOtp))
                return new GenericResponse(false, "Both OTPs are required.");

            if (currentEmail == newEmail)
                return new GenericResponse(false, "Both emails must be different.");

            // 1) Verify both OTPs (nothing is consumed yet)
            var firstCheck = CheckDualOtp(currentEmail, currentOtp);
            var secondCheck = CheckDualOtp(newEmail, newOtp);

            if (firstCheck != DualOtpCheckResult.Valid)
                return new GenericResponse(false, DescribeDualOtpFailure("First", firstCheck));

            if (secondCheck != DualOtpCheckResult.Valid)
                return new GenericResponse(false, DescribeDualOtpFailure("Second", secondCheck));

            // 2) Re-check DB state (it may have changed since the OTPs were sent)
            var user = await _userManager.FindByEmailAsync(currentEmail);
            if (user == null)
                return new GenericResponse(false, "No account found with the current email.");

            var existingNewEmailUser = await _userManager.FindByEmailAsync(newEmail);
            if (existingNewEmailUser != null && existingNewEmailUser.Id != user.Id)
            {
                // Someone took the new email in the meantime -> OTPs are useless now
                _cache.Remove(GetDualOtpCacheKey(currentEmail));
                _cache.Remove(GetDualOtpCacheKey(newEmail));
                return new GenericResponse(false, Messages.EmailAlreadyUsed.ToDescriptionString());
            }

            // 3) Update the email in the DB
            //    ChangeEmailAsync sets Email + NormalizedEmail, marks EmailConfirmed = true
            //    (the new email was just verified by OTP) and refreshes the security stamp.
            var oldEmail = user.Email;
            var changeToken = await _userManager.GenerateChangeEmailTokenAsync(user, newEmail);
            var changeResult = await _userManager.ChangeEmailAsync(user, newEmail, changeToken);

            if (!changeResult.Succeeded)
            {
                var error = changeResult.Errors.FirstOrDefault()?.Description ?? "Failed to update email.";
                _logger.LogWarning("Dual email OTP: ChangeEmailAsync failed for user {UserId}: {Error}", user.Id, error);
                return new GenericResponse(false, error);
            }

            // If the username is the email (accounts created through RegisterAsync), keep it in sync
            // so the user can still sign in. Phone-based usernames are left untouched.
            if (string.Equals(user.UserName, oldEmail, StringComparison.OrdinalIgnoreCase))
            {
                var userNameResult = await _userManager.SetUserNameAsync(user, newEmail);
                if (!userNameResult.Succeeded)
                {
                    _logger.LogError(
                        "Dual email OTP: SetUserNameAsync failed for user {UserId}: {Errors}. Rolling back email.",
                        user.Id, string.Join("; ", userNameResult.Errors.Select(e => e.Description)));

                    // Best-effort rollback so email and username don't end up out of sync
                    var rollbackToken = await _userManager.GenerateChangeEmailTokenAsync(user, oldEmail);
                    await _userManager.ChangeEmailAsync(user, oldEmail, rollbackToken);

                    return new GenericResponse(false, "Failed to update email. Please try again.");
                }
            }

            // 4) Success -> consume both OTPs so they can't be reused
            _cache.Remove(GetDualOtpCacheKey(currentEmail));
            _cache.Remove(GetDualOtpCacheKey(newEmail));

            _logger.LogInformation("Email changed via dual OTP for user {UserId}", user.Id);

            return new GenericResponse(true, "Email verified and updated successfully. You can now log in with your new email.");
        }


        private enum DualOtpCheckResult
        {
            Valid,
            ExpiredOrNotFound,
            Invalid,
            TooManyAttempts
        }

        private sealed class DualOtpEntry
        {
            public DualOtpEntry(string code)
            {
                Code = code;
            }

            public string Code { get; }
            public int Attempts { get; set; }
            public object SyncRoot { get; } = new object();
        }

        private DualOtpCheckResult CheckDualOtp(string email, string otp)
        {
            var key = GetDualOtpCacheKey(email);

            if (!_cache.TryGetValue(key, out DualOtpEntry entry) || entry == null)
                return DualOtpCheckResult.ExpiredOrNotFound;

            lock (entry.SyncRoot)
            {
                if (entry.Attempts >= DualOtpMaxAttempts)
                {
                    _cache.Remove(key);
                    return DualOtpCheckResult.TooManyAttempts;
                }

                // Constant-time comparison
                var expected = Encoding.UTF8.GetBytes(entry.Code);
                var provided = Encoding.UTF8.GetBytes(otp);
                var isMatch = expected.Length == provided.Length &&
                              CryptographicOperations.FixedTimeEquals(expected, provided);

                if (!isMatch)
                {
                    entry.Attempts++;
                    if (entry.Attempts >= DualOtpMaxAttempts)
                    {
                        _cache.Remove(key);
                        return DualOtpCheckResult.TooManyAttempts;
                    }

                    return DualOtpCheckResult.Invalid;
                }
            }

            return DualOtpCheckResult.Valid;
        }

        private static string DescribeDualOtpFailure(string which, DualOtpCheckResult result)
        {
            return result switch
            {
                DualOtpCheckResult.ExpiredOrNotFound => $"{which} email OTP is expired or was not requested.",
                DualOtpCheckResult.TooManyAttempts => $"Too many invalid attempts for {which.ToLower()} email. Please request new OTPs.",
                _ => $"{which} email OTP is invalid."
            };
        }

        private static string GetDualOtpCacheKey(string normalizedEmail)
            => $"{DualOtpCachePrefix}{normalizedEmail}";

        private static string NormalizeEmail(string email)
            => string.IsNullOrWhiteSpace(email) ? string.Empty : email.Trim().ToLowerInvariant();

        private static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        // Cryptographically secure 6-digit OTP (100000 - 999999)
        private static string GenerateSecureOtp()
            => RandomNumberGenerator.GetInt32(100000, 1000000).ToString();

        private static string BuildDualOtpEmailBody(string otp)
        {
            return $@"<p>Dear User, your verification code is:</p><h2 style='color:#2d8fdd;'><strong>{otp}</strong></h2>
                <p>This code is valid for <strong>{DualOtpExpiryMinutes} minutes</strong>. Do not share it with anyone.</p>
                <p>Thank you,<br/>WleeSend Support Team</p>";
        }
}
}




    
