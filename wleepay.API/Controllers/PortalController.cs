using AutoMapper;
using DevExpress.Data.ODataLinq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using wleepay.API.Resources;
using wleepay.API.Validators;
using wleepay.Core.Common;
using wleepay.Core.Models;
using wleepay.Core.Services;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Http;
using wleepay.Communication.Interface;
using DevExpress.Data.Filtering.Helpers;

namespace wleepay.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class PortalController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserDetailService _userDetailsService;
        private readonly ITransferFeeService _transferFeeService;
        private readonly ILogger<PortalController> _logger;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly ISMSService _SMSService;


        public PortalController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IUserDetailService userDetailsService,
            ITransferFeeService transferFeeService,
            ILogger<PortalController> logger,
            IConfiguration configuration,
            IMapper mapper,
            ISMSService sMSService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userDetailsService = userDetailsService;
            _transferFeeService = transferFeeService;
            _logger = logger;
            _configuration = configuration;
            _mapper = mapper;
            _SMSService = sMSService;
        }

        
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] PortalLoginRequest loginRequest)
        {
            var validator = new PortalLoginRequestValidator();
            var validationResult = await validator.ValidateAsync(loginRequest);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var user = await _userManager.FindByEmailAsync(loginRequest.Email);
            if (user == null)
                return BadRequest("No user found.");
          
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginRequest.Password, false);
            if(result.IsLockedOut) return Ok(new
            {
                UserId = user.Id,
                Success = false,
                Message = "You have 3 failed login attemps. Your Wleepay portal account is locked. You must contact system administrator to unlock your account",
            });

            if (result.Succeeded)
            {
                //get user
                var userDetails = await _userDetailsService.GetUserDetailsById(user.Id);
                //generate token
                var token = GenerateJSONWebToken(userDetails);
                //Get user role
                var userRole = await _userManager.GetRolesAsync(user);
                //updated user
                UserDetail updatedUser = null;
                //set logged in status to true if first log in
                
                //if (!userDetails.IsLoggedIn)
                //    updatedUser = await _userDetailsService.UpdateUserLoggedInStatus(userDetails.UserId, true);

                return Ok(new
                {
                    UserId = user.Id,
                    Success = true,
                    //Token = token,
                    //IsLoggedInVerified = updatedUser != null ? updatedUser.IsLoggedIn : userDetails.IsLoggedIn,
                    Role = userRole?[0],
                });

            }
            return BadRequest("Invalid login request.");
        }

        [HttpPost("TaxRates")]
        public async Task<IActionResult> UpdateTaxRates([FromBody] TaxRateResourceModel model)
        {
            var validator = new TaxRateRequestValidator();
            var validationResult = await validator.ValidateAsync(model);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var taxRates = _mapper.Map<TaxRateResourceModel, TransferFee>(model);
            var updatedRates = await _transferFeeService.UpdateTransferFee(taxRates);

            var updatedRatesResource = _mapper.Map<TransferFee, TaxRateResourceModel>(updatedRates);
            return Ok(updatedRatesResource);
        }

        [HttpPost("LockAccount")]
        public async Task<IActionResult> LockAccount([FromBody] LockAccount model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user != null)
            {
                // Lock the user account
                await _userManager.SetLockoutEnabledAsync(user, true);
                await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.MaxValue); // Lock indefinitely
            }
            return Ok("You have 3 failed login attemps. Your Wleepay portal account is locked. You must contact system administrator to unlock your account");

        }

        [HttpPost("UnLockAccount")]
        public async Task<IActionResult> UnlockAccount([FromBody] LockAccount model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user != null)
            {
                // Lock the user account
                await _userManager.SetLockoutEnabledAsync(user, false);

            }
            return Ok("Your account is unlocked by system administrator");

        }

        [HttpPost("alertMail")]
        public void SendEmail()
        {
            try
            {
                var subject = "Failed Login attempt";
                var recipientEmail = "admintest1@yopmail.com";
                var body = "You have 3 failed login attemps. Your Wleepay portal account is locked. You must contact system administrator to unlock your account";

                // Sender's email address and credentials
                string senderEmail = "ariel.ranjot@gmail.com";
                string senderPassword = "R@nj0t@20*$"; // Use app-specific passwords or secure storage for passwords

                // Create SMTP client
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com"); // Use your SMTP server

                // Port and SSL configuration
                smtpClient.Port = 587; // Port for SMTP (e.g., 587 for Gmail)
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(senderEmail, senderPassword);

                MailMessage mailMessage = new MailMessage(senderEmail, recipientEmail, subject, body);

                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                // Handle exceptions or errors here
                Console.WriteLine("Error sending email: " + ex.Message);
            }
        }


        [HttpPost("SendCode")]
        public async Task<IActionResult> SendOTP([FromBody] SendCode model)
        {
            try
            {
                var userDetails = await _userDetailsService.GetUserDetailsById(model.UserId);
                string verificationCode = GenerateVerificationCode(4); // Generate a 4-digit verification code
                string message = "Your one time code is " + verificationCode;
                var code = _SMSService.sendMessage(message, userDetails.PhoneNumber);
                var PhoneNumber = MaskCreditCard(userDetails.PhoneNumber);

                if (code != null && code != " ")
                {

                    var updateDatabase = await _userDetailsService.UpdateUserVerificationCode(model.UserId, verificationCode);
                    return Ok(new
                    {
                        Phone = PhoneNumber,
                        Sucess = true

                    });
                }
                return BadRequest();

            }
            catch (Exception ex)
            {

                throw ex;
            }
           

        }

        [HttpPost("VerifyCode")]
        public async Task<IActionResult> VerifyCode([FromBody]VerificationModel model)
        {
            try
            {
                var userDetails = await _userDetailsService.GetUserDetailsById(model.UserId);
                var expiryDate = userDetails.VerificationExpiry;
                DateTime currentDate = DateTime.UtcNow; // Use UtcNow to compare in UTC

                TimeSpan difference = (TimeSpan)(currentDate - expiryDate);
                int minutesDifference = (int)difference.TotalMinutes;

                // Check if the given date is expired (more than 2 minutes ago)
                bool isExpired = minutesDifference >= 2;

                object Data = null;
                if (!isExpired)
                {
                    if (userDetails.VerificationCode == model.Code)
                    {
                        var token = GenerateJSONWebToken(userDetails);
                       
                        //updated user
                        UserDetail updatedUser = null;
                        //set logged in status to true if first log in
                        if (!userDetails.IsLoggedIn)
                            updatedUser = await _userDetailsService.UpdateUserLoggedInStatus(userDetails.UserId, true);

                        var ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();
                        _logger.LogInformation($"Action: {ControllerContext.ActionDescriptor.ActionName}, User: {userDetails.UserId}, Timestamp: {DateTime.UtcNow}, IP: {ipAddress}");
                       
                        return Ok(new
                        {
                            Data = (new
                            {
                                UserId = userDetails.UserId,
                                Token = token,
                                IsLoggedInVerified = updatedUser != null ? updatedUser.IsLoggedIn : userDetails.IsLoggedIn,

                            }),
                            isExpired = false,
                            Success= true,
                            Message="Verification is successfull"
                        });
                    }
                    else
                    {
                        return Ok(new
                        {
                            Data,
                            isExpired = false,
                            Success = true,
                            Message ="You entered wrong OTP! please try again.",
                        });
                    }
                }
                else
                {
                    return Ok(new 
                    {
                        Data,
                        isExpired = true,
                        success=false,
                        Message = "Your OTP is expired! please try again."
                    });

                }

                return BadRequest();
                
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        #region helper methods
        private string GenerateJSONWebToken(UserDetail userInfo)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userInfo.FirstName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

            var token = new JwtSecurityToken(
                //issuer: _configuration["JWT:ValidIssuer"],
                //audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(30),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GenerateVerificationCode(int length)
        {
            const string validChars = "0123456789"; // Characters to use in the verification code
            StringBuilder verificationCode = new StringBuilder();
            Random random = new Random();

            for (int i = 0; i < length; i++)
            {
                int randomIndex = random.Next(0, validChars.Length);
                verificationCode.Append(validChars[randomIndex]);
            }

            return verificationCode.ToString();
        }

        private string MaskCreditCard(string PhoneNumber)
        {
            // Check if the Phone number is at least 4 digits long
            if (PhoneNumber.Length >= 4)
            {
                // Get the last 2 digits of the Phone number
                string lastTwoDigits = PhoneNumber.Substring(PhoneNumber.Length - 2);

                // Mask the rest of the Phone number with asterisks (*)
                string maskedPhoneNumber = new string('*', PhoneNumber.Length - 2) + lastTwoDigits;

                return maskedPhoneNumber;
            }
            else
            {
                // If the Phone number is less than 4 digits, return it as it is (no masking)
                return PhoneNumber;
            }
        }

        #endregion
    }
}
