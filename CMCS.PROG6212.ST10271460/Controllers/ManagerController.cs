using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using CMCS.PROG6212.ST10271460.Models;
using Microsoft.AspNetCore.SignalR;
using CMCS.PROG6212.ST10271460.Hubs;
using System.Linq;

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
            if (HttpContext.Session.GetString("UserRole") != "Manager")
            {
                return RedirectToAction("AccessDenied", "Account");
            }

            var claims = _context.Claims.ToList();
            return View(claims);
        }

        public IActionResult ApproveClaim(int id)
        {
            if (HttpContext.Session.GetString("UserRole") != "Manager")
            {
                return RedirectToAction("AccessDenied", "Account");
            }

            var claim = _context.Claims.FirstOrDefault(c => c.Id == id);
            if (claim != null)
            {
                claim.Status = ClaimStatus.Approved.ToString();
                _context.SaveChanges();

                // Notify via SignalR
                _hubContext.Clients.All.SendAsync("Notify", $"Claim ID {id} has been approved.");
            }

            return RedirectToAction("Dashboard");
        }

        public IActionResult RejectClaim(int id)
        {
            if (HttpContext.Session.GetString("UserRole") != "Manager")
            {
                return RedirectToAction("AccessDenied", "Account");
            }

            var claim = _context.Claims.FirstOrDefault(c => c.Id == id);
            if (claim != null)
            {
                claim.Status = ClaimStatus.Rejected.ToString();
                _context.SaveChanges();

                // Notify via SignalR
                _hubContext.Clients.All.SendAsync("Notify", $"Claim ID {id} has been rejected.");
            }

            return RedirectToAction("Dashboard");
        }

        public IActionResult Analytics()
        {
            if (HttpContext.Session.GetString("UserRole") != "Manager")
            {
                return RedirectToAction("AccessDenied", "Account");
            }

            var pendingClaims = _context.Claims.Count(c => c.Status == ClaimStatus.Pending.ToString());
            var approvedClaims = _context.Claims.Count(c => c.Status == ClaimStatus.Approved.ToString());
            var rejectedClaims = _context.Claims.Count(c => c.Status == ClaimStatus.Rejected.ToString());

            ViewBag.PendingClaims = pendingClaims;
            ViewBag.ApprovedClaims = approvedClaims;
            ViewBag.RejectedClaims = rejectedClaims;

            return View();
        }
    }
}















