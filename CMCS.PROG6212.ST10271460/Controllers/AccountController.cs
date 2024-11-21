using Microsoft.AspNetCore.Mvc;
using CMCS.PROG6212.ST10271460.Models;
using Microsoft.AspNetCore.Http;

namespace CMCS.PROG6212.ST10271460.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login(string role)
        {
            ViewBag.Role = role;
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (IsValidLogin(model.Username, model.Password))
            {
                HttpContext.Session.SetString("Username", model.Username);
                HttpContext.Session.SetString("UserRole", model.Role);

                if (model.Role == "Lecturer")
                {
                    return RedirectToAction("Dashboard", "Lecturer");
                }
                else if (model.Role == "Manager")
                {
                    return RedirectToAction("Dashboard", "Manager");
                }
                else if (model.Role == "HR")
                {
                    return RedirectToAction("Dashboard", "HR");
                }
            }

            ViewBag.ErrorMessage = "Invalid login credentials.";
            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        private bool IsValidLogin(string username, string password)
        {
            return username.Length == 4 && password.Length == 8 &&
                   !password.Any(char.IsWhiteSpace) &&
                   !password.Contains("=") && !password.Contains("+");
        }
    }
}
  
