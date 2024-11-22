using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMCS.PROG6212.ST10271460.Controllers
{
    public class HRController : Controller
    {
        [Authorize(Roles = "HR")]
        public IActionResult Dashboard()
        {
            // Your dashboard logic
            return View();
        }

    }
}
