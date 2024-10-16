using Microsoft.AspNetCore.Mvc;
using CMCS.PROG6212.ST10271460.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace CMCS.PROG6212.ST10271460.Controllers
{
    public class AccountController : Controller
    {
        // Display the Login page
        public IActionResult Login(string role)
        {
            var model = new LoginViewModel { Role = role };
            ViewBag.Role = role;  // Pass role to the view for display purposes
            return View(model);
        }

        // Handle the Login form submission
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Dummy hardcoded users for simplicity
                var users = new List<User>
                {
                    new User { Name = "Dean", Email = "dean@example.com", Role = Role.Lecturer, Password = "deanPass" },
                    new User { Name = "Kyle", Email = "kyle@example.com", Role = Role.Lecturer, Password = "kylePass" },
                    new User { Name = "Hanna", Email = "hanna@example.com", Role = Role.Lecturer, Password = "hannaPass" },
                    new User { Name = "Lolile", Email = "lolile@example.com", Role = Role.Lecturer, Password = "lolilePass" },
                    new User { Name = "Mike", Email = "mike@example.com", Role = Role.Lecturer, Password = "mikePass" },
                    new User { Name = "Ethan", Email = "ethan@example.com", Role = Role.Manager, Password = "ethanPass" },
                    new User { Name = "Liam", Email = "liam@example.com", Role = Role.Manager, Password = "liamPass" },
                    new User { Name = "Reuven", Email = "reuven@example.com", Role = Role.Manager, Password = "reuvenPass" },
                    new User { Name = "Chyra", Email = "chyra@example.com", Role = Role.Manager, Password = "chyraPass" },
                    new User { Name = "Sihle", Email = "sihle@example.com", Role = Role.Manager, Password = "sihlePass" }
                };

                // Find the user based on credentials
                var user = users.FirstOrDefault(u => u.Name == model.Username && u.Password == model.Password && u.Role.ToString() == model.Role);

                if (user != null)
                {
                    // Set up session or authentication mechanism here
                    HttpContext.Session.SetString("Username", user.Name);
                    HttpContext.Session.SetString("Role", user.Role.ToString());

                    // Redirect based on role
                    if (user.Role == Role.Lecturer)
                    {
                        return RedirectToAction("Dashboard", "Lecturer");
                    }
                    else if (user.Role == Role.Coordinator || user.Role == Role.Manager)
                    {
                        return RedirectToAction("Dashboard", "Manager");
                    }
                }

                // Invalid credentials
                ViewBag.ErrorMessage = "Invalid login attempt. Please check your credentials.";
            }

            // If model is invalid, return back to the login page
            return View(model);
        }

        // Logout action to clear session
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Clear all session data
            return RedirectToAction("Login"); // Redirect to login page
        }
    }
}






