using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
            // Validate username and password length in the model itself
            if (!ModelState.IsValid)
            {
                ViewBag.ErrorMessage = "Invalid login attempt. Please check your credentials.";
                return View(model);
            }

            // Dummy logic to allow login for valid usernames and passwords
            HttpContext.Session.SetString("Username", model.Username);
            HttpContext.Session.SetString("Role", model.Role);

            if (model.Role == "Lecturer")
            {
                return RedirectToAction("Dashboard", "Lecturer");
            }
            else if (model.Role == "Manager" || model.Role == "Coordinator")
            {
                return RedirectToAction("Dashboard", "Manager");
            }

            ViewBag.ErrorMessage = "Invalid role specified.";
            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}























