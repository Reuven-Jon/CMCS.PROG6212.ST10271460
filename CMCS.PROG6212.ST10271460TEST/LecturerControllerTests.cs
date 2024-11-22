using Microsoft.VisualStudio.TestTools.UnitTesting;
using CMCS.PROG6212.ST10271460.Controllers;
using CMCS.PROG6212.ST10271460.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using CMCS.PROG6212.ST10271460.Data;

namespace CMCS.Tests
{
    [TestClass]
    public class LecturerControllerTests
    {
        private LecturerController? _controller;
        private Mock<HttpContext>? _mockHttpContext;


        [TestInitialize]
        public void Setup()
        {
            // Mock HttpContext to simulate session
            _mockHttpContext = new Mock<HttpContext>();
            var mockSession = new Mock<ISession>();
            _mockHttpContext.Setup(x => x.Session).Returns(mockSession.Object);

            var mockContext = new Mock<ApplicationDbContext>(); // Mock DbContext
            var mockEnvironment = new Mock<IWebHostEnvironment>(); // Mock Hosting environment

            _controller = new LecturerController(mockContext.Object, mockEnvironment.Object);
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = _mockHttpContext.Object
            };
        }

        [TestMethod]
        public async Task SubmitClaim_ValidModel_AddsClaimAndRedirects()
        {
            // Arrange
            var model = new ClaimViewModel
            {
                ClaimPeriod = DateTime.Now,
                HoursWorked = 10,
                HourlyRate = 3,
            };

            var fileMock = new Mock<IFormFile>();
            var ms = new MemoryStream();
            fileMock.Setup(f => f.OpenReadStream()).Returns(ms);
            fileMock.Setup(f => f.FileName).Returns("test.pdf");
            fileMock.Setup(f => f.Length).Returns(100);

            // Act
            var result = await _controller.SubmitClaim(model, fileMock.Object) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("YourClaims", result.ActionName);
        }
    }
}

