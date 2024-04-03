using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using Microsoft.AspNetCore.Mvc;
using wleepay.API.Controllers;
using wleepay.Core.Models;
using wleepay.Core.Services;

namespace ApiUnitTests.Controllers
{

    [TestFixture]
    public class IssueTypeControllerTests
    {
        private IssueTypeController _issueTypeController;
        private Mock<ILookupService> _lookupServiceMock;

        [SetUp]
        public void SetUp()
        {
            _lookupServiceMock = new Mock<ILookupService>();
            _issueTypeController = new IssueTypeController(_lookupServiceMock.Object);
        }

        [Test]
        public async Task GetAllIssueTypesAsync_ValidData_ReturnsOk()
        {
            // Arrange
            var issueTypes = new List<IssueType>
        {
            new IssueType { Id = 1, Type = "IssueType A", Description=""},
            new IssueType { Id = 2, Type = "IssueType B", Description=""},
            new IssueType { Id = 3, Type = "IssueType C", Description="" }
        };

            _lookupServiceMock.Setup(service => service.GetAllIssueTypes()).ReturnsAsync(issueTypes);

            // Act
            var result = await _issueTypeController.GetAllIssueTypesAsync();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult.Value);
            Assert.IsInstanceOf<List<IssueType>>(okResult.Value);
            Assert.AreEqual(issueTypes, okResult.Value);
        }

        [Test]
        public async Task GetAllIssueTypesAsync_ExceptionThrown_ReturnsBadRequest()
        {
            // Arrange
            _lookupServiceMock.Setup(service => service.GetAllIssueTypes()).ThrowsAsync(new Exception("Test exception"));

            // Act
            var result = await _issueTypeController.GetAllIssueTypesAsync();

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);

            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult.Value);
            Assert.AreEqual("Test exception", badRequestResult.Value);
        }
    }

}
