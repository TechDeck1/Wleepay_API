using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wleepay.API.Controllers;
using wleepay.API.Resources;
using wleepay.Core.Models;
using wleepay.Core.Services;

namespace ApiUnitTests.Controllers
{
    public class TransferTypeControllerTests
    {
        private TransferTypeController _controller;
        private Mock<ITransferTypeService> _mockTransferTypeService;
        private Mock<IMapper> _mockMapper;

        [SetUp]
        public void Setup()
        {
            _mockTransferTypeService = new Mock<ITransferTypeService>();
            _mockMapper = new Mock<IMapper>();

            _controller = new TransferTypeController(
                _mockTransferTypeService.Object,
                _mockMapper.Object
            );

        }
       
        [Test]
        public async Task GetTransferType_ValidIds_ReturnsOkResult()
        {
            // Arrange
            int fromWalletTypeId = 1;
            int toWalletTypeId = 2;
            var expectedTransferType = new TransferType
            {
                Id = 1,
                Name = "Transfer Type 1",
                FromWalletType = 1, // Assuming wallet type IDs exist
                ToWalletType = 2,
                Description = "Description for Transfer Type 1",
                TransferFee = 10,
            };

            var expectedTransferTypeResource = new TransferTypeResource
            {
                Id = 1,
                Name = "Transfer Type 1"
            };
          
            _mockTransferTypeService.Setup(x => x.GetTransferTypeForTransaction(fromWalletTypeId, toWalletTypeId))
                                    .ReturnsAsync(expectedTransferType);
            _mockMapper.Setup(mapper => mapper.Map<TransferType, TransferTypeResource>(expectedTransferType)).Returns(expectedTransferTypeResource);


            // Act
            var result = await _controller.GetTransferType(fromWalletTypeId, toWalletTypeId);

            // Assert
            var okResult = result as OkObjectResult;
            var transerferTypeResult = okResult.Value as TransferTypeResource;
            Assert.IsInstanceOf<OkObjectResult>(result);
            Assert.IsInstanceOf<TransferTypeResource>(okResult.Value);
            Assert.AreEqual(expectedTransferTypeResource, okResult.Value);
        }
        
        [Test]
        public async Task GetTransferType_InvalidIds_ReturnsNotFoundResult()
        {
            // Arrange
            int fromWalletTypeId = -1; // Invalid id
            int toWalletTypeId = 2;

            _mockTransferTypeService.Setup(x => x.GetTransferTypeForTransaction(fromWalletTypeId, toWalletTypeId))
                                    .ReturnsAsync((TransferType)null);

            // Act
            var result = await _controller.GetTransferType(fromWalletTypeId, toWalletTypeId);

            // Assert
            var okResult = result as BadRequestObjectResult;
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            Assert.AreEqual("Invalid Wallet Ids.", okResult.Value.ToString());
        }

        [Test]
        public async Task GetTransferType_NoTransferTypeFound_ReturnsNotFoundResult()
        {
            // Arrange
            int fromWalletTypeId = 1;
            int toWalletTypeId = 2;
            var mockTransferTypeService = new Mock<ITransferTypeService>();
            mockTransferTypeService.Setup(x => x.GetTransferTypeForTransaction(fromWalletTypeId, toWalletTypeId))
                                    .ReturnsAsync((TransferType)null);

            // Act
            var result = await _controller.GetTransferType(fromWalletTypeId, toWalletTypeId);

            // Assert
            var okResult = result as BadRequestObjectResult;
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            Assert.AreEqual("No result found", okResult.Value.ToString());
        }

        //Test when an exception occurs during retrieval:
        [Test]
        public async Task GetTransferType_ExceptionThrown_ReturnsInternalServerErrorResult()
        {
            // Arrange
            int fromWalletTypeId = 1;
            int toWalletTypeId = 2;
           
            _mockTransferTypeService.Setup(x => x.GetTransferTypeForTransaction(fromWalletTypeId, toWalletTypeId)).ThrowsAsync(new Exception());

            // Act
            var result = await _controller.GetTransferType(fromWalletTypeId, toWalletTypeId);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var statusCodeResult = (BadRequestObjectResult)result;
            Assert.AreEqual(StatusCodes.Status400BadRequest, statusCodeResult.StatusCode);
        }

        [Test]
        public async Task GetTransferType_ValidId_ReturnsOkResult()
        {
            // Arrange
            int typeId = 1;
            var expectedTransferType = new TransferType { Id = typeId, Name = "Transfer Type 1" };
            var expectedTransferTypeResource = new TransferTypeResource { Id = typeId, Name = "Transfer Type Resource 1" };
            
            _mockTransferTypeService.Setup(x => x.GetTransferTypeById(typeId))
                                    .ReturnsAsync(expectedTransferType);
            _mockMapper.Setup(mapper => mapper.Map<TransferType, TransferTypeResource>(expectedTransferType)).Returns(expectedTransferTypeResource);

            // Act
            var result = await _controller.GetTransferType(typeId);

            // Assert
             var okResult = result as OkObjectResult;
             var transferTypeResourceResult = okResult.Value as TransferTypeResource;
             Assert.IsInstanceOf<OkObjectResult>(result);
             Assert.IsInstanceOf<TransferTypeResource>(okResult.Value);
             Assert.AreEqual(expectedTransferTypeResource, transferTypeResourceResult);
        }

        [Test]
        public async Task GetTransferType_InvalidId_ReturnsNotFoundResult()
        {
            // Arrange
            int typeId = -1; // Invalid typeId

            _mockTransferTypeService.Setup(x => x.GetTransferTypeById(typeId))
                                    .ReturnsAsync((TransferType)null);
            

            // Act
            var result = await _controller.GetTransferType(typeId);

            // Assert
            var okResult = result as BadRequestObjectResult;
            var transferTypeResourceResult = okResult.Value as TransferTypeResource;
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            Assert.AreEqual("Type id cannot be 0 or null", okResult.Value.ToString());
        }

        [Test]
        public async Task GetTransferType_ExceptionThrown_ReturnsStatus400BadRequestResult()
        {
            // Arrange
            int typeId = -1; // Invalid typeId

            _mockTransferTypeService.Setup(x => x.GetTransferTypeById(typeId))
                            .ThrowsAsync(new Exception());

            // Act
            var result = await _controller.GetTransferType(typeId);

            // Assert
            var okResult = result as BadRequestObjectResult;
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            Assert.AreEqual(StatusCodes.Status400BadRequest, okResult.StatusCode);
        }



    }
}
