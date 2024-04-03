using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wleepay.API.Areas.Portal.Controllers;
using wleepay.API.Controllers;
using wleepay.API.Resources;
using wleepay.Communication.Interface;
using wleepay.Core.Models;
using wleepay.Core.Services;

namespace ApiUnitTests.Controllers
{
    public class WalletControllerTests
    {
        private WalletController _controller;
        private Mock<IWalletService> _mockWalletService;
        private Mock<IMapper> _mockMapper;

        [SetUp]
        public void Setup()
        {
            _mockWalletService = new Mock<IWalletService>();
            _mockMapper = new Mock<IMapper>();

            _controller = new WalletController(
                _mockMapper.Object,
                _mockWalletService.Object
            );

        }
        [Test]
        public async Task AddOrUpdateWallet_ValidData_ReturnsOk()
        {
            // Arrange
            var walletInfo = new AddWalletResource
            {
                Id = 1,
                UserId = "user1",
                CurrencyId = 1,
                Type = 1,
                Balance = 1000,
                CreatedDate = DateTime.Now.AddDays(-7),
                UpdatedDate = DateTime.Now.AddDays(-1)
            };
            var validationResult = new ValidationResult(new List<ValidationFailure>());
            var wallet = new Wallet
            {
                Id = 1,
                UserId = "user1",
                CurrencyId = 1,
                Type = 1,
                Balance = 1000,
                CreatedDate = DateTime.Now.AddDays(-7),
                UpdatedDate = DateTime.Now.AddDays(-1)
            };
          
            // Add more wallet resources as needed

            _mockMapper.Setup(mapper => mapper.Map<AddWalletResource, Wallet>(walletInfo)).Returns(wallet);
            _mockWalletService.Setup(service => service.AddOrUpdateWallet(wallet)).ReturnsAsync(wallet);

            // Act
            var result = await _controller.AddOrUpdateWallet(walletInfo);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.AreEqual(wallet, okResult.Value);
        }

        [Test]
        public async Task AddOrUpdateWallet_InvalidData_ReturnsBadRequest()
        {
            // Arrange
            var walletInfo = new AddWalletResource
            {
                Id = 1,
                UserId = "",
                CurrencyId = 1,
                Type = 1,
                Balance = 1000,
                CreatedDate = DateTime.Now.AddDays(-7),
                UpdatedDate = DateTime.Now.AddDays(-1)
            };
            var wallet = new Wallet
            {
                Id = 1,
                UserId = "user1",
                CurrencyId = 1,
                Type = 1,
                Balance = 1000,
                CreatedDate = DateTime.Now.AddDays(-7),
                UpdatedDate = DateTime.Now.AddDays(-1)
            };
            var validationErrors = new List<ValidationFailure> { new ValidationFailure("UserId", "User Id' must not be empty.") };
            var validationResult = new ValidationResult(validationErrors);

            _mockMapper.Setup(mapper => mapper.Map<AddWalletResource, Wallet>(walletInfo)).Returns(new Wallet());
            _mockWalletService.Setup(service => service.AddOrUpdateWallet(It.IsAny<Wallet>())).ReturnsAsync((Wallet)null);

            // Act
            var result = await _controller.AddOrUpdateWallet(walletInfo);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult.Value);
        }

        [Test]
        public async Task AddOrUpdateWallet_NoDataAdded_ReturnsBadRequest()
        {
            // Arrange
            var walletInfo = new AddWalletResource
            {
                Id = 1,
                UserId = "user121",
                CurrencyId = 1,
                Type = 1,
                Balance = 1000,
                CreatedDate = DateTime.Now.AddDays(-7),
                UpdatedDate = DateTime.Now.AddDays(-1)
            };
            var validationResult = new ValidationResult(new List<ValidationFailure>());
            var wallet = new Wallet
            {
                Id = 1,
                UserId = "user121",
                CurrencyId = 1,
                Type = 1,
                Balance = 1000,
                CreatedDate = DateTime.Now.AddDays(-7),
                UpdatedDate = DateTime.Now.AddDays(-1)
            };

            _mockMapper.Setup(mapper => mapper.Map<AddWalletResource, Wallet>(walletInfo)).Returns(wallet);
            _mockWalletService.Setup(service => service.AddOrUpdateWallet(wallet)).ReturnsAsync((Wallet)null);

            // Act
            var result = await _controller.AddOrUpdateWallet(walletInfo);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.AreEqual("No data Added/Updated for wallet", badRequestResult.Value);
        }

        [Test]
        public async Task GetUserWallet_ValidUserId_ReturnsOk()
        {
            // Arrange
            var userId = "testUser";
            var walletData = new List<Wallet>
            {
               new Wallet
                {
                    Id = 1,
                    UserId = "user1",
                    CurrencyId = 1,
                    Type = 1,
                    Balance = 1000,
                    CreatedDate = DateTime.Now.AddDays(-7),
                    UpdatedDate = DateTime.Now.AddDays(-1),
               
                },
                new Wallet
                {
                    Id = 2,
                    UserId = "user2",
                    CurrencyId = 2,
                    Type = 2,
                    Balance = 500,
                    CreatedDate = DateTime.Now.AddDays(-10),
                    UpdatedDate = DateTime.Now.AddDays(-2),
                },
            };
            
            var walletResourceList = new List<WalletResource>
            {
                new WalletResource
                {
                    Id = 1,
                    UserId = "user1",
                    CurrencyId = 1,
                    Type = 1,
                    Balance = 1000,
                    CreatedDate = DateTime.Now.AddDays(-7),
                    UpdatedDate = DateTime.Now.AddDays(-1),
                    Currency = new CurrencyResource { /* fill with currency data */ },
                    WalletType = new WalletTypeResource { /* fill with wallet type data */ }
                },
                new WalletResource
                {
                    Id = 2,
                    UserId = "user2",
                    CurrencyId = 2,
                    Type = 2,
                    Balance = 500,
                    CreatedDate = DateTime.Now.AddDays(-10),
                    UpdatedDate = DateTime.Now.AddDays(-2),
                    Currency = new CurrencyResource { /* fill with currency data */ },
                    WalletType = new WalletTypeResource { /* fill with wallet type data */ }
                },
                // Add more wallet resources as needed
            };

            _mockWalletService.Setup(service => service.GetWalletsByUserId(userId, 0)).ReturnsAsync(walletData);
            _mockMapper.Setup(mapper => mapper.Map<IEnumerable<Wallet>, IEnumerable<WalletResource>>(walletData)).Returns(walletResourceList);

            // Act
            var result = await _controller.GetUserWallet(userId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);

            var okResult = result as OkObjectResult;
            Assert.AreEqual(walletResourceList, okResult.Value);
        }

        [Test]
        public async Task GetUserWallet_InvalidUserId_ReturnsBadRequest()
        {
            // Arrange
            string userId = null;

            // Act
            var result = await _controller.GetUserWallet(userId);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);

            var badRequestResult = result as BadRequestObjectResult;
            Assert.AreEqual("Please select userId.", badRequestResult.Value);
        }

        [Test]
        public async Task GetWalletByWalletTypeId_ValidId_ReturnsOk()
        {
            // Arrange
            long walletTypeId = 1;
            var walletData = new List<Wallet>
            {
                new Wallet
                {
                    Id = 1,
                    UserId = "user1",
                    CurrencyId = 1,
                    Type = 1,
                    Balance = 1000,
                    CreatedDate = DateTime.Now.AddDays(-7),
                    UpdatedDate = DateTime.Now.AddDays(-1),

                },
                new Wallet
                {
                    Id = 2,
                    UserId = "user2",
                    CurrencyId = 2,
                    Type = 1,
                    Balance = 500,
                    CreatedDate = DateTime.Now.AddDays(-10),
                    UpdatedDate = DateTime.Now.AddDays(-2),
                },
            };
            var walletResourceList = new List<WalletResource>
            {
                new WalletResource
                {
                    Id = 1,
                    UserId = "user1",
                    CurrencyId = 1,
                    Type = 1,
                    Balance = 1000,
                    CreatedDate = DateTime.Now.AddDays(-7),
                    UpdatedDate = DateTime.Now.AddDays(-1),

                },
                new WalletResource
                {
                    Id = 2,
                    UserId = "user2",
                    CurrencyId = 2,
                    Type = 1,
                    Balance = 500,
                    CreatedDate = DateTime.Now.AddDays(-10),
                    UpdatedDate = DateTime.Now.AddDays(-2),
                },
            };

            _mockWalletService.Setup(service => service.GetWalletByWalletTypeId(walletTypeId)).ReturnsAsync(walletData);
            _mockMapper.Setup(mapper => mapper.Map<IEnumerable<Wallet>, IEnumerable<WalletResource>>(walletData)).Returns(walletResourceList);

            // Act
            var result = await _controller.GetWalletByWalletTypeId(walletTypeId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);

            var okResult = result as OkObjectResult;
            Assert.AreEqual(walletResourceList, okResult.Value);
        }

        [Test]
        public async Task GetWalletByWalletTypeId_InvalidId_ReturnsBadRequest()
        {
            // Arrange
            long walletTypeId = 0;

            // Act
            var result = await _controller.GetWalletByWalletTypeId(walletTypeId);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);

            var badRequestResult = result as BadRequestObjectResult;
            Assert.AreEqual("UserId cannot be null or 0 and negative.", badRequestResult.Value);
        }
    }
}
