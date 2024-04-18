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
using System.Text.Json.Serialization;
using System.Web.Http.Results;
using Twilio.Types;
using wleepay.API.Areas.Portal.Controllers;
using wleepay.API.Resources;
using wleepay.API.Validators;
using wleepay.Communication.Interface;
using wleepay.Core.Common;
using wleepay.Core.DTO.Response;
using wleepay.Core.Enums;
using wleepay.Core.Models;
using wleepay.Core.Services;
using wleepay.Services;
using wleepay.Storage.Interface;


namespace ApiUnitTests.Controllers
{

    [TestFixture]
    public class AccountPortalControllerTests
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
        private Mock<IValidator<AddStaffDetailsResource>> _mockValidator;

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
            _mockValidator = new Mock<IValidator<AddStaffDetailsResource>>();



            _controller = new AccountController(
                _mockUserManager.Object,
                _mockRoleManager.Object,
                _mockSignInManager.Object,
                _mockUserDetailsService.Object,
                _mockWalletService.Object,
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
                _mockVerificationService.Object,
                _mockLookupService.Object
            );

        }

        [Test]
        public async Task Login_WithValidCredentials_ReturnsToken()
        {
            // Arrange
            var loginRequest = new PortalLoginRequest { Email = "wleeadmin@wleepay.com", Password = "11118888wleePay@", LoginFor = new string[] { "SuperAdmin" } };
            var user = new ApplicationUser { Id = "07ac193d-26e7-4039-9440-fbb53988b878", Email = "wleeadmin@wleepay.com" };
            var userDetails = new UserDetail { UserId = "07ac193d-26e7-4039-9440-fbb53988b878", Status = 1 /* Add other required properties */ };
            var userRole = new List<string> { "SuperAdmin" };
            var token = "token";

            _mockUserManager.Setup(m => m.FindByEmailAsync(loginRequest.Email)).ReturnsAsync(user);
            _mockUserDetailsService.Setup(m => m.GetUserDetailsById(user.Id)).ReturnsAsync(userDetails);
            _mockUserManager.Setup(m => m.GetRolesAsync(user)).ReturnsAsync(userRole);
            _mockUserDetailsService.Setup(x => x.GenerateJSONWebToken(userDetails, userRole[0])).ReturnsAsync(token);
            _mockSignInManager.Setup(m => m.PasswordSignInAsync(user, loginRequest.Password, false, true)).ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);

            // Act
            var result = await _controller.Login(loginRequest);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);

            string userId = (string)(okResult.Value?.GetType().GetProperty("UserId")?.GetValue(okResult.Value));
            bool success = (bool)(okResult.Value?.GetType().GetProperty("Success")?.GetValue(okResult.Value));
            string resultToken = (string)okResult.Value?.GetType().GetProperty("Token")?.GetValue(okResult.Value);
            bool isLoggedInVerified = (bool)okResult.Value?.GetType().GetProperty("IsLoggedInVerified")?.GetValue(okResult.Value);
            string role = (string)okResult.Value?.GetType().GetProperty("Role")?.GetValue(okResult.Value);

            Assert.IsTrue(success);
            Assert.AreEqual(user.Id, userId);
            Assert.IsNotNull(resultToken);
            Assert.AreEqual(userRole[0], role);

            // Add more assertions as needed
        }

        [Test]
        public async Task Test_SendVerificationCode_Success()
        {
            // Arrange

            var loginRequest = new PortalLoginRequest { Email = "wleeadmin@wleepay.com", Password = "11118888wleePay@", LoginFor = new string[] { "SuperAdmin" } };
            var user = new ApplicationUser { Id = "07ac193d-26e7-4039-9440-fbb53988b878", Email = "wleeadmin@wleepay.com" };
            var userDetails = new UserDetail { UserId = "07ac193d-26e7-4039-9440-fbb53988b878", Status = 1, PhoneNumber = "+919878192599" };
            var userRole = new List<string> { "SuperAdmin" };
            var token = "token";
            var model = new SendCode { UserId = "26f54dd3-c2e4-4076-9c46-de1ef9f5ec27" }; // Provide valid user id here
            var verificationCode = "1234"; // Provide valid verification code here

            _mockUserDetailsService.Setup(m => m.GetUserDetailsById(model.UserId)).ReturnsAsync(userDetails);
            _mockSMSService.Setup(x => x.sendMessage("message", userDetails.PhoneNumber)).Returns("queued");
            _mockUserDetailsService.Setup(x => x.UpdateUserVerificationCode(model.UserId, verificationCode)).ReturnsAsync(userDetails);

            // Act
            var result = await _controller.SendOTP(model);
            var okResult = result as OkObjectResult;

            // Assert

            Assert.NotNull(okResult);
            Assert.IsNotNull(okResult);
            string phone = (string)(okResult.Value?.GetType().GetProperty("Phone")?.GetValue(okResult.Value));
            bool success = (bool)(okResult.Value?.GetType().GetProperty("Sucess")?.GetValue(okResult.Value));
            Assert.IsTrue(success);
        }

        [Test]
        public async Task Test_SendVerificationCode_Failure()
        {
            // Arrange
            var loginRequest = new PortalLoginRequest { Email = "wleeadmin@wleepay.com", Password = "11118888wleePay@", LoginFor = new string[] { "SuperAdmin" } };
            var user = new ApplicationUser { Id = "07ac193d-26e7-4039-9440-fbb53988b878", Email = "wleeadmin@wleepay.com" };
            var userDetails = new UserDetail { UserId = "07ac193d-26e7-4039-9440-fbb53988b878", Status = 1, PhoneNumber = "+919878192599" };
            var userRole = new List<string> { "SuperAdmin" };
            var token = "token";
            var model = new SendCode { UserId = "26f54dd3-c2e4-4076-9c46-de1ef9f5ec27" }; // Provide valid user id here
            var verificationCode = "1234"; // Provide valid verification code here

            _mockUserDetailsService.Setup(m => m.GetUserDetailsById(model.UserId)).ReturnsAsync((UserDetail)null);
            _mockUserDetailsService.Setup(x => x.UpdateUserVerificationCode(model.UserId, verificationCode)).ReturnsAsync(userDetails);

            // Act
            var result = await _controller.SendOTP(model);
          
            var okResult = result as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(okResult);
           
            _mockSMSService.Verify(x => x.sendMessage("message", userDetails.PhoneNumber), Times.Never);



        }
        [Test]
        public async Task VerifyCode_ValidCode_ReturnsToken()
        {
            // Arrange
            var model = new VerificationModel
            {
                UserId = "07ac193d-26e7-4039-9440-fbb53988b878",
                Email = "test@example.com",
                Code = "123456" // Valid verification code
            };
            var userDetails = new UserDetail
            {
                UserId = "07ac193d-26e7-4039-9440-fbb53988b878",
                VerificationExpiry = DateTime.UtcNow.AddMinutes(1), // Valid expiry date
                VerificationCode = "123456", // Valid verification code
                IsLoggedIn = false,
                Permissions = null// Other necessary properties
            };
            var userRole = new List<string> { "SuperAdmin" };
            var token = "token";
            var user = new ApplicationUser { Id = "07ac193d-26e7-4039-9440-fbb53988b878", Email = "test@example.com" };

            _mockUserDetailsService.Setup(s => s.GetUserDetailsById(model.UserId)).ReturnsAsync(userDetails);
            _mockUserManager.Setup(s => s.FindByEmailAsync(model.Email)).ReturnsAsync(user);
            _mockUserManager.Setup(m => m.GetRolesAsync(user)).ReturnsAsync(userRole);
            _mockUserDetailsService.Setup(x => x.GenerateJSONWebToken(userDetails, userRole[0])).ReturnsAsync(token);

            // Act
            var result = await _controller.VerifyCode(model);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult.Value);
            Assert.AreEqual(200, okResult.StatusCode);

            var data = okResult.Value?.GetType().GetProperty("Data")?.GetValue(okResult.Value);
            //string UserId = (string)(data?.GetType().GetProperty("UserId")?.GetValue(data));
            //string Role = (string)data?.GetType().GetProperty("Role")?.GetValue(data);
            //string Token = (string)data?.GetType().GetProperty("Token")?.GetValue(data);

            // Access other properties of the okResult object
            var session = (string)okResult.Value?.GetType().GetProperty("Session")?.GetValue(okResult.Value);
            var isExpired = (bool)okResult.Value?.GetType().GetProperty("IsExpired")?.GetValue(okResult.Value);
            var success = (bool)okResult.Value?.GetType().GetProperty("Success")?.GetValue(okResult.Value);
            var message = (string)okResult.Value?.GetType().GetProperty("Message")?.GetValue(okResult.Value);


            Assert.IsNotNull(data);
            Assert.IsTrue(success);
            Assert.IsFalse(isExpired);
        }

        [Test]
        public async Task ActiveOrDeActiveAccount_ActivateAccount_ReturnsOk()
        {
            // Arrange
            var model = new ActivateDeactivateAccountResource
            {
                EmailId = "test@example.com",
                UserId = "user123",
                Status = (int)Status.Active // Activate account
            };

            var user = new ApplicationUser { Id = "user123", Email = "test@example.com" };
            var userDetails = new UserDetail
            {
                UserId = "user123",
                FirstName = "John",
                PhoneNumber = "1234567890",
                Status = (int)Status.Active
                // Other necessary properties
            };

            _mockUserManager.Setup(s => s.FindByEmailAsync("test@example.com")).ReturnsAsync(user);
            _mockUserDetailsService.Setup(s => s.GetUserDetailsById("user123")).ReturnsAsync(userDetails);
            _mockUserDetailsService.Setup(s => s.UpdateUserStatus("user123", (int)Status.Active)).ReturnsAsync(userDetails);

            // Act
            var result = await _controller.ActiveOrDeActiveAccount(model);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);

            Assert.AreEqual(200, okResult.StatusCode);
            var valueAsString = okResult.Value.ToString(); // Convert the value to string
            int intValue = int.Parse(valueAsString, System.Globalization.NumberStyles.HexNumber);
            Assert.IsTrue(intValue == 0 || intValue == 1);
        }

        [Test]
        public async Task RefreshToken_ValidToken_ReturnsNewAccessToken()
        {
            // Arrange
            var tokenModel = new RefreshTokenModel
            {
                Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiQWRtaW4iLCJqdGkiOiIyYThkZTRlMS1lOGFjLTQ0MjItODhlNi00OGI2MmIyNjk3MGYiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJTdXBlckFkbWluIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvZW1haWxhZGRyZXNzIjoid2xlZWFkbWluQHdsZWVwYXkuY29tIiwiZXhwIjoxNzExNjI0MjI5LCJpc3MiOiJodHRwczovL3dsZWVwYXktYXBpLWtzMC5jb252ZXlvci5jbG91ZCIsImF1ZCI6Imh0dHA6Ly9sb2NhbGhvc3Q6NDIwMCJ9.WldbnSTyrZ8mYlaZkgw-0yBoREkYJ-0nX3xs1x8tQRo"
            };

            var user = new ApplicationUser { Id = "user123", Email = "wleeadmin@wleepay.com" };
            var userDetails = new UserDetail
            {
                UserId = "user123",
                FirstName = "John",
                PhoneNumber = "1234567890",
                Status = (int)Status.Active
                // Mock necessary properties of UserDetail
            };
            var userRole = new List<string> { "SuperAdmin" };
            var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiQWRtaW4iLCJqdGkiOiIyYThkZTRlMS1lOGFjLTQ0MjItODhlNi00OGI2MmIyNjk3MGYiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJTdXBlckFkbWluIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvZW1haWxhZGRyZXNzIjoid2xlZWFkbWluQHdsZWVwYXkuY29tIiwiZXhwIjoxNzExNjI0MjI5LCJpc3MiOiJodHRwczovL3dsZWVwYXktYXBpLWtzMC5jb252ZXlvci5jbG91ZCIsImF1ZCI6Imh0dHA6Ly9sb2NhbGhvc3Q6NDIwMCJ9.WldbnSTyrZ8mYlaZkgw-0yBoREkYJ-0nX3xs1x8tQRo";

            _mockUserManager.Setup(s => s.FindByEmailAsync("wleeadmin@wleepay.com")).ReturnsAsync(user);
            _mockUserDetailsService.Setup(s => s.GetUserDetailsById(user.Id)).ReturnsAsync(userDetails);
            _mockUserManager.Setup(m => m.GetRolesAsync(user)).ReturnsAsync(userRole);
            _mockUserDetailsService.Setup(x => x.GenerateJSONWebToken(userDetails, userRole[0])).ReturnsAsync(token);

            // Act
            var result = await _controller.RefreshToken(tokenModel);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.IsNotNull(okResult.Value);
            var resultToken = (string)okResult.Value?.GetType().GetProperty("accessToken")?.GetValue(okResult.Value);
            Assert.IsNotNull(resultToken);
            //Assert.AreSame(tokenModel.Token, resultToken);

        }

        [Test]
        public async Task GetSystemAllLogs_FilterByEmail_ReturnsFilteredLogs()
        {
            // Arrange
            var request = new Pagination { Email = "", Index = 1, Size = 0 };
            var filterLogs = new ActivityResponseResource
            {
                TotalCount = 3, // Assuming there are 3 activity logs in total
                ActivityLogs = new List<SystemActivityLog>
                {
                    new SystemActivityLog { Id = 1, Description = "First activity log", Email="test@example.com" },
                    new SystemActivityLog { Id = 2, Description = "Second activity log",  Email="test@example.com" },
                    new SystemActivityLog { Id = 3, Description = "Third activity log", Email="test@example.com" }
                }
            };

            var expectedAllLogs = new ActivityResponseResource
            {
                TotalCount = 3, // Assuming there are 3 activity logs in total
                ActivityLogs = new List<SystemActivityLog>
                {
                    new SystemActivityLog { Id = 1, Description = "First activity log", Email="test@example.com" },
                    new SystemActivityLog { Id = 2, Description = "Second activity log",  Email="wleepayAdmin@wlee.com" },
                    new SystemActivityLog { Id = 3, Description = "Third activity log", Email="abcd@wlee.com" }
                }
            };
            _mockSystemActivityLogsService.Setup(s => s.FilterLogsByEmail(request.Email, request.Index, request.Size))
                .ReturnsAsync(filterLogs);

            _mockSystemActivityLogsService.Setup(s => s.GetAllSystemLogs(request.Index, request.Size))
               .ReturnsAsync(expectedAllLogs);

            // Act
            var result = await _controller.GetSystemAllLogs(request);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            //Assert.AreEqual(expectedAllLogs, okResult.Value); // Assuming the returned logs match the expected logs
        }

        [Test]
        public async Task GetSystemAllLogs_ByUserId_ReturnsLogsWithSameUserId()
        {
            // Arrange
            var userId = "1";
            var index = 0;
            var size = 10;
            var expectedAllLogs = new ActivityResponseResource
            {
                TotalCount = 3, // Assuming there are 3 activity logs in total
                ActivityLogs = new List<SystemActivityLog>
                {
                    new SystemActivityLog { Id = 1, Description = "First activity log", Email="test@example.com" },
                    new SystemActivityLog { Id = 1, Description = "Second activity log",  Email="test@example.com" },
                    new SystemActivityLog { Id = 1, Description = "Third activity log", Email="test@example.com" }
                }
            };

            _mockSystemActivityLogsService.Setup(s => s.GetAllSystemById(userId, index, size))
               .ReturnsAsync(expectedAllLogs);

            // Act
            var result = await _controller.GetSystemLogsById(userId, index, size);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            //Assert.AreEqual(expectedAllLogs, okResult.Value); // Assuming the returned logs match the expected logs
        }

        [Test]
        public async Task DeleteLogsById_ValidId_ReturnsOk()
        {
            // Arrange
            int id = 1;
            _mockSystemActivityLogsService.Setup(x => x.DeleteLogsById(It.IsAny<int>())).ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteLogsById(id);
            var okResult = result as OkObjectResult;

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            var valueAsString = okResult.Value; // Convert the value to string
            Assert.IsTrue((bool?)valueAsString);
        }

        [Test]
        public async Task DeleteLogsById_ValidId_ReturnsBadRequest()
        {
            // Arrange
            int id = 0;
            _mockSystemActivityLogsService.Setup(x => x.DeleteLogsById(It.IsAny<int>())).ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteLogsById(id);
            var okResult = result as BadRequestObjectResult;

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(okResult);
            Assert.AreEqual(400, okResult.StatusCode);

        }

        [Test]
        public async Task UpdatePermissions_ValidInput_ReturnsOk()
        {
            // Arrange
            var staffPermissionAndRoleResource = new StaffPermissionAndRoleResource
            {
                EmailId = "test@example.com",
                Permissions = new int[] { 1, 2, 3 }, // Assuming permission types 1 and 2 are valid
                StaffRoleId = 1 // Assuming a valid staff role ID
            };

            var user = new ApplicationUser { Id = "user123", Email = "test@example.com" };
            _mockUserManager.Setup(x => x.FindByEmailAsync(staffPermissionAndRoleResource.EmailId)).ReturnsAsync(user);

            var userDetail = new UserDetail
            {
                UserId = "user123",
                FirstName = "John",
                PhoneNumber = "1234567890",
                Status = (int)Status.Active,
                Permissions = ""
            };
            _mockUserDetailsService.Setup(x => x.GetUserDetailsById(user.Id)).ReturnsAsync(userDetail);

            var usersStaffRole = new UsersStaffRole { UserId = user.Id, StaffRoleId = staffPermissionAndRoleResource.StaffRoleId };
            _mockUsersStaffRoleService.Setup(x => x.UpdateUserStaffRole(It.IsAny<UsersStaffRole>())).ReturnsAsync(usersStaffRole);
            _mockUserDetailsService.Setup(x => x.AddOrUpdateUserDetails(It.IsAny<UserDetail>())).ReturnsAsync(userDetail);

            // Act
            var result = await _controller.UpdatePermissions(staffPermissionAndRoleResource);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var userDetailResult = okResult.Value as UserDetail;
            Assert.IsNotNull(userDetail);
            Assert.IsNotNull(userDetailResult.Permissions);
        }

        [Test]
        public async Task UpdatePermissions_InvalidUser_ReturnsBadRequest()
        {
            // Arrange
            var staffPermissionAndRoleResource = new StaffPermissionAndRoleResource
            {
                EmailId = "test@example.com"
            };

            _mockUserManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync((ApplicationUser)null);

            _mockUserDetailsService.Setup(x => x.GetUserDetailsById(It.IsAny<string>())).ReturnsAsync((UserDetail)null);

            // Act
            var result = await _controller.UpdatePermissions(staffPermissionAndRoleResource);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public async Task ForgotPinSystemAdmin_ValidInput_ReturnsOk()
        {
            // Arrange
            var forgotPinResourceSystemAdmin = new ForgotPinResourceSystemAdmin
            {
                Email = "test@example.com",
                PhoneNumber = "1234567890",
                ClientURI = "http://localhost:52000"

            };
            var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiQWRtaW4iLCJqdGkiOiIyYThkZTRlMS1lOGFjLTQ0MjItODhlNi00OGI2MmIyNjk3MGYiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJTdXBlckFkbWluIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvZW1haWxhZGRyZXNzIjoid2xlZWFkbWluQHdsZWVwYXkuY29tIiwiZXhwIjoxNzExNjI0MjI5LCJpc3MiOiJodHRwczovL3dsZWVwYXktYXBpLWtzMC5jb252ZXlvci5jbG91ZCIsImF1ZCI6Imh0dHA6Ly9sb2NhbGhvc3Q6NDIwMCJ9.WldbnSTyrZ8mYlaZkgw-0yBoREkYJ-0nX3xs1x8tQRo";

            var user = new ApplicationUser { Id = "1", Email = forgotPinResourceSystemAdmin.Email };
            var userDetails = new UserDetail { UserId = user.Id, PhoneNumber = forgotPinResourceSystemAdmin.PhoneNumber, FirstName = "john", Status = (int)Status.Active };

            _mockUserManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(user);
            _mockUserDetailsService.Setup(x => x.GetUserDetailsById(user.Id)).ReturnsAsync(userDetails);
            _mockUserManager.Setup(x => x.GeneratePasswordResetTokenAsync(user)).ReturnsAsync(token);

            _mockConfiguration.Setup(x => x.GetSection("BaseUrl").Value).Returns("http://example.com");
            _mockConfiguration.Setup(x => x.GetSection("SecretKeyForEncryptAndDecrypt").Value).Returns("1234567890123456");

            _mockEmailSender.Setup(x => x.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                            .ReturnsAsync("someCode");

            // Act
            var result = await _controller.ForgotPinSystemAdmin(forgotPinResourceSystemAdmin);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            bool success = (bool)(okResult.Value?.GetType().GetProperty("Success")?.GetValue(okResult.Value));
            string message = (string)(okResult.Value?.GetType().GetProperty("Message")?.GetValue(okResult.Value));
            Assert.IsTrue(success);
            Assert.AreEqual(Messages.LinkSentForResetPassword.ToDescriptionString(), message);
        }

        [Test]
        public async Task ForgotPinSystemAdmin_ValidInput_ReturnsBadRequest()
        {
            // Arrange
            var forgotPinResourceSystemAdmin = new ForgotPinResourceSystemAdmin
            {
                Email = "test@example.com",
                PhoneNumber = "1234567890",
                ClientURI = "http://localhost:52000"

            };
            var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiQWRtaW4iLCJqdGkiOiIyYThkZTRlMS1lOGFjLTQ0MjItODhlNi00OGI2MmIyNjk3MGYiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJTdXBlckFkbWluIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvZW1haWxhZGRyZXNzIjoid2xlZWFkbWluQHdsZWVwYXkuY29tIiwiZXhwIjoxNzExNjI0MjI5LCJpc3MiOiJodHRwczovL3dsZWVwYXktYXBpLWtzMC5jb252ZXlvci5jbG91ZCIsImF1ZCI6Imh0dHA6Ly9sb2NhbGhvc3Q6NDIwMCJ9.WldbnSTyrZ8mYlaZkgw-0yBoREkYJ-0nX3xs1x8tQRo";

            var user = new ApplicationUser { Id = "1", Email = forgotPinResourceSystemAdmin.Email };
            var userDetails = new UserDetail { UserId = user.Id, PhoneNumber = forgotPinResourceSystemAdmin.PhoneNumber, FirstName = "john", Status = (int)Status.Active };

            _mockUserManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync((ApplicationUser)null);
            _mockUserDetailsService.Setup(x => x.GetUserDetailsById(user.Id)).ReturnsAsync((UserDetail)null);
            _mockUserManager.Setup(x => x.GeneratePasswordResetTokenAsync(user)).ReturnsAsync(token);

            _mockConfiguration.Setup(x => x.GetSection("BaseUrl").Value).Returns("http://example.com");
            _mockConfiguration.Setup(x => x.GetSection("SecretKeyForEncryptAndDecrypt").Value).Returns("1234567890123456");

            _mockEmailSender.Setup(x => x.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                            .ReturnsAsync("someCode");

            // Act
            var result = await _controller.ForgotPinSystemAdmin(forgotPinResourceSystemAdmin);
            var okResult = result as BadRequestObjectResult;

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(okResult);
            Assert.AreEqual(400, okResult.StatusCode);



        }

        [Test]
        public async Task ForgotPinSystemAdmin_ValidInput_ReturnsOk_DetailsNotCorrect()
        {
            // Arrange
            var forgotPinResourceSystemAdmin = new ForgotPinResourceSystemAdmin
            {
                Email = "test@example.com",
                PhoneNumber = "1234567890",
                ClientURI = "http://localhost:52000"

            };
            var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiQWRtaW4iLCJqdGkiOiIyYThkZTRlMS1lOGFjLTQ0MjItODhlNi00OGI2MmIyNjk3MGYiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJTdXBlckFkbWluIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvZW1haWxhZGRyZXNzIjoid2xlZWFkbWluQHdsZWVwYXkuY29tIiwiZXhwIjoxNzExNjI0MjI5LCJpc3MiOiJodHRwczovL3dsZWVwYXktYXBpLWtzMC5jb252ZXlvci5jbG91ZCIsImF1ZCI6Imh0dHA6Ly9sb2NhbGhvc3Q6NDIwMCJ9.WldbnSTyrZ8mYlaZkgw-0yBoREkYJ-0nX3xs1x8tQRo";

            var user = new ApplicationUser { Id = "1", Email = "wleeadmin@wlee.com" };
            var userDetails = new UserDetail { UserId = user.Id, PhoneNumber = forgotPinResourceSystemAdmin.PhoneNumber, FirstName = "john", Status = (int)Status.Active };

            _mockUserManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(user);
            _mockUserDetailsService.Setup(x => x.GetUserDetailsById(user.Id)).ReturnsAsync(userDetails);
            _mockUserManager.Setup(x => x.GeneratePasswordResetTokenAsync(user)).ReturnsAsync(token);

            _mockConfiguration.Setup(x => x.GetSection("BaseUrl").Value).Returns("http://example.com");
            _mockConfiguration.Setup(x => x.GetSection("SecretKeyForEncryptAndDecrypt").Value).Returns("1234567890123456");

            _mockEmailSender.Setup(x => x.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                            .ReturnsAsync("someCode");

            // Act
            var result = await _controller.ForgotPinSystemAdmin(forgotPinResourceSystemAdmin);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            bool success = (bool)(okResult.Value?.GetType().GetProperty("Success")?.GetValue(okResult.Value));
            string message = (string)(okResult.Value?.GetType().GetProperty("Message")?.GetValue(okResult.Value));
            Assert.IsFalse(success);
            Assert.AreEqual(Messages.DetailsNotCorrect.ToDescriptionString(), message);
        }

        [Test]
        public async Task VerifyPhoneForSystemAdmin_ValidVerification_ReturnsOk()
        {
            // Arrange
            var model = new SystemAdminVerificationModel
            {
                PhoneNumber = "1234567890",
                Code = "1234"
            };

            var verificationDetails = new Verification
            {
                VerificationExpiry = DateTime.UtcNow.AddSeconds(-30), // set expiry to 1 minute ago (expired)
                VerificationCode = model.Code,
                PhoneNumber = model.PhoneNumber,
            };

            _mockVerificationService.Setup(x => x.GetVerificationByPhone(model.PhoneNumber)).ReturnsAsync(verificationDetails);

            // Act
            var result = await _controller.VerifyPhoneForSystemAdmin(model);
            var okResult = result as OkObjectResult;
            bool success = (bool)(okResult.Value?.GetType().GetProperty("Success")?.GetValue(okResult.Value));
            string message = (string)(okResult.Value?.GetType().GetProperty("Message")?.GetValue(okResult.Value));
            bool isExpired = (bool)(okResult.Value?.GetType().GetProperty("IsExpired")?.GetValue(okResult.Value));

            // Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.IsFalse(isExpired);
            Assert.IsTrue(success);
            Assert.AreEqual(Messages.VerificationSuccessfull.ToDescriptionString(), message);
        }

        [Test]
        public async Task VerifyPhoneForSystemAdmin_ExpiredVerification_ReturnsOk()
        {
            // Arrange
            var model = new SystemAdminVerificationModel
            {
                PhoneNumber = "1234567890",
                Code = "1234"
            };

            var verificationDetails = new Verification
            {
                VerificationExpiry = DateTime.UtcNow.AddMinutes(-1), // set expiry to 1 minute ago (expired)
                VerificationCode = "1234",
                PhoneNumber = "1234567890",
            };

            _mockVerificationService.Setup(x => x.GetVerificationByPhone(model.PhoneNumber)).ReturnsAsync(verificationDetails);

            // Act
            var result = await _controller.VerifyPhoneForSystemAdmin(model);

            // Assert
            var okResult = result as OkObjectResult;
            bool success = (bool)(okResult.Value?.GetType().GetProperty("Success")?.GetValue(okResult.Value));
            string message = (string)(okResult.Value?.GetType().GetProperty("Message")?.GetValue(okResult.Value));
            bool isExpired = (bool)(okResult.Value?.GetType().GetProperty("IsExpired")?.GetValue(okResult.Value));

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.IsFalse(success);
            Assert.IsTrue(isExpired);
            Assert.AreEqual(Messages.OtpExpired.ToDescriptionString(), message);
        }

        [Test]
        public async Task VerifyPhoneForSystemAdmin_WrongOtp_ReturnsOk()
        {
            // Arrange
            var model = new SystemAdminVerificationModel
            {
                PhoneNumber = "1234567890",
                Code = "1234"
            };

            var verificationDetails = new Verification
            {
                VerificationExpiry = DateTime.UtcNow.AddSeconds(-30), // set expiry to 1 minute ago (expired)
                VerificationCode = "3454",
                PhoneNumber = "1234567890",
            };

            _mockVerificationService.Setup(x => x.GetVerificationByPhone(model.PhoneNumber)).ReturnsAsync(verificationDetails);

            // Act
            var result = await _controller.VerifyPhoneForSystemAdmin(model);

            var okResult = result as OkObjectResult;

            bool success = (bool)(okResult.Value?.GetType().GetProperty("Success")?.GetValue(okResult.Value));
            string message = (string)(okResult.Value?.GetType().GetProperty("Message")?.GetValue(okResult.Value));
            bool isExpired = (bool)(okResult.Value?.GetType().GetProperty("IsExpired")?.GetValue(okResult.Value));

            // Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.IsFalse(success);
            Assert.IsFalse(isExpired);
            Assert.AreEqual(Messages.WrongOtp.ToDescriptionString(), message);
        }

        [Test]
        public async Task ForgotPasswordForStaff_ValidInput_ReturnsOk()
        {
            // Arrange
            var forgotPasswordDto = new ForgotPasswordResource
            {
                Email = "test@example.com",
                PhoneNumber = "1234567890",
                Answer = "answer",
                SecurityQuestionId = 1,
                ClientURI = "http://example.com/reset-password"
            };

            var user = new ApplicationUser { Id = "1", Email = forgotPasswordDto.Email };

            var userDetails = new UserDetail
            {
                UserId = user.Id,
                PhoneNumber = forgotPasswordDto.PhoneNumber,
                SecurityAnswerText = forgotPasswordDto.Answer,
                SecurityQuestionId = forgotPasswordDto.SecurityQuestionId
            };
            var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiQWRtaW4iLCJqdGkiOiIyYThkZTRlMS1lOGFjLTQ0MjItODhlNi00OGI2MmIyNjk3MGYiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJTdXBlckFkbWluIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvZW1haWxhZGRyZXNzIjoid2xlZWFkbWluQHdsZWVwYXkuY29tIiwiZXhwIjoxNzExNjI0MjI5LCJpc3MiOiJodHRwczovL3dsZWVwYXktYXBpLWtzMC5jb252ZXlvci5jbG91ZCIsImF1ZCI6Imh0dHA6Ly9sb2NhbGhvc3Q6NDIwMCJ9.WldbnSTyrZ8mYlaZkgw-0yBoREkYJ-0nX3xs1x8tQRo";


            _mockUserManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(user);
            _mockUserDetailsService.Setup(x => x.GetUserDetailsById(user.Id)).ReturnsAsync(userDetails);
            _mockUserManager.Setup(x => x.GeneratePasswordResetTokenAsync(user)).ReturnsAsync(token);
            _mockConfiguration.Setup(x => x.GetSection("SecretKeyForEncryptAndDecrypt").Value).Returns("1234567890123456");
            _mockEmailSender.Setup(x => x.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                            .ReturnsAsync(Task.CompletedTask.ToString());

            // Act
            var result = await _controller.ForgotPasswordForStaff(forgotPasswordDto);

            // Assert
            var okResult = result as OkObjectResult;

            bool success = (bool)(okResult.Value?.GetType().GetProperty("Success")?.GetValue(okResult.Value));
            string message = (string)(okResult.Value?.GetType().GetProperty("Message")?.GetValue(okResult.Value));

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.IsTrue(success);
            Assert.AreEqual(Messages.LinkSentForResetPassword.ToDescriptionString(), message);
        }

        [Test]
        public async Task ForgotPasswordForStaff_InvalidUser_ReturnsBadRequest()
        {
            // Arrange
            var forgotPasswordDto = new ForgotPasswordResource
            {
                Email = "test@example.com",
                PhoneNumber = "1234567890",
                Answer = "answer",
                SecurityQuestionId = 1,
                ClientURI = "http://example.com/reset-password"
            };
            var user = new ApplicationUser { Id = "1", Email = forgotPasswordDto.Email };


            _mockUserManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync((ApplicationUser)null);
            //_mockUserManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync((ApplicationUser)null);
            _mockUserDetailsService.Setup(x => x.GetUserDetailsById(user.Id)).ReturnsAsync((UserDetail)null);

            // Act
            var result = await _controller.ForgotPasswordForStaff(forgotPasswordDto);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);
        }

        [Test]
        public async Task ForgotPasswordForStaff_InValidInput_ReturnsOk()
        {
            // Arrange
            var forgotPasswordDto = new ForgotPasswordResource
            {
                Email = "test@example.com",
                PhoneNumber = "1234567890",
                Answer = "answer",
                SecurityQuestionId = 1,
                ClientURI = "http://example.com/reset-password"
            };

            var user = new ApplicationUser { Id = "1", Email = forgotPasswordDto.Email };

            var userDetails = new UserDetail
            {
                UserId = user.Id,
                PhoneNumber = "63345364345",
                SecurityAnswerText = "erewtr",
                SecurityQuestionId = forgotPasswordDto.SecurityQuestionId
            };
            var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiQWRtaW4iLCJqdGkiOiIyYThkZTRlMS1lOGFjLTQ0MjItODhlNi00OGI2MmIyNjk3MGYiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJTdXBlckFkbWluIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvZW1haWxhZGRyZXNzIjoid2xlZWFkbWluQHdsZWVwYXkuY29tIiwiZXhwIjoxNzExNjI0MjI5LCJpc3MiOiJodHRwczovL3dsZWVwYXktYXBpLWtzMC5jb252ZXlvci5jbG91ZCIsImF1ZCI6Imh0dHA6Ly9sb2NhbGhvc3Q6NDIwMCJ9.WldbnSTyrZ8mYlaZkgw-0yBoREkYJ-0nX3xs1x8tQRo";


            _mockUserManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(user);
            _mockUserDetailsService.Setup(x => x.GetUserDetailsById(user.Id)).ReturnsAsync(userDetails);
            _mockUserManager.Setup(x => x.GeneratePasswordResetTokenAsync(user)).ReturnsAsync(token);
            _mockConfiguration.Setup(x => x.GetSection("SecretKeyForEncryptAndDecrypt").Value).Returns("1234567890123456");
            _mockEmailSender.Setup(x => x.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                            .ReturnsAsync(Task.CompletedTask.ToString());

            // Act
            var result = await _controller.ForgotPasswordForStaff(forgotPasswordDto);

            // Assert
            var okResult = result as OkObjectResult;

            bool success = (bool)(okResult.Value?.GetType().GetProperty("Success")?.GetValue(okResult.Value));
            string message = (string)(okResult.Value?.GetType().GetProperty("Message")?.GetValue(okResult.Value));

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.IsFalse(success);
            Assert.AreEqual(Messages.DetailsNotCorrect.ToDescriptionString(), message);
        }

        [Test]
        public async Task ResetPassword_ValidToken_ReturnsOk()
        {
            // Arrange
            var resetPasswordDto = new ResetPasswordResource
            {
                Email = "nzUAzZAabBtCpKSHb2O4CfbLo1Oq1CgsCp+orlmNCsI=",
                Token = "token",
                Password = "newpassword",
                ConfirmPassword = "newpassword"
            };

            var user = new ApplicationUser { Id = "1", Email = "test@example.com" };
            var userDetails = new UserDetail
            {
                UserId = user.Id,
                PhoneNumber = "2345678999976",
            };

            _mockConfiguration.Setup(x => x.GetSection("SecretKeyForEncryptAndDecrypt").Value).Returns("1234567890123456");
            _mockUserManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(user);
            _mockUserManager.Setup(x => x.VerifyUserTokenAsync(user, It.IsAny<string>(), "ResetPassword", resetPasswordDto.Token)).ReturnsAsync(true);
            _mockUserDetailsService.Setup(x => x.GetUserDetailsById(user.Id)).ReturnsAsync(userDetails);
            _mockUserManager.Setup(x => x.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.ConfirmPassword)).ReturnsAsync(IdentityResult.Success);
            _mockUserDetailsService.Setup(x => x.GetUserByRole((int)Role.SuperAdmin)).ReturnsAsync(new List<ApplicationUser>());

            _mockEmailSender.Setup(x => x.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                            .ReturnsAsync(Task.CompletedTask.ToString());

            // Act
            var result = await _controller.ResetPassword(resetPasswordDto);

            // Assert
            var okResult = result as OkObjectResult;
            bool success = (bool)(okResult.Value?.GetType().GetProperty("Success")?.GetValue(okResult.Value));
            string message = (string)(okResult.Value?.GetType().GetProperty("Message")?.GetValue(okResult.Value));

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.IsTrue(success);
            Assert.AreEqual(Messages.PasswordUpdated.ToDescriptionString(), message);
        }

        [Test]
        public async Task ResetPassword_InvalidToken_ReturnsUnauthorized()
        {
            // Arrange
            var resetPasswordDto = new ResetPasswordResource
            {
                Email = "nzUAzZAabBtCpKSHb2O4CfbLo1Oq1CgsCp+orlmNCsI=",
                Token = "invalidtoken",
                Password = "newpassword",
                ConfirmPassword = "newpassword"
            };

            var user = new ApplicationUser { Id = "1", Email = "test@example.com" };
            var userDetails = new UserDetail
            {
                UserId = user.Id,
                PhoneNumber = "2345678999976",
            };

            _mockConfiguration.Setup(x => x.GetSection("SecretKeyForEncryptAndDecrypt").Value).Returns("1234567890123456");
            _mockUserManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(user);
            _mockUserDetailsService.Setup(x => x.GetUserDetailsById(user.Id)).ReturnsAsync(userDetails);
            _mockUserManager.Setup(x => x.VerifyUserTokenAsync(user, It.IsAny<string>(), "ResetPassword", resetPasswordDto.Token)).ReturnsAsync(false);

            // Act
            var result = await _controller.ResetPassword(resetPasswordDto);

            // Assert
            var unauthorizedResult = result as UnauthorizedObjectResult;
            Assert.IsNotNull(unauthorizedResult);
            Assert.AreEqual(401, unauthorizedResult.StatusCode);
        }

        [Test]
        public async Task SetupLoginPasswordForStaff_ValidInput_ReturnsOk()
        {
            //encrypted string for email "nzUAzZAabBtCpKSHb2O4CfbLo1Oq1CgsCp+orlmNCsI="
            // Arrange
            var setupLogin = new SetupLoginResources
            {
                Email = "nzUAzZAabBtCpKSHb2O4CfbLo1Oq1CgsCp+orlmNCsI=",
                Password = "newpassword",
                ConfirmPassword = "newpassword",
                Answer = "answer",
                SecurityQuestionId = 1,
                Token = "valid token"
            };

            var user = new ApplicationUser { Id = "1", Email = "test@example.com" };
            var userDetails = new UserDetail { UserId = user.Id, PhoneNumber = "523254423523", SecurityAnswerText = "" };
            SystemActivityLog systemActivityLog = new();

            _mockConfiguration.Setup(x => x.GetSection("SecretKeyForEncryptAndDecrypt").Value).Returns("1234567890123456");
            _mockUserManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(user);
            _mockUserDetailsService.Setup(x => x.GetUserDetailsById(user.Id)).ReturnsAsync(userDetails);
            _mockUserManager.Setup(x => x.AddPasswordAsync(user, setupLogin.ConfirmPassword)).ReturnsAsync(IdentityResult.Success);
            _mockUserDetailsService.Setup(x => x.AddOrUpdateUserDetails(userDetails)).ReturnsAsync(userDetails);
            _mockEmailSender.Setup(x => x.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(Task.CompletedTask.ToString());
            _mockUserDetailsService.Setup(x => x.GetUserByRole((int)Role.SystemAdmin)).ReturnsAsync(new List<ApplicationUser>());
            _mockSystemActivityLogsService.Setup(x => x.AddSystemLogs(It.IsAny<SystemActivityLog>())).ReturnsAsync(systemActivityLog);

            // Act
            var result = await _controller.SetupLoginPasswordForStaff(setupLogin);

            // Assert
            var okResult = result as OkObjectResult;
            bool success = (bool)(okResult.Value?.GetType().GetProperty("Success")?.GetValue(okResult.Value));
            string message = (string)(okResult.Value?.GetType().GetProperty("Message")?.GetValue(okResult.Value));

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.IsTrue(success);
            Assert.AreEqual(Messages.PasswordGenerated.ToDescriptionString(), message);
        }

        [Test]
        public async Task SetupLoginPasswordForStaff_InvalidUser_ReturnsBadRequest()
        {
            // Arrange
            var setupLogin = new SetupLoginResources
            {
                Email = "nzUAzZAabBtCpKSHb2O4CfbLo1Oq1CgsCp+orlmNCsI=",
                Password = "newpassword",
                ConfirmPassword = "newpassword",
                Answer = "answer",
                SecurityQuestionId = 1,
                Token = "valid token"
            };

            _mockConfiguration.Setup(x => x.GetSection("SecretKeyForEncryptAndDecrypt").Value).Returns("1234567890123456");
            _mockUserManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync((ApplicationUser)null);

            // Act
            var result = await _controller.SetupLoginPasswordForStaff(setupLogin);

            // Assert
            var okResult = result as OkObjectResult;
            bool success = (bool)(okResult.Value?.GetType().GetProperty("Success")?.GetValue(okResult.Value));
            string message = (string)(okResult.Value?.GetType().GetProperty("Message")?.GetValue(okResult.Value));

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.IsFalse(success);
            Assert.AreEqual(Messages.InvalidRequest.ToDescriptionString(), message);
        }

        [Test]
        public async Task GetStaffRoles_ReturnsOk()
        {
            // Arrange
            var staffRoles = new List<StaffRoles>
            {
                new StaffRoles { Id = 1, Name = "Role1" },
                new StaffRoles { Id = 2, Name = "Role2" },
                new StaffRoles { Id = 3, Name = "Role3" }
            };

            _mockLookupService.Setup(x => x.GetAllStaffRoles()).ReturnsAsync(staffRoles);

            // Act
            var result = await _controller.GetStaffRoles();

            // Assert
            var okResult = result as OkObjectResult;
            var staffRolesResult = okResult.Value as List<StaffRoles>;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreSame(staffRoles, staffRolesResult);
        }

        [Test]
        public async Task GetStaffPermissions_ReturnsOk()
        {
            // Arrange
            var staffPermissions = new List<StaffPermissionType>
            {
                new StaffPermissionType { Id = 1, Name = "Permission1" },
                new StaffPermissionType { Id = 2, Name = "Permission2" },
                new StaffPermissionType { Id = 3, Name = "Permission3" }
            };

            _mockLookupService.Setup(x => x.GetAllStaffPermissions()).ReturnsAsync(staffPermissions);

            // Act
            var result = await _controller.GetStaffPermissions();

            // Assert
            var okResult = result as OkObjectResult;
            var staffPermissionsResult = okResult.Value as List<StaffPermissionType>;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreSame(staffPermissions, staffPermissionsResult);
        }

        [Test]
        public async Task GetSecurityQuestions_ReturnsOk()
        {
            // Arrange
            var securityQuestions = new List<SecurityQuestions>
            {
                new SecurityQuestions { Id = 1, Question = "Question1" },
                new SecurityQuestions { Id = 2, Question = "Question2" },
                new SecurityQuestions { Id = 3, Question = "Question3" }
            };

            _mockLookupService.Setup(x => x.GetAllSecurityQuestions()).ReturnsAsync(securityQuestions);

            // Act
            var result = await _controller.GetSecurityQuestions();

            // Assert
            var okResult = result as OkObjectResult;
            var securityQuestionsResult = okResult.Value as List<SecurityQuestions>;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreSame(securityQuestions, securityQuestionsResult);
        }

        [Test]
        public async Task SetPinForSystemAdmin_UserFound_SetsPinAndSendsEmail()
        {
            // Arrange
            var setupLogin = new SetPinResources
            {
                Email = "nzUAzZAabBtCpKSHb2O4CfbLo1Oq1CgsCp+orlmNCsI=",
                ConfirmPin = "11118888wleepay@",
                Pin = "11118888wleepay@"
            };

            var user = new ApplicationUser { Id = "1", Email = "test@example.com" };
            var userDetails = new UserDetail { UserId = user.Id, FirstName = "John", PhoneNumber = "1234567890" };
            SystemActivityLog systemActivityLog = new();

            _mockConfiguration.Setup(x => x.GetSection("SecretKeyForEncryptAndDecrypt").Value).Returns("1234567890123456");
            _mockUserManager.Setup(x => x.FindByEmailAsync("test@example.com")).ReturnsAsync(user);
            _mockUserDetailsService.Setup(x => x.GetUserDetailsById(user.Id)).ReturnsAsync(userDetails);
            _mockUserManager.Setup(x => x.AddPasswordAsync(user, setupLogin.ConfirmPin)).ReturnsAsync(IdentityResult.Success);
            _mockEmailSender.Setup(x => x.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(Task.CompletedTask.ToString());
            _mockSystemActivityLogsService.Setup(x => x.AddSystemLogs(It.IsAny<SystemActivityLog>())).ReturnsAsync(systemActivityLog);

            // Act
            var result = await _controller.SetPinForSystemAdmin(setupLogin);

            // Assert
            var okResult = result as OkObjectResult;

            bool success = (bool)(okResult.Value?.GetType().GetProperty("Success")?.GetValue(okResult.Value));
            string message = (string)(okResult.Value?.GetType().GetProperty("Message")?.GetValue(okResult.Value));

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.IsTrue(success);
            Assert.AreEqual(Messages.PasswordGenerated.ToDescriptionString(), message);
        }

        [Test]
        public async Task SetPinForSystemAdmin_UserNotFound_ReturnsNotFound()
        {
            // Arrange
            var setupLogin = new SetPinResources
            {
                Email = "nzUAzZAabBtCpKSHb2O4CfbLo1Oq1CgsCp+orlmNCsI=",
                ConfirmPin = "11118888wleepay@",
                Pin = "11118888wleepay@"
            };

            var user = new ApplicationUser { Id = "1", Email = "test@example.com" };
            var userDetails = new UserDetail { UserId = user.Id, FirstName = "John", PhoneNumber = "1234567890" };
            SystemActivityLog systemActivityLog = new();

            _mockConfiguration.Setup(x => x.GetSection("SecretKeyForEncryptAndDecrypt").Value).Returns("1234567890123456");
            _mockUserManager.Setup(x => x.FindByEmailAsync("tester@example.com")).ReturnsAsync((ApplicationUser)null);

            // Act
            var result = await _controller.SetPinForSystemAdmin(setupLogin);

            // Assert
            var okResult = result as OkObjectResult;
            bool success = (bool)(okResult.Value?.GetType().GetProperty("Success")?.GetValue(okResult.Value));
            string message = (string)(okResult.Value?.GetType().GetProperty("Message")?.GetValue(okResult.Value));

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.IsFalse(success);
            Assert.AreEqual(Messages.InvalidRequest.ToDescriptionString(), message);
        }

        [Test]
        public async Task RegisterStaff_ValidInput_CreatesUserAndReturnsToken()
        {
            // Arrange

            var userDetail = new AddStaffDetailsResource
            {
                EmailId = "test@example.com",
                Role = Role.Staff, // Assuming role is not 0 (SuperAdmin)
                PhoneNumber = "1234567890",
                PermissionTypes = null,
                CountyId = 5,
                Gender = "male",
                FirstName = "john",
                KYCStatusId = 1
                // Populate other properties as needed for valid input
            };
            var newUser = new ApplicationUser { Id = Guid.NewGuid().ToString(), Email = "test@example.com" };
            var userDetails = new UserDetail { UserId = newUser.Id };
            UsersStaffRole usersStaffRole = new();
            var validationResult = new ValidationResult();
            var userDetailsToInsert = new UserDetail(); // Create an instance of UserDetail
            var token = "token";

            _mockValidator.Setup(x => x.ValidateAsync(It.IsAny<ValidationContext<AddStaffDetailsResource>>(), default))
                .ReturnsAsync(validationResult);
            _mockConfiguration.Setup(x => x.GetSection("BaseUrl").Value).Returns("http://example.com");
            _mockUserManager.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(IdentityResult.Success);
            _mockUserManager.Setup(x => x.AddToRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
            _mockUserDetailsService.Setup(x => x.AddOrUpdateUserDetails(It.IsAny<UserDetail>())).ReturnsAsync(userDetails);
            _mockUsersStaffRoleService.Setup(x => x.AddUserStaffRole(It.IsAny<UsersStaffRole>())).ReturnsAsync(usersStaffRole);
            _mockUserManager.Setup(x => x.FindByEmailAsync(userDetail.EmailId)).ReturnsAsync((ApplicationUser)null);
            _mockUserDetailsService.Setup(x => x.IsPhoneAlreadyRegistered(It.IsAny<string>())).ReturnsAsync(false);
            _mockMapper.Setup(m => m.Map<AddStaffDetailsResource, UserDetail>(It.IsAny<AddStaffDetailsResource>()))
                .Returns(userDetailsToInsert);
            _mockUserDetailsService.Setup(x => x.GenerateJSONWebToken(userDetails, userDetail.Role.ToString())).ReturnsAsync(token);

            // Act
            var result = await _controller.RegisterStaff(userDetail);

            // Assert
            var okResult = result as OkObjectResult;
            bool success = (bool)(okResult.Value?.GetType().GetProperty("Success")?.GetValue(okResult.Value));
            //string resultedToken = (string)(okResult.Value?.GetType().GetProperty("Token")?.GetValue(okResult.Value));
            string userId = (string)(okResult.Value?.GetType().GetProperty("UserId")?.GetValue(okResult.Value));

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.IsTrue(success);
            //Assert.IsNotNull(resultedToken);
            Assert.IsNotNull(userId);

        }

        [Test]
        public async Task RegisterStaff_InvalidRole_ReturnsBadRequest()
        {
            // Arrange
            _mockValidator = new Mock<IValidator<AddStaffDetailsResource>>();

            var userDetail = new AddStaffDetailsResource
            {
                EmailId = "test@example.com",
                Role = Role.SuperAdmin, // Assuming role is not 0 (SuperAdmin)
                PhoneNumber = "1234567890",
                PermissionTypes = null,
                CountyId = 5,
                Gender = "male",
                FirstName = "john",
                KYCStatusId = 1
                // Populate other properties as needed for valid input
            };

            var validationResult = new ValidationResult();

            _mockValidator.Setup(x => x.ValidateAsync(It.IsAny<ValidationContext<AddStaffDetailsResource>>(), default)).ReturnsAsync(validationResult);
            _mockConfiguration.Setup(x => x.GetSection("BaseUrl").Value).Returns("http://example.com");

            // Act
            var result = await _controller.RegisterStaff(userDetail);

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
            _mockValidator = new Mock<IValidator<AddStaffDetailsResource>>();

            var userDetail = new AddStaffDetailsResource
            {
                EmailId = "test@example.com",
                Role = Role.Staff, // Assuming role is not 0 (SuperAdmin)
                PhoneNumber = "1234567890",
                PermissionTypes = null,
                CountyId = 5,
                Gender = "male",
                FirstName = "john",
                KYCStatusId = 1
                // Populate other properties as needed for valid input
            };
            var validationResult = new ValidationResult();
            var newUser = new ApplicationUser { Id = "123456", Email = "test@example.com" };
            var userDetails = new UserDetail { UserId = newUser.Id };

            _mockValidator.Setup(x => x.ValidateAsync(It.IsAny<ValidationContext<AddStaffDetailsResource>>(), default)).ReturnsAsync(validationResult);
            _mockConfiguration.Setup(x => x.GetSection("BaseUrl").Value).Returns("http://example.com");
            _mockUserManager.Setup(x => x.FindByEmailAsync(userDetail.EmailId)).ReturnsAsync(newUser);
            _mockUserDetailsService.Setup(x => x.IsPhoneAlreadyRegistered(It.IsAny<string>())).ReturnsAsync(true);

            // Act
            var result = await _controller.RegisterStaff(userDetail);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);
            Assert.AreEqual(Messages.PhoneAndEmailAlreadyUsed.ToDescriptionString(), badRequestResult.Value.ToString());
            // Add assertions for returned message
        }

        [Test]
        public async Task RegisterStaff_UserCreationFailed_ReturnsBadRequest()
        {
            // Arrange
            _mockValidator = new Mock<IValidator<AddStaffDetailsResource>>();

            var userDetail = new AddStaffDetailsResource
            {
                EmailId = "test@example.com",
                Role = Role.Staff, // Assuming role is not 0 (SuperAdmin)
                PhoneNumber = "1234567890",
                PermissionTypes = null,
                CountyId = 5,
                Gender = "male",
                FirstName = "john",
                KYCStatusId = 1
                // Populate other properties as needed for valid input
            };
            var validationResult = new ValidationResult();
            var newUser = new ApplicationUser { Id = "123456", Email = "test@example.com" };
            var userDetails = new UserDetail { UserId = newUser.Id };
            IdentityResult identityResult = new();
            IdentityError identityError = new IdentityError();


            _mockUserManager.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(IdentityResult.Failed(identityError));
            _mockValidator.Setup(x => x.ValidateAsync(It.IsAny<ValidationContext<AddStaffDetailsResource>>(), default)).ReturnsAsync(validationResult);
            _mockConfiguration.Setup(x => x.GetSection("BaseUrl").Value).Returns("http://example.com");
            _mockUserManager.Setup(x => x.FindByEmailAsync(userDetail.EmailId)).ReturnsAsync((ApplicationUser)null);
            _mockUserDetailsService.Setup(x => x.IsPhoneAlreadyRegistered(It.IsAny<string>())).ReturnsAsync(false);

            // Act
            var result = await _controller.RegisterStaff(userDetail);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);
            // Add assertions for returned message
        }

        [Test]
        public async Task RegisterSystemAdmin_ValidInput_CreatesUserAndReturnsToken()
        {
            // Arrange

            var userDetail = new AddSystemAdminResource
            {
                EmailId = "test@example.com",
                Role = Role.SystemAdmin, // Assuming role is not 0 (SuperAdmin)
                PhoneNumber = "1234567890",
                CountyId = 5,
                Gender = "male",
                FirstName = "john",
                KYCStatusId = 1,
                Otp = "1234"
                // Populate other properties as needed for valid input
            };
            var newUser = new ApplicationUser { Id = Guid.NewGuid().ToString(), Email = "test@example.com" };
            var userDetails = new UserDetail { UserId = newUser.Id,
                VerificationCode = "1234"
            };
            UsersStaffRole usersStaffRole = new();
            var validationResult = new ValidationResult();
            var userDetailsToInsert = new UserDetail(); // Create an instance of UserDetail
            var token = "token";
            var verification = new Verification
            {
                Id = 1,
                PhoneNumber = "1234567890",
                VerificationCode = "1234",
                VerificationExpiry = DateTime.UtcNow.AddHours(1), // Set expiry time to 1 hour from now
                CreatedDate = DateTime.UtcNow
            };

            _mockValidator.Setup(x => x.ValidateAsync(It.IsAny<ValidationContext<AddStaffDetailsResource>>(), default))
                .ReturnsAsync(validationResult);
            _mockConfiguration.Setup(x => x.GetSection("BaseUrl").Value).Returns("http://example.com");
            _mockUserManager.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(IdentityResult.Success);
            _mockUserManager.Setup(x => x.AddToRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
            _mockUserDetailsService.Setup(x => x.AddOrUpdateUserDetails(It.IsAny<UserDetail>())).ReturnsAsync(userDetails);
            _mockUsersStaffRoleService.Setup(x => x.AddUserStaffRole(It.IsAny<UsersStaffRole>())).ReturnsAsync(usersStaffRole);
            _mockUserManager.Setup(x => x.FindByEmailAsync(userDetail.EmailId)).ReturnsAsync((ApplicationUser)null);
            _mockUserDetailsService.Setup(x => x.IsPhoneAlreadyRegistered(It.IsAny<string>())).ReturnsAsync(false);
            _mockMapper.Setup(m => m.Map<AddSystemAdminResource, UserDetail>(It.IsAny<AddSystemAdminResource>()))
                .Returns(userDetailsToInsert);
            _mockUserDetailsService.Setup(x => x.GenerateJSONWebToken(userDetails, userDetail.Role.ToString())).ReturnsAsync(token);
            _mockVerificationService.Setup(x => x.GetVerificationByPhone(userDetail.PhoneNumber)).ReturnsAsync(verification);


            // Act
            var result = await _controller.RegisterSystemAdmin(userDetail);

            // Assert
            var okResult = result as OkObjectResult;
            bool success = (bool)(okResult.Value?.GetType().GetProperty("Success")?.GetValue(okResult.Value));
            //string resultedToken = (string)(okResult.Value?.GetType().GetProperty("Token")?.GetValue(okResult.Value));
            string userId = (string)(okResult.Value?.GetType().GetProperty("UserId")?.GetValue(okResult.Value));

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.IsTrue(success);
            //Assert.IsNotNull(resultedToken);
            Assert.IsNotNull(userId);

        }

        [Test]
        public async Task RegisterSystemAdmin_InvalidRole_ReturnsBadRequest()
        {
            // Arrange

            var userDetail = new AddSystemAdminResource
            {
                EmailId = "test@example.com",
                Role = Role.SuperAdmin, // Assuming role is not 0 (SuperAdmin)
                PhoneNumber = "1234567890",
                CountyId = 5,
                Gender = "male",
                FirstName = "john",
                KYCStatusId = 1
                // Populate other properties as needed for valid input
            };

            var validationResult = new ValidationResult();

            _mockValidator.Setup(x => x.ValidateAsync(It.IsAny<ValidationContext<AddStaffDetailsResource>>(), default)).ReturnsAsync(validationResult);
            _mockConfiguration.Setup(x => x.GetSection("BaseUrl").Value).Returns("http://example.com");

            // Act
            var result = await _controller.RegisterSystemAdmin(userDetail);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);
            Assert.AreEqual(Messages.IsSuperAdmin.ToDescriptionString(), badRequestResult.Value.ToString());
            // Add assertions for returned message
        }

        [Test]
        public async Task RegisterSystemAdmin_PhoneNumberAndEmailExists_ReturnsBadRequest()
        {
            // Arrange

            var userDetail = new AddSystemAdminResource
            {
                EmailId = "test@example.com",
                Role = Role.SystemAdmin, // Assuming role is not 0 (SuperAdmin)
                PhoneNumber = "1234567890",
                CountyId = 5,
                Gender = "male",
                FirstName = "john",
                KYCStatusId = 1
                // Populate other properties as needed for valid input
            };
            var validationResult = new ValidationResult();
            var newUser = new ApplicationUser { Id = "123456", Email = "test@example.com" };
            var userDetails = new UserDetail { UserId = newUser.Id };

            _mockValidator.Setup(x => x.ValidateAsync(It.IsAny<ValidationContext<AddStaffDetailsResource>>(), default)).ReturnsAsync(validationResult);
            _mockConfiguration.Setup(x => x.GetSection("BaseUrl").Value).Returns("http://example.com");
            _mockUserManager.Setup(x => x.FindByEmailAsync(userDetail.EmailId)).ReturnsAsync(newUser);
            _mockUserDetailsService.Setup(x => x.IsPhoneAlreadyRegistered(It.IsAny<string>())).ReturnsAsync(true);

            // Act
            var result = await _controller.RegisterSystemAdmin(userDetail);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);
            Assert.AreEqual(Messages.PhoneAndEmailAlreadyUsed.ToDescriptionString(), badRequestResult.Value.ToString());
            // Add assertions for returned message
        }

        [Test]
        public async Task RegisterSystemAdmin_UserCreationFailed_ReturnsBadRequest()
        {
            // Arrange
            var userDetail = new AddSystemAdminResource
            {
                EmailId = "test@example.com",
                Role = Role.SystemAdmin, // Assuming role is not 0 (SuperAdmin)
                PhoneNumber = "1234567890",
                CountyId = 5,
                Gender = "male",
                FirstName = "john",
                KYCStatusId = 1
                // Populate other properties as needed for valid input
            };
            var validationResult = new ValidationResult();
            var newUser = new ApplicationUser { Id = "123456", Email = "test@example.com" };
            var userDetails = new UserDetail { UserId = newUser.Id };
            IdentityResult identityResult = new();
            IdentityError identityError = new IdentityError();


            _mockUserManager.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(IdentityResult.Failed(identityError));
            _mockValidator.Setup(x => x.ValidateAsync(It.IsAny<ValidationContext<AddStaffDetailsResource>>(), default)).ReturnsAsync(validationResult);
            _mockConfiguration.Setup(x => x.GetSection("BaseUrl").Value).Returns("http://example.com");
            _mockUserManager.Setup(x => x.FindByEmailAsync(userDetail.EmailId)).ReturnsAsync((ApplicationUser)null);
            _mockUserDetailsService.Setup(x => x.IsPhoneAlreadyRegistered(It.IsAny<string>())).ReturnsAsync(false);

            // Act
            var result = await _controller.RegisterSystemAdmin(userDetail);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);
            // Add assertions for returned message
        }

        [Test]
        public async Task CheckIfAccountAlreadySetup_UserNotFound_ReturnsNotFoundMessage()
        {
            // Arrange
            var resource = new AccountExistVerificationResource { Email = "nzUAzZAabBtCpKSHb2O4CfbLo1Oq1CgsCp+orlmNCsI=" };

            _mockConfiguration.Setup(x => x.GetSection("SecretKeyForEncryptAndDecrypt").Value).Returns("1234567890123456");
            _mockUserManager.Setup(m => m.FindByEmailAsync(resource.Email)).ReturnsAsync((ApplicationUser)null);

            // Act
            var result = await _controller.CheckIfAccountAlreadySetup(resource);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);

            bool success = (bool)(okResult.Value?.GetType().GetProperty("Success")?.GetValue(okResult.Value));
            string message = (string)(okResult.Value?.GetType().GetProperty("Message")?.GetValue(okResult.Value));

            Assert.IsFalse(success);
            Assert.AreEqual(Messages.NotFound.ToDescriptionString(), message);
        }

        [Test]
        public async Task CheckIfAccountAlreadySetup_AccountNotSetup_ReturnsAccountNotSetupMessage()
        {
            // Arrange
            var resource = new AccountExistVerificationResource { Email = "nzUAzZAabBtCpKSHb2O4CfbLo1Oq1CgsCp+orlmNCsI=" };

            _mockConfiguration.Setup(x => x.GetSection("SecretKeyForEncryptAndDecrypt").Value).Returns("1234567890123456");
             var user = new ApplicationUser() { }; // assuming user exists but has no password set
            _mockUserManager.Setup(m => m.FindByEmailAsync("test@example.com")).ReturnsAsync(user);
            _mockUserManager.Setup(m => m.HasPasswordAsync(user)).ReturnsAsync(false);

            // Act
            var result = await _controller.CheckIfAccountAlreadySetup(resource);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);

            bool success = (bool)(okResult.Value?.GetType().GetProperty("Success")?.GetValue(okResult.Value));
            string message = (string)(okResult.Value?.GetType().GetProperty("Message")?.GetValue(okResult.Value));

            Assert.IsTrue(success);
            Assert.AreEqual(Messages.AccountNotAlreadySetup.ToDescriptionString(), message);
        }

        [Test]
        public async Task CheckIfAccountAlreadySetup_AccountSetup_ReturnsAccountSetupMessage()
        {
            // Arrange
            var resource = new AccountExistVerificationResource { Email = "nzUAzZAabBtCpKSHb2O4CfbLo1Oq1CgsCp+orlmNCsI=" };

            _mockConfiguration.Setup(x => x.GetSection("SecretKeyForEncryptAndDecrypt").Value).Returns("1234567890123456");
            var user = new ApplicationUser() { }; // assuming user exists but has no password set
            _mockUserManager.Setup(m => m.FindByEmailAsync("test@example.com")).ReturnsAsync(user);
            _mockUserManager.Setup(m => m.HasPasswordAsync(user)).ReturnsAsync(true);

            // Act
            var result = await _controller.CheckIfAccountAlreadySetup(resource);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);

            bool success = (bool)(okResult.Value?.GetType().GetProperty("Success")?.GetValue(okResult.Value));
            string message = (string)(okResult.Value?.GetType().GetProperty("Message")?.GetValue(okResult.Value));

            Assert.IsFalse(success);
            Assert.AreEqual(Messages.AccountAlreadySetup.ToDescriptionString(), message);
        }

        [Test]
        public async Task CheckIfLinkIsExpired_InvalidModelState_ReturnsBadRequest()
        {
            // Arrange           
            var resource = new CheckTokenExpirationResource() { };

            // Act
            var result = await _controller.CheckIfLinkIsExpired(resource);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public async Task CheckIfLinkIsExpired_UserNotFound_ReturnsBadRequestWithNotFoundMessage()
        {
            // Arrange
            var resource = new CheckTokenExpirationResource { Email = "nzUAzZAabBtCpKSHb2O4CfbLo1Oq1CgsCp+orlmNCsI=", Token = "token" };

            _mockConfiguration.Setup(x => x.GetSection("SecretKeyForEncryptAndDecrypt").Value).Returns("1234567890123456");
            _mockUserManager.Setup(m => m.FindByEmailAsync("test@example.com")).ReturnsAsync((ApplicationUser)null);

            // Act
            var result = await _controller.CheckIfLinkIsExpired(resource);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.NotNull(badRequestResult);

            string message = (string)badRequestResult.Value;
            Assert.AreEqual(Messages.NotFound.ToDescriptionString(), message);
        }

        [Test]
        public async Task CheckIfLinkIsExpired_ValidToken_ReturnsResetPasswordMessage()
        {
            // Arrange
            var resource = new CheckTokenExpirationResource { Email = "nzUAzZAabBtCpKSHb2O4CfbLo1Oq1CgsCp+orlmNCsI=", Token = "validToken" };
            var user = new ApplicationUser();
            _mockConfiguration.Setup(x => x.GetSection("SecretKeyForEncryptAndDecrypt").Value).Returns("1234567890123456");
            _mockUserManager.Setup(m => m.FindByEmailAsync("test@example.com")).ReturnsAsync(user);
            _mockUserManager.Setup(m => m.VerifyUserTokenAsync(user, It.IsAny<string>(), It.IsAny<string>(), resource.Token)).ReturnsAsync(true);

            // Act
            var result = await _controller.CheckIfLinkIsExpired(resource);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);

            bool success = (bool)(okResult.Value?.GetType().GetProperty("Success")?.GetValue(okResult.Value));
            string message = (string)(okResult.Value?.GetType().GetProperty("Message")?.GetValue(okResult.Value));

            Assert.IsTrue(success);
            Assert.AreEqual(Messages.ResetPassword.ToDescriptionString(), message);
        }

        [Test]
        public async Task CheckIfLinkIsExpired_ExpiredToken_ReturnsLinkExpiredMessage()
        {
            // Arrange
            var resource = new CheckTokenExpirationResource { Email = "nzUAzZAabBtCpKSHb2O4CfbLo1Oq1CgsCp+orlmNCsI=", Token = "expiredToken" };
            var user = new ApplicationUser();
            _mockConfiguration.Setup(x => x.GetSection("SecretKeyForEncryptAndDecrypt").Value).Returns("1234567890123456");
            _mockUserManager.Setup(m => m.FindByEmailAsync("test@example.com")).ReturnsAsync(user);
            _mockUserManager.Setup(m => m.VerifyUserTokenAsync(user, It.IsAny<string>(), It.IsAny<string>(), resource.Token)).ReturnsAsync(false);

            // Act
            var result = await _controller.CheckIfLinkIsExpired(resource);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);

            bool success = (bool)(okResult.Value?.GetType().GetProperty("Success")?.GetValue(okResult.Value));
            string message = (string)(okResult.Value?.GetType().GetProperty("Message")?.GetValue(okResult.Value));

            Assert.IsFalse(success);
            Assert.AreEqual(Messages.LinkExpired.ToDescriptionString(), message);
        }

        // Add more test cases as needed


        // Add more test cases following a similar structure for other scenarios


    }
}



