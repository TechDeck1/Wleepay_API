using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using wleepay.API.Helpers;
using wleepay.API.Resources;
using wleepay.API.Validators;
using wleepay.Communication.Interface;
using wleepay.Communication.Templates;
using wleepay.Core.Common;
using wleepay.Core.Enums;
using wleepay.Core.Models;
using wleepay.Core.Services;
using wleepay.Storage.Interface;
using static wleepay.Communication.EmailTemplates.EmailTemplates;
using ApplicationRole = wleepay.Core.Models.ApplicationRole;
using IEmailSender = wleepay.Communication.Interface.IEmailSender;

namespace wleepay.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserDetailService _userDetailsService;
        private readonly IKYCDocumentsService _kycDocumentsService;
        private readonly ISmileIdentityService _smileIdentityService;
        private readonly ISMSService _smsService;
        private readonly IWalletService _walletService;
        private readonly IStorageService _storageService;
        private readonly IAddressService _addressService;
        private readonly IAccountService _accountService;
        private readonly ILogger<AccountController> _logger;
        private readonly IMapper _mapper;
         private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;
        private readonly IStorageService _documentUploaderService;
        private readonly IEmailSender _emailSender;
        private readonly ISystemActivityLogsService _systemActivityLogsService;
        private readonly IUsersStaffRoleService _usersStaffRoleService;
        private readonly IVerificationService _verificationService;
        private readonly IDataProtector _emailVerificationProtector;
        private readonly IDataProtector _emailChangeProtector;

        public AccountController(UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            IUserDetailService userDetailsService,
            IWalletService walletService,
            ISmileIdentityService smileIdentityService,
            IStorageService storageService,
            ILogger<AccountController> logger,
            IKYCDocumentsService kycDocumentsService,
            IAddressService addressService,
            ISMSService smsService,
            IConfiguration configuration,
            IMapper mapper,
            IAccountService accountService,
            IStorageService uploadDocument,
            IWebHostEnvironment env,
            IEmailSender emailSender,
            ISystemActivityLogsService systemActivityLogsService,
            IUsersStaffRoleService usersStaffRoleService,
            IVerificationService verificationService,
            IDataProtectionProvider dataProtectionProvider)
        {
            _userManager = userManager;
            _env = env;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _userDetailsService = userDetailsService;
            _walletService = walletService;
            _accountService = accountService;
            _addressService = addressService;
            _smileIdentityService = smileIdentityService;
            _storageService = storageService;
            _logger = logger;
            _kycDocumentsService = kycDocumentsService;
            _smsService = smsService;
            _configuration = configuration;
            _mapper = mapper;
            _documentUploaderService = uploadDocument;
            _emailSender = emailSender;
            _systemActivityLogsService = systemActivityLogsService;
            _usersStaffRoleService = usersStaffRoleService;
            _verificationService = verificationService;
            _emailVerificationProtector = dataProtectionProvider.CreateProtector("WleeSend.EmailVerification");
            _emailChangeProtector = dataProtectionProvider.CreateProtector("WleeSend.EmailChange");
        }


        
      


        [AllowAnonymous]
        [HttpPost("Registration")]
        public async Task<IActionResult> Register([FromBody] AddUserDetailsResource userDetail)
        {
            string createdUserId = null;

            try
            {
                var baseUrl = _configuration["AppSettings:BaseUrl"];
                var validator = new AddUserDetailsValidator(userDetail.Role.ToString());
                var validationResult = await validator.ValidateAsync(userDetail);

                if (!validationResult.IsValid)
                {
                    var validationMessages = string.Join("; ", validationResult.Errors.Select(e => $"{e.PropertyName}: {e.ErrorMessage}"));
                    await LogSignupErrorAsync(
                        "Signup Validation Failed",
                        $"Signup validation failed for phone {userDetail.PhoneNumber}. Errors: {validationMessages}");
                    return BadRequest(validationResult.Errors);
                }

                //check for super admin
                if (userDetail.Role == 0)
                {
                    return await SignupBadRequestAsync(
                        "Signup Failed - Super Admin",
                        Messages.IsSuperAdmin.ToDescriptionString(),
                        extraDetail: $"Phone: {userDetail.PhoneNumber}");
                }

                //check if user exists with email
                ApplicationUser applicationUser = new ApplicationUser();
                if (!string.IsNullOrEmpty(userDetail.EmailId))
                    applicationUser = await _userManager.FindByEmailAsync(userDetail.EmailId);

                if (applicationUser != null && !string.IsNullOrEmpty(applicationUser.Id))
                {
                    return await SignupBadRequestAsync(
                        "Signup Failed - Email Already Used",
                        Messages.EmailAlreadyUsed.ToDescriptionString(),
                        userId: applicationUser.Id,
                        extraDetail: $"Email: {userDetail.EmailId}");
                }

                //check if phone number already exists
                var userExistWithPhone = await _userDetailsService.IsPhoneAlreadyRegistered(userDetail.PhoneNumber);
                if (userExistWithPhone == true)
                {
                    return await SignupBadRequestAsync(
                        "Signup Failed - Phone Already Used",
                        Messages.PhoneAlreadyUsed.ToDescriptionString(),
                        extraDetail: $"Phone: {userDetail.PhoneNumber}");
                }

                //create new user with identity
                var newUser = new ApplicationUser()
                { UserName = userDetail.PhoneNumber, Id = Guid.NewGuid().ToString() };
                if (!string.IsNullOrEmpty(userDetail.EmailId))
                    newUser.Email = userDetail.EmailId;

                IdentityResult isUserCreated = new IdentityResult();
                if (string.IsNullOrEmpty(userDetail.Pin))
                {
                    isUserCreated = await _userManager.CreateAsync(newUser);
                }
                else
                {
                    isUserCreated = await _userManager.CreateAsync(newUser, userDetail?.Pin?.ToString());
                }

                if (isUserCreated.Succeeded)
                {
                    createdUserId = newUser.Id;

                    //assign role to user
                    var roleEnumStringValue = userDetail.Role.HasValue ? ((Core.Enums.Role)userDetail.Role) : ((Core.Enums.Role)Core.Enums.Role.Member);
                    await _userManager.AddToRoleAsync(newUser, roleEnumStringValue.ToString());

                    if (!string.IsNullOrEmpty(userDetail.Pin))
                    {
                        //sign in user with identity
                        var result = await _signInManager.PasswordSignInAsync(newUser, userDetail.Pin.ToString(), false, true);
                    }

                    //insert data in userdetails
                    var userDetailsToInsert = _mapper.Map<AddUserDetailsResource, UserDetail>(userDetail);
                    userDetailsToInsert.UserId = newUser.Id;
                    userDetailsToInsert.Gender = string.IsNullOrWhiteSpace(userDetailsToInsert.Gender) ? "Unknown" : userDetailsToInsert.Gender;
                    userDetailsToInsert.CountyId = userDetailsToInsert.CountyId == 0 ? 1 : userDetailsToInsert.CountyId;

                    //generate account number for user
                    userDetailsToInsert.AccountNumber = await GenerateUniqueAccountNumber();

                    //save smile identity image to blob for user profile
                    if (!string.IsNullOrEmpty(userDetailsToInsert.Image))
                    {
                        var profileURL = await _storageService.UploadImageWithURL(userDetailsToInsert.Image, "user-profile-picture", newUser.Id);
                        userDetailsToInsert.Image = profileURL;
                    }

                    if (!string.IsNullOrEmpty(userDetailsToInsert.KYCDocumentImage))
                    {
                        var documentURL = _storageService.UploadKYCDocument(userDetailsToInsert.KYCDocumentImage, newUser.Id);
                        userDetailsToInsert.KYCDocumentImage = documentURL.ToString();
                        if (userDetailsToInsert.KYCLevel == Core.Enums.KYCLevel.L1.GetHashCode())
                        {
                            //update kyc level from l1 to l2 for new document upload
                            userDetailsToInsert.KYCLevel = Core.Enums.KYCLevel.L2.GetHashCode();
                        }
                    }

                    userDetailsToInsert.Status = (int)wleepay.Core.Enums.Status.DeActive;
                    //save data
                    var updatedDetails = await _userDetailsService.AddOrUpdateUserDetails(userDetailsToInsert);
                    if (updatedDetails.UserId != null)
                    {
                        //kyc document is optional in registration
                        if (userDetail.DocumentTypeId.HasValue)
                        {
                            //insert data in kyc document
                            var kycDetailsToAdd = _mapper.Map<AddUserDetailsResource, KYCdocuments>(userDetail);
                            kycDetailsToAdd.UserId = newUser.Id;
                            kycDetailsToAdd.CreatedDate = DateTime.Now;
                            kycDetailsToAdd.UpdatedDate = DateTime.Now;

                            await _kycDocumentsService.AddOrUpdateKYCDocument(kycDetailsToAdd);
                        }

                        //create wallet for register user by role
                        if (roleEnumStringValue == Core.Enums.Role.Member)
                        {
                            await _walletService.CreateCustomerWallet(newUser.Id);
                        }
                        else if (roleEnumStringValue == Core.Enums.Role.Agent)
                        {
                            await _walletService.CreateAgentWallet(newUser.Id);
                        }

                        //add user address
                        if (userDetail.Address != null)
                        {
                            var addressToInsert = _mapper.Map<AddressResource, Address>(userDetail.Address);
                            addressToInsert.UserId = updatedDetails.UserId;
                            await _addressService.AddOrUpdateAddress(addressToInsert);
                        }

                        //send email verification before activation
                        if (!string.IsNullOrWhiteSpace(newUser.Email))
                        {
                            var emailToken = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
                            var payload = $"{newUser.Id}|{emailToken}|{DateTime.UtcNow:o}";
                            var packedToken = _emailVerificationProtector.Protect(payload);
                            var encodedToken = HttpUtility.UrlEncode(packedToken);
                            var webBaseUrl = _configuration["AppSettings:BaseUrl"];
                            var verifyUrl = $"{webBaseUrl}/account/verify?token={encodedToken}";
                            var emailTemplate = new EmailHtmlTemplates().EmailTemplateForEmailVerification(verifyUrl, updatedDetails.FirstName);
                            await _emailSender.SendEmailAsync(newUser.Email, emailTemplate, updatedDetails.FirstName, "Verify your WleeSend email");
                        }

                        //generate jwt token
                        var token = GenerateJSONWebToken(userDetailsToInsert);

                        CompleteSignUpResourceModel completeSignUpResourceModel = new CompleteSignUpResourceModel();
                        completeSignUpResourceModel.Email = userDetail.EmailId;
                        completeSignUpResourceModel.PhoneNumber = userDetail.PhoneNumber;
                        completeSignUpResourceModel.ClientURI = baseUrl + "/agent-account-setup";
                        var sendSms = await GenerateLinkForComleteSignUp(completeSignUpResourceModel);

                        //send registration sucess message on mobile number
                        _smsService.sendMessage(MessagesTemplates.RegistrationSuccessMessage(updatedDetails.AccountNumber, updatedDetails?.KYCLevelDetail?.Name), updatedDetails.PhoneNumber);
                        return Ok(new
                        {
                            Success = true,
                            Token = token,
                            UserId = userDetailsToInsert.UserId,
                            FirstName = updatedDetails.FirstName,
                            LastName = updatedDetails.LastName,
                            ProfilePic = updatedDetails.Image,
                            PhoneNumber = updatedDetails.PhoneNumber,
                            AccountNumber = updatedDetails.AccountNumber
                        });
                    }
                    else
                    {
                        return await SignupBadRequestAsync(
                            "Signup Failed - User Details Not Saved",
                            Messages.UserNotCreated.ToDescriptionString(),
                            userId: newUser.Id,
                            extraDetail: $"Phone: {userDetail.PhoneNumber}");
                    }
                }
                else
                {
                    var identityErrors = string.Join("; ", isUserCreated.Errors.Select(e => $"{e.Code}: {e.Description}"));
                    await LogSignupErrorAsync(
                        "Signup Failed - Identity Create User",
                        $"Identity CreateAsync failed for phone {userDetail.PhoneNumber}. Errors: {identityErrors}",
                        newUser.Id);
                    return BadRequest(isUserCreated.Errors.FirstOrDefault().Description);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());

                await LogSignupErrorAsync(
                    "Signup Exception",
                    $"Unhandled exception during signup for phone {userDetail?.PhoneNumber}. " +
                    $"Message: {ex.Message} | Inner: {ex.InnerException?.Message} | StackTrace: {ex.StackTrace}",
                    createdUserId);

                throw;
            }
        }


        [AllowAnonymous]
        [HttpPost("Mobile-Registration")]
        public async Task<IActionResult> RegisterMobile([FromBody] AddUserDetailsResource userDetail)
        {
            string createdUserId = null;

            try
            {
                var baseUrl = _configuration.GetSection("BaseUrl").Value;
                var validator = new AddUserDetailsValidator(userDetail.Role.ToString());
                var validationResult = await validator.ValidateAsync(userDetail);

                if (!validationResult.IsValid)
                {
                    var validationMessages = string.Join("; ", validationResult.Errors.Select(e => $"{e.PropertyName}: {e.ErrorMessage}"));
                    await LogSignupErrorAsync(
                        "Signup Validation Failed",
                        $"Signup validation failed for phone {userDetail.PhoneNumber}. Errors: {validationMessages}");
                    return BadRequest(validationResult.Errors);
                }

                if (userDetail.Role == 0)
                {
                    return await SignupBadRequestAsync(
                        "Signup Failed - Super Admin",
                        Messages.IsSuperAdmin.ToDescriptionString(),
                        extraDetail: $"Phone: {userDetail.PhoneNumber}");
                }

                ApplicationUser applicationUser = new ApplicationUser();
                if (!string.IsNullOrEmpty(userDetail.EmailId))
                    applicationUser = await _userManager.FindByEmailAsync(userDetail.EmailId);

                if (applicationUser != null && !string.IsNullOrEmpty(applicationUser.Id))
                {
                    return await SignupBadRequestAsync(
                        "Signup Failed - Email Already Used",
                        Messages.EmailAlreadyUsed.ToDescriptionString(),
                        userId: applicationUser.Id,
                        extraDetail: $"Email: {userDetail.EmailId}");
                }

                var userExistWithPhone = await _userDetailsService.IsPhoneAlreadyRegistered(userDetail.PhoneNumber);
                if (userExistWithPhone == true)
                {
                    return await SignupBadRequestAsync(
                        "Signup Failed - Phone Already Used",
                        Messages.PhoneAlreadyUsed.ToDescriptionString(),
                        extraDetail: $"Phone: {userDetail.PhoneNumber}");
                }

                var newUser = new ApplicationUser()
                { UserName = userDetail.PhoneNumber, Id = Guid.NewGuid().ToString() };
                if (!string.IsNullOrEmpty(userDetail.EmailId))
                    newUser.Email = userDetail.EmailId;

                IdentityResult isUserCreated = string.IsNullOrEmpty(userDetail.Pin)
                    ? await _userManager.CreateAsync(newUser)
                    : await _userManager.CreateAsync(newUser, userDetail.Pin.ToString());

                if (!isUserCreated.Succeeded)
                {
                    var identityErrors = string.Join("; ", isUserCreated.Errors.Select(e => $"{e.Code}: {e.Description}"));
                    await LogSignupErrorAsync(
                        "Signup Failed - Identity Create User",
                        $"Identity CreateAsync failed for phone {userDetail.PhoneNumber}. Errors: {identityErrors}",
                        newUser.Id);
                    return BadRequest(isUserCreated.Errors.FirstOrDefault().Description);
                }

                createdUserId = newUser.Id;

                var roleEnumStringValue = userDetail.Role.HasValue ? ((Core.Enums.Role)userDetail.Role) : Core.Enums.Role.Member;
                await _userManager.AddToRoleAsync(newUser, roleEnumStringValue.ToString());

                var userDetailsToInsert = _mapper.Map<AddUserDetailsResource, UserDetail>(userDetail);
                userDetailsToInsert.UserId = newUser.Id;
                userDetailsToInsert.Gender = string.IsNullOrWhiteSpace(userDetailsToInsert.Gender) ? "Unknown" : userDetailsToInsert.Gender;
                userDetailsToInsert.CountyId = userDetailsToInsert.CountyId == 0 ? 1 : userDetailsToInsert.CountyId;
                userDetailsToInsert.AccountNumber = await GenerateUniqueAccountNumber();

                if (!string.IsNullOrEmpty(userDetailsToInsert.Image))
                {
                    var profileURL = await _storageService.UploadImageWithURL(userDetailsToInsert.Image, "user-profile-picture", newUser.Id);
                    userDetailsToInsert.Image = profileURL;
                }

                if (!string.IsNullOrEmpty(userDetailsToInsert.KYCDocumentImage))
                {
                    var documentURL = _storageService.UploadKYCDocument(userDetailsToInsert.KYCDocumentImage, newUser.Id);
                    userDetailsToInsert.KYCDocumentImage = documentURL.ToString();
                    if (userDetailsToInsert.KYCLevel == Core.Enums.KYCLevel.L1.GetHashCode())
                        userDetailsToInsert.KYCLevel = Core.Enums.KYCLevel.L2.GetHashCode();
                }

                // Mobile users are activated directly (email verification is sent but does not gate activation)
                userDetailsToInsert.Status = (int)wleepay.Core.Enums.Status.Active;

                var updatedDetails = await _userDetailsService.AddOrUpdateUserDetails(userDetailsToInsert);
                if (updatedDetails.UserId == null)
                {
                    return await SignupBadRequestAsync(
                        "Signup Failed - User Details Not Saved",
                        Messages.UserNotCreated.ToDescriptionString(),
                        userId: newUser.Id,
                        extraDetail: $"Phone: {userDetail.PhoneNumber}");
                }

                if (userDetail.DocumentTypeId.HasValue)
                {
                    var kycDetailsToAdd = _mapper.Map<AddUserDetailsResource, KYCdocuments>(userDetail);
                    kycDetailsToAdd.UserId = newUser.Id;
                    kycDetailsToAdd.CreatedDate = DateTime.Now;
                    kycDetailsToAdd.UpdatedDate = DateTime.Now;
                    await _kycDocumentsService.AddOrUpdateKYCDocument(kycDetailsToAdd);
                }

                if (roleEnumStringValue == Core.Enums.Role.Member)
                    await _walletService.CreateCustomerWallet(newUser.Id);
                else if (roleEnumStringValue == Core.Enums.Role.Agent)
                    await _walletService.CreateAgentWallet(newUser.Id);

                if (userDetail.Address != null)
                {
                    var addressToInsert = _mapper.Map<AddressResource, Address>(userDetail.Address);
                    addressToInsert.UserId = updatedDetails.UserId;
                    await _addressService.AddOrUpdateAddress(addressToInsert);
                }

                // Send email verification (same as web registration)
                if (!string.IsNullOrWhiteSpace(newUser.Email))
                {
                    try
                    {
                        var emailToken = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
                        var payload = $"{newUser.Id}|{emailToken}|{DateTime.UtcNow:o}";
                        var packedToken = _emailVerificationProtector.Protect(payload);
                        var encodedToken = HttpUtility.UrlEncode(packedToken);
                        var webBaseUrl = _configuration["BackendUrl"];
                        var verifyUrl = $"{webBaseUrl}/api/account/verify?token={encodedToken}";
                        var emailTemplate = new EmailHtmlTemplates().EmailTemplateForEmailVerification(verifyUrl, updatedDetails.FirstName);
                        await _emailSender.SendEmailAsync(newUser.Email, emailTemplate, updatedDetails.FirstName, "Verify your WleeSend email");
                    }
                    catch (Exception emailEx)
                    {
                        // Don't fail the whole signup if the email can't be sent
                        _logger.LogError(emailEx, "Failed to send verification email for user {UserId}", newUser.Id);
                        await LogSignupErrorAsync(
                            "Signup Email Failed",
                            $"Verification email failed for phone {userDetail.PhoneNumber}. Message: {emailEx.Message}",
                            newUser.Id);
                    }
                }

                var token = GenerateJSONWebToken(userDetailsToInsert);

                _smsService.sendMessage(MessagesTemplates.RegistrationSuccessMessage(updatedDetails.AccountNumber, updatedDetails?.KYCLevelDetail?.Name), updatedDetails.PhoneNumber);

                return Ok(new
                {
                    Success = true,
                    Token = token,
                    UserId = userDetailsToInsert.UserId,
                    FirstName = updatedDetails.FirstName,
                    LastName = updatedDetails.LastName,
                    ProfilePic = updatedDetails.Image,
                    PhoneNumber = updatedDetails.PhoneNumber,
                    AccountNumber = updatedDetails.AccountNumber
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                await LogSignupErrorAsync(
                    "Signup Exception",
                    $"Unhandled exception during signup for phone {userDetail?.PhoneNumber}. " +
                    $"Message: {ex.Message} | Inner: {ex.InnerException?.Message} | StackTrace: {ex.StackTrace}",
                    createdUserId);
                throw;
            }
        }


        [AllowAnonymous]
        [HttpGet("verify")]
        public async Task<IActionResult> VerifyEmail([FromQuery] string token)
        {
            const int tokenLifetimeHours = 24;

            if (string.IsNullOrWhiteSpace(token))
                return VerificationPage(StatusCodes.Status400BadRequest, VerificationState.Invalid);

            try
            {
                // 1. Decode the packed token created in RegisterMobile
                string payload;
                try
                {
                    payload = _emailVerificationProtector.Unprotect(token);
                }
                catch (Exception ex) when (ex is CryptographicException || ex is FormatException)
                {
                    _logger.LogWarning(ex, "Email verification: token could not be unprotected");
                    return VerificationPage(StatusCodes.Status400BadRequest, VerificationState.Invalid);
                }

                // payload = "{userId}|{emailToken}|{issuedAtUtc:o}"
                var parts = payload.Split('|', 3);
                if (parts.Length != 3
                    || string.IsNullOrWhiteSpace(parts[0])
                    || string.IsNullOrWhiteSpace(parts[1])
                    || !DateTime.TryParse(parts[2], CultureInfo.InvariantCulture,
                           DateTimeStyles.RoundtripKind, out var issuedAt))
                {
                    return VerificationPage(StatusCodes.Status400BadRequest, VerificationState.Invalid);
                }

                if (DateTime.UtcNow - issuedAt.ToUniversalTime() > TimeSpan.FromHours(tokenLifetimeHours))
                {
                    _logger.LogWarning("Email verification: expired token for user {UserId}", parts[0]);
                    return VerificationPage(StatusCodes.Status400BadRequest, VerificationState.Invalid);
                }

                var userId = parts[0];
                var emailToken = parts[1];

                // 2. Find the user and their email
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null || string.IsNullOrWhiteSpace(user.Email))
                    return VerificationPage(StatusCodes.Status400BadRequest, VerificationState.Invalid);

                // 3. Already confirmed
                if (user.EmailConfirmed)
                    return VerificationPage(StatusCodes.Status200OK, VerificationState.AlreadyVerified, user.Email);

                // 4. Confirm (sets EmailConfirmed = true and saves the user)
                var result = await _userManager.ConfirmEmailAsync(user, emailToken);
                if (!result.Succeeded)
                {
                    var errors = string.Join("; ", result.Errors.Select(e => $"{e.Code}: {e.Description}"));
                    _logger.LogWarning("Email verification failed for user {UserId}: {Errors}", userId, errors);
                    return VerificationPage(StatusCodes.Status400BadRequest, VerificationState.Invalid);
                }

                return VerificationPage(StatusCodes.Status200OK, VerificationState.Success, user.Email);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled error during email verification");
                return VerificationPage(StatusCodes.Status500InternalServerError, VerificationState.Invalid);
            }
        }


        private enum VerificationState { Success, AlreadyVerified, Invalid }

        private static ContentResult VerificationPage(int statusCode, VerificationState state, string email = null)
        {
            string icon, color, title, message;
            switch (state)
            {
                case VerificationState.Success:
                    icon = "&#10003;"; color = "#1f9d55";
                    title = "Email verified";
                    message = "Your email address has been verified successfully.";
                    break;
                case VerificationState.AlreadyVerified:
                    icon = "i"; color = "#232870";
                    title = "Email already confirmed";
                    message = "This email address has already been verified. No further action is needed.";
                    break;
                default:
                    icon = "!"; color = "#CF3228";
                    title = "Failed to verify";
                    message = "This verification link is invalid or has expired. Please request a new verification email from the app.";
                    break;
            }

            var emailLine = string.IsNullOrEmpty(email)
                ? ""
                : "<p class='email'>" + HttpUtility.HtmlEncode(email) + "</p>";

            var hint = state == VerificationState.Invalid
                ? "You can close this tab."
                : "You can now close this tab and continue to log in on your device.";


            var html = template
                .Replace("{{TITLE}}", HttpUtility.HtmlEncode(title))
                .Replace("{{MESSAGE}}", HttpUtility.HtmlEncode(message))
                .Replace("{{HINT}}", HttpUtility.HtmlEncode(hint))
                .Replace("{{COLOR}}", color)
                .Replace("{{ICON}}", icon)
                .Replace("{{EMAIL}}", emailLine);


            return new ContentResult
            {
                StatusCode = statusCode,
                ContentType = "text/html; charset=utf-8",
                Content = html
            };
        }


        const string template = @"<!DOCTYPE html>
            <html lang='en'>
            <head>
            <meta charset='utf-8'>
            <meta name='viewport' content='width=device-width, initial-scale=1'>
            <title>Wleesend - {{TITLE}}</title>
            <style>
              * { box-sizing: border-box; }
              body { margin: 0; min-height: 100vh; display: flex; align-items: center; justify-content: center;
                     background: rgba(23,27,77,.55); font-family: system-ui, -apple-system, 'Segoe UI', Roboto, sans-serif; padding: 20px; }
              .dialog { background: #fff; width: 100%; max-width: 400px; border-radius: 20px; padding: 32px 24px 28px;
                        text-align: center; box-shadow: 0 24px 60px rgba(0,0,0,.3); }
              .icon { width: 64px; height: 64px; border-radius: 50%; background: {{COLOR}}; color: #fff; font-size: 32px;
                      font-weight: 700; display: flex; align-items: center; justify-content: center; margin: 0 auto 16px; }
              h1 { margin: 0 0 8px; font-size: 22px; color: #171B4D; }
              p { margin: 0 0 12px; font-size: 15px; line-height: 1.5; color: #5B6080; }
              .email { font-weight: 600; color: #171B4D; word-break: break-all; }
              .hint { margin-bottom: 0; color: #171B4D; font-weight: 600; }
            </style>
            </head>
            <body>
              <div class='dialog' role='dialog' aria-modal='true' aria-labelledby='t'>
                <div class='icon'>{{ICON}}</div>
                <h1 id='t'>{{TITLE}}</h1>
                <p>{{MESSAGE}}</p>
                {{EMAIL}}
                <p class='hint'>{{HINT}}</p>
              </div>
            </body>
            </html>";



        [AllowAnonymous]
        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token))
                return BadRequest("Invalid verification request.");

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return BadRequest("User not found.");

            var decodedToken = HttpUtility.UrlDecode(token);
            var result = await _userManager.ConfirmEmailAsync(user, decodedToken);
            if (!result.Succeeded)
                return BadRequest(result.Errors.FirstOrDefault()?.Description ?? "Invalid verification token.");

            var userDetails = await _userDetailsService.GetUserDetailsById(userId);
            if (userDetails != null && userDetails.Status != Status.Active.GetHashCode())
            {
                userDetails.Status = Status.Active.GetHashCode();
                await _userDetailsService.AddOrUpdateUserDetails(userDetails);
            }

            return Ok("Email verified successfully.");
        }

        // Constructor must have these injected (add if not already present):
        // private readonly IConfiguration _configuration;
        // assigned in constructor as usual via DI

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            try
            {
                var validator = new LoginRequestValidator();
                var validationResult = await validator.ValidateAsync(loginRequest);
                if (validationResult.Errors.Count > 0)
                {
                    if (validationResult.Errors[0].ErrorMessage[3].ToString() == "m")
                    {
                        validationResult.Errors[0].ErrorMessage = "Pin should be 4 characters long.";
                    }
                }

                //if (!validationResult.IsValid)
                //    return BadRequest(validationResult.Errors);

                var userDetails = await _userDetailsService.GetUserDetailsByPhone(loginRequest.PhoneNo);
                if (userDetails == null)
                    return BadRequest("Account does not exists.");

                var user = await _userManager.FindByNameAsync(userDetails.PhoneNumber);
                if (user == null)
                    return BadRequest("Account does not exists.");

                var isLocalTestBypass = _env.IsDevelopment()
                    && _configuration.GetValue<bool>("TestAuth:Enabled")
                    && !string.IsNullOrEmpty(_configuration["TestAuth:BypassPin"])
                    && loginRequest.Pin.ToString() == _configuration["TestAuth:BypassPin"];

                if (isLocalTestBypass)
                {
                    _logger.LogWarning("TEST AUTH BYPASS USED for {Phone}", loginRequest.PhoneNo);
                }

                Microsoft.AspNetCore.Identity.SignInResult result = isLocalTestBypass
                    ? Microsoft.AspNetCore.Identity.SignInResult.Success
                    : await _signInManager.PasswordSignInAsync(user, loginRequest.Pin.ToString(), false, true);
                // ---- END DEV-ONLY TEST BYPASS ----

                // Check if the user is locked out
                if (userDetails.FailedLoginAttempt >= 3)
                {
                    var lockoutDuration = DateTime.UtcNow - userDetails.UpdatedDate;
                    if (lockoutDuration.TotalMinutes < 30)  // Check if user is locked out
                    {
                        return BadRequest("Your account is locked. Please try again after 30 minutes.");
                    }
                    else
                    {
                        await _userDetailsService.UpdateUserLoggedInStatus(userDetails.UserId, false, 0);
                    }
                }

                if (result.Succeeded)
                {
                    if (userDetails.Status == Core.Enums.Status.DeActive.GetHashCode())
                    {
                        var failedAttempt = userDetails.FailedLoginAttempt + 1;
                        await _userDetailsService.UpdateUserLoggedInStatus(userDetails.UserId, false, failedAttempt);
                        return BadRequest(new
                        {
                            Success = false,
                            Message = Messages.AccountDeActivated.ToDescriptionString(),
                        });
                    }

                    //generate token
                    var token = GenerateJSONWebToken(userDetails);

                    //get role details
                    var userRole = await _userManager.GetRolesAsync(user);
                    //get wallet details for user
                    var wallet = await _walletService.GetWalletsByUserId(userDetails.UserId);

                    if (wallet.Count() < 1 && userRole[0] == ((Core.Enums.Role)Core.Enums.Role.Member.GetHashCode()).ToString())
                    {
                        //create new wallet
                        wallet = await _walletService.CreateCustomerWallet(userDetails.UserId);
                    }

                    var usdBalance = wallet.FirstOrDefault(w => w.CurrencyId == Core.Enums.Currencies.USD.GetHashCode())?.Balance;
                    var lrdBalance = wallet.FirstOrDefault(w => w.CurrencyId == Core.Enums.Currencies.LRD.GetHashCode())?.Balance;
                    var response = new
                    {
                        UserId = user.Id,
                        Success = true,
                        Token = token,
                        IsLoggedInVerified = userDetails.IsLoggedIn,
                        FirstName = userDetails.FirstName,
                        LastName = userDetails.LastName,
                        ProfilePic = System.IO.File.Exists(userDetails.Image) ? System.Convert.ToBase64String(System.IO.File.ReadAllBytes(userDetails.Image)) : userDetails.Image,
                        PhoneNumber = userDetails.PhoneNumber,
                        AccountNumber = userDetails.AccountNumber,
                        KYCLevel = userDetails.KYCLevelDetail.Name,
                        USDBalance = usdBalance,
                        lrdBalance = lrdBalance,
                        Role = userRole?[0]
                    };

                    string userId = userDetails.UserId;
                    await _userDetailsService.UpdateUserLoggedInStatus(userId, true, 0);

                    return Ok(response);
                }
                else
                {
                    if (user != null)
                    {
                        if (userDetails != null)
                        {
                            if (userDetails.FailedLoginAttempt >= 3) // Threshold for sending SMS
                            {
                                var sendSms = _smsService.sendMessage("Multiple failed login attempts detected.", userDetails.PhoneNumber);
                            }

                            var failedAttempt = userDetails.FailedLoginAttempt + 1;
                            await _userDetailsService.UpdateUserLoggedInStatus(userDetails.UserId, false, failedAttempt);
                        }
                        return BadRequest("Invalid login request.");
                    }
                }
                return BadRequest("Invalid login request.");
            }
            catch (Exception ex)
            {
                string logDir = @"C:\API_LOGS";

                if (!Directory.Exists(logDir))
                    Directory.CreateDirectory(logDir);

                string logFilePath = Path.Combine(logDir, "logs.txt");

                string logMessage = $@"
            --------------------------------------------
            Date: {DateTime.UtcNow}
            Message: {ex.Message}
            StackTrace: {ex.StackTrace}";

                await System.IO.File.AppendAllTextAsync(logFilePath, logMessage);

                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [AllowAnonymous]
        [HttpPost("SendOTP")]
        public async Task<IActionResult> SendOTP([FromBody] OTPRequest otpRequest)
        {
            var validator = new LoginRequestValidator();
            var validationResult = await validator.ValidateAsync(otpRequest);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            try
            {
                string message = MessagesTemplates.ValidateNumberOTPMessage(otpRequest.Pin.ToString());
                var smsStatus = _smsService.sendMessage(message, otpRequest.PhoneNo);

                //if status is not qued then it's will be an error message
                if (smsStatus.Trim().Length > 7)
                    return BadRequest(smsStatus);

                return Ok(smsStatus);
            }
            catch (Exception ex)
            {
                return BadRequest(ex?.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("UpdatePin")]
        public async Task<IActionResult> UpdatePIN([FromBody] LoginRequest updatePinRequest)
        {
            var validator = new LoginRequestValidator();
            var validationResult = await validator.ValidateAsync(updatePinRequest);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            try
            {
                var userDetail = await _userDetailsService.GetUserDetailsByPhone(updatePinRequest.PhoneNo);
                //update pin 
                if (userDetail == null)
                    return BadRequest("Account does not exist.");
                var user = await _userManager.FindByNameAsync(userDetail.PhoneNumber);
                //generate password reset token
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var updatedDetail = await _userManager.ResetPasswordAsync(user, token, updatePinRequest.Pin.ToString());

                if (updatedDetail.Succeeded)
                    return Ok("Pin is successfully updated.");
                else
                    return BadRequest(updatedDetail.Errors.FirstOrDefault());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [AllowAnonymous]
        [HttpGet("isPhoneNumberExists")]
        public async Task<IActionResult> CheckPhoneNumberExists(string phoneNumber)
        {
            if (phoneNumber.First().ToString() == " ")
            {
                phoneNumber = phoneNumber.Replace(" ", "+");
            }
            var result = await _userDetailsService.IsPhoneAlreadyRegistered(phoneNumber);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("SmileIdentityResponse")]
        public async Task<IActionResult> SmileIdentityStatus([FromBody] SmileIdentityResponse response)
        {
            var kycStatus = Core.Enums.KYCStatus.Pending;
            var userId = response.PartnerParams?.user_id?.ToString();
            if (response != null && !string.IsNullOrEmpty(userId))
            {
                switch (response.ResultCode)
                {

                    case "0815":
                    case "0814":
                    case "0812":
                    case "1213":
                    case "1012":
                        kycStatus = Core.Enums.KYCStatus.UnderReview;
                        break;

                    case "0911":
                    case "0912":
                    case "0811":
                    case "0813":
                    case "1211":
                    case "1212":
                    case "1013":
                    case "1014":
                        kycStatus = Core.Enums.KYCStatus.Rejected;
                        //await _smileIdentityService.ReEnrollUser(userId);
                        break;

                    case "0810":
                    case "1210":
                        kycStatus = Core.Enums.KYCStatus.Approved;
                        break;

                    default:
                        kycStatus = Core.Enums.KYCStatus.Pending;
                        break;
                }
            }

            if (kycStatus == Core.Enums.KYCStatus.Pending)
                return Ok();

            //update user kyc status
            await _userDetailsService.UpdateUserKYCStatus(userId, kycStatus.GetHashCode());

            return Ok();
        }



        [AllowAnonymous]
        [HttpPost("verify-email")]
        public async Task<IActionResult> VerifyEmail([FromBody] VerifyEmailRequest request)
        {
            if (string.IsNullOrWhiteSpace(request?.Token))
                return BadRequest(new { Success = false, Message = "Token is required." });

            try
            {
                var decoded = HttpUtility.UrlDecode(request.Token);
                var payload = _emailVerificationProtector.Unprotect(decoded);
                var parts = payload.Split('|', 3);
                if (parts.Length < 3)
                    return BadRequest(new { Success = false, Message = "Invalid verification token." });

                var userId = parts[0];
                var emailToken = parts[1];
                var issuedAt = DateTime.Parse(parts[2], null, System.Globalization.DateTimeStyles.RoundtripKind);

                var expiryHours = _configuration.GetValue<int?>("EmailVerificationTokenExpiryHours") ?? 24;
                if (DateTime.UtcNow > issuedAt.AddHours(expiryHours))
                    return BadRequest(new { Success = false, Message = "Verification link has expired. Please request a new one." });

                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                    return BadRequest(new { Success = false, Message = "User not found." });

                if (user.EmailConfirmed)
                    return Ok(new { Success = true, Message = "Email is already verified." });

                var result = await _userManager.ConfirmEmailAsync(user, emailToken);
                if (!result.Succeeded)
                    return BadRequest(new { Success = false, Message = result.Errors.FirstOrDefault()?.Description ?? "Invalid verification token." });

                var userDetails = await _userDetailsService.GetUserDetailsById(userId);
                if (userDetails != null && userDetails.Status != Status.Active.GetHashCode())
                {
                    userDetails.Status = Status.Active.GetHashCode();
                    await _userDetailsService.AddOrUpdateUserDetails(userDetails);
                }

                return Ok(new { Success = true, Message = "Email verified successfully. Your account is now active." });
            }
            catch (System.Security.Cryptography.CryptographicException)
            {
                return BadRequest(new { Success = false, Message = "Invalid or tampered verification token." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error verifying email");
                return BadRequest(new { Success = false, Message = "Invalid verification token." });
            }
        }

        [AllowAnonymous]
        [HttpPost("resend-verification-email")]
        public async Task<IActionResult> ResendVerificationEmail([FromBody] ResendVerificationRequest request)
        {
            if (string.IsNullOrWhiteSpace(request?.Email))
                return BadRequest(new { Success = false, Message = "Email is required." });

            try
            {
                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user == null)
                    return Ok(new { Success = true, Message = "If the email is registered, a verification link has been sent." });

                if (user.EmailConfirmed)
                    return BadRequest(new { Success = false, Message = "Email is already verified." });

                var userDetails = await _userDetailsService.GetUserDetailsById(user.Id);
                var firstName = userDetails?.FirstName ?? "User";

                var emailToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var payload = $"{user.Id}|{emailToken}|{DateTime.UtcNow:o}";
                var packedToken = _emailVerificationProtector.Protect(payload);
                var encodedToken = HttpUtility.UrlEncode(packedToken);
                var webBaseUrl = _configuration["AppSettings:BaseUrl"];
                var verifyUrl = $"{webBaseUrl}/account/verify?token={encodedToken}";
                var emailTemplate = new EmailHtmlTemplates().EmailTemplateForEmailVerification(verifyUrl, firstName);
                await _emailSender.SendEmailAsync(user.Email, emailTemplate, firstName, "Verify your WleeSend email");

                return Ok(new { Success = true, Message = "Verification email sent. Please check your inbox." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error resending verification email");
                return StatusCode(500, new { Success = false, Message = "An error occurred while sending the verification email." });
            }
        }

        [HttpPost("change-email")]
        public async Task<IActionResult> ChangeEmail([FromBody] ChangeEmailRequest request)
        {
            if (string.IsNullOrWhiteSpace(request?.NewEmail))
                return BadRequest(new { Success = false, Message = "New email is required." });

            try
            {
                var userId = User.FindFirstValue("UserId") ?? User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized(new { Success = false, Message = "User not authenticated." });

                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                    return BadRequest(new { Success = false, Message = "User not found." });

                if (user.Email?.ToLower() == request.NewEmail.ToLower())
                    return BadRequest(new { Success = false, Message = "New email is the same as the current email." });

                var existingUser = await _userManager.FindByEmailAsync(request.NewEmail);
                if (existingUser != null)
                    return BadRequest(new { Success = false, Message = Messages.EmailAlreadyUsed.ToDescriptionString() });

                var userDetails = await _userDetailsService.GetUserDetailsById(userId);
                var firstName = userDetails?.FirstName ?? "User";

                var changeToken = await _userManager.GenerateChangeEmailTokenAsync(user, request.NewEmail);
                var payload = $"{userId}|{request.NewEmail}|{changeToken}|{DateTime.UtcNow:o}";
                var packedToken = _emailChangeProtector.Protect(payload);
                var encodedToken = HttpUtility.UrlEncode(packedToken);
                var webBaseUrl = _configuration["AppSettings:BaseUrl"];
                var confirmUrl = $"{webBaseUrl}/account/confirm-email-change?token={encodedToken}";
                var emailTemplate = new EmailHtmlTemplates().EmailTemplateForEmailChange(confirmUrl, firstName, request.NewEmail);
                await _emailSender.SendEmailAsync(request.NewEmail, emailTemplate, firstName, "Confirm your WleeSend email change");

                return Ok(new { Success = true, Message = "A confirmation link has been sent to your new email address." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error changing email");
                return StatusCode(500, new { Success = false, Message = "An error occurred while processing the email change." });
            }
        }

        [AllowAnonymous]
        [HttpPost("confirm-email-change")]
        public async Task<IActionResult> ConfirmEmailChange([FromBody] ConfirmEmailChangeRequest request)
        {
            if (string.IsNullOrWhiteSpace(request?.Token))
                return BadRequest(new { Success = false, Message = "Token is required." });

            try
            {
                var decoded = HttpUtility.UrlDecode(request.Token);
                var payload = _emailChangeProtector.Unprotect(decoded);
                var parts = payload.Split('|', 4);
                if (parts.Length < 4)
                    return BadRequest(new { Success = false, Message = "Invalid email change token." });

                var userId = parts[0];
                var newEmail = parts[1];
                var changeToken = parts[2];
                var issuedAt = DateTime.Parse(parts[3], null, System.Globalization.DateTimeStyles.RoundtripKind);

                var expiryHours = _configuration.GetValue<int?>("EmailVerificationTokenExpiryHours") ?? 24;
                if (DateTime.UtcNow > issuedAt.AddHours(expiryHours))
                    return BadRequest(new { Success = false, Message = "Email change link has expired. Please request a new one." });

                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                    return BadRequest(new { Success = false, Message = "User not found." });

                var existingUser = await _userManager.FindByEmailAsync(newEmail);
                if (existingUser != null && existingUser.Id != userId)
                    return BadRequest(new { Success = false, Message = Messages.EmailAlreadyUsed.ToDescriptionString() });

                var result = await _userManager.ChangeEmailAsync(user, newEmail, changeToken);
                if (!result.Succeeded)
                    return BadRequest(new { Success = false, Message = result.Errors.FirstOrDefault()?.Description ?? "Invalid email change token." });

                return Ok(new { Success = true, Message = "Email address changed successfully." });
            }
            catch (System.Security.Cryptography.CryptographicException)
            {
                return BadRequest(new { Success = false, Message = "Invalid or tampered email change token." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error confirming email change");
                return BadRequest(new { Success = false, Message = "Invalid email change token." });
            }
        }


        [HttpPost("send-dual-email-otp")]
        [AllowAnonymous]
        public async Task<IActionResult> SendDualEmailOtp([FromBody] SendDualEmailOtpRequest request)
        {
            var result = await _accountService.SendDualEmailOtpAsync(request);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("verify-dual-email-otp")]
        [AllowAnonymous]
        public async Task<IActionResult> VerifyDualEmailOtp([FromBody] VerifyDualEmailOtpRequest request)
        {
            var result = await _accountService.VerifyDualEmailOtpAsync(request);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        #region helper methods
        private string GenerateJSONWebToken(UserDetail userInfo)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userInfo.FirstName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Email, userInfo.ApplicationUser.Email)

                };

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<string> GenerateUniqueAccountNumber()
        {
            bool isAccountExists = true;
            string accountNumber = string.Empty;
            while (isAccountExists == true)
            {

                Random rnd = new Random();
                accountNumber = rnd.Next(10000000, 99999999).ToString();
                isAccountExists = await _userDetailsService.IsAccountAlreadyExists(accountNumber);
            }

            return accountNumber;
        }


   

        private async Task<string> uploadDocument(IFormFile file, string userId, string containerName = "kyc-document")
        {
            try
            {
                var getFileExtension = Path.GetExtension(file.FileName);
                var generatedNewFileName = $"{Guid.NewGuid().ToString()}{getFileExtension}";
                var ms = new MemoryStream();

                await file.CopyToAsync(ms);
                ms.ToArray();
                ms.Seek(0, SeekOrigin.Begin);
                var filePath = await _documentUploaderService.UploadData(ms, generatedNewFileName, containerName, getFileExtension, userId);
                return filePath;
            }
            catch (Exception Ex)
            {
                _logger.LogError("Error in uploadDocument helper method");
                throw Ex.InnerException;
            }
        }

        private string GetClientIpAddress()
        {
            if (HttpContext.Connection.RemoteIpAddress != null)
            {
                return HttpContext.Connection.RemoteIpAddress.ToString();
            }
            return "Unknown";
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
                    // Your secret key for encryption (16, 24, or 32 bytes for AES)
                    var secretKey = _configuration.GetSection("SecretKeyForEncryptAndDecrypt").Value; //secret key

                    // Encrypt the text
                    string encryptedText = Common.Encrypt(completeSignUpResourceModel.Email, secretKey);
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


        private async Task LogSignupErrorAsync(string action, string description, string userId = null)
        {
            try
            {
                await _systemActivityLogsService.AddSystemLogs(new SystemActivityLog
                {
                    UserId = userId,
                    Action = action,
                    Description = description,
                    CreatedDate = DateTime.UtcNow
                });
            }
            catch (Exception logEx)
            {
                _logger.LogError(logEx, "Failed to write signup system log. Action: {Action}", action);
            }
        }


        private async Task<IActionResult> SignupBadRequestAsync(string action, string message, string userId = null, string extraDetail = null)
        {
            var description = string.IsNullOrWhiteSpace(extraDetail) ? message : $"{message} | {extraDetail}";
            await LogSignupErrorAsync(action, description, userId);
            return BadRequest(message);
        }

        #endregion
    }
}
