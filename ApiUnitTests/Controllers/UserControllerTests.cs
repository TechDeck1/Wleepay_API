using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wleepay.API.Controllers;
using wleepay.API.Resources;
using wleepay.Communication.Interface;
using wleepay.Core.Enums;
using wleepay.Core.Models;
using wleepay.Core.Services;
using wleepay.Services;
using wleepay.Storage.Interface;
using wleepay.Storage.Services;

namespace ApiUnitTests.Controllers
{
    public class UserControllerTests
    {
        private UserController _controller;
        private Mock<IMapper> _mockMapper;
        private Mock<IValidator<CreateTransactionResource>> _mockValidator;
        private Mock<IUserDetailService> _mockUserDetailService;
        private Mock<UserManager<ApplicationUser>> _mockUserManager;
        private Mock<IKYCDocumentsService> _mockKYCDocumentsService;
        private Mock<IAddressService> _mockAddressService;
        private Mock<IUploadDocument> _mockUploadDocument;
        private Mock<IUsersStaffRoleService> _mockUsersStaffRoleService;
        private Mock<ILogger<UserController>> _mockLogger;
        private Mock<IUploadDocument> _mockStorageService;
        private Mock<ISmileIdentityJobService> _mockSmileIdentityJobService;

        [SetUp]
        public void Setup()
        {
            _mockMapper = new Mock<IMapper>();
            _mockValidator = new Mock<IValidator<CreateTransactionResource>>();
            _mockUserDetailService = new Mock<IUserDetailService>();
            _mockUserManager = new Mock<UserManager<ApplicationUser>>(Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);
            _mockKYCDocumentsService = new Mock<IKYCDocumentsService>();
            _mockAddressService = new Mock<IAddressService>();
            _mockUploadDocument = new Mock<IUploadDocument>();
            _mockUsersStaffRoleService = new Mock<IUsersStaffRoleService>();
            _mockLogger = new Mock<ILogger<UserController>>();
            _mockSmileIdentityJobService = new Mock<ISmileIdentityJobService>();
            _mockStorageService = new Mock<IUploadDocument>();


            _controller = new UserController(
                 _mockUserManager.Object,
                 _mockUserDetailService.Object,
                 _mockUploadDocument.Object,
                 _mockAddressService.Object,
                 _mockKYCDocumentsService.Object,
                 _mockSmileIdentityJobService.Object,
                 _mockMapper.Object,
                 _mockLogger.Object,
                 _mockUsersStaffRoleService.Object,
                 _mockStorageService.Object

             );

        }

        [Test]
        public async Task GetById_ReturnsOkWithUserDetailResource_WhenUserExists()
        {
            // Arrange
            string userId = "123"; // Assuming this userId exists
            var userDetail = new UserDetail { /* Initialize user detail properties */ };
            var userResource = new UserDetailResource { /* Initialize user resource properties */ };

            _mockUserDetailService.Setup(x => x.GetUserDetailsById(userId)).ReturnsAsync(userDetail);
            _mockMapper.Setup(x => x.Map<UserDetail, UserDetailResource>(userDetail)).Returns(userResource);

            // Act
            var result = await _controller.GetById(userId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.AreEqual(userResource, okResult.Value);
        }

        [Test]
        public async Task GetById_ReturnsBadRequest_WhenUserIdIsNull()
        {
            // Arrange
            string nullUserId = null;

            // Act
            var result = await _controller.GetById(nullUserId);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public async Task GetById_ReturnsBadRequest_WhenUserDoesNotExist()
        {
            // Arrange
            string nonExistentUserId = "999"; // Assuming this userId doesn't exist
            _mockUserDetailService.Setup(x => x.GetUserDetailsById(nonExistentUserId)).ReturnsAsync((UserDetail)null);

            // Act
            var result = await _controller.GetById(nonExistentUserId);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public async Task GetById_ThrowsException_WhenExceptionOccurs()
        {
            // Arrange
            string userId = "123"; // Assuming this userId exists
            _mockUserDetailService.Setup(x => x.GetUserDetailsById(userId)).ThrowsAsync(new Exception("Some error occurred."));

            // Act & Assert
            Assert.ThrowsAsync<Exception>(() => _controller.GetById(userId));
        }

        [Test]
        public async Task GetApplicationUserById_ReturnsOkWithUserResource_WhenUserExists()
        {
            // Arrange
            string userId = "123"; // Assuming this userId exists
            var applicationUser = new ApplicationUser()
            {
                UserDetail = new UserDetail()
                {
                    Image = "image",
                    KYCDocumentImage = "kycimage"
                }
            };
            var userDetail = new UserDetail { };
            var userResource = new ApplicationUserResource { };

            _mockUserDetailService.Setup(x => x.GetApplicationUserById(userId)).ReturnsAsync((applicationUser));
            _mockMapper.Setup(x => x.Map<ApplicationUser, ApplicationUserResource>(applicationUser)).Returns(userResource);

            // Act
            var result = await _controller.GetApplicationUserById(userId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.AreEqual(userResource, okResult.Value);
        }

        [Test]
        public async Task GetApplicationUserById_ReturnsBadRequest_WhenUserIdIsNull()
        {
            // Arrange
            string nullUserId = null;

            // Act
            var result = await _controller.GetApplicationUserById(nullUserId);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public async Task GetApplicationUserById_ReturnsBadRequest_WhenUserDoesNotExist()
        {
            // Arrange
            string nonExistentUserId = "999"; // Assuming this userId doesn't exist
            _mockUserDetailService.Setup(x => x.GetApplicationUserById(nonExistentUserId)).ReturnsAsync((ApplicationUser)(null));

            // Act
            var result = await _controller.GetApplicationUserById(nonExistentUserId);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public async Task GetApplicationUserById_ThrowsException_WhenExceptionOccurs()
        {
            // Arrange
            string userId = "123"; // Assuming this userId exists
            _mockUserDetailService.Setup(x => x.GetApplicationUserById(userId)).ThrowsAsync(new Exception("Some error occurred."));

            // Act & Assert
            Assert.ThrowsAsync<Exception>(() => _controller.GetApplicationUserById(userId));
        }

        [Test]
        public async Task SearchUser_ReturnsOkWithUserResource_WhenPhoneNumberProvided()
        {
            // Arrange
            var request = new SearchRequest { PhoneNumber = "1234567890", Role = Role.SuperAdmin }; // Provide valid phone number and role
            var userDetail = new ApplicationUser { /* Initialize application user properties */ };
            var userResource = new ApplicationUserResource { /* Initialize user resource properties */ };

            _mockUserDetailService.Setup(x => x.GetUserAccountByPhone(request.PhoneNumber, request.Role.GetHashCode())).ReturnsAsync(userDetail);
            _mockMapper.Setup(x => x.Map<ApplicationUser, ApplicationUserResource>(userDetail)).Returns(userResource);

            // Act
            var result = await _controller.SearchUser(request);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.AreEqual(userResource, okResult.Value);
        }

        [Test]
        public async Task SearchUser_ReturnsOkWithUserResource_WhenLoginNameProvided()
        {
            // Arrange
            var request = new SearchRequest { LoginName = "username", Role = Role.SuperAdmin }; // Provide valid login name
            var userDetail = new ApplicationUser { /* Initialize application user properties */ };
            var userResource = new ApplicationUserResource { /* Initialize user resource properties */ };

            _mockUserDetailService.Setup(x => x.GetUserAccountByName(request.LoginName, (int)request.Role)).ReturnsAsync(userDetail);
            _mockMapper.Setup(x => x.Map<ApplicationUser, ApplicationUserResource>(userDetail)).Returns(userResource);

            // Act
            var result = await _controller.SearchUser(request);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.AreEqual(userResource, okResult.Value);
        }

        [Test]
        public async Task SearchUser_ReturnsOkWithEmptyString_WhenNoUserFound()
        {
            // Arrange
            var request = new SearchRequest { PhoneNumber = "1234567890", Role = Role.SuperAdmin }; // Provide valid phone number and role

            _mockUserDetailService.Setup(x => x.GetUserAccountByPhone(request.PhoneNumber, request.Role.GetHashCode())).ReturnsAsync((ApplicationUser)null);

            // Act
            var result = await _controller.SearchUser(request);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.AreEqual(string.Empty, okResult.Value);
        }

        [Test]
        public async Task SearchUser_ReturnsOkWithEmptyString_WhenRequestIsNull()
        {
            // Arrange
            SearchRequest nullRequest = null;

            // Act
            var result = await _controller.SearchUser(nullRequest);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public async Task SearchUser_ReturnsOkWithEmptyString_WhenBothPhoneNumberAndLoginNameAreNullOrEmpty()
        {
            // Arrange
            var request = new SearchRequest(); // Empty request

            // Act
            var result = await _controller.SearchUser(request);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.AreEqual(string.Empty, okResult.Value);
        }

        [Test]
        public async Task GetNearByAgent_ReturnsOkWithAgentList_WhenAgentsExist()
        {
            // Arrange
            var agentList = new List<ApplicationUser>
            {
                new ApplicationUser { /* Initialize agent properties */ },
                new ApplicationUser { /* Initialize agent properties */ }
            };
            var agentListResource = agentList.Select(agent => new UserAccountResource { /* Map agent properties to resource */ });

            _mockUserDetailService.Setup(x => x.GetUserByRole(Role.Agent.GetHashCode())).ReturnsAsync(agentList);
            _mockMapper.Setup(x => x.Map<IEnumerable<ApplicationUser>, IEnumerable<UserAccountResource>>(agentList)).Returns(agentListResource);

            // Act
            var result = await _controller.GetNearByAgent();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.AreEqual(agentListResource, okResult.Value);
        }

        [Test]
        public async Task GetNearByAgent_ReturnsOkWithEmptyList_WhenNoAgentsExist()
        {
            // Arrange
            var emptyAgentList = Enumerable.Empty<ApplicationUser>();

            _mockUserDetailService.Setup(x => x.GetUserByRole(Role.Agent.GetHashCode())).ReturnsAsync(emptyAgentList);

            // Act
            var result = await _controller.GetNearByAgent();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.AreEqual(Enumerable.Empty<UserAccountResource>(), okResult.Value);
        }

    }
}
