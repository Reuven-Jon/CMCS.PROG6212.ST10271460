using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CMCS.PROG6212.ST10271460.Models;
using System.Linq;
using CMCS.PROG6212.ST10271460.Data;

namespace CMCS.PROG6212.ST10271460.Controllers
{
    [Authorize(Roles = "AcademicManager")]
    public class ManagerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ManagerController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetString("UserRole") != "AcademicManager")
            {
                return RedirectToAction("AccessDenied", "Account");
            }

            var claims = _context.Claims.ToList();
            return View(claims);
        }

        public IActionResult ManageClaims()
        {
            if (HttpContext.Session.GetString("UserRole") != "AcademicManager")
            {
                return RedirectToAction("AccessDenied", "Account");
            }

            var pendingClaims = _context.Claims.Where(c => c.Status.Equals(ClaimStatus.Pending)).ToList();
            return View(pendingClaims);
        }

        [HttpPost]
        public IActionResult ApproveClaim(int claimId)
        {
            var claim = _context.Claims.FirstOrDefault(c => c.Id == claimId);
            if (claim != null)
            {
                claim.Status = ClaimStatus.Approved;
                _context.SaveChanges();
            }
            return RedirectToAction("ManageClaims");
        }

        [HttpPost]
        public IActionResult RejectClaim(int claimId)
        {
            var claim = _context.Claims.FirstOrDefault(c => c.Id == claimId);
            if (claim != null)
            {
                claim.Status = ClaimStatus.Rejected;
                _context.SaveChanges();
            }
            return RedirectToAction("ManageClaims");
        }
    }
}
















