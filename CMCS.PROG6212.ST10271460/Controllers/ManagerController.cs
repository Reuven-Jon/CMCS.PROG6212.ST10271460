using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace CMCS.PROG6212.ST10271460.Controllers
{
    public class ManagerController : Controller
    {
        // GET: Coordinator/Manager Dashboard
        public IActionResult Dashboard()
        {
            var username = HttpContext.Session.GetString("Username");
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Login", "Account");
            }

            ViewBag.Username = username;
            return View();  // Navigate to Manager's dashboard
        }
    }
}



