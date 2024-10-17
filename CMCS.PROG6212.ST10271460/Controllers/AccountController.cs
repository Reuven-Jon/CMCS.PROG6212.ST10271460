﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using CMCS.PROG6212.ST10271460.Models;

namespace CMCS.PROG6212.ST10271460.Controllers
{
    public class AccountController : Controller
    {
        // GET: Display the Login page
        public IActionResult Login(string role)
        {
            ViewBag.Role = role;  // Pass role to the view
            return View();
        }

        // POST: Handle the Login form submission
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ErrorMessage = "Please fill out all required fields.";
                return View(model);
            }

            // Dummy user data
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

            var user = users.FirstOrDefault(u => u.Name == model.Username && u.Password == model.Password && u.Role.ToString() == model.Role);

            if (user != null)
            {
                // Set session
                HttpContext.Session.SetString("Username", user.Name);
                HttpContext.Session.SetString("Role", user.Role.ToString());

                // Redirect based on role
                if (user.Role == Role.Lecturer)
                {
                    return RedirectToAction("Dashboard", "Lecturer");
                }
                else if (user.Role == Role.Manager || user.Role == Role.Coordinator)
                {
                    return RedirectToAction("Dashboard", "Manager");
                }
            }

            // Invalid login
            ViewBag.ErrorMessage = "Invalid login attempt. Please check your credentials.";
            return View(model);
        }

        // GET: Logout and clear session
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Clear all session data
            return RedirectToAction("Login"); // Redirect to login page
        }
    }
}


















               





