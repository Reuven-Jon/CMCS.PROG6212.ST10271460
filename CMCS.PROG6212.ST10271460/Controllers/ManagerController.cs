using Microsoft.AspNetCore.Mvc;
using System.Linq;
using CMCS.PROG6212.ST10271460.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using CMCS.PROG6212.ST10271460.Hubs; // Add this using directive

namespace CMCS.PROG6212.ST10271460.Controllers
{
    public class ManagerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHubContext<NotificationHub> _hubContext;

        public ManagerController(ApplicationDbContext context, IHubContext<NotificationHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
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
            var claim = _context.Claims.FirstOrDefault(c => c.Id == id);
            if (claim != null)
            {
                claim.Status = ClaimStatus.Approved.ToString(); // Update to "Approved"
                _context.SaveChanges();

                // Notify connected clients of the change
                _hubContext.Clients.All.SendAsync("ReceiveClaimUpdate", claim.Id, claim.Status, claim.Notes);
            }
            return RedirectToAction("Dashboard");
        }

        public IActionResult RejectClaim(int id)
        {
            var claim = _context.Claims.FirstOrDefault(c => c.Id == id);
            if (claim != null)
            {
                claim.Status = ClaimStatus.Rejected.ToString(); // Update to "Rejected"
                _context.SaveChanges();

                // Notify connected clients of the change
                _hubContext.Clients.All.SendAsync("ReceiveClaimUpdate", claim.Id, claim.Status, claim.Notes);
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













