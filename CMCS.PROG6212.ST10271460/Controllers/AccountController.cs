using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using CMCS.PROG6212.ST10271460.Models;

namespace CMCS.PROG6212.ST10271460.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login(string role = "")
        {
            ViewBag.Role = role;
            return View(new LoginViewModel { Role = role });
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            // Validate username and password requirements
            if (ModelState.IsValid && IsValidLogin(model.Username, model.Password))
            {
                // Set session variables for user login
                HttpContext.Session.SetString("Username", model.Username);
                HttpContext.Session.SetString("UserRole", model.Role);

                // Redirect to the appropriate dashboard
                switch (model.Role)
                {
                    case "Lecturer":
                        return RedirectToAction("Dashboard", "Lecturer");
                    case "Manager":
                        return RedirectToAction("Dashboard", "Manager");
                    case "HR":
                        return RedirectToAction("Dashboard", "HR");
                    default:
                        return RedirectToAction("AccessDenied");
                }
            }

            // Show error message if validation fails
            ViewBag.ErrorMessage = "Invalid login credentials. Please try again.";
            return View(model);
        }

        public IActionResult Logout()
        {
            // Clear session variables on logout
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        private bool IsValidLogin(string username, string password)
        {
            // Ensure username is exactly 4 characters and password is exactly 8 characters
            return username.Length == 4 &&
                   password.Length == 8 &&
                   !password.Any(char.IsWhiteSpace) &&
                   !password.Contains("=") &&
                   !password.Contains("+");
        }
    }
}
