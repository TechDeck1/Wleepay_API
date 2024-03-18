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
using wleepay.Core.Enums;
using Microsoft.SqlServer.Management.Smo;
using wleepay.API.Helpers;
using System.Text.Json;
using wleepay.Communication.Services;
using wleepay.Communication.Templates;
using wleepay.Services;
using static wleepay.Communication.EmailTemplates.EmailLockOutTemplate;
using static wleepay.Communication.EmailTemplates.AccountActivatedTemplate;
using static wleepay.Communication.EmailTemplates.AccountDeactivatedTemplate;
using SendGrid.Helpers.Mail;
using wleepay.API.Middleware;

namespace wleepay.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]

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
        private readonly IEmailSender _emailSender;
        private readonly ISystemActivityLogsService _systemActivityLogsService;
        private readonly ITransferTypeService _transferTypeService;

        public PortalController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IUserDetailService userDetailsService,
            ITransferFeeService transferFeeService,
            ILogger<PortalController> logger,
            IConfiguration configuration,
            IMapper mapper,
            ISMSService sMSService,
            IEmailSender emailSender,
            ISystemActivityLogsService systemActivityLogsService,
            ITransferTypeService transferTypeService
            )
            

        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userDetailsService = userDetailsService;
            _transferFeeService = transferFeeService;
            _logger = logger;
            _configuration = configuration;
            _mapper = mapper;
            _SMSService = sMSService;
            _emailSender = emailSender;
            _systemActivityLogsService = systemActivityLogsService;
            _transferTypeService = transferTypeService;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] PortalLoginRequest loginRequest)
        {
            var validator = new PortalLoginRequestValidator();
            var validationResult = await validator.ValidateAsync(loginRequest);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var user = await _userManager.FindByEmailAsync(loginRequest.Email);
            if (user == null)
                return BadRequest(Messages.NotFound.ToDescriptionString());

            var userDetails = await _userDetailsService.GetUserDetailsById(user.Id);
            var result = await _signInManager.PasswordSignInAsync(user, loginRequest.Password ,false, lockoutOnFailure: true);
           
            //Get user role
            var userRole = await _userManager.GetRolesAsync(user);
            if(!loginRequest.LoginFor.Contains(userRole[0]))
            {
                return Ok(new
                {
                    //UserId = user.Id,
                    Success = false,
                    Message = Messages.WrongLoginPage.ToDescriptionString(),
                });

            }
            
            SystemActivityLog systemActivityLog = new SystemActivityLog();
            systemActivityLog.UserId = user.Id;
            systemActivityLog.Email = loginRequest.Email;
            systemActivityLog.IpAddress = GetClientIpAddress();
            systemActivityLog.Action = ActionsForSystemLogs.Login.ToDescriptionString();

            //Status statusEnumFromDatabase = (Status)userDetails.Status;

            if (userDetails.Status == (int)Status.DeActive)
            {
                systemActivityLog.Description = GenericMessagesForSystemLogs.AccountDeActivated.ToDescriptionString();
                var addLogs = await _systemActivityLogsService.AddSystemLogs(systemActivityLog);
                return Ok(new
                {
                    UserId = user.Id,
                    Success = false,
                    Message = Messages.AccountDeActivated.ToDescriptionString(),
                });
            }

            if (result.IsLockedOut)
            {
                await _userDetailsService.UpdateUserStatus(user.Id, (int)Status.DeActive);

                EmailLockOutTemplateForStaff emailLockOutTemplateForStaff = new EmailLockOutTemplateForStaff
                {
                    StaffEmail = loginRequest.Email,
                    StaffFirstName = userDetails.FirstName,
                    StaffPhoneNumber = userDetails.PhoneNumber
                };
                var subject = EmailSubjectEnums.LoginFailed.ToDescriptionString();
                string htmlTemplateForStaff = emailLockOutTemplateForStaff.GenerateHtml();
                await _emailSender.SendEmailAsync(loginRequest.Email, htmlTemplateForStaff, userDetails.FirstName, subject);

                EmailLockOutTemplateForSystemAdmin emailLockOutTemplateForSystemAdmin = new EmailLockOutTemplateForSystemAdmin
                {
                    StaffEmail = loginRequest.Email,
                    StaffFirstName = userDetails.FirstName,
                    StaffPhoneNumber = userDetails.PhoneNumber
                };

                string htmlTemplateForSystemAdmin = emailLockOutTemplateForSystemAdmin.GenerateHtml();
                var allAdmins = await _userDetailsService.GetUserByRole((int)Role.SuperAdmin);
                foreach (var admin in allAdmins)
                {
                    await _emailSender.SendEmailAsync(admin.Email, htmlTemplateForSystemAdmin, admin.UserDetail.FirstName, subject);
                    //await _emailSender.SendPasswordChangeEmailToSystemAdminstrator(decryptedEmail, userDetails.FirstName, user.PhoneNumber);
                }

                systemActivityLog.Description = GenericMessagesForSystemLogs.AccountLocked.ToDescriptionString();
                 await _systemActivityLogsService.AddSystemLogs(systemActivityLog);

                return Ok(new
                {
                    UserId = user.Id,
                    Success = false,
                    Message = Messages.AccountLocked.ToDescriptionString(),
                });
            }

            if (result.Succeeded)
            {
                

                //generate token
                var token = GenerateJSONWebToken(userDetails, userRole?[0]);
                
                //updated user
                UserDetail updatedUser = null;

                systemActivityLog.Description = GenericMessagesForSystemLogs.SuccessfullyLoggedIn.ToDescriptionString();
                
                await _systemActivityLogsService.AddSystemLogs(systemActivityLog);

                if (userRole[0] == "Staff" || userRole[0] == Role.SystemAdmin.ToDescriptionString())
                {
                    return Ok(new
                    {
                        UserId = user.Id,
                        Success = true,
                        Role = userRole?[0],
                    });
                }

                if (!userDetails.IsLoggedIn)
                    updatedUser = await _userDetailsService.UpdateUserLoggedInStatus(userDetails.UserId, true);

                return Ok(new
                {
                    UserId = user.Id,
                    Success = true,
                    Token = token,
                    IsLoggedInVerified = updatedUser != null ? updatedUser.IsLoggedIn : userDetails.IsLoggedIn,
                    Role = userRole?[0],
                });

            }

            return BadRequest(Messages.BadRequest.ToDescriptionString());
        }

        [SystemLock]
        [HttpPost("SetCashOutTaxRate")]
        public async Task<IActionResult> UpdateTaxRates([FromBody] TaxRateResourceModel taxRateResourceModel)
        {
            //var updatedRates = await _transferFeeService.UpdateTransferFee(taxRates);
            if (taxRateResourceModel.OrganizationFee.Equals(0)) return BadRequest(Messages.InvalidRequest.ToDescriptionString());

            var taxRates = await _transferFeeService.GetTransferFeeByDesc();
            taxRateResourceModel.AgentCommission = taxRates.AgentCommission;
            taxRateResourceModel.OrganizationFee = taxRateResourceModel.OrganizationFee;
            taxRateResourceModel.TaxAmount = taxRates.TaxAmount;
            taxRateResourceModel.Description = taxRates.Description;

            var updatedTaxRatesResource = _mapper.Map<TaxRateResourceModel, TransferFee>(taxRateResourceModel);
            var updatedRates = await _transferFeeService.AddTransferFee(updatedTaxRatesResource);
            if (updatedRates != null)
            {
                var types = await _transferTypeService.GetTransferTypeById((int)TransferTypes.CashOut);
                types.TransferFee = updatedRates.Id;
                await _transferTypeService.UpdateTransferTypeById(types);

                var allAgents = await _userDetailsService.GetUserByRole((int)Role.Agent);
                var agentsListResource = _mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<ApplicationUserResource>>(allAgents);

                foreach (var agent in agentsListResource)
                {
                    var message = MessagesTemplates.TaxRateUpdateMessage(taxRateResourceModel.OrganizationFee.ToString(), agent.UserDetail.FirstName, TaxRates.CashOutFeeRate.ToDescriptionString());

                    var result = _SMSService.sendMessage(message, agent.UserDetail.PhoneNumber);
                }

            }

            return Ok(updatedRates);
        }

        [SystemLock]
        [HttpPost("SetAgentCommissionTaxRates")]
        public async Task<IActionResult> SetAgentCommissionTaxRates([FromBody] TaxRateResourceModel taxRateResourceModel)
        {

            //var updatedRates = await _transferFeeService.UpdateTransferFee(taxRates);
            if(taxRateResourceModel.AgentCommission.Equals(0)) return BadRequest(Messages.InvalidRequest.ToDescriptionString());

            var taxRates = await _transferFeeService.GetTransferFeeByDesc();
            taxRateResourceModel.AgentCommission = taxRateResourceModel.AgentCommission;
            taxRateResourceModel.OrganizationFee = taxRates.OrganizationFee;
            taxRateResourceModel.TaxAmount = taxRates.TaxAmount;
            taxRateResourceModel.Description = taxRates.Description;

            var updatedTaxRatesResource = _mapper.Map<TaxRateResourceModel, TransferFee>(taxRateResourceModel);
            var updatedRates = await _transferFeeService.AddTransferFee(updatedTaxRatesResource);
            if (updatedRates != null)
            {
                var types = await _transferTypeService.GetTransferTypeById((int)TransferTypes.CashOut);
                types.TransferFee = updatedRates.Id;
                await _transferTypeService.UpdateTransferTypeById(types);

                var allAgents = await _userDetailsService.GetUserByRole((int)Role.Agent);
                var agentsListResource = _mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<ApplicationUserResource>>(allAgents);

                foreach (var agent in agentsListResource)
                {
                    var message = MessagesTemplates.TaxRateUpdateMessage(taxRateResourceModel.AgentCommission.ToString(), agent.UserDetail.FirstName, TaxRates.AgentCommisionRate.ToDescriptionString());

                    var result = _SMSService.sendMessage(message, agent.UserDetail.PhoneNumber);
                }
            }
            return Ok(updatedRates);
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
            return Ok(GenericMessagesForSystemLogs.AccountLocked.ToDescriptionString());

        }

        [AllowAnonymous]
        [HttpPost("UnLockAccount")]
        public async Task<IActionResult> UnlockAccount([FromBody] LockAccount model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user != null)
            {
                await _userDetailsService.UpdateUserStatus(user.Id, (int)Status.Active);
                
                // UnLock the user account
                await _userManager.SetLockoutEnabledAsync(user, false);

            }
            return Ok(Messages.UnlockAccount.ToDescriptionString());

        }

        [AllowAnonymous]
        [HttpPost("SendCode")]
        public async Task<IActionResult> SendOTP([FromBody] SendCode model)
        {
            try
            {
                //get user details
                var userDetails = await _userDetailsService.GetUserDetailsById(model.UserId);
                string verificationCode = Common.GenerateVerificationCode(4); // Generate a 4-digit verification code
                string message = "Your one time code is " + verificationCode;
                var code = _SMSService.sendMessage(message, userDetails.PhoneNumber);// service for send otp
                var PhoneNumber = Common.MaskPhoneNumber(userDetails.PhoneNumber);

                if (code != null && code != " ")
                {
                    //update database
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

        [AllowAnonymous]
        [HttpPost("VerifyCode")]
        public async Task<IActionResult> VerifyCode([FromBody]VerificationModel model)
        {
            try
            {
                //get user details
                var userDetails = await _userDetailsService.GetUserDetailsById(model.UserId);

                var user = await _userManager.FindByEmailAsync(model.Email);

                //logic for getting time difference from expiry date and current date
                var expiryDate = userDetails.VerificationExpiry;
                DateTime currentDate = DateTime.UtcNow; // Use UtcNow to compare in UTC

                TimeSpan difference = (TimeSpan)(currentDate - expiryDate);
                int minutesDifference = (int)difference.TotalMinutes;

                // Check if the given date is expired (more than 2 minutes ago)
                bool isExpired = minutesDifference >= 1;

                object Data = null;
                if (!isExpired)
                {
                    if (userDetails.VerificationCode == model.Code)
                    {
                        var userRole = await _userManager.GetRolesAsync(user);

                        var token = GenerateJSONWebToken(userDetails, userRole?[0].ToString());
                       
                        //updated user
                        UserDetail updatedUser = null;

                        //set logged in status to true if first log in
                        if (!userDetails.IsLoggedIn)
                            updatedUser = await _userDetailsService.UpdateUserLoggedInStatus(userDetails.UserId, true);

                        var ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();
                        _logger.LogInformation($"Action: {ControllerContext.ActionDescriptor.ActionName}, User: {userDetails.UserId}, Timestamp: {DateTime.UtcNow}, IP: {ipAddress}");
                       
                        if (!string.IsNullOrEmpty(userDetails?.Permissions))
                        {
                            string[] deserializedNumbers = JsonSerializer.Deserialize<string[]>(userDetails?.Permissions);

                            string jsonString = JsonSerializer.Serialize(deserializedNumbers);
                            
                            var secretKey = _configuration.GetSection("SecretKeyForEncryptAndDecrypt").Value; //secret key

                            userDetails.Permissions = Common.Encrypt(jsonString, secretKey).ToString();
                        }

                        return Ok(new
                        {
                            Data = (new
                            {
                                UserId = userDetails.UserId,
                                Token = token,
                                IsLoggedInVerified = updatedUser != null ? updatedUser.IsLoggedIn : userDetails.IsLoggedIn,
                                Role = userRole?[0]

                            }),
                            Session = userDetails.Permissions,
                            IsExpired = false,
                            Success= true,
                            Message=Messages.VerificationSuccessfull.ToDescriptionString()

                        });
                    }
                    else
                    {
                        return Ok(new
                        {
                            Data,
                            IsExpired = false,
                            Success = true,
                            Message =Messages.WrongOtp.ToDescriptionString(),

                        });
                    }
                }
                else
                {
                    return Ok(new 
                    {
                        Data,
                        IsExpired = true,
                        Success=false,
                        Message = Messages.OtpExpired.ToDescriptionString(),

                    });

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        [AllowAnonymous]
        [HttpPost("ActiveOrDeactivateAccount")]
        public async Task<IActionResult> ActiveOrDeActiveAccount([FromBody] ActivateDeactivateAccountResource model)
        {
            try
            {
                //get user details using identity
                var user = await _userManager.FindByEmailAsync(model.EmailId);
                if (user == null)
                    return BadRequest("No user found.");

                var userDetails = await _userDetailsService.GetUserDetailsById(user.Id);
                if (userDetails != null)
                {
                    SystemActivityLog systemActivityLog = new SystemActivityLog();
                    systemActivityLog.UserId = user.Id;
                    systemActivityLog.Email = model.EmailId;
                    systemActivityLog.IpAddress = GetClientIpAddress();
                    
                    //update activity status for user
                    var result = await _userDetailsService.UpdateUserStatus(model.UserId, model.Status);
                    if (model.Status == (int)Status.Active)
                    {
                        systemActivityLog.Action = ActionsForSystemLogs.Activate.ToDescriptionString();
                        systemActivityLog.Description = GenericMessagesForSystemLogs.AccountActivated.ToDescriptionString();

                        var subject = EmailSubjectEnums.AccountSetupConfirmation.ToDescriptionString();

                        //send email to staff for activation
                        AccountActivatedTemplateForStaff templateForstaff = new AccountActivatedTemplateForStaff
                        {
                            StaffEmail = model.EmailId,
                            StaffFirstName = userDetails.FirstName,
                            StaffPhoneNumber = userDetails.PhoneNumber,

                        };

                        string htmlTemplateForStaff = templateForstaff.GenerateHtml();
                        await _emailSender.SendEmailAsync(model.EmailId, htmlTemplateForStaff, userDetails.FirstName, subject);

                        //send email to all admins for activation
                        var allSystemAdmins = await _userDetailsService.GetUserByRole((int)Role.SystemAdmin);
                        var allSystemAdminsList = _mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<ApplicationUserResource>>(allSystemAdmins);
                        AccountActivatedTemplateForSystemAdmin template = new AccountActivatedTemplateForSystemAdmin
                        {
                            StaffEmail = model.EmailId,
                            StaffFirstName = userDetails.FirstName,
                            StaffPhoneNumber = userDetails.PhoneNumber,
                            StaffPermissions = userDetails.Permissions

                        };
                        string htmlTemplate = template.GenerateHtml();
                        foreach (var systemAdmin in allSystemAdminsList)
                        {
                            await _emailSender.SendEmailAsync(systemAdmin.Email, htmlTemplate, systemAdmin.UserDetail.FirstName, subject);
                        }
                    }
                    else
                    {
                        string message = "The wleepay staff with this email " + model.EmailId + " is now deactivated";
                        systemActivityLog.Action = ActionsForSystemLogs.Deactivate.ToDescriptionString();
                        systemActivityLog.Description = GenericMessagesForSystemLogs.AccountDeActivated.ToDescriptionString();
                        _SMSService.sendMessage(message, userDetails.PhoneNumber);
                        
                        //send email to staff for deactivation
                        AccountDeactivatedTemplateForStaff accountDeactivatedTemplateForStaff = new AccountDeactivatedTemplateForStaff
                        {
                            StaffEmail = model.EmailId,
                            StaffFirstName = userDetails.FirstName,
                            StaffPhoneNumber = userDetails.PhoneNumber
                        };
                        var subject = EmailSubjectEnums.AccountDeactivate.ToDescriptionString();
                        string htmlTemplateForStaff = accountDeactivatedTemplateForStaff.GenerateHtml();
                        await _emailSender.SendEmailAsync(model.EmailId, htmlTemplateForStaff, userDetails.FirstName, subject);

                        //send email to all admins for deactivation
                        var allSystemAdmins = await _userDetailsService.GetUserByRole((int)Role.SystemAdmin);
                        var allSystemAdminsList = _mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<ApplicationUserResource>>(allSystemAdmins);

                        AccountDeactivatedTemplateForSystemAdmin template = new AccountDeactivatedTemplateForSystemAdmin
                        {
                            StaffEmail = model.EmailId,
                            StaffFirstName = userDetails.FirstName,
                            StaffPhoneNumber = userDetails.PhoneNumber,
                            StaffPermissions = userDetails.Permissions

                        };

                        string htmlTemplate = template.GenerateHtml();
                        foreach (var systemAdmin in allSystemAdminsList)
                        {
                            await _emailSender.SendEmailAsync(systemAdmin.Email, htmlTemplate, userDetails.FirstName, subject);
                        }
                    }

                    var addLogs = await _systemActivityLogsService.AddSystemLogs(systemActivityLog);
                    return Ok(result.Status);

                }

                return BadRequest();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        [HttpGet("TaxRates")]
        public async Task<IActionResult> GetAllTaxRatesHistory([FromQuery]PagingRequestForRates pagingRequestForRates)
        {
            var transferFees = await _transferFeeService.GetAllTransferFees(pagingRequestForRates.Index, pagingRequestForRates.Size);
            return Ok(transferFees);
        }

        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(RefreshTokenModel tokenModel)
        {
            if (string.IsNullOrEmpty(tokenModel.Token))
            {
                return BadRequest("Invalid client request");
            }

            string? accessToken = tokenModel.Token;
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(accessToken) as JwtSecurityToken;
            string emailAddress = "";
            if (jsonToken != null)
            {
                // Get the emailAddress claim
                emailAddress = jsonToken.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").Value;
                Console.WriteLine("emailAddress: " + emailAddress);
            }
            else
            {
                return BadRequest(Messages.InvalidAccessToken.ToDescriptionString());
            }
            var user = await _userManager.FindByEmailAsync(emailAddress);

            if (user == null)
            {
                return BadRequest(Messages.InvalidAccessToken.ToDescriptionString());
            }
            var userRole = await _userManager.GetRolesAsync(user);

            var userDetails = await _userDetailsService.GetUserDetailsById(user.Id);

            var newAccessToken = GenerateJSONWebToken(userDetails, userRole?[0]);

            return Ok(new
            {
                accessToken = newAccessToken,
            });
        }

        #region helper methods
        private string GenerateJSONWebToken(UserDetail userInfo, string userRole)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userInfo.FirstName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Role, userRole), // Adding a role claim
                    new Claim(ClaimTypes.Email, userInfo.ApplicationUser.Email)

                };

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(5),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GetClientIpAddress()
        {
            if (HttpContext.Connection.RemoteIpAddress != null)
            {
                return HttpContext.Connection.RemoteIpAddress.ToString();
            }
            return "Unknown";
        }
        #endregion
    }
}
