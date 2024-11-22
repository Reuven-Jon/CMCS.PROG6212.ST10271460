using CMCS.PROG6212.ST10271460.Data;
using CMCS.PROG6212.ST10271460.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CMCS.PROG6212.ST10271460.Controllers
{
    public class ClaimController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClaimController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Submit()
        {
            if (HttpContext.Session.GetString("UserRole") != "Lecturer")
            {
                return RedirectToAction("AccessDenied", "Account");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Submit(Claim claim)
        {
            if (HttpContext.Session.GetString("UserRole") != "Lecturer")
            {
                return RedirectToAction("AccessDenied", "Account");
            }

            if (ModelState.IsValid)
            {
                claim.DateSubmitted = DateTime.Now;
                claim.Status = (ClaimStatus)Enum.Parse(typeof(ClaimStatus), "Pending");

                _context.Add(claim);
                await _context.SaveChangesAsync();
                return RedirectToAction("Dashboard", "Lecturer");
            }

            return View(claim);
        }

        public IActionResult Manage()
        {
            if (HttpContext.Session.GetString("UserRole") != "HR")
            {
                return RedirectToAction("AccessDenied", "Account");
            }

            var claims = _context.Claims.ToList();
            return View(claims);
        }

        public IActionResult StatusReport()
        {
            if (HttpContext.Session.GetString("UserRole") != "Manager")
            {
                return RedirectToAction("AccessDenied", "Account");
            }

            var pendingClaims = _context.Claims.Count(c => c.Status == ClaimStatus.Pending);
            var approvedClaims = _context.Claims.Count(c => c.Status == ClaimStatus.Approved);
            var rejectedClaims = _context.Claims.Count(c => c.Status == ClaimStatus.Rejected);

            ViewBag.PendingClaims = pendingClaims;
            ViewBag.ApprovedClaims = approvedClaims;
            ViewBag.RejectedClaims = rejectedClaims;

            return View();
        }

    }
}

