using Microsoft.AspNetCore.Mvc;
using System.Linq;
using CMCS.PROG6212.ST10271460.Models;
using Microsoft.AspNetCore.Http;

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
            var username = HttpContext.Session.GetString("Username");
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Login", "Account");
            }

            // Fetch all claims to display in the manager dashboard
            var claims = _context.Claims.ToList();

            ViewBag.Username = username;  // Pass username to the view
            return View(claims);
        }


        // Manager can approve a claim
        public IActionResult ApproveClaim(int id)
        {
            // Find the claim by its ID (primary key)
            var claim = _context.Claims.FirstOrDefault(c => c.Id == id);  // Change ClaimID to Id
            if (claim != null)
            {
                claim.Status = ClaimStatus.Approved;
                _context.SaveChanges();
            }
            return RedirectToAction("Dashboard");
        }

        public IActionResult RejectClaim(int id)
        {
            // Find the claim by its ID (primary key)
            var claim = _context.Claims.FirstOrDefault(c => c.Id == id);  // Change ClaimID to Id
            if (claim != null)
            {
                claim.Status = ClaimStatus.Rejected;
                _context.SaveChanges();
            }
            return RedirectToAction("Dashboard");
        }

    }
}





