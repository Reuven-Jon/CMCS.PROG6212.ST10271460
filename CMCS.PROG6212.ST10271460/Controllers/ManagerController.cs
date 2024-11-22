using CMCS.PROG6212.ST10271460.Data;
using Microsoft.AspNetCore.Mvc;

namespace CMCS.PROG6212.ST10271460.Controllers
{
    public class ManagerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ManagerController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetString("UserRole") != "Manager")
            {
                return RedirectToAction("AccessDenied", "Account");
            }

            var claims = _context.Claims.ToList();
            return View(claims);
        }

        public IActionResult ManageClaims()
        {
            if (HttpContext.Session.GetString("UserRole") != "Manager")
            {
                return RedirectToAction("AccessDenied", "Account");
            }

            var claims = _context.Claims.Where(c => c.Status.Equals(ClaimStatus.Pending)).ToList();
            return View(claims);
        }

    }
}



















