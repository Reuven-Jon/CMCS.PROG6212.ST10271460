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

        public ClaimController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Submit()
        {
            return View(); // Render the Submit Claim page
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Submit(Claim claim)
        {
            if (ModelState.IsValid)
            {
                claim.DateSubmitted = DateTime.Now;
                claim.Status = ClaimStatus.Pending.ToString();  // Correct status assignment
                _context.Add(claim);
                await _context.SaveChangesAsync();
                return RedirectToAction("Dashboard", "Lecturer");
            }
            return View(claim);
        }


        public async Task<IActionResult> Manage()
        {
            var claims = await _context.Claims.ToListAsync();
            return View(claims);
        }

        public async Task<IActionResult> Analytics()
        {
            var pendingClaims = await _context.Claims.Where(c => c.Status == ClaimStatus.Pending.ToString()).CountAsync();
            var approvedClaims = await _context.Claims.Where(c => c.Status == ClaimStatus.Approved.ToString()).CountAsync();
            var rejectedClaims = await _context.Claims.Where(c => c.Status == ClaimStatus.Rejected.ToString()).CountAsync();

            ViewBag.PendingClaims = pendingClaims;
            ViewBag.ApprovedClaims = approvedClaims;
            ViewBag.RejectedClaims = rejectedClaims;

            return View();
        }

    }
}
