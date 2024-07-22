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
using wleepay.API.Middleware;
using wleepay.Services;
using static wleepay.Communication.EmailTemplates.AccountDeactivatedTemplate;
using static wleepay.Communication.EmailTemplates.EmailLockOutTemplate;
using wleepay.Communication.Services;
using System.Reflection;
using static wleepay.Communication.EmailTemplates.AccountSuspendAndUnsuspendTemplate;

namespace wleepay.API.Areas.Portal.Controllers
{
    [Route("api/portal/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserDetailService _userDetailsService;
        private readonly IKYCDocumentsService _kycDocumentsService;
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
        private readonly ISystemActivityLogsService _systemActivityLogsService;
        private readonly IUsersStaffRoleService _usersStaffRoleService;
        private readonly IVerificationService _verificationService;
        private readonly ILookupService _lookupService;
        private readonly ISMSService _SMSService;

        public AccountController(UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            IUserDetailService userDetailsService,
            IWalletService walletService,
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
            ISystemActivityLogsService systemActivityLogsService,
            IUsersStaffRoleService usersStaffRoleService,
            IVerificationService verificationService,
            ILookupService lookupService,
            ISMSService sMSService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _userDetailsService = userDetailsService;
            _walletService = walletService;
            _addressService = addressService;
            _storageService = storageService;
            _logger = logger;
            _kycDocumentsService = kycDocumentsService;
            _smsService = smsService;
            _configuration = configuration;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
            _documentUploaderService = uploadDocument;
            _emailSender = emailSender;
            _systemActivityLogsService = systemActivityLogsService;
            _usersStaffRoleService = usersStaffRoleService;
            _verificationService = verificationService;
            _lookupService = lookupService;
            _SMSService = sMSService;
        }

        [SystemLock]
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
                            kycDetailsToAdd.DocumentImage = userDetailsToInsert.KYCDocumentImage;

                            await _kycDocumentsService.AddOrUpdateKYCDocument(kycDetailsToAdd);
                        }

                        UsersStaffRole usersStaffRole = new UsersStaffRole();
                        usersStaffRole.UserId = newUser.Id;
                        usersStaffRole.CreatedDate = DateTime.Now;
                        usersStaffRole.StaffRoleId = userDetail.StaffRoleId;
                        await _usersStaffRoleService.AddUserStaffRole(usersStaffRole);

                        //generate jwt token
                        var token = GenerateJSONWebToken(userDetailsToInsert, roleEnumStringValue.ToString());
                        CompleteSignUpResourceModel completeSignUpResourceModel = new CompleteSignUpResourceModel();
                        completeSignUpResourceModel.Email = userDetail.EmailId;
                        completeSignUpResourceModel.PhoneNumber = userDetail.PhoneNumber;
                        completeSignUpResourceModel.ClientURI = baseUrl + "/account-setup";
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
                throw new CustomException("Sorry, we're having trouble in registering user. Please contact support.");

            }
        }

        [SystemLock]
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

                if (verificationDetails == null)
                {
                    return BadRequest("please verify phone number first.");
                }

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
                                Address address = new Address();
                                address.UserId = updatedDetails.UserId;
                                address.City = userDetail.City;
                                address.District = userDetail.District;
                                await _addressService.AddOrUpdateAddress(address);
                            }

                            //generate jwt token
                            var token = GenerateJSONWebToken(userDetailsToInsert, roleEnumStringValue.ToString());

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
                throw new CustomException("Sorry, we're having trouble in registering user. Please contact support.");
            }
        }

        [SystemLock]
        [HttpPost("RegisterAgent")]
        public async Task<IActionResult> RegisterAgent([FromBody] AddAgentDetailResource userDetail)
        {
            try
            {
                var baseUrl = _configuration.GetSection("BaseUrl").Value;
                var validator = new AddAgentDetailsValidator();
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
                    var userDetailsToInsert = _mapper.Map<AddAgentDetailResource, UserDetail>(userDetail);
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

                    //save data
                    var updatedDetails = await _userDetailsService.AddOrUpdateUserDetails(userDetailsToInsert);
                    if (updatedDetails.UserId != null)
                    {
                        //kyc document is optional in registration
                        if (userDetail.DocumentTypeId.HasValue)
                        {
                            //insert data in kyc document
                            var kycDetailsToAdd = _mapper.Map<AddAgentDetailResource, KYCdocuments>(userDetail);
                            kycDetailsToAdd.UserId = newUser.Id;
                            kycDetailsToAdd.CreatedDate = DateTime.Now;
                            kycDetailsToAdd.UpdatedDate = DateTime.Now;
                            kycDetailsToAdd.DocumentImage = userDetailsToInsert.KYCDocumentImage;

                            await _kycDocumentsService.AddOrUpdateKYCDocument(kycDetailsToAdd);
                        }

                        //generate jwt token
                        var token = GenerateJSONWebToken(userDetailsToInsert, roleEnumStringValue.ToString());
                        CompleteSignUpResourceModel completeSignUpResourceModel = new CompleteSignUpResourceModel();
                        completeSignUpResourceModel.Email = userDetail.EmailId;
                        completeSignUpResourceModel.PhoneNumber = userDetail.PhoneNumber;
                        completeSignUpResourceModel.ClientURI = baseUrl + "/agent-account-setup";
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
                throw new CustomException("Sorry, we're having trouble in registering user. Please contact support.");

            }
        }

        [AllowAnonymous]
        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPasswordForStaff([FromBody] ForgotPasswordResource forgotPasswordDto)
        {
            try
            {
                //get user detail by email
                var user = await _userManager.FindByEmailAsync(forgotPasswordDto.Email);
                if (user == null)
                    return BadRequest(Messages.NotFound.ToDescriptionString());

                var userDetails = await _userDetailsService.GetUserDetailsById(user.Id);
                if (userDetails != null && !string.IsNullOrEmpty(userDetails.SecurityAnswerText))
                {
                    //check if users phonenumber, email, security answer and security question matches
                    if (userDetails.PhoneNumber == forgotPasswordDto.PhoneNumber && user.Email == forgotPasswordDto.Email && userDetails.SecurityAnswerText.ToLower() == forgotPasswordDto.Answer.ToLower() && userDetails.SecurityQuestionId == forgotPasswordDto.SecurityQuestionId)
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
                            Success = false,
                            Message = Messages.DetailsNotCorrect.ToDescriptionString()

                        });

                    }
                }
                return BadRequest(new
                {
                    Success = false,
                    Message = Messages.AccountNotSetup.ToDescriptionString()

                });
            }
            catch (Exception)
            {

                throw new CustomException("Sorry, we're having trouble sending password reset link. Please contact support.");
            }
        }

        [AllowAnonymous]
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordResource resetPasswordDto)
        {
            try
            {
                if (string.IsNullOrEmpty(resetPasswordDto.Password) && string.IsNullOrEmpty(resetPasswordDto.ConfirmPassword))
                {
                    return BadRequest();
                }
                var secretKey = _configuration.GetSection("SecretKeyForEncryptAndDecrypt").Value; //secret key
                string decryptedEmail = Common.Decrypt(resetPasswordDto.Email, secretKey); // helper function for decryption

                //get user by email
                var user = await _userManager.FindByEmailAsync(decryptedEmail);
                if (user == null)
                    return BadRequest(Messages.NotFound.ToDescriptionString());

                var isValidToken = await _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", resetPasswordDto.Token);

                var userDetails = await _userDetailsService.GetUserDetailsById(user.Id);
                if (isValidToken == true)
                {
                    //generate password reset token
                    var updatedDetail = await _userManager.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.ConfirmPassword.ToString());

                    if (updatedDetail.Succeeded)
                    {

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
                    return Unauthorized(new
                    {
                        Success = false,
                        Message = Messages.LinkExpired.ToDescriptionString(),

                    });
                }
            }
            catch (Exception ex)
            {

                throw new CustomException("Sorry, we're having trouble reset your password. Please contact support.");
            }

        }

        [HttpPost("LoginSetup")]
        public async Task<IActionResult> SetupLoginPasswordForStaff([FromBody] SetupLoginResources setupLogin)
        {
            try
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
                        AccountActivatedTemplateForUser templateForstaff = new AccountActivatedTemplateForUser
                        {
                            Email = decryptedEmail,
                            FirstName = userDetails.FirstName,
                            PhoneNumber = userDetails.PhoneNumber,
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
                            await _emailSender.SendEmailAsync(systemAdmin.Email, htmlTemplate, systemAdmin.UserDetail.FirstName, subject);
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
            return Ok(new
            {
                Success = false,
                Message = Messages.InvalidRequest.ToDescriptionString()
            });
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
        }

        [AllowAnonymous]
        [HttpPost("AccountAlreadySetup")]
        public async Task<IActionResult> CheckIfAccountAlreadySetup(AccountExistVerificationResource resource)
        {
            try
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
            catch (Exception ex)
            {

                throw ex.InnerException;
            }

        }

        [AllowAnonymous]
        [HttpPost("CheckIfLinkIsExpired")]
        public async Task<IActionResult> CheckIfLinkIsExpired(CheckTokenExpirationResource resource)
        {
            try
            {

            if (!ModelState.IsValid)
                return BadRequest();

            var secretKey = _configuration.GetSection("SecretKeyForEncryptAndDecrypt").Value; //secret key
            string decryptedEmail = Common.Decrypt(resource.Email, secretKey); // helper function for decryption

            //get user by email
            var user = await _userManager.FindByEmailAsync(decryptedEmail);

            if (user == null)
                return BadRequest(Messages.NotFound.ToDescriptionString());

            var isValidToken = await _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", resource.Token);

            //return response if link is not expired
            if (isValidToken == true)
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
            catch (Exception ex)
            {

                throw ex.InnerException;
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
                var validator = new VerifyPhoneNumberValidator();
                var validationResult = await validator.ValidateAsync(model);

                if (!validationResult.IsValid)
                    return BadRequest(validationResult.Errors);

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
        [HttpPost("ForgotPinSystemAdmin")]
        public async Task<IActionResult> ForgotPinSystemAdmin([FromBody] ForgotPinResourceSystemAdmin forgotPinResourceSystemAdmin)
        {
            try
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
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
        }

        [AllowAnonymous]
        [HttpPost("SetPin")]
        public async Task<IActionResult> SetPin([FromBody] SetPinResources setupLogin)
        {
            try
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

                        AccountCreationTemplate template = new AccountCreationTemplate
                        {
                            Email = decryptedEmail,
                            FirstName = userDetails.FirstName,
                            PhoneNumber = userDetails.PhoneNumber
                        };

                        var subject = EmailSubjectEnums.AccountSetupConfirmation.ToDescriptionString();
                        string htmlTemplate = template.GenerateHtml();
                        await _emailSender.SendEmailAsync(decryptedEmail, htmlTemplate, userDetails.FirstName, subject);

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
            catch (Exception ex)
            {

                throw new CustomException("Sorry, we're having trouble in set password. Please contact support.");
            }
        }

        [SystemLock]
        [HttpPost("UpdatePermissions")]
        public async Task<IActionResult> UpdatePermissions([FromBody] StaffPermissionAndRoleResource staffPermissionAndRoleResource)
        {
            try
            {

            var user = await _userManager.FindByEmailAsync(staffPermissionAndRoleResource.EmailId);
            if (user == null)
            {
                return BadRequest(Messages.NotFound.ToDescriptionString());
            }
            var userDetail = await _userDetailsService.GetUserDetailsById(user.Id);
            if (userDetail == null)
            {
                return BadRequest(Messages.NotFound.ToDescriptionString());
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
            catch (Exception ex)
            {

                throw new CustomException("Sorry, we're having trouble in updating permissions. Please contact support.");
            }

        }

        [HttpGet("StaffRoles")]
        public async Task<IActionResult> GetStaffRoles()
        {
            try
            {
                var staffRoles = await _lookupService.GetAllStaffRoles();
                return Ok(staffRoles);
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
        }

        [HttpGet("StaffPermissions")]
        public async Task<IActionResult> GetStaffPermissions()
        {
            try
            {
                var staffRoles = await _lookupService.GetAllStaffPermissions();
                return Ok(staffRoles);
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
        }

        [AllowAnonymous]
        [HttpGet("SecurityQuestion")]
        public async Task<IActionResult> GetSecurityQuestions()
        {
            try
            {
                var questions = await _lookupService.GetAllSecurityQuestions();
                return Ok(questions);
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] PortalLoginRequest loginRequest)
        {
            try
            {
            var validator = new PortalLoginRequestValidator();
            var validationResult = await validator.ValidateAsync(loginRequest);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var user = await _userManager.FindByEmailAsync(loginRequest.Email);
            if (user == null)
                return BadRequest(Messages.EmailNotFound.ToDescriptionString());

            var userDetails = await _userDetailsService.GetUserDetailsById(user.Id);
            var result = await _signInManager.PasswordSignInAsync(user, loginRequest.Password, false, lockoutOnFailure: true);

            //Get user role
            var userRole = await _userManager.GetRolesAsync(user);
            if (!loginRequest.LoginFor.Contains(userRole[0]))
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
                     updatedUser = await _userDetailsService.UpdateUserLoggedInStatus(userDetails.UserId, true,0);
                    
                await _systemActivityLogsService.AddSystemLogs(systemActivityLog);

                return Ok(new
                {
                    UserId = user.Id,
                    Success = true,
                    Token = token,
                    IsLoggedInVerified = updatedUser != null ? updatedUser.IsLoggedIn : userDetails.IsLoggedIn,
                    Role = userRole?[0],
                });

            }

            return BadRequest(new { Message = Messages.BadRequest.ToDescriptionString() });

            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
      
        [HttpPost("LockAccount")]
        public async Task<IActionResult> LockAccount([FromBody] LockAccount model)
        {
            try
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
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
        }

        [AllowAnonymous]
        [HttpPost("UnLockAccount")]
        public async Task<IActionResult> UnlockAccount([FromBody] LockAccount model)
        {
            try
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
            catch (Exception ex)
            {

                throw ex;
            }
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
                return BadRequest($"Sorry, we're having trouble sending OTP. Please contact support.");

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [AllowAnonymous]
        [HttpPost("VerifyCode")]
        public async Task<IActionResult> VerifyCode([FromBody] VerificationModel model) 
        {
            try
            {
                //get user details
                var userDetails = await _userDetailsService.GetUserDetailsById(model.UserId);

                var userResource = _mapper.Map<UserDetail, UserDetailResource>(userDetails);


                var user = await _userManager.FindByEmailAsync(model.Email);

                //logic for getting time difference from expiry date and current date
                var expiryDate = userResource.VerificationExpiry;
                DateTime currentDate = DateTime.UtcNow; // Use UtcNow to compare in UTC

                TimeSpan difference = (TimeSpan)(currentDate - expiryDate);
                int minutesDifference = (int)difference.TotalMinutes;

                // Check if the given date is expired (more than 2 minutes ago)
                bool isExpired = minutesDifference >= 1;

                object Data = null;
                if (!isExpired)
                {
                    if (userResource.VerificationCode == model.Code)
                    {
                        var userRole = await _userManager.GetRolesAsync(user);

                        var token = GenerateJSONWebToken(userDetails, userRole?[0].ToString());

                        //updated user
                        UserDetail updatedUser = null;

                        //set logged in status to true if first log in
                        if (!userDetails.IsLoggedIn)
                            updatedUser = await _userDetailsService.UpdateUserLoggedInStatus(userDetails.UserId, true,0);

                        var ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();
                        _logger.LogInformation($"Action: {ControllerContext.ActionDescriptor.ActionName}, User: {userDetails.UserId}, Timestamp: {DateTime.UtcNow}, IP: {ipAddress}");

                        if (!string.IsNullOrEmpty(userResource?.Permissions))
                        {
                            string[] deserializedNumbers = JsonSerializer.Deserialize<string[]>(userResource?.Permissions);

                            string jsonString = JsonSerializer.Serialize(deserializedNumbers);

                            var secretKey = _configuration.GetSection("SecretKeyForEncryptAndDecrypt").Value; //secret key

                            userResource.Permissions = Common.Encrypt(jsonString, secretKey).ToString();
                        }

                        SystemActivityLog systemActivityLog = new SystemActivityLog();
                        systemActivityLog.UserId = user.Id;
                        systemActivityLog.Email = model.Email;
                        systemActivityLog.IpAddress = GetClientIpAddress();
                        systemActivityLog.Action = ActionsForSystemLogs.Login.ToDescriptionString();
                        systemActivityLog.Description = GenericMessagesForSystemLogs.SuccessfullyLoggedIn.ToDescriptionString();

                        await _systemActivityLogsService.AddSystemLogs(systemActivityLog);

                        return Ok(new
                        {
                            Data = (new
                            {
                                UserId = userDetails.UserId,
                                Token = token,
                                IsLoggedInVerified = updatedUser != null ? updatedUser.IsLoggedIn : userDetails.IsLoggedIn,
                                Role = userRole?[0]

                            }),
                            Session = userResource.Permissions,
                            IsExpired = false,
                            Success = true,
                            Message = Messages.VerificationSuccessfull.ToDescriptionString()

                        });
                    }
                    else
                    {
                        return Ok(new
                        {
                            Data,
                            IsExpired = false,
                            Success = true,
                            Message = Messages.WrongOtp.ToDescriptionString(),

                        });
                    }
                }
                else
                {
                    return Ok(new
                    {
                        Data,
                        IsExpired = true,
                        Success = false,
                        Message = Messages.OtpExpired.ToDescriptionString(),

                    });

                }

            }
            catch (Exception ex)
            {
                throw new CustomException("Sorry, we're having trouble verifying your account. Please contact support.");
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
                        AccountActivatedTemplateForUser templateForstaff = new AccountActivatedTemplateForUser
                        {
                            Email = model.EmailId,
                            FirstName = userDetails.FirstName,
                            PhoneNumber = userDetails.PhoneNumber,

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
                    else if (model.Status == (int)Status.Suspend)
                    {
                        systemActivityLog.Action = ActionsForSystemLogs.Suspend.ToDescriptionString();
                        systemActivityLog.Description = GenericMessagesForSystemLogs.AccountSuspended.ToDescriptionString();

                        var subject = EmailSubjectEnums.AccountSuspensionConfirmation.ToDescriptionString();

                        //send email to staff for activation
                        AccountSuspendedTemplateForUser templateForUser = new AccountSuspendedTemplateForUser
                        {
                            Email = model.EmailId,
                            FirstName = userDetails.FirstName,
                            PhoneNumber = userDetails.PhoneNumber,

                        };

                        string htmlTemplateForUser = templateForUser.GenerateHtml();
                        await _emailSender.SendEmailAsync(model.EmailId, htmlTemplateForUser, userDetails.FirstName, subject);
                    }
                    else if(model.Status == (int)Status.UnSuspend)
                    {
                        systemActivityLog.Action = ActionsForSystemLogs.UnSuspend.ToDescriptionString();
                        systemActivityLog.Description = GenericMessagesForSystemLogs.AccountUnsuspended.ToDescriptionString();

                        var subject = EmailSubjectEnums.AccountSuspensionConfirmation.ToDescriptionString();

                        //send email to staff for activation
                        AccountUnsuspendedTemplateForUser templateForUser = new AccountUnsuspendedTemplateForUser
                        {
                            FirstName = userDetails.FirstName,
                            PhoneNumber = userDetails.PhoneNumber,
                            Email = model.EmailId,

                        };

                        string htmlTemplateForUser = templateForUser.GenerateHtml();
                        await _emailSender.SendEmailAsync(model.EmailId, htmlTemplateForUser, userDetails.FirstName, subject);
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

        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(RefreshTokenModel tokenModel)
        {
            try
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
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
        }

        [HttpGet("systemActivity")]
        public async Task<IActionResult> GetSystemAllLogs([FromQuery] Pagination request)
        {
            try
            {
                if (!string.IsNullOrEmpty(request.Email))
                {
                    var logs = await _systemActivityLogsService.FilterLogsByEmail(request.Email, request.Index, request.Size);
                    return Ok(logs);
                }
                else
                {
                    var logs = await _systemActivityLogsService.GetAllSystemLogs(request.Index, request.Size);
                    return Ok(logs);
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

        }

        [HttpGet("systemActivity/{userId}")]
        public async Task<IActionResult> GetSystemLogsById(string userId, [FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 10)
        {
            try
            {
                var logs = await _systemActivityLogsService.GetAllSystemById(userId, pageIndex, pageSize);
                return Ok(logs);

            }
            catch (System.Exception ex)
            {

                throw ex.InnerException;
            }

        }

        [SystemLock]
        [HttpDelete("systemActivity/{id}")]
        public async Task<IActionResult> DeleteLogsById(int id)
        {
            try
            {
                var result = await _systemActivityLogsService.DeleteLogsById(id);
                return Ok(result);
            }
            catch (System.Exception ex)
            {

                throw new CustomException("Sorry, we're having trouble deleting logs. Please contact support.");
            }

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
