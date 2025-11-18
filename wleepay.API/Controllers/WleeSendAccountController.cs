using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Mvc;
using wleepay.API.Resources;
using Microsoft.Extensions.Logging;
using wleepay.Core.Services;
using Stripe.Tax;
using wleepay.Core.DTO.Request;
using Microsoft.AspNetCore.Identity;
using wleepay.API.Validators;
using wleepay.Communication.Templates;
using wleepay.Core.Enums;
using wleepay.Core.Models;
using wleepay.Services;
using wleepay.Storage.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using wleepay.Core.Common;
using AutoMapper;
using System.Web;
using wleepay.API.Helpers;
using wleepay.Communication.Interface;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Linq;
using Twilio.Jwt.AccessToken;



namespace wleepay.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WleeSendAccountController: ControllerBase
    {
        private readonly IWleeSendUserDetailService _wleeSendUserDetailsService;
        private readonly IConfiguration _configuration;
        private readonly UserManager<WleeSendUser> _userManager;
        private readonly SignInManager<WleeSendUser> _signInManager;
        private readonly IMapper _mapper;
        private readonly ISMSService _smsService;
        private readonly ILogger<WleeSendAccountController> _logger;
        private readonly IAddressService _addressService;
        IWleeSendUserDetailService wleeSendUserDetailsService;


        public WleeSendAccountController(
        IWleeSendUserDetailService wleeSendUserDetailsService,
        ILogger<WleeSendAccountController> logger,
        IAddressService addressService,
        IConfiguration configuration,
        UserManager<WleeSendUser> userManager,
        SignInManager<WleeSendUser> signInManager,
        IMapper mapper,
        ISMSService smsService
)
        {
            _wleeSendUserDetailsService = wleeSendUserDetailsService;
            _logger = logger;
            _addressService = addressService;
            _configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _smsService = smsService;
        }

        [HttpPost("WleeSend-Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            if (string.IsNullOrWhiteSpace(loginRequest.PhoneNo))
                return BadRequest("Phone number is required.");

            if (string.IsNullOrWhiteSpace(loginRequest.Pin) || loginRequest.Pin.Length != 4)
                return BadRequest("Pin should be 4 characters long.");

            var userDetails = await _wleeSendUserDetailsService.GetWleeSendUserDetailsByPhone(loginRequest.PhoneNo);
            if (userDetails == null)
                return BadRequest("Account does not exist.");

            //if (loginRequest.Pin != userDetails.Pin)
            //{
            //    await _userDetailsService.UpdateUserLoggedInStatus(userDetails.UserId, false, userDetails.FailedLoginAttempt + 1);
            //    return BadRequest("Invalid pin.");
            //}

            await _wleeSendUserDetailsService.UpdateUserLoggedInStatus(userDetails.UserId, true, 0);

            var response = new
            {
                UserId = userDetails.UserId,
                Success = true,
                FirstName = userDetails.FirstName,
                LastName = userDetails.LastName,
                ProfilePic = System.IO.File.Exists(userDetails.Image)
                                ? Convert.ToBase64String(System.IO.File.ReadAllBytes(userDetails.Image))
                                : userDetails.Image,
                PhoneNumber = userDetails.PhoneNumber,
                AccountNumber = userDetails.AccountNumber
            };

            return Ok(response);
        }


        [AllowAnonymous]
        [HttpPost("WleeSend-Registration")]
        public async Task<IActionResult> Register([FromBody] AddUserDetailsResource userDetail)
        {
            try
            {
                var baseUrl = _configuration.GetSection("BaseUrl").Value;
                var validator = new AddUserDetailsValidator(userDetail.Role.ToString());
                var validationResult = await validator.ValidateAsync(userDetail);

                if (!validationResult.IsValid)
                    return BadRequest(validationResult.Errors);

                //check if user exists with email
                WleeSendUser applicationUser = new WleeSendUser();
                if (!string.IsNullOrEmpty(userDetail.EmailId))
                    applicationUser = await _userManager.FindByEmailAsync(userDetail.EmailId);

                if (applicationUser != null && !string.IsNullOrEmpty(applicationUser.Id))
                    return BadRequest(Messages.EmailAlreadyUsed.ToDescriptionString());

                //check if phone number already exists
                var userExistWithPhone = await _wleeSendUserDetailsService.IsPhoneAlreadyRegistered(userDetail.PhoneNumber);
                if (userExistWithPhone == true)
                    return BadRequest(Messages.PhoneAlreadyUsed.ToDescriptionString());

                //create new user with identity
                var newUser = new WleeSendUser() { UserName = userDetail.PhoneNumber, Id = Guid.NewGuid().ToString() };
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
                    //assign role to user
                    var roleEnumStringValue = userDetail.Role.HasValue ? ((Core.Enums.Role)userDetail.Role) : ((Core.Enums.Role)Core.Enums.Role.Member);
                    await _userManager.AddToRoleAsync(newUser, roleEnumStringValue.ToString());

                    if (!string.IsNullOrEmpty(userDetail.Pin))
                    {
                        //sign in user with identity
                        var result = await _signInManager.PasswordSignInAsync(newUser, userDetail.Pin.ToString(), false, true);
                    }

                    //insert data in userdetails
                    var userDetailsToInsert = _mapper.Map<AddUserDetailsResource, WleeSendUserDetails>(userDetail);
                    userDetailsToInsert.UserId = newUser.Id;

                    //generate account number for user
                    userDetailsToInsert.AccountNumber = await GenerateUniqueAccountNumber();

                    userDetailsToInsert.Status = (int)wleepay.Core.Enums.Status.Active;
                    //save data
                    var updatedDetails = await _wleeSendUserDetailsService.AddOrUpdateUserDetails(userDetailsToInsert);
                    if (updatedDetails.UserId != null)
                    {
                        //add user address
                        if (userDetail.Address != null)
                        {
                            var addressToInsert = _mapper.Map<AddressResource, Address>(userDetail.Address);
                            addressToInsert.UserId = updatedDetails.UserId;
                            await _addressService.AddOrUpdateAddress(addressToInsert);
                        }

                        //generate jwt token
                        var token = GenerateJSONWebToken(userDetailsToInsert);

                        CompleteSignUpResourceModel completeSignUpResourceModel = new CompleteSignUpResourceModel();
                        completeSignUpResourceModel.Email = userDetail.EmailId;
                        completeSignUpResourceModel.PhoneNumber = userDetail.PhoneNumber;
                        completeSignUpResourceModel.ClientURI = baseUrl + "/agent-account-setup";
                        var sendSms = await GenerateLinkForComleteSignUp(completeSignUpResourceModel);

                        //send registration sucess message on mobile number
                        _smsService.sendMessage(MessagesTemplates.RegistrationSuccessMessage(updatedDetails.AccountNumber, updatedDetails?.FirstName), updatedDetails.PhoneNumber);
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
                        return BadRequest(Messages.UserNotCreated.ToDescriptionString());
                }
                else
                    return BadRequest(isUserCreated.Errors.FirstOrDefault().Description);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw ex.InnerException;
            }
        }



        private async Task<string> GenerateUniqueAccountNumber()
        {
            bool isAccountExists = true;
            string accountNumber = string.Empty;
            while (isAccountExists == true)
            {

                Random rnd = new Random();
                accountNumber = rnd.Next(10000000, 99999999).ToString();
                isAccountExists = await _wleeSendUserDetailsService.IsAccountAlreadyExists(accountNumber);
            }

            return accountNumber;
        }

        private async Task<bool> GenerateLinkForComleteSignUp(CompleteSignUpResourceModel completeSignUpResourceModel)
        {
            var user = await _userManager.FindByEmailAsync(completeSignUpResourceModel.Email);
            if (user == null)
                return false;

            var userDetails = await _wleeSendUserDetailsService.GetUserDetailsById(user.Id);
            if (userDetails != null)
            {
                if (userDetails.PhoneNumber == completeSignUpResourceModel.PhoneNumber && user.Email == completeSignUpResourceModel.Email)
                {
                    var secretKey = _configuration.GetSection("SecretKeyForEncryptAndDecrypt").Value; //secret key

                    string encryptedText = Common.Encrypt(completeSignUpResourceModel.Email, secretKey);
                    var token = HttpUtility.UrlEncode(encryptedText);
                    string callbackUrl = $"{completeSignUpResourceModel.ClientURI}?email={(token)}";
                    string message = "Your account for wleepay is successfully created, To setup account please open below link and complete registration." + " " + callbackUrl;

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

        private string GenerateJSONWebToken(WleeSendUserDetails userInfo)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userInfo.FirstName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Email, userInfo.WleeSendUser.Email)

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



    }
}
