using Microsoft.AspNetCore.Mvc;

namespace CMCS.PROG6212.ST10271460.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Welcome()
        {
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View("AccessDenied");
        }
    }
}



