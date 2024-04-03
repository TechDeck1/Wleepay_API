using AutoMapper;
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
using wleepay.Core.Services;

namespace ApiUnitTests.Controllers
{
    public class CountryControllerTests
    {
        private CountryController _controller;
        private Mock<IMapper> _mockMapper;
        private Mock<ICountyService> _mockCountyService;

        [SetUp]
        public void Setup()
        {

            _mockMapper = new Mock<IMapper>();
            _mockCountyService = new Mock<ICountyService>();

            _controller = new CountryController(
                _mockCountyService.Object,
                _mockMapper.Object);

        }

        [Test]
        public async Task GetAllCounties_ValidData_ReturnsSortedLookupResources()
        {
            // Arrange
            IEnumerable<wleepay.Core.Models.County> countyList = new List<wleepay.Core.Models.County>
            {
                new wleepay.Core.Models.County { Id = 1, Name = "County A" },
                new wleepay.Core.Models.County { Id = 2, Name = "County B" },
                new wleepay.Core.Models.County { Id = 3, Name = "County C" }
            };

            var lookupResourceList = new List<LookupResource>
            {
                new LookupResource { Id = 1, Name = "County A" },
                new LookupResource { Id = 2, Name = "County B" },
                new LookupResource { Id = 3, Name = "County C" }
            };

            _mockCountyService.Setup(service => service.GetAll()).ReturnsAsync(countyList);
            _mockMapper.Setup(mapper => mapper.Map<IEnumerable<wleepay.Core.Models.County>, IEnumerable<LookupResource>>(countyList)).Returns(lookupResourceList);

            // Act
            var result = await _controller.GetAllCounties();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<LookupResource>>(result);

            var resultList = result as List<LookupResource>;
            Assert.AreEqual(3, resultList.Count);

            // Ensure the result is sorted
            for (int i = 0; i < resultList.Count - 1; i++)
            {
                Assert.IsTrue(string.Compare(resultList[i].Name, resultList[i + 1].Name) < 0);
            }
        }

        [Test]
        public async Task GetAllCounties_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            _mockCountyService.Setup(service => service.GetAll()).ThrowsAsync(new ArgumentException("Test exception"));

            // Act
            var result = Assert.ThrowsAsync<ArgumentException>(() => _controller.GetAllCounties());

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Test exception", result.Message);
           
        }
    }
}
