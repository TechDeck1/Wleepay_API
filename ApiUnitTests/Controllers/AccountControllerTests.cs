using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using System.Web.Http.Results;
using wleepay.API.Controllers;
using wleepay.API.Resources;
using wleepay.API.Validators;
using wleepay.Communication.Interface;
using wleepay.Core.Common;
using wleepay.Core.Enums;
using wleepay.Core.Models;
using wleepay.Core.Services;
using wleepay.Storage.Interface;

namespace ApiUnitTests.Controllers
{
    public class AccountControllerTests
    {
        private AccountController _controller;
        private Mock<UserManager<ApplicationUser>> _mockUserManager;
        private Mock<RoleManager<ApplicationRole>> _mockRoleManager;
        private Mock<SignInManager<ApplicationUser>> _mockSignInManager;
        private Mock<IUserDetailService> _mockUserDetailsService;
        private Mock<IWalletService> _mockWalletService;
        private Mock<IUploadDocument> _mockUploadDocument;
        private Mock<ILogger<AccountController>> _mockLogger;
        private Mock<IKYCDocumentsService> _mockKYCDocumentsService;
        private Mock<IAddressService> _mockAddressService;
        private Mock<ISMSService> _mockSMSService;
        private Mock<IConfiguration> _mockConfiguration;
        private Mock<IMapper> _mockMapper;
        private Mock<IHostingEnvironment> _mockHostingEnvironment;
        private Mock<IEmailSender> _mockEmailSender;
        private Mock<ISystemActivityLogsService> _mockSystemActivityLogsService;
        private Mock<IUsersStaffRoleService> _mockUsersStaffRoleService;
        private Mock<IVerificationService> _mockVerificationService;
        private Mock<ILookupService> _mockLookupService;
        private Mock<IValidator<AddUserDetailsValidator>> _mockValidator;
        private Mock<ISmileIdentityService> _mockSmileIdentityService;



        [SetUp]
        public void Setup()
        {

            _mockUserManager = new Mock<UserManager<ApplicationUser>>(Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);
            _mockRoleManager = new Mock<RoleManager<ApplicationRole>>(Mock.Of<IRoleStore<ApplicationRole>>(), null, null, null, null);
            _mockSignInManager = new Mock<SignInManager<ApplicationUser>>(_mockUserManager.Object, Mock.Of<IHttpContextAccessor>(), Mock.Of<IUserClaimsPrincipalFactory<ApplicationUser>>(), null, null, null, null);
            _mockUserDetailsService = new Mock<IUserDetailService>();
            _mockWalletService = new Mock<IWalletService>();
            _mockUploadDocument = new Mock<IUploadDocument>();
            _mockLogger = new Mock<ILogger<AccountController>>();
            _mockKYCDocumentsService = new Mock<IKYCDocumentsService>();
            _mockAddressService = new Mock<IAddressService>();
            _mockSMSService = new Mock<ISMSService>();
            _mockConfiguration = new Mock<IConfiguration>();
            _mockMapper = new Mock<IMapper>();
            _mockHostingEnvironment = new Mock<IHostingEnvironment>();
            _mockEmailSender = new Mock<IEmailSender>();
            _mockSystemActivityLogsService = new Mock<ISystemActivityLogsService>();
            _mockUsersStaffRoleService = new Mock<IUsersStaffRoleService>();
            _mockVerificationService = new Mock<IVerificationService>();
            _mockLookupService = new Mock<ILookupService>();
            _mockValidator = new Mock<IValidator<AddUserDetailsValidator>>();
            _mockSmileIdentityService = new Mock<ISmileIdentityService>();


            _controller = new AccountController(
                _mockUserManager.Object,
                _mockRoleManager.Object,
                _mockSignInManager.Object,
                _mockUserDetailsService.Object,
                _mockWalletService.Object,
                _mockSmileIdentityService.Object,
                _mockUploadDocument.Object,
                _mockLogger.Object,
                _mockKYCDocumentsService.Object,
                _mockAddressService.Object,
                _mockSMSService.Object,
                _mockConfiguration.Object,
                _mockMapper.Object,
                _mockUploadDocument.Object,
                _mockHostingEnvironment.Object,
                _mockEmailSender.Object,
                _mockSystemActivityLogsService.Object,
                _mockUsersStaffRoleService.Object,
                _mockVerificationService.Object

            );

        }

        [Test]
        public async Task Register_ValidInputForMember_CreatesUserAndReturnsToken()
        {
            // Arrange
            var userDetail = new AddUserDetailsResource
            {
                EmailId = "test@example.com",
                Role = Role.Staff, // Assuming role is not 0 (SuperAdmin)
                PhoneNumber = "1234567890",
                CountyId = 5,
                Gender = "male",
                FirstName = "john",
                KYCStatusId = 1,
                Pin="SuperAdmin@12345"
                // Populate other required properties
            };
            var newUser = new ApplicationUser { Id = Guid.NewGuid().ToString(), Email = "test@example.com" };
            var userDetails = new UserDetail { UserId = newUser.Id };
            var validationResult = new ValidationResult();
            var userDetailsToInsert = new UserDetail();
            var token = "token";

            _mockValidator.Setup(x => x.ValidateAsync(It.IsAny<ValidationContext<AddUserDetailsValidator>>(), default))
              .ReturnsAsync(validationResult);
            _mockConfiguration.Setup(x => x.GetSection("BaseUrl").Value).Returns("http://example.com");
            _mockUserManager.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), userDetail.Pin.ToString())).ReturnsAsync(IdentityResult.Success);
            _mockUserManager.Setup(x => x.AddToRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
            _mockUserDetailsService.Setup(x => x.AddOrUpdateUserDetails(It.IsAny<UserDetail>())).ReturnsAsync(userDetails);
            _mockUserManager.Setup(x => x.FindByEmailAsync(userDetail.EmailId)).ReturnsAsync((ApplicationUser)null);
            _mockUserDetailsService.Setup(x => x.IsPhoneAlreadyRegistered(It.IsAny<string>())).ReturnsAsync(false);
            _mockMapper.Setup(m => m.Map<AddUserDetailsResource, UserDetail>(It.IsAny<AddUserDetailsResource>()))
                .Returns(userDetailsToInsert);


            _mockUserDetailsService.Setup(x => x.GenerateJSONWebToken(userDetails, userDetail.Role.ToString())).ReturnsAsync(token);

            // Act
            var result = await _controller.Register(userDetail);

            // Assert
            var okResult = result as OkObjectResult;
            bool success = (bool)(okResult.Value?.GetType().GetProperty("Success")?.GetValue(okResult.Value));
            string userId = (string)(okResult.Value?.GetType().GetProperty("UserId")?.GetValue(okResult.Value));

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.IsTrue(success);
            Assert.IsNotNull(userId);
        }

        [Test]
        public async Task RegisterStaff_InvalidRole_ReturnsBadRequest()
        {
            // Arrange
            _mockValidator = new Mock<IValidator<AddUserDetailsValidator>>();

            var userDetail = new AddUserDetailsResource
            {
                EmailId = "test@example.com",
                Role = Role.SuperAdmin, // Assuming role is not 0 (SuperAdmin)
                PhoneNumber = "1234567890",
                CountyId = 5,
                Gender = "male",
                FirstName = "john",
                KYCStatusId = 1,
                Pin = "SuperAdmin@12345"
                // Populate other required properties
            };

            var validationResult = new ValidationResult();

            _mockValidator.Setup(x => x.ValidateAsync(It.IsAny<ValidationContext<AddUserDetailsResource>>(), default)).ReturnsAsync(validationResult);
            _mockConfiguration.Setup(x => x.GetSection("BaseUrl").Value).Returns("http://example.com");

            // Act
            var result = await _controller.Register(userDetail);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);
            Assert.AreEqual(Messages.IsSuperAdmin.ToDescriptionString(), badRequestResult.Value.ToString());
            // Add assertions for returned message
        }

        [Test]
        public async Task RegisterStaff_PhoneNumberAndEmailExists_ReturnsBadRequest()
        {
            // Arrange
            _mockValidator = new Mock<IValidator<AddUserDetailsValidator>>();

            var userDetail = new AddUserDetailsResource
            {
                EmailId = "test@example.com",
                Role = Role.Staff, // Assuming role is not 0 (SuperAdmin)
                PhoneNumber = "1234567890",
                CountyId = 5,
                Gender = "male",
                FirstName = "john",
                KYCStatusId = 1,
                Pin = "SuperAdmin@12345"
                // Populate other required properties
            };
            var validationResult = new ValidationResult();
            var newUser = new ApplicationUser { Id = "123456", Email = "test@example.com" };
            var userDetails = new UserDetail { UserId = newUser.Id };

            _mockValidator.Setup(x => x.ValidateAsync(It.IsAny<ValidationContext<AddUserDetailsResource>>(), default)).ReturnsAsync(validationResult);
            _mockConfiguration.Setup(x => x.GetSection("BaseUrl").Value).Returns("http://example.com");
            _mockUserManager.Setup(x => x.FindByEmailAsync(userDetail.EmailId)).ReturnsAsync(newUser);
            _mockUserDetailsService.Setup(x => x.IsPhoneAlreadyRegistered(It.IsAny<string>())).ReturnsAsync(true);

            // Act
            var result = await _controller.Register(userDetail);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);
            Assert.AreEqual(Messages.EmailAlreadyUsed.ToDescriptionString(), badRequestResult.Value.ToString());
            // Add assertions for returned message
        }

        [Test]
        public async Task RegisterStaff_UserCreationFailed_ReturnsBadRequest()
        {
            // Arrange
            _mockValidator = new Mock<IValidator<AddUserDetailsValidator>>();

            var userDetail = new AddUserDetailsResource
            {
                EmailId = "test@example.com",
                Role = Role.Staff, // Assuming role is not 0 (SuperAdmin)
                PhoneNumber = "1234567890",
                CountyId = 5,
                Gender = "male",
                FirstName = "john",
                KYCStatusId = 1,
                Pin = "SuperAdmin@12345"
                // Populate other required properties
            };
            var validationResult = new ValidationResult();
            var newUser = new ApplicationUser { Id = "123456", Email = "test@example.com" };
            var userDetails = new UserDetail { UserId = newUser.Id };
            IdentityResult identityResult = new();
            IdentityError identityError = new IdentityError();


            _mockUserManager.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), userDetail.Pin.ToString())).ReturnsAsync(IdentityResult.Failed(identityError));
            _mockValidator.Setup(x => x.ValidateAsync(It.IsAny<ValidationContext<AddUserDetailsResource>>(), default)).ReturnsAsync(validationResult);
            _mockConfiguration.Setup(x => x.GetSection("BaseUrl").Value).Returns("http://example.com");
            _mockUserManager.Setup(x => x.FindByEmailAsync(userDetail.EmailId)).ReturnsAsync((ApplicationUser)null);
            _mockUserDetailsService.Setup(x => x.IsPhoneAlreadyRegistered(It.IsAny<string>())).ReturnsAsync(false);

            // Act
            var result = await _controller.Register(userDetail);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);
            // Add assertions for returned message
        }

        [Test]
        public async Task SendOTP_With_Valid_Request_Returns_OkResult()
        {
            // Arrange
            var otpRequest = new OTPRequest { Pin = "1234", PhoneNo = "1234567890" };
            //_mockValidator = new Mock<IValidator<AddUserDetailsValidator>>();
            var validationResult = new ValidationResult();

            _mockValidator.Setup(x => x.ValidateAsync(It.IsAny<ValidationContext<AddUserDetailsResource>>(), default)).ReturnsAsync(validationResult);
            _mockSMSService.Setup(s => s.sendMessage(It.IsAny<string>(), otpRequest.PhoneNo)).Returns("queued");

            // Act
            var result = await _controller.SendOTP(otpRequest);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsInstanceOf<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }

        [Test]
        public async Task SendOTP_With_Invalid_Request_Returns_BadRequest()
        {
            // Arrange
            var otpRequest = new OTPRequest { Pin = "1234", PhoneNo = "1234567890" };
            _mockValidator = new Mock<IValidator<AddUserDetailsValidator>>();
            var validationResult = new ValidationResult();

            _mockValidator.Setup(x => x.ValidateAsync(It.IsAny<ValidationContext<AddUserDetailsResource>>(), default)).ReturnsAsync(validationResult);
            _mockSMSService.Setup(s => s.sendMessage(It.IsAny<string>(), otpRequest.PhoneNo)).Returns("some error message");


            // Act
            var result = await _controller.SendOTP(otpRequest);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            Assert.NotNull(badRequestResult.Value);
        }

        [Test]
        public async Task SendOTP_With_Valid_Request_Returns_BadRequest_On_SmsService_Error()
        {
            // Arrange
            var otpRequest = new OTPRequest { Pin = "", PhoneNo = "" };
            _mockValidator = new Mock<IValidator<AddUserDetailsValidator>>();
            var validationResult = new ValidationResult();

            _mockValidator.Setup(x => x.ValidateAsync(It.IsAny<ValidationContext<AddUserDetailsResource>>(), default)).ReturnsAsync(validationResult);

            _mockSMSService.Setup(s => s.sendMessage(It.IsAny<string>(), otpRequest.PhoneNo)).Returns("Some error message");

            // Act
            var result = await _controller.SendOTP(otpRequest);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            Assert.NotNull(badRequestResult.Value);
        }

        [Test]
        public async Task SendOTP_Throws_Exception_Returns_BadRequest()
        {
            // Arrange
            var otpRequest = new OTPRequest { Pin = "1234", PhoneNo = "1234567890" };

            _mockSMSService.Setup(s => s.sendMessage(It.IsAny<string>(), otpRequest.PhoneNo)).Throws(new Exception());

            // Act

            Exception caughtException = null;

            try
            {
                await _controller.SendOTP(otpRequest);
            }
            catch (Exception ex)
            {
                caughtException = ex;
            }

            // Assert
            Assert.NotNull(caughtException);

        }

        [Test]
        public async Task UpdatePIN_With_Valid_Request_Returns_OkResult()
        {
            // Arrange
            var updatePinRequest = new LoginRequest { PhoneNo = "12676767634", Pin = "1234" };
           
            var userDetail = new UserDetail { PhoneNumber = updatePinRequest.PhoneNo };
            var user = new ApplicationUser()
            {
                Email="abc@example.com",
                Id="user123"
            };
            var token = "passwordResetToken";

            _mockUserDetailsService.Setup(x => x.GetUserDetailsByPhone(updatePinRequest.PhoneNo))
                .ReturnsAsync(userDetail);
            _mockUserManager.Setup(x => x.FindByNameAsync(userDetail.PhoneNumber))
                .ReturnsAsync(user);
            _mockUserManager.Setup(x => x.GeneratePasswordResetTokenAsync(user))
                .ReturnsAsync(token);
            _mockUserManager.Setup(x => x.ResetPasswordAsync(user, token, updatePinRequest.Pin))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _controller.UpdatePIN(updatePinRequest);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsInstanceOf<OkObjectResult>(result);
            Assert.AreEqual("Pin is successfully updated.", okResult.Value);
        }

        [Test]
        public async Task UpdatePIN_With_Invalid_Request_Returns_BadRequest()
        {
            // Arrange
            var updatePinRequest = new LoginRequest { PhoneNo = "12676767634", Pin = "1234" };


            _mockUserDetailsService.Setup(x => x.GetUserDetailsByPhone(updatePinRequest.PhoneNo))
                .ReturnsAsync((UserDetail)null);
            //Act
            var result = await _controller.UpdatePIN(updatePinRequest);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            Assert.NotNull(badRequestResult.Value);
        }

        [Test]
        public async Task UpdatePIN_Returns_BadRequest_When_User_Not_Found()
        {
            // Arrange
            var updatePinRequest = new LoginRequest { PhoneNo = "12676767634", Pin = "1234" };
           
            _mockUserDetailsService.Setup(x => x.GetUserDetailsByPhone(updatePinRequest.PhoneNo))
                .ReturnsAsync((UserDetail)null); // User not found

            // Act
            var result = await _controller.UpdatePIN(updatePinRequest);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            Assert.AreEqual("Account does not exist.", badRequestResult.Value);
        }
    }
}
