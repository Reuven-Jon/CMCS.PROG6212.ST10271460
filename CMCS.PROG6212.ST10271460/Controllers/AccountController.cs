using Microsoft.AspNetCore.Mvc;
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
            ViewBag.Role = role;  // Assign role for display
            return View();
        }

        [HttpPost]
        // In your AccountController (Login action):
        public IActionResult Login(string username, string password)
        {
<<<<<<< HEAD
            // Validate the form input
            if (IsValidLogin(model.Username, model.Password))
            {
                // Store the user session
                HttpContext.Session.SetString("Username", model.Username);
                HttpContext.Session.SetString("UserRole", model.Role);

                // Redirect to the appropriate dashboard based on role
                if (model.Role == "Lecturer")
                {
                    return RedirectToAction("Dashboard", "Lecturer");
                }
                else if (model.Role == "Manager" || model.Role == "Coordinator")
                {
                    return RedirectToAction("Dashboard", "Manager");
                }
            }

            // If validation fails, show an error message
            ViewBag.ErrorMessage = "Invalid login credentials.";
            return View(model);  // Return the login view
=======
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
>>>>>>> e67e039fed6ea280849229b3d400860b8a52c9b7
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

<<<<<<< HEAD
        // Helper function for login validation
        private bool IsValidLogin(string username, string password)
        {
            // Check for username and password length and characters as per requirements
            if (username.Length == 4 && password.Length == 8 &&
                !password.Any(char.IsWhiteSpace) &&
                !password.Contains("=") && !password.Contains("+"))
            {
                return true;
            }
            return false;
=======
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
>>>>>>> e67e039fed6ea280849229b3d400860b8a52c9b7
        }
    }
}



















<<<<<<< HEAD





=======
>>>>>>> e67e039fed6ea280849229b3d400860b8a52c9b7
