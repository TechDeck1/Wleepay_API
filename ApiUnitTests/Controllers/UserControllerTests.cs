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
using wleepay.API.Helpers;
using wleepay.API.Resources;
using wleepay.Communication.Interface;
using wleepay.Core.Common;
using wleepay.Core.DTO.Response;
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

        [Test]
        public async Task GetUserList_ReturnsOkWithUserList_WhenSearchTextProvided()
        {
            // Arrange
            var request = new PagingRequest
            {
                SearchText = "example@example.com",
                Role = Role.SuperAdmin, // Provide valid role
                Index = 0,
                Size = 10
            };
            var searchResult = new UserDetailsResponseResource
            {
                TotalCount = 2,
                UserDetails = new List<ApplicationUser>
                {
                    new ApplicationUser { /* Initialize user properties */ },
                    new ApplicationUser { /* Initialize user properties */ }
                }
            };
            var mapResult = new List<ApplicationUserResource>() { };
;
            _mockUserDetailService.Setup(x => x.SearchUsersByEmail(request.SearchText, request.Role, request.Index, request.Size)).ReturnsAsync(searchResult);
            _mockMapper.Setup(x => x.Map<IEnumerable<ApplicationUser>, IEnumerable<ApplicationUserResource>>(searchResult.UserDetails)).Returns(mapResult);

            // Act
            var result = await _controller.GetUserList(request);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            IEnumerable<ApplicationUserResource> UserDetails = (IEnumerable<ApplicationUserResource>)(okResult.Value?.GetType().GetProperty("UserDetails")?.GetValue(okResult.Value));
            int totalCount = (int)(okResult.Value?.GetType().GetProperty("TotalCount")?.GetValue(okResult.Value));
            Assert.AreEqual(searchResult.TotalCount, totalCount);
            Assert.AreEqual(mapResult, UserDetails);
        }

        [Test]
        public async Task GetUserList_ReturnsOkWithUserList_WhenNoSearchTextProvided()
        {
            // Arrange
            var request = new PagingRequest
            {
                Role = Role.SuperAdmin, // Provide valid role
                Index = 0,
                Size = 10
            };
            var userListResult = new UserDetailsResponseResource
            {
                TotalCount = 2,
                UserDetails = new List<ApplicationUser>
                {
                    new ApplicationUser { /* Initialize user properties */ },
                    new ApplicationUser { /* Initialize user properties */ }
                }
            };
            var mapResult = new List<ApplicationUserResource>() { };

            _mockUserDetailService.Setup(x => x.GetUsers(request.Role, request.Index, request.Size)).ReturnsAsync(userListResult);
            _mockMapper.Setup(x => x.Map<IEnumerable<ApplicationUser>, IEnumerable<ApplicationUserResource>>(userListResult.UserDetails)).Returns(mapResult);

            // Act
            var result = await _controller.GetUserList(request);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            IEnumerable<ApplicationUserResource> UserDetails = (IEnumerable<ApplicationUserResource>)(okResult.Value?.GetType().GetProperty("UserDetails")?.GetValue(okResult.Value));
            int totalCount = (int)(okResult.Value?.GetType().GetProperty("TotalCount")?.GetValue(okResult.Value));
            Assert.AreEqual(userListResult.TotalCount, totalCount);
            //Assert.AreEqual(mapResult, UserDetails);
        }

        [Test]
        public async Task GetUserList_ReturnsBadRequest_WhenRequestIsNull()
        {
            // Arrange
            PagingRequest nullRequest = null;

            // Act
            var result = await _controller.GetUserList(nullRequest);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public async Task UpdateUserKYCStatus_ReturnsOk_WhenKYCStatusUpdatedSuccessfully()
        {
            // Arrange
            string userId = "123"; // Provide valid user id
            int statusId = 1; // Provide valid status id

            var updatedUserDetail = new UserDetail { /* Initialize updated user detail */ };

            _mockUserDetailService.Setup(x => x.UpdateUserKYCStatus(userId, statusId)).ReturnsAsync(updatedUserDetail);

            // Act
            var result = await _controller.UpdateUserKYCStatus(userId, statusId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.AreEqual("KYC Status Updated", okResult.Value);
        }

        [Test]
        public async Task UpdateUserKYCStatus_ReturnsBadRequest_WhenUserDoesNotExist()
        {
            // Arrange
            string userId = "123"; // Provide valid user id
            int statusId = 1; // Provide valid status id

            _mockUserDetailService.Setup(x => x.UpdateUserKYCStatus(userId, statusId)).ReturnsAsync((UserDetail)null);

            // Act
            var result = await _controller.UpdateUserKYCStatus(userId, statusId);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var okResult = (BadRequestObjectResult)result;
            Assert.AreEqual("User does not exist", okResult.Value);
        }

        [Test]
        public async Task UpdateUserAddress_ReturnsOk_WhenAddressUpdatedSuccessfully()
        {
            // Arrange
            var addressModel = new AddressResource
            {
                UserId = "123", // Provide valid user id
                /* Provide other necessary address properties */
            };

            var user = new UserDetail { /* Initialize user detail */ };
            var addressToUpdate = new Address { /* Initialize address to update */ };
            var updatedAddress = new Address { /* Initialize updated address */ };
            var addressResource = new AddressResource { /* Initialize address resource */ };

            _mockUserDetailService.Setup(x => x.GetUserDetailsById(addressModel.UserId)).ReturnsAsync(user);
            _mockMapper.Setup(x => x.Map<AddressResource, Address>(addressModel)).Returns(addressToUpdate);
            _mockAddressService.Setup(x => x.AddOrUpdateAddress(addressToUpdate)).ReturnsAsync(updatedAddress);
            _mockMapper.Setup(x => x.Map<Address, AddressResource>(updatedAddress)).Returns(addressResource);

            // Act
            var result = await _controller.UpdateUserAddress(addressModel);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.AreEqual(addressResource, okResult.Value);
        }

        [Test]
        public async Task UpdateUserAddress_ReturnsBadRequest_WhenAddressModelIsNull()
        {
            // Arrange
            AddressResource nullAddressModel = null;

            // Act
            var result = await _controller.UpdateUserAddress(nullAddressModel);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public async Task UpdateUserAddress_ReturnsBadRequest_WhenUserIdIsNullOrEmpty()
        {
            // Arrange
            var addressModel = new AddressResource
            {
                UserId = null, // Provide null user id
                /* Provide other necessary address properties */
            };

            // Act
            var result = await _controller.UpdateUserAddress(addressModel);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public async Task UpdateUserAddress_ReturnsBadRequest_WhenUserNotFound()
        {
            // Arrange
            var addressModel = new AddressResource
            {
                UserId = "123", // Provide valid user id
                /* Provide other necessary address properties */
            };

            _mockUserDetailService.Setup(x => x.GetUserDetailsById(addressModel.UserId)).ReturnsAsync((UserDetail)null);

            // Act
            var result = await _controller.UpdateUserAddress(addressModel);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public async Task GetIncomingOutGoingTopUpRequests_ReturnsOkWithTransactionResources_WhenPaymentRequestExists()
        {
            // Arrange
            var paymentRequestParam = new PaymentRequest
            {
                UserId = "123", // Provide valid user id
                Type = PaymentRequestType.InComing, // Provide valid payment type
                TransferStatus = wleepay.Core.Enums.TransactionStatus.Approved, // Provide valid transfer status
                PageSize = 10,
                PageIndex = 0
            };

            var paymentRequest = new List<Transaction>
                {
                    new Transaction { /* Initialize transaction properties */ },
                    new Transaction { /* Initialize transaction properties */ }
                };

            var transactionResource = paymentRequest.Select(transaction => new TransactionHistoryResource { /* Map transaction properties to resource */ });

            _mockUserDetailService.Setup(x => x.GetIncomingOutGoingPaymentRequest(paymentRequestParam.UserId, paymentRequestParam.Type, paymentRequestParam.TransferStatus, paymentRequestParam.PageSize, paymentRequestParam.PageIndex)).ReturnsAsync(paymentRequest);
            _mockMapper.Setup(x => x.Map<IEnumerable<Transaction>, IEnumerable<TransactionHistoryResource>>(paymentRequest)).Returns(transactionResource);

            // Act
            var result = await _controller.GetIncomingOutGoingTopUpRequests(paymentRequestParam);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.AreEqual(transactionResource, okResult.Value);
        }

        [Test]
        public async Task GetIncomingOutGoingTopUpRequests_ReturnsOkWithEmptyList_WhenPaymentRequestDoesNotExist()
        {
            // Arrange
            var paymentRequestParam = new PaymentRequest
            {
                UserId = "123", // Provide valid user id
                Type = PaymentRequestType.InComing, // Provide valid payment type
                TransferStatus = wleepay.Core.Enums.TransactionStatus.Approved, // Provide valid transfer status
                PageSize = 10,
                PageIndex = 0
            };

            _mockUserDetailService.Setup(x => x.GetIncomingOutGoingPaymentRequest(paymentRequestParam.UserId, paymentRequestParam.Type, paymentRequestParam.TransferStatus, paymentRequestParam.PageSize, paymentRequestParam.PageIndex)).ReturnsAsync((List<Transaction>)null);

            // Act
            var result = await _controller.GetIncomingOutGoingTopUpRequests(paymentRequestParam);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.AreEqual(Enumerable.Empty<TransactionHistoryResource>(), okResult.Value);
        }

        [Test]
        public async Task GetIncomingOutGoingTopUpRequests_ReturnsBadRequest_WhenPaymentRequestParamIsNull()
        {
            // Arrange
            PaymentRequest nullPaymentRequestParam = null;

            // Act
            var result = await _controller.GetIncomingOutGoingTopUpRequests(nullPaymentRequestParam);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public async Task GetAllUsersById_ReturnsOkWithUserList_WhenUsersExist()
        {
            // Arrange
            string userId = "123"; // Provide valid user id
            int roleId = 1; // Provide valid role id
            int pageIndex = 0;
            int pageSize = 10;

            var role = Role.Agent; // Provide valid role
            var userList = new UserDetailsResponseResource
            {
                TotalCount = 2,
                UserDetails = new List<ApplicationUser>
                {
                    new ApplicationUser { /* Initialize user properties */ },
                    new ApplicationUser { /* Initialize user properties */ }
                }
            };

            var userListResource =  new List<ApplicationUserResource> { /* Map user properties to resource */ };
            _mockUserDetailService.Setup(x => x.GetAllUsersById(role, pageIndex, pageSize, userId)).ReturnsAsync(userList);
            _mockMapper.Setup(x => x.Map<IEnumerable<ApplicationUser>, IEnumerable<ApplicationUserResource>>(userList.UserDetails)).Returns(userListResource);

            // Act
            var result = await _controller.GetAllUsersById(userId, roleId, pageIndex, pageSize);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            IEnumerable<ApplicationUserResource> UserDetails = (IEnumerable<ApplicationUserResource>)(okResult.Value?.GetType().GetProperty("UserDetails")?.GetValue(okResult.Value));
            int totalCount = (int)(okResult.Value?.GetType().GetProperty("TotalCount")?.GetValue(okResult.Value));
            Assert.AreEqual(userList.TotalCount, totalCount);
            Assert.AreEqual(userListResource, UserDetails);
        }

        [Test]
        public async Task GetAllUsersById_ReturnsOkWithEmptyList_WhenNoUsersExist()
        {
            // Arrange
            string userId = "123"; // Provide valid user id
            int roleId = 1; // Provide valid role id
            int pageIndex = 0;
            int pageSize = 10;

            var role = Role.Agent; // Provide valid role
            var emptyUserList = new UserDetailsResponseResource
            {
                TotalCount = 0,
                UserDetails = null
            };

            _mockUserDetailService.Setup(x => x.GetAllUsersById(role, pageIndex, pageSize, userId)).ReturnsAsync(emptyUserList);
            var userListResource = new List<ApplicationUserResource> { /* Map user properties to resource */ };
            _mockMapper.Setup(x => x.Map<IEnumerable<ApplicationUser>, IEnumerable<ApplicationUserResource>>(emptyUserList.UserDetails)).Returns(userListResource);

            // Act
            var result = await _controller.GetAllUsersById(userId, roleId, pageIndex, pageSize);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            IEnumerable<ApplicationUserResource> UserDetails = (IEnumerable<ApplicationUserResource>)(okResult.Value?.GetType().GetProperty("UserDetails")?.GetValue(okResult.Value));
            int totalCount = (int)(okResult.Value?.GetType().GetProperty("TotalCount")?.GetValue(okResult.Value));
            Assert.AreEqual(emptyUserList.TotalCount, totalCount);
            Assert.IsEmpty(UserDetails);
        }

        [Test]
        public async Task GetAllUsersById_ReturnsBadRequest_WhenUserIdIsNullOrEmpty()
        {
            // Arrange
            string nullOrEmptyUserId = null;
            int roleId = 1; // Provide valid role id
            int pageIndex = 0;
            int pageSize = 10;

            // Act
            var result = await _controller.GetAllUsersById(nullOrEmptyUserId, roleId, pageIndex, pageSize);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public async Task VerifyUserAccountByPhoneAsync_ReturnsOkWithUserDetailsAndTransactions_WhenPhoneNumberExists()
        {
            // Arrange
            string phoneNumber = "1234567890"; // Provide valid phone number

            var result = new TransactionAndUserResponseResource
            {
                UserDetails = new ApplicationUser { /* Initialize user properties */ },
                Transactions = new List<Transaction>
                {
                    new Transaction { /* Initialize transaction properties */ },
                    new Transaction { /* Initialize transaction properties */ }
                }
            };
            var userDetailResource = new ApplicationUserResource { /* Map user properties to resource */ };
            var transactionResourceList = result.Transactions.Select(transaction => new TransactionHistoryResource { /* Map transaction properties to resource */ });

            _mockUserDetailService.Setup(x => x.VerifyUserAccountByPhone(phoneNumber)).ReturnsAsync(result);
            _mockMapper.Setup(x => x.Map<ApplicationUser, ApplicationUserResource>(result.UserDetails)).Returns(userDetailResource);
            _mockMapper.Setup(x => x.Map<IEnumerable<Transaction>, IEnumerable<TransactionHistoryResource>>(result.Transactions)).Returns(transactionResourceList);

            // Act
            var controllerResult = await _controller.VerifyUserAccountByPhoneAsync(phoneNumber);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(controllerResult);
            var okResult = (OkObjectResult)controllerResult;
            
            var UserDetails =(okResult.Value?.GetType().GetProperty("UserDetails")?.GetValue(okResult.Value));
            bool status = (bool)(okResult.Value?.GetType().GetProperty("status")?.GetValue(okResult.Value));

            Assert.IsTrue(status);
        }

        [Test]
        public async Task VerifyUserAccountByPhoneAsync_ReturnsOkWithNullUserDetailsAndStatusFalse_WhenPhoneNumberDoesNotExist()
        {
            // Arrange
            string nonExistentPhoneNumber = "9999999999"; // Provide non-existent phone number

            _mockUserDetailService.Setup(x => x.VerifyUserAccountByPhone(nonExistentPhoneNumber)).ReturnsAsync((TransactionAndUserResponseResource)null);

            // Act
            var result = await _controller.VerifyUserAccountByPhoneAsync(nonExistentPhoneNumber);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
           
            bool status = (bool)(okResult.Value?.GetType().GetProperty("status")?.GetValue(okResult.Value));
            string message = (string)(okResult.Value?.GetType().GetProperty("message")?.GetValue(okResult.Value));

            Assert.IsFalse(status);
            Assert.AreEqual(false, status);
            Assert.AreEqual("Phone Number does not exists", message);
        }

        [Test]
        public async Task VerifyUserAccountByPhoneAsync_ReturnsBadRequest_WhenPhoneNumberIsNullOrEmpty()
        {
            // Arrange
            string nullOrEmptyPhoneNumber = null;

            // Act
            var result = await _controller.VerifyUserAccountByPhoneAsync(nullOrEmptyPhoneNumber);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public async Task GetStaffPermissionsById_ReturnsOkWithStaffPermissions_WhenUserExists()
        {
            // Arrange
            string userId = "123"; // Provide valid user id

            var userDetail = new ApplicationUser
            {
                UserDetail = new UserDetail { Permissions= "[\"permission1\", \"permission2\"]" }
            };

            var staffDetails = new StaffPermissionsResponseResource() { };
           
            userDetail.UserDetail.Permissions = "[\"permission1\", \"permission2\"]"; // Provide valid permissions

            _mockUserDetailService.Setup(x => x.GetStaffPermissionsById(userId)).ReturnsAsync(userDetail);
            _mockMapper.Setup(x => x.Map<ApplicationUser, StaffPermissionsResponseResource>(userDetail)).Returns(staffDetails);

            // Act
            var result = await _controller.GetStaffPermissionsById(userId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
           
            var session = (okResult.Value?.GetType().GetProperty("session")?.GetValue(okResult.Value));
            var userResource = (okResult.Value?.GetType().GetProperty("userResource")?.GetValue(okResult.Value));

            Assert.AreEqual(staffDetails, userResource);
        }

        [Test]
        public async Task GetStaffPermissionsById_ReturnsOkWithNullSession_WhenUserPermissionsAreNullOrEmpty()
        {
            // Arrange
            string userId = "123"; // Provide valid user id

            var userDetail = new ApplicationUser
            {
                UserDetail = new UserDetail { Permissions = null }
            };

            var staffDetails = new StaffPermissionsResponseResource() { };

           // userDetail.UserDetail.Permissions = null; // Provide valid permissions

            _mockUserDetailService.Setup(x => x.GetStaffPermissionsById(userId)).ReturnsAsync(userDetail);
            _mockMapper.Setup(x => x.Map<ApplicationUser, StaffPermissionsResponseResource>(userDetail)).Returns(staffDetails);

            // Act
            var result = await _controller.GetStaffPermissionsById(userId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            
            var session = (okResult.Value?.GetType().GetProperty("session")?.GetValue(okResult.Value));
            var userResource = (okResult.Value?.GetType().GetProperty("userResource")?.GetValue(okResult.Value));

            Assert.IsNull(session);
            Assert.AreEqual(staffDetails, userResource);
        }

        [Test]
        public async Task GetStaffPermissionsById_ReturnsOkWithEncryptedSession_WhenUserPermissionsExist()
        {
            // Arrange
            string userId = "123"; // Provide valid user id

            var userDetail = new ApplicationUser
            {
                UserDetail = new UserDetail { Permissions = "[\"permission1\",\"permission2\"]" }
            };

            var staffDetails = new StaffPermissionsResponseResource() { };

            userDetail.UserDetail.Permissions = "[\"permission1\",\"permission2\"]"; // Provide valid permissions

            _mockUserDetailService.Setup(x => x.GetStaffPermissionsById(userId)).ReturnsAsync(userDetail);
            _mockMapper.Setup(x => x.Map<ApplicationUser, StaffPermissionsResponseResource>(userDetail)).Returns(staffDetails);

            var secretKey = "1234567890123456";

            // Act
            var result = await _controller.GetStaffPermissionsById(userId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            
            string session = (string)(okResult.Value?.GetType().GetProperty("session")?.GetValue(okResult.Value));
            var userResource = (okResult.Value?.GetType().GetProperty("userResource")?.GetValue(okResult.Value));
           
            Assert.AreEqual(staffDetails, userResource);

            // Decrypt the session to verify it matches the expected permissions
            string decryptedSession = Common.Decrypt(session, secretKey);
            Assert.AreEqual("[\"permission1\",\"permission2\"]", decryptedSession);
        }

        [Test]
        public async Task GetStaffPermissionsById_ReturnsBadRequest_WhenUserIdIsNullOrEmpty()
        {
            // Arrange
            string nullOrEmptyUserId = null;

            // Act
            var result = await _controller.GetStaffPermissionsById(nullOrEmptyUserId);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public async Task EditUserDetails_ReturnsOk_WhenUserDetailsEditedSuccessfully()
        {
            // Arrange
            var editUserDetailResource = new EditUserDetailResource
            {
                UserId = "123", // Provide valid user id
                PhoneNumber = "1234567890", // Provide valid phone number
                StaffRoleId = 1, // Provide valid staff role id
                                 // Provide other necessary properties for editing user details
            };

            var applicationUser = new ApplicationUser { /* Initialize application user properties */ };
            var userDetail = new UserDetail { /* Initialize user detail properties */ };
            var isUserUpdatedResult = IdentityResult.Success; // Simulate successful user update

            _mockUserManager.Setup(x => x.FindByIdAsync(editUserDetailResource.UserId)).ReturnsAsync(applicationUser);
            _mockUserDetailService.Setup(x => x.GetUserDetailsById(applicationUser.Id)).ReturnsAsync(userDetail);
            _mockUserDetailService.Setup(x => x.IsPhoneAlreadyRegistered(editUserDetailResource.PhoneNumber)).ReturnsAsync(false);
            _mockUserManager.Setup(x => x.UpdateAsync(applicationUser)).ReturnsAsync(isUserUpdatedResult);
            _mockMapper.Setup(x => x.Map<EditUserDetailResource, UserDetail>(editUserDetailResource)).Returns(userDetail);
            _mockUserDetailService.Setup(x => x.AddOrUpdateUserDetails(userDetail)).ReturnsAsync(new UserDetail());
            _mockUsersStaffRoleService.Setup(x => x.UpdateUserStaffRole(It.IsAny<UsersStaffRole>())).ReturnsAsync(new UsersStaffRole());

            // Act
            var result = await _controller.EditUserDetails(editUserDetailResource);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
          
            var Success = (okResult.Value?.GetType().GetProperty("Success")?.GetValue(okResult.Value));
            var Message = (okResult.Value?.GetType().GetProperty("Message")?.GetValue(okResult.Value));
            var UserId = (okResult.Value?.GetType().GetProperty("UserId")?.GetValue(okResult.Value));
            var LastName = (okResult.Value?.GetType().GetProperty("LastName")?.GetValue(okResult.Value));
            var FirstName = (okResult.Value?.GetType().GetProperty("FirstName")?.GetValue(okResult.Value));
            Assert.AreEqual(true, Success);
            Assert.AreEqual(editUserDetailResource.UserId, UserId);
            Assert.AreEqual(editUserDetailResource.FirstName, FirstName);
            Assert.AreEqual(editUserDetailResource.LastName, LastName);
            Assert.AreEqual(Messages.InfoUpdated.ToDescriptionString(), Message);
        }

        [Test]
        public async Task EditUserDetails_ReturnsBadRequest_WhenUserIdIsNullOrEmpty()
        {
            // Arrange
            var editUserDetailResource = new EditUserDetailResource
            {
                UserId = null, // Provide null user id
                               // Provide other necessary properties for editing user details
            };

            // Act
            var result = await _controller.EditUserDetails(editUserDetailResource);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public async Task EditUserDetails_ReturnsBadRequest_whenNotableToRetriveApplicationUser()
        {
            // Arrange
            var editUserDetailResource = new EditUserDetailResource
            {
                UserId = "123", // Provide valid user id
                PhoneNumber = "1234567890", // Provide valid phone number
                StaffRoleId = 1, // Provide valid staff role id
                                 // Provide other necessary properties for editing user details
            };

            var applicationUser = new ApplicationUser { /* Initialize application user properties */ };
            var userDetail = new UserDetail { /* Initialize user detail properties */ };
            var isUserUpdatedResult = IdentityResult.Success; // Simulate successful user update

            _mockUserManager.Setup(x => x.FindByIdAsync(editUserDetailResource.UserId)).ReturnsAsync((ApplicationUser)null);
           
            // Act
            var result = await _controller.EditUserDetails(editUserDetailResource);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);

        }

        [Test]
        public async Task EditUserDetails_ReturnsBadRequest_whenNotableToRetriveUserDetails()
        {
            // Arrange
            var editUserDetailResource = new EditUserDetailResource
            {
                UserId = "123", // Provide valid user id
                PhoneNumber = "1234567890", // Provide valid phone number
                StaffRoleId = 1, // Provide valid staff role id
                                 // Provide other necessary properties for editing user details
            };

            var applicationUser = new ApplicationUser { /* Initialize application user properties */ };
            var userDetail = new UserDetail { /* Initialize user detail properties */ };
            var isUserUpdatedResult = IdentityResult.Success; // Simulate successful user update

            _mockUserManager.Setup(x => x.FindByIdAsync(editUserDetailResource.UserId)).ReturnsAsync(applicationUser);
            _mockUserDetailService.Setup(x => x.GetUserDetailsById(applicationUser.Id)).ReturnsAsync((UserDetail)null);


            // Act
            var result = await _controller.EditUserDetails(editUserDetailResource);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);

        }

        [Test]
        public async Task EditUserDetails_ReturnsBadRequest_WhenUpdateAsyncReturnsFailure()
        {
            // Arrange
            var editUserDetailResource = new EditUserDetailResource
            {
                UserId = "123", // Provide valid user id
                PhoneNumber = "1234567890", // Provide valid phone number
                StaffRoleId = 1, // Provide valid staff role id
                                 // Provide other necessary properties for editing user details
            };

            var applicationUser = new ApplicationUser { /* Initialize application user properties */ };
            var userDetail = new UserDetail { /* Initialize user detail properties */ };
            var isUserUpdatedResult = IdentityResult.Failed(new IdentityError[2]); // Simulate successful user update

            _mockUserManager.Setup(x => x.FindByIdAsync(editUserDetailResource.UserId)).ReturnsAsync(applicationUser);
            _mockUserDetailService.Setup(x => x.GetUserDetailsById(applicationUser.Id)).ReturnsAsync(userDetail);
            _mockUserDetailService.Setup(x => x.IsPhoneAlreadyRegistered(editUserDetailResource.PhoneNumber)).ReturnsAsync(false);
            _mockUserManager.Setup(x => x.UpdateAsync(applicationUser)).ReturnsAsync(isUserUpdatedResult);
            
            // Act
            var result = await _controller.EditUserDetails(editUserDetailResource);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
          
        }


    }
}
