using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using Moq;
using CMCS.PROG6212.ST10271460.Controllers; // Reference your AccountController namespace

namespace CMCS.PROG6212.ST10271460TEST
{
    [TestClass]
    public class SwitchRoleTests
    {
        private AccountController _controller;

        [TestInitialize]
        public void Setup()
        {
            // Initialize the controller before each test
            _controller = new AccountController();  // Adjust as necessary if your controller has dependencies
        }

        [TestMethod]
        public void SwitchRole_RedirectsToLoginPage()
        {
            // Act
            var result = _controller.SwitchRole() as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Login", result.ActionName);
        }
    }
}

// Add this to your AccountController class
public class AccountController : Controller
{
    public IActionResult SwitchRole()
    {
        return RedirectToAction("Login");
    }
}


