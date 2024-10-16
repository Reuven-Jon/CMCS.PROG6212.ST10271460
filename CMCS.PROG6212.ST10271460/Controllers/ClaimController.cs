using CMCS.PROG6212.ST10271460.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CMCS.PROG6212.ST10271460.Controllers
{
    public class ClaimController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Constructor to inject ApplicationDbContext (database context)
        public ClaimController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Claim/Submit
        public IActionResult Submit()
        {
            return View(); // Render the Submit Claim page
        }

        // POST: Submit a new claim
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Submit(Claim claim)
        {
            if (ModelState.IsValid)
            {
                claim.Status = ClaimStatus.Pending; // Default status
                claim.DateSubmitted = DateTime.Now;
                _context.Add(claim);
                await _context.SaveChangesAsync();
                return RedirectToAction("Dashboard", "Lecturer"); // Redirect to Lecturer's Dashboard after submission
            }
            return View(claim);
        }

        // GET: Claim/Manage (for managers to approve/reject)
        public async Task<IActionResult> Manage()
        {
            var claims = await _context.Claims
                .Include(c => c.Contractor)
                .ToListAsync();
            return View(claims);
        }

        // GET: Claim/Analytics (for managers to view analytics)
        public async Task<IActionResult> Analytics()
        {
            var totalClaims = await _context.Claims.CountAsync();
            var pendingClaims = await _context.Claims.Where(c => c.Status == ClaimStatus.Pending).CountAsync();
            var approvedClaims = await _context.Claims.Where(c => c.Status == ClaimStatus.Approved).CountAsync();
            var rejectedClaims = await _context.Claims.Where(c => c.Status == ClaimStatus.Rejected).CountAsync();

            ViewBag.TotalClaims = totalClaims;
            ViewBag.PendingClaims = pendingClaims;
            ViewBag.ApprovedClaims = approvedClaims;
            ViewBag.RejectedClaims = rejectedClaims;

            return View();
        }
    }
}



