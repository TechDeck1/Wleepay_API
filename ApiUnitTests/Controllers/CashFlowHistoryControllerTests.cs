using AutoMapper;
using AutoMapper.Configuration;
using FluentValidation;
using Microsoft.AspNetCore.Hosting;
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
using System.Web.Http.Results;
using wleepay.API.Areas.Portal.Controllers;
using wleepay.API.Resources;
using wleepay.Communication.Interface;
using wleepay.Core.Common;
using wleepay.Core.DTO.Response;
using wleepay.Core.Enums;
using wleepay.Core.Models;
using wleepay.Core.Services;
using wleepay.Storage.Interface;

namespace ApiUnitTests.Controllers
{
    public class CashFlowHistoryControllerTests
    {
        private CashFlowHistoryController _controller;
        private Mock<IUserDetailService> _mockUserDetailsService;
        private Mock<IWalletService> _mockWalletService;
        private Mock<ISMSService> _mockSMSService;
        private Mock<IMapper> _mockMapper;
        private Mock<ICashFlowService> _mockCashFlowService;


        [SetUp]
        public void Setup()
        {
            _mockUserDetailsService = new Mock<IUserDetailService>();
            _mockWalletService = new Mock<IWalletService>();
            _mockSMSService = new Mock<ISMSService>();
            _mockMapper = new Mock<IMapper>();
            _mockCashFlowService=new Mock<ICashFlowService>();

            _controller = new CashFlowHistoryController(
                _mockCashFlowService.Object,
                _mockMapper.Object,
                _mockWalletService.Object,
                _mockUserDetailsService.Object,
                _mockSMSService.Object
            );

        }

        [Test]
        public async Task AddCashFlowHistoryAsync_ValidData_ReturnsOk()
        {
            // Arrange
            var cashFlowHistoryResource = new CashFlowHistoryResource
            {
                WalletId = 1,
                DepositAmount = 100,
            };
            var walletBalance = new Wallet { Balance = 500, CurrencyId = 1, UserId = "123454" };
            var cashFlowHistory = new CashFlowHistory();
            var applicationUser = new ApplicationUser
            {
                Email = "test@example.com",
                UserDetail = new UserDetail
                {
                    UserId = "1234566",
                    FirstName = "JOHN"
                }
            };

            _mockWalletService.Setup(w => w.GetWallet(1)).ReturnsAsync(walletBalance);
            _mockMapper.Setup(m => m.Map<CashFlowHistoryResource, CashFlowHistory>(cashFlowHistoryResource)).Returns(cashFlowHistory);
            _mockCashFlowService.Setup(c => c.AddCashFlowHistoryAsync(cashFlowHistory)).ReturnsAsync(cashFlowHistory);
            _mockUserDetailsService.Setup(c => c.GetApplicationUserById(walletBalance.UserId)).ReturnsAsync(applicationUser);

            // Act
            var result = await _controller.AddCashFlowHistoryAsync(cashFlowHistoryResource);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsInstanceOf<OkObjectResult>(result);
            Assert.AreEqual(200, okResult.StatusCode);

        }

        [Test]
        public async Task AddCashFlowHistoryAsync_NullOrZeroWalletId_ReturnsNotFound()
        {
            // Arrange
            var cashFlowHistoryResource = new CashFlowHistoryResource
            {
                WalletId = 0, // Or WalletId = 0
                DepositAmount = 100,
            };

            // Act
            var result = await _controller.AddCashFlowHistoryAsync(cashFlowHistoryResource);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            Assert.AreEqual(400, badRequestResult.StatusCode);
            Assert.AreEqual(Messages.WalletIdNull.ToDescriptionString(), badRequestResult.Value.ToString());


        }

        [Test]
        public async Task AddCashFlowHistoryAsync_DepositWithNullDepositAmount_ReturnsBadRequest()
        {
            // Arrange
            var cashFlowHistoryResource = new CashFlowHistoryResource
            {
                WalletId = 1,
                DepositAmount = null, // Assuming null DepositAmount
            };
            var walletBalance = new Wallet { Balance = 500, CurrencyId = 1, UserId = "123454" };

            _mockWalletService.Setup(w => w.GetWallet(1)).ReturnsAsync(walletBalance);

            // Act
            var result = await _controller.AddCashFlowHistoryAsync(cashFlowHistoryResource);


            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            Assert.AreEqual(400, badRequestResult.StatusCode);
            Assert.AreEqual(Messages.DepositAmount.ToDescriptionString(), badRequestResult.Value.ToString());

        }

        [Test]
        public async Task AddCashFlowHistoryAsync_ErrorHandling_ReturnsBadRequest()
        {
            // Arrange
            var cashFlowHistoryResource = new CashFlowHistoryResource
            {
                WalletId = 1,
                DepositAmount = 100,
                WithdrawAmount = null // Assuming no withdrawal
            };
            var exceptionMessage = "Test exception";
            _mockWalletService.Setup(w => w.GetWallet(1)).ThrowsAsync(new Exception(exceptionMessage));

            // Act
            var result = await _controller.AddCashFlowHistoryAsync(cashFlowHistoryResource);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.AreEqual(exceptionMessage, badRequestResult.Value.ToString());
        }

        [Test]
        public async Task GetCashFlowHistoryAsync_ValidInput_ReturnsOk()
        {
            // Arrange
            var pagingRequest = new PagingRequestForCashFlow
            {
                CurrencyId = 1,
                Index = 0,
                Size = 10
            };

            var cashFlowHistories = new CashFlowHistoryResponse
            {
                TotalCount = 2,
                CashFlowHistory = new List<CashFlowHistory> 
                {
                    new CashFlowHistory
                    {
                        Id = 1,
                        WalletId = 1001,
                        TotalAmount = 500,
                        DepositAmount = 300,
                        WithdrawAmount = null,
                        PreviousAmount = 200,
                        CurrencyId = 1,
                        CreatedDate = DateTime.Now.AddDays(-2),
                    },
                    new CashFlowHistory
                    {
                        Id = 2,
                        WalletId = 1002,
                        TotalAmount = 700,
                        DepositAmount = null,
                        WithdrawAmount = 200,
                        PreviousAmount = 900,
                        CurrencyId = 2,
                        CreatedDate = DateTime.Now.AddDays(-1),
                    }
                }
                // Add more CashFlowHistory instances as needed
            };

            _mockCashFlowService.Setup(service => service.GetAllCashFlowHistoryAsync(pagingRequest.CurrencyId, pagingRequest.Index, pagingRequest.Size))
                                .ReturnsAsync(cashFlowHistories);

            // Act
            var result = await _controller.GetCashFlowHistoryAsync(pagingRequest);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            var CashFlowHistoryRes = okResult.Value as CashFlowHistoryResponse;
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(cashFlowHistories.CashFlowHistory, CashFlowHistoryRes.CashFlowHistory);
        }

        [Test]
        public async Task GetCashFlowHistoryAsync_InvalidInput_ReturnsBadRequest()
        {
            // Arrange
            var pagingRequest = new PagingRequestForCashFlow
            {
                CurrencyId = -1, // Invalid CurrencyId
                Index = 0,
                Size = 10
            };

            _mockCashFlowService.Setup(service => service.GetAllCashFlowHistoryAsync(pagingRequest.CurrencyId, pagingRequest.Index, pagingRequest.Size))
                                .ThrowsAsync(new ArgumentException("Invalid currency ID"));

            // Act
            var result = await _controller.GetCashFlowHistoryAsync(pagingRequest);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.AreEqual("currency id can only be 0 or 1 and cannot be negative", badRequestResult.Value);
        }
    }
}
