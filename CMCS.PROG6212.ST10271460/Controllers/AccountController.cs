using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using CMCS.PROG6212.ST10271460.Models;
using Microsoft.AspNetCore.Http;
using CMCS.PROG6212.ST10271460.Services; // Add this line to include the namespace for IUserService

namespace CMCS.PROG6212.ST10271460.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService; // Add this line to declare _userService

        public AccountController(IUserService userService) // Add this constructor to inject the dependency
        {
            _userService = userService;
        }

        public IActionResult Login(string role)
        {
            ViewBag.Role = role;
            return View();
        }

        [HttpPost]
        // In your AccountController (Login action):
        public IActionResult Login(string username, string password)
        {
            // Authenticate the user (pseudo-code)
            var user = _userService.Authenticate(username, password);

            if (user != null)
            {
                HttpContext.Session.SetString("Username", user.Name); // Changed from user.Username to user.Name
                HttpContext.Session.SetString("UserRole", user.Role.ToString());  // Store role in session
                if (user.Role == Role.Lecturer)
                {
                    return RedirectToAction("Dashboard", "Lecturer");
                }
                else if (user.Role == Role.Coordinator)
                {
                    return RedirectToAction("Dashboard", "Manager");
                }

            }

            return View();  // Invalid login attempt
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        public IActionResult SwitchRole()
        {
            // Get current role from session
            var currentRole = HttpContext.Session.GetString("UserRole");

            // Switch role between Lecturer and Coordinator/Manager
            if (currentRole == "Lecturer")
            {
                HttpContext.Session.SetString("UserRole", "Coordinator");  // Switch to Coordinator
                return RedirectToAction("Dashboard", "Manager");
            }
            else if (currentRole == "Coordinator")
            {
                HttpContext.Session.SetString("UserRole", "Lecturer");  // Switch to Lecturer
                return RedirectToAction("Dashboard", "Lecturer");
            }

            return RedirectToAction("Login", "Account");  // Fallback in case of error
        }
    }
}



















