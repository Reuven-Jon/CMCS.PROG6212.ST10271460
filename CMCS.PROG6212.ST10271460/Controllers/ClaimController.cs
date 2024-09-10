using CMCS.PROG6212.ST10271460.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Needed for .Include()
using System.Threading.Tasks;
using System.Linq;
using CMCS.PROG6212.ST10271460.Models.CMCS.PROG6212.ST10271460.Models;

//Reuven-Jon Kadalie ST10271460

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

        // GET: Claim/Manage
        public async Task<IActionResult> Manage()
        {
            // Load all claims and include the associated Contractor (User)
            var claims = await _context.Claims
                .Include(c => c.Contractor) // Eager load the Contractor (User) associated with each claim
                .ToListAsync();

            // If no claims are available, send a message to the view
            if (claims == null || !claims.Any())
            {
                ViewBag.Message = "No claims available.";
            }

            return View(claims); // Pass the claims to the Manage view
        }

        // GET: Claim/Analytics
        public async Task<IActionResult> Analytics()
        {
            var totalClaims = await _context.Claims.CountAsync();
            var pendingClaims = await _context.Claims.Where(c => c.Status == ClaimStatus.Pending).CountAsync();
            var approvedClaims = await _context.Claims.Where(c => c.Status == ClaimStatus.Approved).CountAsync();
            var rejectedClaims = await _context.Claims.Where(c => c.Status == ClaimStatus.Rejected).CountAsync();

            // Assign to ViewBag without using '?? 0' because int cannot be null
            ViewBag.TotalClaims = totalClaims;
            ViewBag.PendingClaims = pendingClaims;
            ViewBag.ApprovedClaims = approvedClaims;
            ViewBag.RejectedClaims = rejectedClaims;

            return View(); // Render the Analytics view with this data
        }


        public async Task<IActionResult> Index()
        {
            // Load claims from database (assumed you're using Entity Framework or similar)
            var claims = await _context.Claims.ToListAsync(); // Ensure you are loading claims

            // If no claims are available, make sure to pass an empty list instead of null
            if (claims == null)
            {
                claims = new List<Claim>(); // Initialize to an empty list to avoid null references
            }

            return View(claims); // Pass the claims list to the view
        }


    }
}


