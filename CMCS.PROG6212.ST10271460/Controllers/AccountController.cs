using Microsoft.AspNetCore.Mvc;
using CMCS.PROG6212.ST10271460.Models;
using Microsoft.AspNetCore.Http;

namespace CMCS.PROG6212.ST10271460.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login(string role)
        {
            ViewBag.Role = role;  // Assign role for display
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
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
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

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
        }

        public IActionResult SubmitClaim()
        {
            var model = new ClaimViewModel();
            return View(model);
        }

    }
}

