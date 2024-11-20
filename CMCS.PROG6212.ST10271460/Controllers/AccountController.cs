using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using CMCS.PROG6212.ST10271460.Models;

namespace CMCS.PROG6212.ST10271460.Controllers
{
    public class AccountController : Controller
    {
        // GET: Login page
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: Handle login form submission
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid && model.Username == "user" && model.Password == "password")
            {
                // Simulated role validation
                HttpContext.Session.SetString("Username", model.Username);
                HttpContext.Session.SetString("Role", model.Role);

                if (model.Role == "Lecturer")
                {
                    return RedirectToAction("Dashboard", "Lecturer");
                }
                else if (model.Role == "Manager")
                {
                    return RedirectToAction("Dashboard", "Manager");
                }
                else
                {
                    ViewBag.ErrorMessage = "Unsupported role.";
                    return View(model);
                }
            }

            ViewBag.ErrorMessage = "Invalid login credentials.";
            return View(model);
        }

        // Logout action
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}


