using AutoMapper;
using DevExpress.Xpo.Logger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using wleepay.API.Resources;
using wleepay.API.Validators;
using wleepay.Core.Models;
using wleepay.Core.Services;
using wleepay.Storage.Interface;

namespace wleepay.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = "Bearer")]
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

        public UserController(UserManager<ApplicationUser> userManager,
            IUserDetailService userDetailsService,
            IUploadDocument uploadDocument,
            IAddressService addressService,
            IKYCDocumentsService kycDocumentsService,
            ISmileIdentityJobService smileIdentityJobService,
            IMapper mapper,
            ILogger<UserController> logger)
        {
            _userManager = userManager;
            _userDetailsService = userDetailsService;
            _documentUploaderService = uploadDocument;
            _addressService = addressService;
            _kycDocumentsService = kycDocumentsService;
            _smileIdentityJobService = smileIdentityJobService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetById(string userId)
        {
            var userDetail = await _userDetailsService.GetUserDetailsById(userId);
            var userResource = _mapper.Map<UserDetail, UserDetailResource>(userDetail);
            return Ok(userResource);
        }

        [HttpGet("{userId}/extraInfo")]
        public async Task<IActionResult> GetApplicationUserById(string userId)
        {
            try
            {
            var userDetail = await _userDetailsService.GetApplicationUserById(userId);
                if (System.IO.File.Exists(userDetail.UserDetail.Image))
                {
                    userDetail.UserDetail.Image = System.Convert.ToBase64String((System.IO.File.ReadAllBytes(userDetail.UserDetail.Image)));
                }
                if (System.IO.File.Exists(userDetail.UserDetail.KYCDocumentImage))
                {
                    userDetail.UserDetail.KYCDocumentImage = System.Convert.ToBase64String((System.IO.File.ReadAllBytes(userDetail.UserDetail.KYCDocumentImage)));
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

            var userDetailResource = _mapper.Map<ApplicationUser, UserAccountResource>(userDetail);
            return Ok(userDetailResource != null ? userDetailResource : string.Empty);
        }

        [HttpGet("nearByAgent")]
        public async Task<IActionResult> GetNearByAgent()
        {
            var agentList = await _userDetailsService.GetUserByRole(Core.Enums.Role.Agent.GetHashCode());
            var agentListResource = _mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<UserAccountResource>>(agentList);
            return Ok(agentListResource);
        }

        [HttpPost("all")]
        public async Task<IActionResult> GetUserList([FromBody] PagingRequest request)
        {
            var userList = await _userDetailsService.GetUsers(request.Role, request.Index, request.Size);
            var userListResource = _mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<ApplicationUserResource>>(userList.UserDetails);
            return Ok(new
            {
                userDetails= userListResource,
                TotalCount=userList.TotalCount
            });
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
            return Ok(transactionResource);
        }

        [HttpPost("GetAllUsersById")]
        public async Task<IActionResult> GetAllUsersById([FromBody] AgentRequest request)
        {
            var userList = await _userDetailsService.GetAllUsersById(request.Role ,request.Index, request.Size, request.CreatedBy);
            var userListResource = _mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<ApplicationUserResource>>(userList.UserDetails);
            return Ok(new
            {
                TotalCount=userList.TotalCount,
                UserDetails= userListResource
            });
        }

        [HttpPost("GetAllUsersByRole/{role}")]
        public async Task<IActionResult> GetAllUsersByRole(int role)
        {
            var userList = await _userDetailsService.GetUserByRole(role);
            var userListResource = _mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<ApplicationUserResource>>(userList);
            return Ok(userListResource);
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
