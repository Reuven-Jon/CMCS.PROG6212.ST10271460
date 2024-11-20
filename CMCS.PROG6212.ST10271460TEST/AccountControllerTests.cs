using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using Moq;
using CMCS.PROG6212.ST10271460.Controllers;
using CMCS.PROG6212.ST10271460.Models;
using Microsoft.AspNetCore.Http;

namespace CMCS.PROG6212.ST10271460.Controllers
{
    [TestClass]
    public class AccountControllerTests
    {
        private AccountController? _controller;
        private Mock<ISession>? _mockSession;
        private Mock<HttpContext>? _mockHttpContext;

        [TestInitialize]
        public void Setup()
        {
            _mockHttpContext = new Mock<HttpContext>();
            _mockSession = new Mock<ISession>();

            _mockHttpContext.Setup(x => x.Session).Returns(_mockSession.Object);

            _controller = new AccountController
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = _mockHttpContext.Object
                }
            };
        }

        [TestMethod]
        public void Login_Get_ReturnsView()
        {
            Assert.IsNotNull(_controller, "_controller is null, Setup might have failed.");

            // Act
            var result = _controller.Login() as ViewResult;

            // Assert
            Assert.IsNotNull(result, "_controller.Login() should return a ViewResult");
        }

        [TestMethod]
        public void Login_Post_ValidCredentials_RedirectsToLecturerDashboard()
        {
            Assert.IsNotNull(_controller, "_controller is null, Setup might have failed.");

            // Arrange
            var model = new LoginViewModel
            {
                Username = "user",
                Password = "password",
                Role = "Lecturer"
            };

            _mockSession?.Setup(s => s.SetString("Username", model.Username));
            _mockSession?.Setup(s => s.SetString("Role", model.Role));


            // Act
            var result = _controller.Login(model) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result, "_controller.Login() should return a RedirectToActionResult");
            Assert.AreEqual("Dashboard", result?.ActionName);
            Assert.AreEqual("Lecturer", result?.ControllerName);
        }

        [TestMethod]
        public void Login_Post_InvalidCredentials_ShowsError()
        {
            Assert.IsNotNull(_controller, "_controller is null, Setup might have failed.");

            // Arrange
            var model = new LoginViewModel
            {
                Username = "invalid",
                Password = "wrongpass",
                Role = "Lecturer"
            };

            // Act
            var result = _controller.Login(model) as ViewResult;

            // Assert
            Assert.IsNotNull(result, "_controller.Login() should return a ViewResult");
            Assert.AreEqual("Invalid login credentials.", result?.ViewData["ErrorMessage"]);
        }

        [TestMethod]
        public void Logout_ClearsSessionAndRedirectsToLogin()
        {
            Assert.IsNotNull(_controller, "_controller is null, Setup might have failed.");
            Assert.IsNotNull(_mockSession, "_mockSession is null, Setup might have failed.");

            // Arrange
            _mockSession.Setup(s => s.Clear());

            // Act
            var result = _controller.Logout() as RedirectToActionResult;

            // Assert
            _mockSession.Verify(s => s.Clear(), Times.Once);
            Assert.IsNotNull(result, "_controller.Logout() should return a RedirectToActionResult");
            Assert.AreEqual("Login", result?.ActionName);
        }

    }
}
