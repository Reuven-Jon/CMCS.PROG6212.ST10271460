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
            var role = HttpContext.Session.GetString("UserRole");
            if (role != "Coordinator")
            {
                return RedirectToAction("Login", "Account");
            }

            var claims = _context.Claims.ToList();
            return View(claims);
        }


        // Manager can approve a claim
        public IActionResult ApproveClaim(int id)
        {
            var claim = _context.Claims.FirstOrDefault(c => c.Id == id);
            if (claim != null)
            {
                claim.Status = ClaimStatus.Approved.ToString(); // Convert enum to string
                _context.SaveChanges();
            }
            return RedirectToAction("Dashboard");
        }

        public IActionResult RejectClaim(int id)
        {
            var claim = _context.Claims.FirstOrDefault(c => c.Id == id);
            if (claim != null)
            {
                claim.Status = ClaimStatus.Rejected.ToString(); // Convert enum to string
                _context.SaveChanges();
            }
            return RedirectToAction("Dashboard");
        }


        [HttpPost]
        public IActionResult UpdateClaimStatus(int claimId, string status, string managerNote)
        {
            var claim = _context.Claims.Find(claimId);
            if (claim != null)
            {
                claim.Status = status;
                claim.Notes = managerNote;
                _context.SaveChanges();
            }

            return Ok();
        }


    }
}





