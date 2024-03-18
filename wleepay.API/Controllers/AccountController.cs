using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using wleepay.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wleepay.Core.Common;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using wleepay.API.Resources;
using wleepay.API.Validators;
using AutoMapper;
using wleepay.Core.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using wleepay.Communication.Interface;
using Microsoft.Extensions.Configuration;
using wleepay.Communication.Templates;
using wleepay.Storage.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using IEmailSender = wleepay.Communication.Interface.IEmailSender;
using System.Web;
using wleepay.Core.Enums;
using wleepay.API.Helpers;
using System.Text.Json;
using static wleepay.Communication.EmailTemplates.EmailTemplates;
using static wleepay.Communication.EmailTemplates.PasswordChangeTemplate;
using static wleepay.Communication.EmailTemplates.AccountActivatedTemplate;

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
        private readonly IUploadDocument _storageService;
        private readonly IAddressService _addressService;
        private readonly ILogger<AccountController> _logger;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public readonly IHostingEnvironment _hostingEnvironment;
        private readonly IUploadDocument _documentUploaderService;
        private readonly IEmailSender _emailSender;
        private readonly ITokenStorageService _tokenStorageService;
        private readonly ISystemActivityLogsService _systemActivityLogsService;
        private readonly IUsersStaffRoleService _usersStaffRoleService;
        private readonly IVerificationService _verificationService;

        public AccountController(UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            IUserDetailService userDetailsService,
            IWalletService walletService,
            ISmileIdentityService smileIdentityService,
            IUploadDocument storageService,
            ILogger<AccountController> logger,
            IKYCDocumentsService kycDocumentsService,
            IAddressService addressService,
            ISMSService smsService,
            IConfiguration configuration,
            IMapper mapper,
            IUploadDocument uploadDocument,
            IHostingEnvironment hostingEnvironment,
            IEmailSender emailSender,
            ITokenStorageService tokenStorageService,
            ISystemActivityLogsService systemActivityLogsService,
            IUsersStaffRoleService usersStaffRoleService,
            IVerificationService verificationService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _userDetailsService = userDetailsService;
            _walletService = walletService;
            _addressService = addressService;
            _smileIdentityService = smileIdentityService;
            _storageService = storageService;
            _logger = logger;
            _kycDocumentsService = kycDocumentsService;
            _smsService = smsService;
            _configuration = configuration;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
            _documentUploaderService = uploadDocument;
            _emailSender = emailSender;
            _tokenStorageService = tokenStorageService;
            _systemActivityLogsService = systemActivityLogsService;
            _usersStaffRoleService = usersStaffRoleService;
            _verificationService = verificationService;
        }

        [AllowAnonymous]
        [HttpPost("Registration")]
        public async Task<IActionResult> Register([FromBody] AddUserDetailsResource userDetail)
        {
            try
            {
                var validator = new AddUserDetailsValidator(userDetail.Role.ToString());
                var validationResult = await validator.ValidateAsync(userDetail);

                if (!validationResult.IsValid)
                    return BadRequest(validationResult.Errors);

                //check for super admin
                if (userDetail.Role == 0)
                    return BadRequest(Messages.IsSuperAdmin.ToDescriptionString());

                //check if user exists with email
                ApplicationUser applicationUser = new ApplicationUser();
                if (!string.IsNullOrEmpty(userDetail.EmailId))
                    applicationUser = await _userManager.FindByEmailAsync(userDetail.EmailId);

                if (applicationUser != null && !string.IsNullOrEmpty(applicationUser.Id))
                    return BadRequest(Messages.EmailAlreadyUsed.ToDescriptionString());

                //check if phone number already exists
                var userExistWithPhone = await _userDetailsService.IsPhoneAlreadyRegistered(userDetail.PhoneNumber);
                if (userExistWithPhone == true)
                    return BadRequest(Messages.PhoneAlreadyUsed.ToDescriptionString());

                //create new user with identity

                var newUser = new ApplicationUser() { UserName = userDetail.PhoneNumber, Id = Guid.NewGuid().ToString() };
                if (!string.IsNullOrEmpty(userDetail.EmailId))
                    newUser.Email = userDetail.EmailId;

                var isUserCreated = await _userManager.CreateAsync(newUser, userDetail.Pin.ToString());

                if (isUserCreated.Succeeded)
                {
                    //assign role to user
                    var roleEnumStringValue = userDetail.Role.HasValue ? ((Core.Enums.Role)userDetail.Role) : ((Core.Enums.Role)Core.Enums.Role.Member);
                    await _userManager.AddToRoleAsync(newUser, roleEnumStringValue.ToString());
                    
                    //sign in user with identity
                    var result = await _signInManager.PasswordSignInAsync(newUser, userDetail.Pin.ToString(), false, true);

                    //insert data in userdetails
                    var userDetailsToInsert = _mapper.Map<AddUserDetailsResource, UserDetail>(userDetail);
                    userDetailsToInsert.UserId = newUser.Id;

                    //generate account number for user
                    userDetailsToInsert.AccountNumber = await GenerateUniqueAccountNumber();

                    //save smile identity image to blob for user profile
                    if (!string.IsNullOrEmpty(userDetailsToInsert.Image))
                    {
                        var profileURL = await _storageService.UploadImageWithURL(userDetailsToInsert.Image, "user-profile-picture", newUser.Id);
                        //var profileURL = userDetailsToInsert.Image + "";
                        //var profileURL = _storageService.UploadUserProfileImage(userDetailsToInsert.Image, newUser.Id);
                        userDetailsToInsert.Image = profileURL;
                    }

                    if (!string.IsNullOrEmpty(userDetailsToInsert.KYCDocumentImage))
                    {
                        var documentURL = _storageService.UploadKYCDocument(userDetailsToInsert.KYCDocumentImage, newUser.Id);
                        //var profileURL = userDetailsToInsert.Image + "";
                        userDetailsToInsert.KYCDocumentImage = documentURL.ToString();
                        if (userDetailsToInsert.KYCLevel == Core.Enums.KYCLevel.L1.GetHashCode())
                        {
                            //update kyc level from l1 to l2 for new document upload
                            userDetailsToInsert.KYCLevel = Core.Enums.KYCLevel.L2.GetHashCode();
                        }
                    }

                    userDetailsToInsert.Status = (int)Status.Active;
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

                        //generate jwt token
                        var token = GenerateJSONWebToken(userDetailsToInsert);

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

        [HttpPost("RegisterStaff")]
        public async Task<IActionResult> RegisterStaff([FromBody] AddStaffDetailsResource userDetail)
        {
            try
            {
                var baseUrl = _configuration.GetSection("BaseUrl").Value;
                var validator = new AddStaffDetailsValidator();
                var validationResult = await validator.ValidateAsync(userDetail);

                if (!validationResult.IsValid)
                    return BadRequest(validationResult.Errors);

                //check for super admin
                if (userDetail.Role == 0)
                    return BadRequest(Messages.IsSuperAdmin.ToDescriptionString());

                //check if phone number already exists
                var userExistWithPhone = await _userDetailsService.IsPhoneAlreadyRegistered(userDetail.PhoneNumber);

                //check if user exists with email
                ApplicationUser applicationUser = new ApplicationUser();
                if (!string.IsNullOrEmpty(userDetail.EmailId))
                    applicationUser = await _userManager.FindByEmailAsync(userDetail.EmailId);

                if ((applicationUser != null && !string.IsNullOrEmpty(applicationUser.Id)) || userExistWithPhone == true)
                {
                    if (applicationUser != null && !string.IsNullOrEmpty(applicationUser.Id) && userExistWithPhone == true)
                    return BadRequest(Messages.PhoneAndEmailAlreadyUsed.ToDescriptionString());

                    if (applicationUser != null && !string.IsNullOrEmpty(applicationUser.Id))
                    return BadRequest(Messages.EmailAlreadyUsed.ToDescriptionString());

                    if (userExistWithPhone == true)
                    return BadRequest(Messages.PhoneAlreadyUsed.ToDescriptionString());
                }

                //create new user with identity
                var newUser = new ApplicationUser() { UserName = userDetail.PhoneNumber, Id = Guid.NewGuid().ToString() };
                if (!string.IsNullOrEmpty(userDetail.EmailId))
                    newUser.Email = userDetail.EmailId;

                var isUserCreated = await _userManager.CreateAsync(newUser);

                if (isUserCreated.Succeeded)
                {
                    //assign role to user
                    var roleEnumStringValue = userDetail.Role.HasValue ? ((Core.Enums.Role)userDetail.Role) : ((Core.Enums.Role)Core.Enums.Role.Member);
                    await _userManager.AddToRoleAsync(newUser, roleEnumStringValue.ToString());
                   
                    //insert data in userdetails
                    var userDetailsToInsert = _mapper.Map<AddStaffDetailsResource, UserDetail>(userDetail);
                    userDetailsToInsert.UserId = newUser.Id;

                    //generate account number for user
                    userDetailsToInsert.AccountNumber = await GenerateUniqueAccountNumber();

                    //save smile identity image to blob for user profile
                    if (!string.IsNullOrEmpty(userDetailsToInsert.Image))
                    {
                        //var profileURL = await _storageService.UploadImageWithURL(userDetailsToInsert.Image, "user-profile-picture", newUser.Id);
                        //var profileURL = userDetailsToInsert.Image + "";
                        var profileURL = _storageService.UploadUserProfileImage(userDetailsToInsert.Image, newUser.Id);
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

                    if (userDetail.PermissionTypes != null)
                    {

                        // Initialize an empty list to store enum values as strings
                        List<string> permissionType = new List<string>();

                        foreach (var permission in userDetail.PermissionTypes)
                        {
                            // Convert the integer to the corresponding enum value
                            PermissionTypes enumPermission = (PermissionTypes)permission;

                            // Append the enum value as string to the list
                            permissionType.Add(enumPermission.ToString());
                        }

                        // Convert the list to an array if necessary
                        string[] permissionTypeArray = permissionType.ToArray();
                        var permissionTypes = JsonSerializer.Serialize(permissionTypeArray);
                        userDetailsToInsert.Permissions = permissionTypes;
                    }
                    
                    //save data
                    var updatedDetails = await _userDetailsService.AddOrUpdateUserDetails(userDetailsToInsert);
                    if (updatedDetails.UserId != null)
                    {
                        //kyc document is optional in registration
                        if (userDetail.DocumentTypeId.HasValue)
                        {
                            //insert data in kyc document
                            var kycDetailsToAdd = _mapper.Map<AddStaffDetailsResource, KYCdocuments>(userDetail);
                            kycDetailsToAdd.UserId = newUser.Id;
                            kycDetailsToAdd.CreatedDate = DateTime.Now;
                            kycDetailsToAdd.UpdatedDate = DateTime.Now;

                            await _kycDocumentsService.AddOrUpdateKYCDocument(kycDetailsToAdd);
                        }

                        UsersStaffRole usersStaffRole = new UsersStaffRole();
                        usersStaffRole.UserId = newUser.Id;
                        usersStaffRole.CreatedDate= DateTime.Now;
                        usersStaffRole.StaffRoleId = userDetail.StaffRoleId;
                        await _usersStaffRoleService.AddUserStaffRole(usersStaffRole);

                        //generate jwt token
                        var token = GenerateJSONWebToken(userDetailsToInsert);
                        CompleteSignUpResourceModel completeSignUpResourceModel = new CompleteSignUpResourceModel();
                        completeSignUpResourceModel.Email = userDetail.EmailId;
                        completeSignUpResourceModel.PhoneNumber = userDetail.PhoneNumber;
                        completeSignUpResourceModel.ClientURI = baseUrl+ "/account-setup";
                        var result = await GenerateLinkForComleteSignUp(completeSignUpResourceModel);
                        
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
                        return BadRequest(Messages.BadRequest.ToDescriptionString());
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

        [HttpPost("RegisterSystemAdmin")]
        public async Task<IActionResult> RegisterSystemAdmin([FromBody] AddSystemAdminResource userDetail)
        {
            try
            {
                var baseUrl = _configuration.GetSection("BaseUrl").Value;
                var validator = new AddSystemAdminDetailsValidator();
                var validationResult = await validator.ValidateAsync(userDetail);

                if (!validationResult.IsValid)
                    return BadRequest(validationResult.Errors);

                //check for super admin
                if (userDetail.Role == 0)
                    return BadRequest(Messages.IsSuperAdmin.ToDescriptionString());


                //check if phone number already exists
                var userExistWithPhone = await _userDetailsService.IsPhoneAlreadyRegistered(userDetail.PhoneNumber);

                //check if user exists with email
                ApplicationUser applicationUser = new ApplicationUser();
                if (!string.IsNullOrEmpty(userDetail.EmailId))
                    applicationUser = await _userManager.FindByEmailAsync(userDetail.EmailId);

                if ((applicationUser != null && !string.IsNullOrEmpty(applicationUser.Id)) || userExistWithPhone == true)
                {
                    if (applicationUser != null && !string.IsNullOrEmpty(applicationUser.Id) && userExistWithPhone == true)
                        return BadRequest(Messages.PhoneAndEmailAlreadyUsed.ToDescriptionString());

                    if (applicationUser != null && !string.IsNullOrEmpty(applicationUser.Id))
                        return BadRequest(Messages.EmailAlreadyUsed.ToDescriptionString());

                    if (userExistWithPhone == true)
                        return BadRequest(Messages.PhoneAlreadyUsed.ToDescriptionString());
                }

                var verificationDetails = await _verificationService.GetVerificationByPhone(userDetail.PhoneNumber);

                if (userDetail.PhoneNumber == verificationDetails.PhoneNumber && userDetail.Otp == verificationDetails.VerificationCode)
                {
                    //create new user with identity
                    var newUser = new ApplicationUser() { UserName = userDetail.PhoneNumber, Id = Guid.NewGuid().ToString() };
                    if (!string.IsNullOrEmpty(userDetail.EmailId))
                        newUser.Email = userDetail.EmailId;

                    var isUserCreated = await _userManager.CreateAsync(newUser);

                    if (isUserCreated.Succeeded)
                    {
                        //assign role to user
                        var roleEnumStringValue = userDetail.Role.HasValue ? ((Core.Enums.Role)userDetail.Role) : ((Core.Enums.Role)Core.Enums.Role.Member);
                        await _userManager.AddToRoleAsync(newUser, roleEnumStringValue.ToString());

                        //insert data in userdetails
                        var userDetailsToInsert = _mapper.Map<AddSystemAdminResource, UserDetail>(userDetail);
                        userDetailsToInsert.UserId = newUser.Id;

                        //generate account number for user
                        userDetailsToInsert.AccountNumber = await GenerateUniqueAccountNumber();

                        //save smile identity image to blob for user profile
                        if (!string.IsNullOrEmpty(userDetailsToInsert.Image))
                        {
                            var profileURL = _storageService.UploadUserProfileImage(userDetailsToInsert.Image, newUser.Id);
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

                        //if (roleEnumStringValue == Core.Enums.Role.SystemAdmin)
                        //{
                        //    await _walletService.CreateSystemAdminWallet(newUser.Id);
                        //}
                        
                        //save data
                        var updatedDetails = await _userDetailsService.AddOrUpdateUserDetails(userDetailsToInsert);

                        if (updatedDetails.UserId != null)
                        {
                            //kyc document is optional in registration
                            if (userDetail.DocumentTypeId.HasValue)
                            {
                                //insert data in kyc document
                                var kycDetailsToAdd = _mapper.Map<AddSystemAdminResource, KYCdocuments>(userDetail);
                                kycDetailsToAdd.UserId = newUser.Id;
                                kycDetailsToAdd.CreatedDate = DateTime.Now;
                                kycDetailsToAdd.UpdatedDate = DateTime.Now;
                                await _kycDocumentsService.AddOrUpdateKYCDocument(kycDetailsToAdd);
                            }

                            //add user address
                            if (userDetail.City != null)
                            {
                                Address address = new  Address();
                                address.UserId = updatedDetails.UserId;
                                address.City = userDetail.City;
                                address.District = userDetail.District;
                                await _addressService.AddOrUpdateAddress(address);
                            }

                            //generate jwt token
                            var token = GenerateJSONWebToken(userDetailsToInsert);

                            CompleteSignUpResourceModel completeSignUpResourceModel = new CompleteSignUpResourceModel();
                            completeSignUpResourceModel.Email = userDetail.EmailId;
                            completeSignUpResourceModel.PhoneNumber = userDetail.PhoneNumber;
                            completeSignUpResourceModel.ClientURI = baseUrl + "/system-admin-account-setup";
                            var result = await GenerateLinkForComleteSignUp(completeSignUpResourceModel);

                            //send registration sucess message on mobile number
                            _smsService.sendMessage(MessagesTemplates.RegistrationSuccessMessage(updatedDetails.AccountNumber, updatedDetails?.KYCLevelDetail?.Name), updatedDetails.PhoneNumber);

                            return Ok(new
                            {
                                Success = true,
                                UserId = userDetailsToInsert.UserId,
                                FirstName = updatedDetails.FirstName,
                                LastName = updatedDetails.LastName,
                                ProfilePic = updatedDetails.Image,
                                PhoneNumber = updatedDetails.PhoneNumber,
                                AccountNumber = updatedDetails.AccountNumber
                            });
                        }
                        else
                            return BadRequest(Messages.BadRequest.ToDescriptionString());
                    }
                    else
                        return BadRequest(isUserCreated.Errors.FirstOrDefault().Description);
                }
                return BadRequest(new 
                { 
                    Success = false,
                    Message = Messages.PhoneNumberNotVerified.ToDescriptionString()
                });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw ex.InnerException;
            }
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var validator = new LoginRequestValidator();
            var validationResult = await validator.ValidateAsync(loginRequest);
            if(validationResult.Errors.Count>0)
            {
                if (validationResult.Errors[0].ErrorMessage[3].ToString() == "m")
                {
                    validationResult.Errors[0].ErrorMessage = "Pin should be 4 characters long.";
                }
                //validationResult.Errors[0].ErrorMessage = "Pin should be 4 characters long.";
                validationResult.Errors[0].ErrorMessage = validationResult.Errors[0].ErrorMessage;
            }
            
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var user = await _userManager.FindByNameAsync(loginRequest.PhoneNo);
            if (user == null)
                return BadRequest("Account does not exists.");
            var result = await _signInManager.PasswordSignInAsync(user, loginRequest.Pin.ToString(), false, true);
            if (result.Succeeded)
            {
                //get user
                var userDetails = await _userDetailsService.GetUserDetailsByPhone(loginRequest.PhoneNo);
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
                //create wallet for register user by role

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
                    //ProfilePic = System.Convert.ToBase64String((System.IO.File.ReadAllBytes(userDetails.Image))),
                    ProfilePic = System.IO.File.Exists(userDetails.Image) ? System.Convert.ToBase64String((System.IO.File.ReadAllBytes(userDetails.Image))) : userDetails.Image,
                    //ProfilePic = userDetails.Image,
                    PhoneNumber = userDetails.PhoneNumber,
                    AccountNumber = userDetails.AccountNumber,
                    KYCLevel = userDetails.KYCLevelDetail.Name,
                    USDBalance = usdBalance,
                    lrdBalance = lrdBalance,
                    Role = userRole?[0]
                };
                //set logged in status to true if first log in
                string userId = userDetails.UserId;
                if (!userDetails.IsLoggedIn)
                    await _userDetailsService.UpdateUserLoggedInStatus(userId, true);

                return Ok(response);
            }
            return BadRequest("Invalid login request.");
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
        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPasswordForStaff([FromBody] ForgotPasswordResource forgotPasswordDto)
        {
            //get user detail by email
            var user = await _userManager.FindByEmailAsync(forgotPasswordDto.Email);
            if (user == null)
                return BadRequest(Messages.NotFound.ToDescriptionString());

            var userDetails = await _userDetailsService.GetUserDetailsById(user.Id);
            if (userDetails != null)
            {
                //check if users phonenumber, email, security answer and security question matches
                if (userDetails.PhoneNumber == forgotPasswordDto.PhoneNumber && user.Email == forgotPasswordDto.Email && userDetails.SecurityAnswerText.ToLower() == forgotPasswordDto.Answer.ToLower() && userDetails.SecurityQuestionId== forgotPasswordDto.SecurityQuestionId)
                {
                    //generated the token
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var secretKey = _configuration.GetSection("SecretKeyForEncryptAndDecrypt").Value; //secret key
                    string encryptedEmail = Common.Encrypt(forgotPasswordDto.Email, secretKey);
                    var EncodedEncryptedEmail = HttpUtility.UrlEncode(encryptedEmail);
                    var encodedToken = HttpUtility.UrlEncode(token);

                    // Created callback url
                    string callbackUrl = $"{forgotPasswordDto.ClientURI}?token={encodedToken}&email={(EncodedEncryptedEmail)}";

                    var username = userDetails.FirstName + " " + userDetails.LastName;

                    // Store token in db by userId
                    TokenStorage tokenStorage = new TokenStorage();
                    tokenStorage.UserId= user.Id;
                    tokenStorage.Token= encodedToken;
                    await _tokenStorageService.UpdateToken(tokenStorage);

                    EmailHtmlTemplates template = new EmailHtmlTemplates();

                    var htmlTemplate = template.EmailTemplate(forgotPasswordDto.Email, callbackUrl, username);

                    var subject = EmailSubjectEnums.ResetPassword.ToDescriptionString();
                    //send mail to user for forgot password link
                    await _emailSender.SendEmailAsync(forgotPasswordDto.Email, htmlTemplate, username, subject);

                    return Ok(new 
                    {
                        Success = true,
                        Message = Messages.LinkSentForResetPassword.ToDescriptionString()
                    });

                }
                else
                {
                    return Ok(new
                    {
                        Success=false,
                        Message=Messages.DetailsNotCorrect.ToDescriptionString()

                    });

                }
            }
            return BadRequest();
        }

        [AllowAnonymous]
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordResource resetPasswordDto)
        {
            if(string.IsNullOrEmpty(resetPasswordDto.Password) && string.IsNullOrEmpty(resetPasswordDto.ConfirmPassword))
            {
                return BadRequest();
            }
            var secretKey = _configuration.GetSection("SecretKeyForEncryptAndDecrypt").Value; //secret key
            string decryptedEmail = Common.Decrypt(resetPasswordDto.Email, secretKey); // helper function for decryption

            //get user by email
            var user = await _userManager.FindByEmailAsync(decryptedEmail);
            var tokenStorage = await _tokenStorageService.GetTokenStorageById(user.Id);

            var expiryDate = tokenStorage.ExpirationTime;
            DateTime currentDate = DateTime.UtcNow; // Use UtcNow to compare in UTC

            TimeSpan difference = (TimeSpan)(currentDate - expiryDate);
            int minutesDifference = (int)difference.TotalHours;

            // Check if the given date is expired (more than 2 minutes ago)
            bool isExpired = minutesDifference >= 2;

            if (user == null)
                return BadRequest(Messages.NotFound.ToDescriptionString());

            var isValidToken = await _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", resetPasswordDto.Token);

            var userDetails = await _userDetailsService.GetUserDetailsById(user.Id);
            if (isValidToken==true && !isExpired)
            {
                //generate password reset token
                var updatedDetail = await _userManager.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.ConfirmPassword.ToString());

                if (updatedDetail.Succeeded)
                {
                  
                    var userRole = await _userManager.GetRolesAsync(user);
                    var allAdmins = await _userDetailsService.GetUserByRole((int)Role.SuperAdmin);
                    var subject = EmailSubjectEnums.PasswordChanged.ToDescriptionString();

                    // send email to staff(user) 
                    PasswordChangeTemplateForStaff passwordChangeTemplateForStaff = new PasswordChangeTemplateForStaff
                    {
                        StaffEmail = decryptedEmail,
                        StaffFirstName = userDetails.FirstName,
                        StaffPhoneNumber = userDetails.PhoneNumber
                    };
                    string htmlTemplateForstaff = passwordChangeTemplateForStaff.GenerateHtml();
                    await _emailSender.SendEmailAsync(decryptedEmail, htmlTemplateForstaff, userDetails.FirstName, subject);

                    // send email to all admins 
                    PasswordChangeTemplateForSystemAdmin passwordChangeTemplateForSystemAdmin = new PasswordChangeTemplateForSystemAdmin
                    {
                        StaffEmail = decryptedEmail,
                        StaffFirstName = userDetails.FirstName,
                        StaffPhoneNumber = userDetails.PhoneNumber
                    };
                    string htmlTemplateForSystemAdmin = passwordChangeTemplateForSystemAdmin.GenerateHtml();
                    foreach (var admin in allAdmins)
                    {
                        await _emailSender.SendEmailAsync(admin.Email, htmlTemplateForSystemAdmin, admin.UserDetail.FirstName, subject);
                    }
                    return Ok(new
                    {
                        Success = true,
                        Message = Messages.PasswordUpdated.ToDescriptionString(),

                    });
                }
                else
                    return BadRequest(updatedDetail.Errors.FirstOrDefault());
            }
            else
            {
                return Unauthorized();
            }

        }

        [HttpPost("LoginSetup")]
        public async Task<IActionResult> SetupLoginPasswordForStaff([FromBody] SetupLoginResources setupLogin)
        {
            var secretKey = _configuration.GetSection("SecretKeyForEncryptAndDecrypt").Value; //secret key
            string decryptedEmail = Common.Decrypt(setupLogin.Email, secretKey); // helper function for decryption

            var user = await _userManager.FindByEmailAsync(decryptedEmail);
            
            //functionality for adding logs to the db
            SystemActivityLog systemActivityLog = new SystemActivityLog();
            systemActivityLog.UserId = user.Id;
            systemActivityLog.Email = decryptedEmail;
            systemActivityLog.IpAddress = GetClientIpAddress();
            systemActivityLog.Action = ActionsForSystemLogs.CompleteLoginProcess.ToDescriptionString();

            if (user == null) //check if user is null
            {
                systemActivityLog.Description = GenericMessagesForSystemLogs.NotExists.ToDescriptionString();
                await _systemActivityLogsService.AddSystemLogs(systemActivityLog);
                return Ok(new
                {
                    Success = false,
                    Message = Messages.NotFound.ToDescriptionString(),
                });
            }

            var userDetails = await _userDetailsService.GetUserDetailsById(user.Id);
            if (userDetails != null)
            {
                if (user.Email == decryptedEmail)
                {
                    //setup new password
                    var addPasswordResult = await _userManager.AddPasswordAsync(user, setupLogin.ConfirmPassword);

                    if (addPasswordResult.Succeeded)
                    {
                        userDetails.Status = (int)Status.Active;
                        userDetails.SecurityAnswerText = setupLogin.Answer;
                        userDetails.SecurityQuestionId = setupLogin.SecurityQuestionId;  
                       
                        //add user details
                        await _userDetailsService.AddOrUpdateUserDetails(userDetails);
                        var subject = EmailSubjectEnums.AccountSetupConfirmation.ToDescriptionString();

                        //send email to user/staff
                        AccountActivatedTemplateForStaff templateForstaff = new AccountActivatedTemplateForStaff
                        {
                            StaffEmail = decryptedEmail,
                            StaffFirstName = userDetails.FirstName,
                            StaffPhoneNumber = userDetails.PhoneNumber,
                        };

                        string htmlTemplateForStaff = templateForstaff.GenerateHtml();
                        await _emailSender.SendEmailAsync(decryptedEmail, htmlTemplateForStaff, userDetails.FirstName, subject);

                        //send email to all admins for staff's account activation
                        var allSystemAdmins = await _userDetailsService.GetUserByRole((int)Role.SystemAdmin);
                        var allSystemAdminsList = _mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<ApplicationUserResource>>(allSystemAdmins);
                       
                        AccountActivatedTemplateForSystemAdmin template = new AccountActivatedTemplateForSystemAdmin
                        {
                            StaffEmail = decryptedEmail,
                            StaffFirstName = userDetails.FirstName,
                            StaffPhoneNumber = userDetails.PhoneNumber,
                            StaffPermissions = userDetails.Permissions
                        };

                        string htmlTemplate = template.GenerateHtml();
                        foreach (var systemAdmin in allSystemAdminsList)
                        {
                            await _emailSender.SendEmailAsync(systemAdmin.Email,htmlTemplate, systemAdmin.UserDetail.FirstName, subject);
                        }

                        //add logs
                        systemActivityLog.Description = GenericMessagesForSystemLogs.CompleteLoginProcess.ToDescriptionString();
                        await _systemActivityLogsService.AddSystemLogs(systemActivityLog);
                        return Ok(new
                        {
                            Success = true,
                            Message = Messages.PasswordGenerated.ToDescriptionString()

                        });
                    }
                }
                else
                {
                    systemActivityLog.Description = GenericMessagesForSystemLogs.Failed.ToDescriptionString();
                    var addLogs = await _systemActivityLogsService.AddSystemLogs(systemActivityLog);
                    return Ok(new
                    {
                        Success = false,
                        Message = Messages.EmailNotValid.ToDescriptionString()

                    });

                }
            }
            return Ok(new{
                Success = false,
                Message = Messages.InvalidRequest.ToDescriptionString()
            });
        }

        [AllowAnonymous]
        [HttpPost("AccountAlreadySetup")]
        public async Task<IActionResult> CheckIfAccountAlreadySetup(AccountExistVerificationResource resource)
        {
            var secretKey = _configuration.GetSection("SecretKeyForEncryptAndDecrypt").Value; //secret key for decryption
            string decryptedEmail = Common.Decrypt(resource.Email, secretKey);
            var user = await _userManager.FindByEmailAsync(decryptedEmail); //Get user details by email
            if (user == null)
            {
                return Ok(new
                {
                    Success = false,
                    Message = Messages.NotFound.ToDescriptionString(),
                });
            }

            var isPasswordGenerated = await _userManager.HasPasswordAsync(user);

            if (!isPasswordGenerated)
            {
                return Ok(new
                {
                    Success = true,
                    Message = Messages.AccountNotAlreadySetup.ToDescriptionString(),
                });
                
            }
            else
            {
                return Ok(new
                {
                    Success = false,
                    Message = Messages.AccountAlreadySetup.ToDescriptionString(),
                });

            }

        }

        [AllowAnonymous]
        [HttpPost("CheckIfLinkIsExpired")]
        public async Task<IActionResult> CheckIfLinkIsExpired(CheckTokenExpirationResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var secretKey = _configuration.GetSection("SecretKeyForEncryptAndDecrypt").Value; //secret key
            string decryptedEmail = Common.Decrypt(resource.Email, secretKey); // helper function for decryption

            //get user by email
            var user = await _userManager.FindByEmailAsync(decryptedEmail);
            var tokenStorage = await _tokenStorageService.GetTokenStorageById(user.Id);

            var expiryDate = tokenStorage.ExpirationTime;
            DateTime currentDate = DateTime.UtcNow; // Use UtcNow to compare in UTC

            TimeSpan difference = (TimeSpan)(currentDate - expiryDate);
            int minutesDifference = (int)difference.TotalHours;

            // Check if the given date is expired (more than 2 minutes ago)
            bool isExpired = minutesDifference >= 2;

            if (user == null)
                return BadRequest(Messages.NotFound.ToDescriptionString());

            var isValidToken = await _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", resource.Token);

            //return response if link is not expired
            if(isValidToken == true && !isExpired)
            {

                return Ok(new
                {
                    Success = true,
                    Message = Messages.ResetPassword.ToDescriptionString(),
                });
            }
            else
            {
                return Ok(new
                {
                    Success = false,
                    Message = Messages.LinkExpired.ToDescriptionString(),
                });
            }

        }

        [HttpPost("SendOtpForSystemAdmin")]
        public async Task<IActionResult> SendOtpForSystemAdmin([FromBody] SendOTPForSystemAdmin model)
        {
            try
            {
                //get user details
                string verificationCode = Common.GenerateVerificationCode(4); // Generate a 4-digit verification code
                string message = "Your one time code is " + verificationCode;
                var code = _smsService.sendMessage(message, model.Phone);// service for send otp

                if (!string.IsNullOrEmpty(code))
                {
                    Verification verification = new Verification();
                    verification.PhoneNumber = model.Phone;
                    verification.VerificationCode = verificationCode;
                    verification.VerificationExpiry = DateTime.UtcNow;
                    
                    //update database
                    var updateDatabase = await _verificationService.AddVerification(verification);
                    return Ok(new
                    {
                        Success = true,
                        Message = Messages.OTPSent.ToDescriptionString()


                    });
                }
                return BadRequest(new 
                {
                    Success = false,
                    Message = code
                });

            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Success = false,
                    Message = ex.Message
                });
            }


        }

        [HttpPost("VerifyPhoneForSystemAdmin")]
        public async Task<IActionResult> VerifyPhoneForSystemAdmin([FromBody] SystemAdminVerificationModel model)
        {
            try
            {
                //get verification details
                var verificationDetails = await _verificationService.GetVerificationByPhone(model.PhoneNumber);

                //logic for getting time difference from expiry date and current date
                var expiryDate = verificationDetails.VerificationExpiry;
                DateTime currentDate = DateTime.UtcNow; // Use UtcNow to compare in UTC

                TimeSpan difference = (TimeSpan)(currentDate - expiryDate);
                int minutesDifference = (int)difference.TotalMinutes;

                // Check if the given date is expired (more than 2 minutes ago)
                bool isExpired = minutesDifference >= 1;

                if (!isExpired)
                {
                    if (verificationDetails.VerificationCode == model.Code)
                    {
                        return Ok(new
                        {
                            Success = true,
                            Message = Messages.VerificationSuccessfull.ToDescriptionString(),
                            IsExpired = false,

                        });
                    }
                    else
                    {
                        return Ok(new
                        {
                            Success = false,
                            Message = Messages.WrongOtp.ToDescriptionString(),
                            IsExpired = false,

                        });
                    }
                }
                else
                {
                    return Ok(new
                    {
                        Success = false,
                        IsExpired = true,
                        Message = Messages.OtpExpired.ToDescriptionString(),
                    });

                }

            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Message = ex.Message
                });
            }

        }

        [AllowAnonymous]
        [HttpPost("SetPin")]
        public async Task<IActionResult> SetPinForSystemAdmin([FromBody] SetPinResources setupLogin)
        {
            var secretKey = _configuration.GetSection("SecretKeyForEncryptAndDecrypt").Value; //secret key
            string decryptedEmail = Common.Decrypt(setupLogin.Email, secretKey); // helper function for decryption

            var user = await _userManager.FindByEmailAsync(decryptedEmail);

            //functionality for adding logs to the db
            SystemActivityLog systemActivityLog = new SystemActivityLog();
            systemActivityLog.UserId = user.Id;
            systemActivityLog.Email = decryptedEmail;
            systemActivityLog.IpAddress = GetClientIpAddress();
            systemActivityLog.Action = ActionsForSystemLogs.CompleteSignUpProcess.ToDescriptionString();

            if (user == null) //check if user is null
            {
                systemActivityLog.Description = GenericMessagesForSystemLogs.NotExists.ToDescriptionString();
                await _systemActivityLogsService.AddSystemLogs(systemActivityLog);
                return Ok(new
                {
                    Success = false,
                    Message = Messages.NotFound.ToDescriptionString(),
                });
            }

            var userDetails = await _userDetailsService.GetUserDetailsById(user.Id);
            if (userDetails != null)
            {
                if (user.Email == decryptedEmail)
                {
                    //setup new password
                    var addPasswordResult = await _userManager.AddPasswordAsync(user, setupLogin.ConfirmPin);

                    if (addPasswordResult.Succeeded)
                    {
                        userDetails.Status = (int)Status.Active;
                        
                        //add user details
                        await _userDetailsService.AddOrUpdateUserDetails(userDetails);

                        AccountCreationTemplateForSystemAdmin template = new AccountCreationTemplateForSystemAdmin
                        {
                            SystemAdminEmail = decryptedEmail,
                            SystemAdminFirstName = userDetails.FirstName,
                            SystemAdminPhoneNumber = userDetails.PhoneNumber
                        };

                        var subject = EmailSubjectEnums.AccountSetupConfirmation.ToDescriptionString();
                        string htmlTemplate = template.GenerateHtml();
                        await _emailSender.SendEmailAsync(decryptedEmail, htmlTemplate, userDetails.FirstName, subject);
                        //await _emailSender.EmailForAccountCreationToSystemAdmin(decryptedEmail, userDetails.FirstName, userDetails.PhoneNumber);

                        systemActivityLog.Description = GenericMessagesForSystemLogs.CompleteLoginProcess.ToDescriptionString();
                        var addLogs = await _systemActivityLogsService.AddSystemLogs(systemActivityLog);
                      
                        //return response
                        return Ok(new
                        {
                            Success = true,
                            Message = Messages.PasswordGenerated.ToDescriptionString()

                        });
                    }
                }
                else
                {
                    systemActivityLog.Description = GenericMessagesForSystemLogs.Failed.ToDescriptionString();
                    var addLogs = await _systemActivityLogsService.AddSystemLogs(systemActivityLog);
                    return Ok(new
                    {
                        Success = false,
                        Message = Messages.EmailNotValid.ToDescriptionString()

                    });
                }
            }
            return Ok(new
            {
                Success = false,
                Message = Messages.InvalidRequest.ToDescriptionString()
            });
        }

        [HttpPost("UpdatePermissions")]
        public async Task<IActionResult> UpdatePermissions([FromBody] StaffPermissionAndRoleResource staffPermissionAndRoleResource)
        {
            var user = await _userManager.FindByEmailAsync(staffPermissionAndRoleResource.EmailId);
            if (user == null)
            {
                return BadRequest();
            }
            var userDetail = await _userDetailsService.GetUserDetailsById(user.Id);
            if (userDetail == null)
            {
                return BadRequest();
            }

            if (staffPermissionAndRoleResource.Permissions != null)
            {

                // Initialize an empty list to store enum values as strings
                List<string> permissionType = new List<string>();

                foreach (var permission in staffPermissionAndRoleResource.Permissions)
                {
                    // Convert the integer to the corresponding enum value
                    PermissionTypes enumPermission = (PermissionTypes)permission;

                    // Append the enum value as string to the list
                    permissionType.Add(enumPermission.ToString());
                }

                // Convert the list to an array if necessary
                string[] perTypeArray = permissionType.ToArray();

                var permissionTypes = JsonSerializer.Serialize(perTypeArray);

                userDetail.Permissions = permissionTypes;
            }

            UsersStaffRole usersStaffRole = new UsersStaffRole();
            usersStaffRole.UserId = user.Id;
            usersStaffRole.CreatedDate = DateTime.Now;
            usersStaffRole.StaffRoleId = staffPermissionAndRoleResource.StaffRoleId;
            await _usersStaffRoleService.UpdateUserStaffRole(usersStaffRole);

            //add user details
            var result = await _userDetailsService.AddOrUpdateUserDetails(userDetail);
            return Ok(result);

        }

        [AllowAnonymous]
        [HttpPost("ForgotPinSystemAdmin")]
        public async Task<IActionResult> ForgotPinSystemAdmin([FromBody] ForgotPinResourceSystemAdmin forgotPinResourceSystemAdmin)
        {
            //get user detail by email
            var user = await _userManager.FindByEmailAsync(forgotPinResourceSystemAdmin.Email);
            if (user == null)
                return BadRequest(Messages.NotFound.ToDescriptionString());

            var userDetails = await _userDetailsService.GetUserDetailsById(user.Id);
            if (userDetails != null)
            {
                //check if users phonenumber, email, security answer and security question matches
                if (userDetails.PhoneNumber == forgotPinResourceSystemAdmin.PhoneNumber && user.Email == forgotPinResourceSystemAdmin.Email)
                {
                    //generated the token
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var baseUrl = _configuration.GetSection("BaseUrl").Value;
                    var secretKey = _configuration.GetSection("SecretKeyForEncryptAndDecrypt").Value; //secret key
                    string encryptedEmail = Common.Encrypt(forgotPinResourceSystemAdmin.Email, secretKey);
                    var EncodedEncryptedEmail = HttpUtility.UrlEncode(encryptedEmail);
                    var encodedToken = HttpUtility.UrlEncode(token);

                    var clientURI = baseUrl + "/admin-set-password";

                    // Created callback url
                    string callbackUrl = $"{clientURI}?token={encodedToken}&email={(EncodedEncryptedEmail)}";

                    var username = userDetails.FirstName + " " + userDetails.LastName;

                    // Store token in db by userId
                    TokenStorage tokenStorage = new TokenStorage();
                    tokenStorage.UserId = user.Id;
                    tokenStorage.Token = encodedToken;
                    await _tokenStorageService.UpdateToken(tokenStorage);

                    EmailHtmlTemplates template = new EmailHtmlTemplates();
                    var htmlTemplate = template.EmailTemplate(forgotPinResourceSystemAdmin.Email, callbackUrl, username);

                    var subject = EmailSubjectEnums.ResetPassword.ToDescriptionString();

                    //send mail to user for forgot password link
                    await _emailSender.SendEmailAsync(forgotPinResourceSystemAdmin.Email, htmlTemplate, username, subject);
                    
                    return Ok(new
                    {
                        Success = true,
                        Message = Messages.LinkSentForResetPassword.ToDescriptionString()
                    });

                }
                else
                {
                    return Ok(new
                    {
                        Success = false,
                        Message = Messages.DetailsNotCorrect.ToDescriptionString()

                    });

                }
            }
            return BadRequest();
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
                    var username = userDetails.FirstName + " " + userDetails.LastName;
                    EmailHtmlTemplates template = new EmailHtmlTemplates();

                    var subject = "Complete sign-up process";
                    var htmlTemplate = template.EmailTemplateForAccountSetup(completeSignUpResourceModel.Email, callbackUrl, username);

                    await _emailSender.SendEmailAsync(completeSignUpResourceModel.Email, htmlTemplate, username, subject);

                    return true;

                }

            }
            return false;
        }

        #endregion
    }
}
