﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using Moq;
using CMCS.PROG6212.ST10271460.Controllers;
using CMCS.PROG6212.ST10271460.Models;
using Microsoft.AspNetCore.Http;

namespace CMCS.PROG6212.ST10271460TEST
{
    [TestClass]
    public class AccountControllerTests
    {
        private AccountController _controller;
        private Mock<HttpContext> _mockHttpContext;

        [TestInitialize]
        public void Setup()
        {
            // Mock HttpContext and Session
            _mockHttpContext = new Mock<HttpContext>();
            var mockSession = new Mock<ISession>();

            _mockHttpContext.Setup(x => x.Session).Returns(mockSession.Object);

            _controller = new AccountController
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = _mockHttpContext.Object
                }
            };
        }

        [TestMethod]
        public void Login_ValidCredentials_RedirectsToLecturerDashboard()
        {
            // Arrange
            var model = new LoginViewModel
            {
                Username = "abcd",
                Password = "password",
                Role = "Lecturer"
            };

            // Act
            var result = _controller.Login(model) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Dashboard", result?.ActionName);
            Assert.AreEqual("Lecturer", result?.ControllerName);
        }

        [TestMethod]
        public void Login_InvalidCredentials_ShowsError()
        {
            // Arrange
            var model = new LoginViewModel
            {
                Username = "abc",
                Password = "pass"
            };

            // Act
            var result = _controller.Login(model) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Invalid login credentials.", result?.ViewData["ErrorMessage"]);
        }

        [TestMethod]
        public void Logout_ClearsSessionAndRedirectsToLogin()
        {
            // Act
            var result = _controller.Logout() as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Login", result?.ActionName);
        }
    }
}

