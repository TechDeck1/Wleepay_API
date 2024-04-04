using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;
using wleepay.API.Controllers;
using wleepay.API.Hub;
using wleepay.API.Resources;
using wleepay.API.Validators;
using wleepay.Core.Common;
using wleepay.Core.Enums;
using wleepay.Core.Models;
using wleepay.Core.Services;

namespace ApiUnitTests.Controllers
{
    public class SystemStatusControllerTests
    {
        private SystemStatusController _controller;
        private Mock<ISystemStatusService> _mockSystemStatusService;
        private Mock<IMapper> _mockMapper;
        private Mock<IHubContext<StatusHubClient, IStatusHubClient>> _mockStatusHub;
        private Mock<IValidator<AddStaffDetailsResource>> _mockValidator;


        [SetUp]
        public void Setup()
        {
            _mockSystemStatusService = new Mock<ISystemStatusService>();
            _mockMapper = new Mock<IMapper>();
            _mockStatusHub = new Mock<IHubContext<StatusHubClient, IStatusHubClient>>();
            _mockValidator = new Mock<IValidator<AddStaffDetailsResource>>();

        _controller = new SystemStatusController(
                _mockSystemStatusService.Object,
                _mockMapper.Object,
                _mockStatusHub.Object
            );

        }

        [Test]
        public async Task AddSystemStatus_ValidSystemStatus_ReturnsOk()
        {
            // Arrange
            var systemStatusResource = new SystemStatusResource { Reason="system is under maintenance", Description="description", Status=true };
            
            var expectedSystemStatus = new SystemStatus()
            {
                Reason = "system is under maintenance",
                Description = "description",
                Status = true,
            };
            var validationResult = new ValidationResult();

            _mockMapper.Setup(x => x.Map<SystemStatusResource, SystemStatus>(systemStatusResource)).Returns(expectedSystemStatus);
            _mockValidator.Setup(x => x.ValidateAsync(It.IsAny<ValidationContext<SystemStatusResource>>(), default)).ReturnsAsync(validationResult); // Mock validation result as valid
            _mockSystemStatusService.Setup(x => x.AddUpdateSystemStatus(It.IsAny<SystemStatus>())).ReturnsAsync(expectedSystemStatus);

            // Act
            var result = await _controller.AddSystemStatus(systemStatusResource);

            // Assert
            var okResult = result as OkObjectResult;
            var addedSystemStatus = okResult.Value as SystemStatus;
            Assert.IsInstanceOf<OkObjectResult>(result);
            Assert.IsInstanceOf<SystemStatus>(okResult.Value);
            Assert.AreEqual(expectedSystemStatus, addedSystemStatus);
        }
        
        [Test]
        public async Task AddSystemStatus_InvalidSystemStatus_ReturnsBadRequestWithErrors()
        {
            // Arrange
            var systemStatusResource = new SystemStatusResource
            {
                Reason = "system is under maintenance",
                Description = "description",
            };
            var validationResult = new ValidationResult(new List<ValidationFailure> { new ValidationFailure("propertyName", "errorMessage") }); // Sample validation errors


            _mockValidator.Setup(x => x.ValidateAsync(It.IsAny<ValidationContext<SystemStatusResource>>(), default)).ReturnsAsync(validationResult); // Mock validation result as valid

            // Act
            var result = await _controller.AddSystemStatus(systemStatusResource);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            Assert.IsInstanceOf<List<ValidationFailure>>(badRequestResult.Value);
            // Assert the content of validation errors as needed
        }

        [Test]
        public async Task AddSystemStatus_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            // Arrange
            var systemStatusResource = new SystemStatusResource 
            {   Reason = "system is under maintenance", 
                Description = "description", 
                Status = true 
            };

            var expectedSystemStatus = new SystemStatus()
            {
                Reason = "system is under maintenance",
                Description = "description",
                Status = true,
            };
            var validationResult = new ValidationResult();

            _mockMapper.Setup(x => x.Map<SystemStatusResource, SystemStatus>(systemStatusResource)).Returns(expectedSystemStatus);
            _mockValidator.Setup(x => x.ValidateAsync(It.IsAny<ValidationContext<SystemStatusResource>>(), default)).ReturnsAsync(validationResult); // Mock validation result as valid
            _mockSystemStatusService.Setup(x => x.AddUpdateSystemStatus(It.IsAny<SystemStatus>())).ThrowsAsync(new Exception());

            // Act & Assert
            Exception caughtException = null;

            try
            {
                await _controller.AddSystemStatus(systemStatusResource);
            }
            catch (Exception ex)
            {
                caughtException = ex;
            }

            // Assert
            Assert.NotNull(caughtException);
        }

        [Test]
        public async Task GetSystemStatus_ValidServiceResponse_ReturnsOk()
        {
            // Arrange
            var expectedSystemStatus = new SystemStatus()
            {
                Id = 1, 
                Reason = "system is under maintenance",
                Description = "description",
                Status = true,
            };

            _mockSystemStatusService.Setup(x => x.GetSystemStatus()).ReturnsAsync(expectedSystemStatus);
            _mockStatusHub.Setup(x => x.Clients.All.SendStatusToUser(expectedSystemStatus)).Returns(Task.CompletedTask);


            // Act
            var result = await _controller.GetSystemStatus();

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsInstanceOf<OkObjectResult>(result);
            var resultValue = okResult.Value as SystemStatus;
            Assert.IsInstanceOf<SystemStatus>(okResult.Value);
            Assert.AreEqual(expectedSystemStatus, resultValue);

            _mockStatusHub.Verify(x => x.Clients.All.SendStatusToUser(expectedSystemStatus), Times.Once);

            // Verify that statusHub.Clients.All.SendStatusToUser is called with expectedStatus
        }

        [Test]
        public async Task GetSystemStatus_ServiceFails_ReturnsInternalServerError()
        {
            // Arrange
            _mockSystemStatusService.Setup(x => x.GetSystemStatus()).ThrowsAsync(new Exception());

            // Act & Assert
            Exception caughtException = null;

            try
            {
                await _controller.GetSystemStatus();
            }
            catch (Exception ex)
            {
                caughtException = ex;
            }

            // Assert
            Assert.NotNull(caughtException);
          

            // Verify that statusHub.Clients.All.SendStatusToUser is not called
            _mockStatusHub.Verify(x => x.Clients.All.SendStatusToUser(It.IsAny<SystemStatus>()), Times.Never);
        }

        [Test]
        public async Task GetSystemStatusById_ValidServiceResponse_ReturnsOk()
        {
            // Arrange
            var id = 1;
            var expectedSystemStatus = new SystemStatus()
            {
                Id = 1,
                Reason = "system is under maintenance",
                Description = "description",
                Status = true,
            };

            _mockSystemStatusService.Setup(x => x.GetSystemStatusById(id)).ReturnsAsync(expectedSystemStatus);

            // Act
            var result = await _controller.GetSystemStatusById(id);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsInstanceOf<OkObjectResult>(result);
            var resultValue = okResult.Value as SystemStatus;
            Assert.IsInstanceOf<SystemStatus>(okResult.Value);
            Assert.AreEqual(expectedSystemStatus, resultValue);
        }

        [Test]
        public async Task GetSystemStatusById_ServiceFails_ReturnsInternalServerError()
        {
            // Arrange
             var id = 1;
           
            _mockSystemStatusService.Setup(x => x.GetSystemStatusById(id)).ThrowsAsync(new Exception());

            // Act & Assert
            Exception caughtException = null;

            try
            {
                await _controller.GetSystemStatusById(id);
            }
            catch (Exception ex)
            {
                caughtException = ex;
            }

            // Assert
            Assert.NotNull(caughtException);

        }
        
        [Test]
        public async Task GetSystemStatusById_WhenIdIsZero_ReturnsOk()
        {
            // Arrange
            var id = 0;
            var expectedSystemStatus = new SystemStatus()
            {
                Id = 1,
                Reason = "system is under maintenance",
                Description = "description",
                Status = true,
            };

            _mockSystemStatusService.Setup(x => x.GetSystemStatusById(id)).ReturnsAsync(expectedSystemStatus);

            // Act
            var result = await _controller.GetSystemStatusById(id);

            // Assert
            var okResult = result as BadRequestObjectResult;
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            Assert.AreEqual(Messages.InvalidRequest.ToDescriptionString(), okResult.Value);
        }

         [Test]
        public async Task UpdateSystemStatus_ValidSystemStatus_ReturnsOk()
        {
            // Arrange
            var systemStatusResource = new SystemStatusResource { Id = 1 ,Reason="system is under maintenance", Description="description", Status=true };
            
            var expectedSystemStatus = new SystemStatus()
            {
                Id = 1,
                Reason = "system is under maintenance",
                Description = "description",
                Status = false,
            };
            var validationResult = new ValidationResult();

            _mockMapper.Setup(x => x.Map<SystemStatusResource, SystemStatus>(systemStatusResource)).Returns(expectedSystemStatus);
            _mockSystemStatusService.Setup(x => x.AddUpdateSystemStatus(It.IsAny<SystemStatus>())).ReturnsAsync(expectedSystemStatus);

            // Act
            var result = await _controller.UpdateSystemStatus(systemStatusResource);

            // Assert
            var okResult = result as OkObjectResult;
            var addedSystemStatus = okResult.Value as SystemStatus;
            Assert.IsInstanceOf<OkObjectResult>(result);
            Assert.IsInstanceOf<SystemStatus>(okResult.Value);
            Assert.AreEqual(expectedSystemStatus, addedSystemStatus);
        }
        
        [Test]
        public async Task UpdateSystemStatus_InvalidSystemStatus_ReturnsBadRequestWithErrors()
        {
            // Arrange
            var systemStatusResource = new SystemStatusResource
            {
                Id = 0,
                Reason = "system is under maintenance",
                Description = "description",
                Status = false,

            };
            var expectedSystemStatus = new SystemStatus()
            {
                Id = 0,
                Reason = "system is under maintenance",
                Description = "description",
                Status = false,
            };

            _mockSystemStatusService.Setup(x => x.AddUpdateSystemStatus(It.IsAny<SystemStatus>())).ReturnsAsync(expectedSystemStatus);

            // Act
            var result = await _controller.UpdateSystemStatus(systemStatusResource);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            Assert.AreEqual(Messages.NotUpdated.ToDescriptionString(), badRequestResult.Value);

        }

        [Test]
        public async Task UpdateSystemStatus_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            // Arrange
            var systemStatusResource = new SystemStatusResource 
            {   
                Id=1,
                Reason = "system is under maintenance", 
                Description = "description", 
                Status = true 
            };

            var expectedSystemStatus = new SystemStatus()
            {
                Id= 1,
                Reason = "system is under maintenance",
                Description = "description",
                Status = true,
            };

            _mockMapper.Setup(x => x.Map<SystemStatusResource, SystemStatus>(systemStatusResource)).Returns(expectedSystemStatus);
            _mockSystemStatusService.Setup(x => x.AddUpdateSystemStatus(It.IsAny<SystemStatus>())).ThrowsAsync(new Exception());

            // Act & Assert
            Exception caughtException = null;

            try
            {
                await _controller.UpdateSystemStatus(systemStatusResource);
            }
            catch (Exception ex)
            {
                caughtException = ex;
            }

            // Assert
            Assert.NotNull(caughtException);
        }

    }
}
