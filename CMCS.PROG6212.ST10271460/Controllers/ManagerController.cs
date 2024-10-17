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
<<<<<<< HEAD
                claim.Status = ClaimStatus.Approved.ToString(); // Update to "Approved"
=======
                claim.Status = ClaimStatus.Approved.ToString(); // Convert enum to string
>>>>>>> e67e039fed6ea280849229b3d400860b8a52c9b7
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
<<<<<<< HEAD
                claim.Status = ClaimStatus.Rejected.ToString(); // Update to "Rejected"
=======
                claim.Status = ClaimStatus.Rejected.ToString(); // Convert enum to string
>>>>>>> e67e039fed6ea280849229b3d400860b8a52c9b7
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













