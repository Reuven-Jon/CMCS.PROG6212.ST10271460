using System.Linq;
using Microsoft.AspNetCore.Mvc;
using CMCS.PROG6212.ST10271460.Models;
using Microsoft.AspNetCore.Authorization;

namespace CMCS.PROG6212.ST10271460.Controllers
{
    [Authorize(Roles = "Lecturer")]
    public class LecturerController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Inject the DbContext into the controller
        public LecturerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Lecturer's Dashboard
        public IActionResult Dashboard()
        {
            var username = HttpContext.Session.GetString("Username");

            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Login", "Account");
            }

            // Call GetClaimsForLecturer method to fetch the lecturer's claims
            var claims = GetClaimsForLecturer(username);

            return View(claims);
        }

        // This method fetches claims for the logged-in lecturer
        private List<Claim> GetClaimsForLecturer(string username)
        {
            return _context.Claims
                .Where(c => c.ContractorName == username)
                .ToList();
        }

        // Submit a new claim
        public IActionResult SubmitClaim()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitClaim(Claim claim)
        {
            if (ModelState.IsValid)
            {
                claim.ContractorName = HttpContext.Session.GetString("Username") ?? "Unknown";
                claim.DateSubmitted = DateTime.Now;
                claim.Status = ClaimStatus.Pending;
                _context.Claims.Add(claim);
                _context.SaveChanges();

                return RedirectToAction("Dashboard");
            }

            return View(claim);
        }
    }
}
