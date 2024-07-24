using AutoMapper;
using DevExpress.Xpo.Logger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.SqlServer.Management.Smo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using wleepay.API.Helpers;
using wleepay.API.Middleware;
using wleepay.API.Resources;
using wleepay.API.Validators;
using wleepay.Communication.Interface;
using wleepay.Core.Common;
using wleepay.Core.DTO.Request;
using wleepay.Core.DTO.Response;
using wleepay.Core.Enums;
using wleepay.Core.Models;
using wleepay.Core.Services;
using wleepay.Storage.Interface;
using wleepay.Storage.Services;

namespace wleepay.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class UserController : Controller
    {

        private readonly IUserDetailService _userDetailsService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUploadDocument _documentUploaderService;
        private readonly IKYCDocumentsService _kycDocumentsService;
        private readonly IAddressService _addressService;
        private readonly ISmileIdentityJobService _smileIdentityJobService;
        private readonly IMapper _mapper;
        private readonly ILogger<UserController> _logger;
        private readonly IUsersStaffRoleService _usersStaffRoleService;
        private readonly IUploadDocument _storageService;
        private readonly IUserDeviceService _userDeviceService;
        private readonly ISMSService _sMSService;
        private readonly IExternalApiService _externalApiService;
        public UserController(UserManager<ApplicationUser> userManager,
            IUserDetailService userDetailsService,
            IUploadDocument uploadDocument,
            IAddressService addressService,
            IKYCDocumentsService kycDocumentsService,
            ISmileIdentityJobService smileIdentityJobService,
            IMapper mapper,
            ILogger<UserController> logger, 
            IUsersStaffRoleService usersStaffRoleService,
            IUploadDocument storageService,
            IUserDeviceService userDeviceService, ISMSService sMSService, IExternalApiService externalApiService)

        {
            _userManager = userManager;
            _userDetailsService = userDetailsService;
            _documentUploaderService = uploadDocument;
            _addressService = addressService;
            _kycDocumentsService = kycDocumentsService;
            _smileIdentityJobService = smileIdentityJobService;
            _mapper = mapper;
            _logger = logger;
            _usersStaffRoleService = usersStaffRoleService;
            _storageService = storageService;
            _userDeviceService = userDeviceService;
            _sMSService = sMSService;
            _externalApiService = externalApiService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetById(string userId)
        {
            try
            {
                var userDetail = await _userDetailsService.GetUserDetailsById(userId);
                var userResource = _mapper.Map<UserDetail, UserDetailResource>(userDetail);
                return Ok(userResource);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        [AllowAnonymous]
        [HttpGet("{userId}/extraInfo")]
        public async Task<IActionResult> GetApplicationUserById(string userId)
        {
            try
            {
            var userDetail = await _userDetailsService.GetApplicationUserById(userId);
                if (System.IO.File.Exists(userDetail?.UserDetail?.Image))
                {
                    userDetail.UserDetail.Image = System.Convert.ToBase64String((System.IO.File.ReadAllBytes(userDetail.UserDetail.Image)));
                }
                if (System.IO.File.Exists(userDetail?.UserDetail?.KYCDocumentImage))
                {
                    userDetail.UserDetail.KYCDocumentImage = System.Convert.ToBase64String((System.IO.File.ReadAllBytes(userDetail.UserDetail.KYCDocumentImage)));
                }
                if (userDetail?.KYCdocuments.Count > 0) {
                    if (System.IO.File.Exists(userDetail?.KYCdocuments?.FirstOrDefault().DocumentImage))
                    {
                        userDetail.KYCdocuments.FirstOrDefault().DocumentImage = System.Convert.ToBase64String((System.IO.File.ReadAllBytes(userDetail.KYCdocuments.FirstOrDefault().DocumentImage)));
                    }
                }

                var userResource = _mapper.Map<ApplicationUser, ApplicationUserResource>(userDetail);
            return Ok(userResource);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        [HttpPost("search")]
        public async Task<IActionResult> SearchUser([FromBody] SearchRequest request)
        {
            ApplicationUser userDetail = null;
            if (!string.IsNullOrEmpty(request.PhoneNumber))
                userDetail = await _userDetailsService.GetUserAccountByPhone(request.PhoneNumber, request.Role.GetHashCode());
            else if (!string.IsNullOrEmpty(request.LoginName))
                userDetail = await _userDetailsService.GetUserAccountByName(request.LoginName);

            var userDetailResource = _mapper.Map<ApplicationUser, ApplicationUserResource>(userDetail);
            return Ok(userDetailResource != null ? userDetailResource : string.Empty);
        }

        [HttpGet("nearByAgent")]
        public async Task<IActionResult> GetNearByAgent()
        {
            var agentList = await _userDetailsService.GetUserByRole(Core.Enums.Role.Agent.GetHashCode());
            var agentListResource = _mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<UserAccountResource>>(agentList);

            var result = agentListResource.Where(agent => agent.UserDetail.Status == 1).ToArray();
            return Ok(result);
        }

        [HttpPost("all")]
        public async Task<IActionResult> GetUserList([FromBody] PagingRequest request)
        {
            if (!string.IsNullOrEmpty(request.SearchText))
            {
                var result = await _userDetailsService.SearchUsersByEmail(request.SearchText, request.Role, request.Index, request.Size);
                var userDetailResource = _mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<ApplicationUserResource>>(result.UserDetails);
                return Ok(new
                {
                    TotalCount = result.TotalCount,
                    UserDetails = userDetailResource
                });
            }
            else
            {
                var userList = await _userDetailsService.GetUsers(request.Role, request.Index, request.Size);
                var userListResource = _mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<ApplicationUserResource>>(userList.UserDetails);
                return Ok(new
                {
                    userDetails = userListResource,
                    TotalCount = userList.TotalCount
                });
            }
        }

        [HttpPut("phoneNumber")]
        public async Task<IActionResult> UpdatePhoneNumber([FromBody] UpdateContactResource requestResource)
        {

            var validator = new UpdateContactValidator();
            var validationResult = await validator.ValidateAsync(requestResource);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            //update user details
            var updatedDetail = await _userDetailsService.UpdateUserPhoneNumber(requestResource.UserId, requestResource.PhoneNumber);
            if (updatedDetail == null)
                return BadRequest("User does not exist");

            //update userName for login
            var applicationUserToUpdate = await _userManager.FindByIdAsync(requestResource.UserId);
            applicationUserToUpdate.UserName = requestResource.PhoneNumber;

            var result = await _userManager.UpdateAsync(applicationUserToUpdate);

            if (result.Succeeded)
                return Ok("Phone Number Updated");
            else
                return BadRequest(result.Errors.FirstOrDefault());
        }

        [HttpPut("emailAddress")]
        public async Task<IActionResult> UpdateUserEmail([FromBody] UpdateContactResource requestResource)
        {
            var validator = new UpdateContactValidator(true);
            var validationResult = await validator.ValidateAsync(requestResource);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            //check if user exists
            var applicationUserToUpdate = await _userManager.FindByIdAsync(requestResource.UserId);
            if (applicationUserToUpdate == null)
                return BadRequest("User don't exists");

            //check if new email already exists in system
            var applicationUser = await _userManager.FindByEmailAsync(requestResource.EmailId);
            if (applicationUser != null)
                return BadRequest("Email is already registered.");

            //update email id for user
            applicationUserToUpdate.Email = requestResource.EmailId;
            applicationUserToUpdate.NormalizedEmail = requestResource.EmailId;

            var result = await _userManager.UpdateAsync(applicationUserToUpdate);
            if (result.Succeeded)
                return Ok("Email Id Updated");
            else
                return BadRequest(result.Errors.FirstOrDefault());
        }

        [HttpPost("profileImage")]
        public async Task<IActionResult> UpdateUserProfileImage(IFormFile file, string userId)
        {
            try
            {
                if (file == null)
                    return BadRequest("No file to upload.");
                var userDetailToUpdate = await _userDetailsService.GetUserDetailsById(userId);
                if (userDetailToUpdate == null)
                    return BadRequest("No user found");

                //upload profile pic and get url
                var imageUrl = await uploadDocument(file, userId, "user-profile-picture");
                //update profile url
                if (!string.IsNullOrEmpty(imageUrl))
                    userDetailToUpdate.Image = imageUrl.ToString();
                else
                    return BadRequest("Can't upload image");

                var updatedUserDetail = await _userDetailsService.AddOrUpdateUserDetails(userDetailToUpdate);
                var updatedUserDetailResource = _mapper.Map<UserDetail, UserDetailResource>(updatedUserDetail);
                updatedUserDetailResource.Image = System.Convert.ToBase64String((System.IO.File.ReadAllBytes(updatedUserDetailResource.Image)));
                return Ok(updatedUserDetailResource);
            }
            catch(Exception EX)
            {
                _logger.LogError(EX.ToString());
                return BadRequest(EX.ToString());
            }
            
        }

        [AllowAnonymous]
        [HttpPost("DocumentKYCImage")]
        public async Task<IActionResult> DocumentKYCImage(documentKycImage documentKycImage)
        {
            try
            {
                if (documentKycImage.image == null)
                    return BadRequest("No file to upload.");
                var userDetailToUpdate = await _userDetailsService.GetUserDetailsById(documentKycImage.userId);
                if (userDetailToUpdate == null)
                    return BadRequest("No user found");

               if (!string.IsNullOrEmpty(documentKycImage.image))
                {
                    var documentURL = _documentUploaderService.UploadKYCDocument(documentKycImage.image, documentKycImage.userId);
                    userDetailToUpdate.KYCDocumentImage = documentURL.ToString();
                }

                if (userDetailToUpdate.KYCLevel == Core.Enums.KYCLevel.L1.GetHashCode())
                {
                    //update kyc level from l1 to l2 for new document upload
                    userDetailToUpdate.KYCLevel = Core.Enums.KYCLevel.L2.GetHashCode();
                    await _userDetailsService.AddOrUpdateUserDetails(userDetailToUpdate);
                }


                var updatedUserDetail = await _userDetailsService.AddOrUpdateUserDetails(userDetailToUpdate);
                var updatedUserDetailResource = _mapper.Map<UserDetail, UserDetailResource>(updatedUserDetail);
                updatedUserDetailResource.KYCLevelDetail = _mapper.Map<Core.Models.KYCLevel, Resources.KYCLevel>(userDetailToUpdate.KYCLevelDetail);
                return Ok(updatedUserDetailResource);
            }
            catch (Exception EX)
            {
                _logger.LogError(EX.ToString());
                return BadRequest(EX.ToString());
            }

        }

        [HttpPut("{userId}/KYCStatus/{statusId}")]
        public async Task<IActionResult> UpdateUserKYCStatus(string userId, int statusId)
        {
            //update user details
            var updatedDetail = await _userDetailsService.UpdateUserKYCStatus(userId, statusId);
            if (updatedDetail == null)
                return BadRequest("User does not exist");
            else
                return Ok("KYC Status Updated");
        }

        [HttpPost("documentImage")]
        public async Task<IActionResult> UpdateUserDocumentImage(IFormFile file, string userId)
        {
            if (file == null)
                return BadRequest("No file to upload.");

            var userDetail = await _userDetailsService.GetUserDetailsById(userId);
            if (userDetail == null)
                return BadRequest("No user found");

            //upload profile pic and get url
            var imageUrl = await uploadDocument(file, userId);

            if (string.IsNullOrEmpty(imageUrl))
                return BadRequest("Can't upload image");

            //update document url
            var kycDocumentToUpdate = await _kycDocumentsService.GetKYCDocumentByUserId(userId);
            if (kycDocumentToUpdate == null)
            {
                kycDocumentToUpdate = new KYCdocuments()
                {
                    DocumentImage = imageUrl.ToString(),
                    UserId = userId,
                    Type = 1,
                    CreatedDate = DateTime.Now,
                    DocumentNumber = string.Empty,
                    ExtractInfo = string.Empty,
                    UpdatedDate = DateTime.Now
                };
            }
            else
            {
                kycDocumentToUpdate.DocumentImage = imageUrl.ToString();
                kycDocumentToUpdate.UpdatedDate = DateTime.Now;
            }

            //if kyc level is l1 then update to l2 on document upload
            //if (userDetail.KYCLevel == Core.Enums.KYCLevel.L1.GetHashCode())
            //{
            //    //update kyc level from l1 to l2 for new document upload
            //    userDetail.KYCLevel = Core.Enums.KYCLevel.L2.GetHashCode();
            //    await _userDetailsService.AddOrUpdateUserDetails(userDetail);
            //}

            var updatedKYCDocument = await _kycDocumentsService.AddOrUpdateKYCDocument(kycDocumentToUpdate);
            var updatedKYCDocumentResource = _mapper.Map<KYCdocuments, KYCDocumentResource>(updatedKYCDocument);
            //send kyc level if updated
            //updatedKYCDocumentResource.KYCLevelDetail = _mapper.Map<Core.Models.KYCLevel, Resources.KYCLevel>(userDetail.KYCLevelDetail);
            return Ok(updatedKYCDocumentResource);
        }

        [HttpPut("address")]
        public async Task<IActionResult> UpdateUserAddress([FromBody] AddressResource addressModel)
        {
            if (addressModel == null || string.IsNullOrEmpty(addressModel.UserId))
                return BadRequest("User Id not found");

            var user = await _userDetailsService.GetUserDetailsById(addressModel.UserId);
            if (user == null)
                return BadRequest("User not found");

            var addressToUpdate = _mapper.Map<AddressResource, Address>(addressModel);

            var address = await _addressService.AddOrUpdateAddress(addressToUpdate);

            var addressResource = _mapper.Map<Address, AddressResource>(address);

            return Ok(addressResource);
        }

        [HttpPost("job")]
        public async Task<IActionResult> SaveIdentityJob([FromBody] SmileIdentityJobResource job)
        {
            var jobToAdd = _mapper.Map<SmileIdentityJobResource, SmileIdentityJob>(job);
            var newJob = await _smileIdentityJobService.AddJob(jobToAdd);
            return Ok(newJob);
        }

        [HttpPost("paymentRequest")]
        public async Task<IActionResult> GetIncomingOutGoingTopUpRequests([FromBody] PaymentRequest paymentRequestParam)
        {
            var paymentRequest = await _userDetailsService.GetIncomingOutGoingPaymentRequest(paymentRequestParam.UserId, paymentRequestParam.Type,paymentRequestParam.TransferStatus,paymentRequestParam.PageSize,paymentRequestParam.PageIndex);

          if (paymentRequest == null)
                return Ok(new List<TransactionResource>());
           
            var transactionResource = _mapper.Map<IEnumerable<Transaction>, IEnumerable<TransactionHistoryResource>>(paymentRequest);
          foreach (var item in transactionResource)
          {
             var time = Helpers.Common.ConvertISTtoLiberiaTime(item.CreatedDate);
             item.CreatedDate = time;
          }
             return Ok(transactionResource);
        }

        [HttpGet("{userId}/createdBy")]
        public async Task<IActionResult> GetAllUsersById(string userId, int roleId , int pageIndex, int pageSize)
        {
            Role role = (Role)roleId;
            var userList = await _userDetailsService.GetAllUsersById(role, pageIndex, pageSize, userId);
            var userListResource = _mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<ApplicationUserResource>>(userList.UserDetails);
            return Ok(new
            {
                TotalCount=userList.TotalCount,
                UserDetails= userListResource
            });
        }

        [HttpGet("{phoneNumber}/VerifyAccountByPhone")]
        public async Task<IActionResult> VerifyUserAccountByPhoneAsync(string phoneNumber)
        {
            TransactionAndUserResponseResource result = null;
            if (!string.IsNullOrEmpty(phoneNumber))
                result = await _userDetailsService.VerifyUserAccountByPhone(phoneNumber);
          
            object userDetails = null;
           
            if (result == null) 
            return Ok(new 
            {
                userDetails,
                status =false,
                message="Phone Number does not exists" 
            });

            var userDetailResource = _mapper.Map<ApplicationUser, ApplicationUserResource>(result.UserDetails);
            var transactionResourceList = _mapper.Map<IEnumerable<Transaction>, IEnumerable<TransactionHistoryResource>>(result.Transactions);

            return Ok(new {
            status=true,
            userDetails = (new
            {
                userDetail = userDetailResource,
                transactions = transactionResourceList
            }),
            });

        }

        [HttpGet("{userId}/staffSession")]
        public async Task<IActionResult> GetStaffPermissionsById(string userId)
        {
            try
            {
                var userDetail = await _userDetailsService.GetUserDetailsById(userId);
                var userResource = _mapper.Map<UserDetail, UserDetailResource>(userDetail);
                string[] deserializedNumbers = System.Text.Json.JsonSerializer.Deserialize<string[]>(userResource?.Permissions);

                if (!string.IsNullOrEmpty(userResource?.Permissions))
                {
                    string jsonString = System.Text.Json.JsonSerializer.Serialize(deserializedNumbers);

                    var secretKey = "1234567890123456";

                    userResource.Permissions = Common.Encrypt(jsonString, secretKey).ToString();
                }
                return Ok(new
                {
                    userResource,
                    session = userResource.Permissions
                });
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [SystemLock]
        [HttpPut("edit")]
        public async Task<IActionResult> EditUserDetails(EditUserDetailResource editUserDetailResource)
        {
            try
            {
                if (string.IsNullOrEmpty(editUserDetailResource.UserId))
                {
                    return BadRequest(Messages.NotFound.ToDescriptionString());
                }

                var applicationUser = await _userManager.FindByIdAsync(editUserDetailResource.UserId);
            
                if(applicationUser == null)
                {
                    return BadRequest(Messages.NotFound.ToDescriptionString());
                }
                var userDetail = await _userDetailsService.GetUserDetailsById(applicationUser.Id);
                if (userDetail == null)
                {
                    return BadRequest(Messages.NotFound.ToDescriptionString());
                }
                if(userDetail.PhoneNumber!= editUserDetailResource.PhoneNumber) 
                {
                    var userExistWithPhone = await _userDetailsService.IsPhoneAlreadyRegistered(editUserDetailResource.PhoneNumber);
                    if (userExistWithPhone == true)
                        return BadRequest(Messages.PhoneAlreadyUsed.ToDescriptionString());

                    applicationUser.PhoneNumber = editUserDetailResource.PhoneNumber;
                    applicationUser.UserName = editUserDetailResource.PhoneNumber;
                }

                var isUserUpdated = await _userManager.UpdateAsync(applicationUser);
                if (isUserUpdated.Succeeded)
                {

                   _mapper.Map<EditUserDetailResource, UserDetail>(editUserDetailResource);
                    var combinedUserData= _mapper.Map(editUserDetailResource, userDetail);

                    if (!string.IsNullOrEmpty(userDetail.Image))
                    {
                        var profileURL = _storageService.UploadUserProfileImage(userDetail.Image, userDetail.UserId);
                        userDetail.Image = profileURL;
                    }

                    if (!string.IsNullOrEmpty(userDetail.KYCDocumentImage))
                    {
                        var documentURL = _storageService.UploadKYCDocument(userDetail.KYCDocumentImage, userDetail.UserId);
                        userDetail.KYCDocumentImage = documentURL.ToString();
                    }
                    if (editUserDetailResource.City != null)
                    {
                        var address = await _addressService.GetAddressByUserId(userDetail.UserId);
                        if (address != null)
                        {
                            address.City = editUserDetailResource.City;
                            address.District = editUserDetailResource.District;
                            await _addressService.AddOrUpdateAddress(address);
                        }
                    }
                    //kyc document is optional in registration
                    if (editUserDetailResource.DocumentTypeId.HasValue)
                    {
                        //insert data in kyc document
                        var kycDetailsToAdd = _mapper.Map<EditUserDetailResource, KYCdocuments>(editUserDetailResource);
                        kycDetailsToAdd.UserId = combinedUserData.UserId;
                        kycDetailsToAdd.UpdatedDate = DateTime.Now;
                        kycDetailsToAdd.DocumentImage = userDetail.KYCDocumentImage;

                        await _kycDocumentsService.UpdateKYCDocument(kycDetailsToAdd);
                    }
                    await _userDetailsService.AddOrUpdateUserDetails(combinedUserData);
                    if (editUserDetailResource.StaffRoleId != 0 )
                    {
                        UsersStaffRole usersStaffRole = new UsersStaffRole();
                        usersStaffRole.UserId = applicationUser.Id;
                        usersStaffRole.UpdatedDate = DateTime.UtcNow;
                        usersStaffRole.StaffRoleId = editUserDetailResource.StaffRoleId;
                        await _usersStaffRoleService.UpdateUserStaffRole(usersStaffRole);
                    }

                }

                return Ok(new
                {
                    Success = true,
                    UserId = editUserDetailResource.UserId,
                    FirstName = editUserDetailResource.FirstName,
                    LastName = editUserDetailResource.LastName,
                    Message = Messages.InfoUpdated.ToDescriptionString()

                });
            }
            catch (Exception ex)
            {
                throw new CustomException("Sorry, we're having trouble in editing info. Please contact support.");
            }

        }

        [AllowAnonymous]
        [HttpPost("AddDevice")]
        public async Task<IActionResult> AddDeviceIdAndToken(FirebaseAddDeviceRequest request)
        {
            if (request.DeviceId == null || request.UserId == null)
            {
                return BadRequest("DeviceId and UserId are required.");
            }

            try
            {
                var device = await _userDeviceService.GetUserDevice(request.UserId, request.DeviceId);
                if (device != null)
                {
                    device.FcmToken = request.DeviceToken;
                }
                else
                {
                    
                    device = new UserDevices
                    {
                        UserId = request.UserId,
                        DeviceId = request.DeviceId,
                        FcmToken = request.DeviceToken,
                        AddedDate = DateTime.UtcNow,
                        IsActive = true,
                    };

                    var allDevices = await _userDeviceService.GetUserDeviceByUserId(request.UserId);
                    if (allDevices.Count != 0)
                    {
                        var checkExistingDevice = allDevices.Contains(device);
                        if (!checkExistingDevice)
                        {
                            var userDeatail = await _userDetailsService.GetUserDetailsById(request.UserId);
                            var message = Messages.UserLoggedInOtherDevice.ToDescriptionString();
                            var sendSms = _sMSService.sendMessage(message, userDeatail.PhoneNumber);
                        }
                    }
                }

                var response = await _userDeviceService.AddOrUpdateUserDevices(device);
                return Ok(response);
            }
            catch (Exception ex)
            {
                // Log exception (logging mechanism not shown)
                return StatusCode(500, "Internal server error.");
            }
        }

        [AllowAnonymous]
        [HttpPost("sendMessage")]
        public async Task<IActionResult> SendSmsToUsers(SendSmsRequestModel request)
        {
            if (request.Note == null || request.PhoneNumber == null)
            {
                return BadRequest("Note and Email are required.");
            }

            try
            {
                var response = _sMSService.sendMessage(request.Note, request.PhoneNumber);
                if (response == "success")
                {
                    return Ok(new{
                        message="Message sent successfully!",
                        success=true
                    });
                }
                else 
                {
                    return BadRequest(new
                    {
                        message = "Something went wrong!",
                        success = false
                    });
                };
                
            }
            catch (Exception ex)
            {
                // Log exception (logging mechanism not shown)
                return StatusCode(500, "Internal server error.");
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("sochitel")]
        public async Task<IActionResult> GetOperators([FromBody] object data)
        {
            try
            {
                var result = await _externalApiService.PostDataAsync(data);

                var products = JsonConvert.DeserializeObject<SochitelOperatorResponse>(result);
                
                List<Operator> operators = new List<Operator>();
                foreach (var kvp in products.Result)
                {
                    var product = kvp.Value;
                    operators.Add(product);
                }

                var newOperatorList = operators;

                return Ok(newOperatorList);
            }
            catch
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        #region helper methods
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
        #endregion
    }
}
