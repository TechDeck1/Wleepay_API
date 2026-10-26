using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using wleepay.Communication.Interface;
using wleepay.Communication.Services;
using wleepay.Communication.Templates;
using wleepay.Communication.EmailTemplates;
using Microsoft.AspNetCore.DataProtection;
using wleepay.Core.Common;
using wleepay.Core.DTO.Request;
using wleepay.Core.DTO.Response;
using wleepay.Core.Enums;
using wleepay.Core.Models;
using wleepay.Core.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using wleepay.Core.Common;
using static Azure.Core.HttpHeader;

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



        //private string ValidateUserInput(RegisterUserRequest request)
        //{
        //    if (string.IsNullOrWhiteSpace(request.FirstName) ||
        //        string.IsNullOrWhiteSpace(request.LastName) ||
        //        string.IsNullOrWhiteSpace(request.EmailId) ||
        //        string.IsNullOrWhiteSpace(request.PhoneNumber) ||
        //        string.IsNullOrWhiteSpace(request.Gender) ||
        //        request.CountyId == 0 ||
        //        string.IsNullOrWhiteSpace(request.Pin))
        //    {
        //        return "All fields are required.";
        //    }

        //    if (!Regex.IsMatch(request.EmailId, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
        //    {
        //        return "Invalid email format.";
        //    }

        //    var pinError = ValidatePin(request.Pin);
        //    return pinError;
        //}

        //private string ValidatePin(string pin)
        //{
        //    if (string.IsNullOrWhiteSpace(pin))
        //        return "PIN is required.";

        //    if (pin.Length < 8)
        //        return "PIN must be at least 8 characters.";

        //    if (!Regex.IsMatch(pin, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@#$%^&*]).{8,}$"))
        //        return "PIN must contain uppercase, lowercase, number, and special character.";

        //    return null;
        //}

        //private string GenerateJSONWebToken(UserDetail userInfo)
        //{
        //    var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
        //    var authClaims = new List<Claim>
        //    {
        //        new Claim(ClaimTypes.Name, userInfo.FirstName),
        //        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        //        new Claim(ClaimTypes.Email, userInfo.ApplicationUser.Email),
        //        new Claim("UserId", userInfo.UserId),
        //        new Claim(ClaimTypes.NameIdentifier, userInfo.UserId)
        //    };

        //    var appUser = _userManager.FindByIdAsync(userInfo.UserId).Result;
        //    if (appUser != null)
        //    {
        //        var roles = _userManager.GetRolesAsync(appUser).Result;
        //        var role = roles.FirstOrDefault();
        //        if (!string.IsNullOrEmpty(role))
        //        {
        //            authClaims.Add(new Claim(ClaimTypes.Role, role));
        //        }
        //    }

        //    var token = new JwtSecurityToken(
        //        issuer: _configuration["JWT:ValidIssuer"],
        //        audience: _configuration["JWT:ValidAudience"],
        //        expires: DateTime.UtcNow.AddHours(3),
        //        claims: authClaims,
        //        signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        //    );

        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}

    }
}

