using CMCS.PROG6212.ST10271460.Models;

using Microsoft.AspNetCore.Mvc;

namespace CMCS.PROG6212.ST10271460.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

