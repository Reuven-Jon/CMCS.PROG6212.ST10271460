using Microsoft.AspNetCore.Mvc;

namespace CMCS.PROG6212.ST10271460.Controllers
{
    public class HRController : Controller
    {
        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetString("UserRole") != "HR")
            {
                return RedirectToAction("AccessDenied", "Account");
            }

            return View();
        }
    }
}


