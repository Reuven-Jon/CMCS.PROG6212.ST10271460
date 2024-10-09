using CMCS.PROG6212.ST10271460.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Needed for .Include()
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;

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
        // Admin/Contractor view: Load all claims and allow search
        public async Task<IActionResult> Manage(string searchQuery = "")
        {
            try
            {
                // Start with the base query to get all claims and include the associated Contractor (User)
                var claimsQuery = _context.Claims
                    .Include(c => c.Contractor) // Eager load the Contractor (User) associated with each claim
                    .AsQueryable(); // Ensure the query is still IQueryable to chain filtering and ordering

                // If a search query is provided, filter claims by Contractor Name (null-safe check)
                if (!string.IsNullOrEmpty(searchQuery))
                {
                    claimsQuery = claimsQuery.Where(c => c.Contractor != null && c.Contractor.Name != null && c.Contractor.Name.Contains(searchQuery));
                }

                // Apply ordering alphabetically by Contractor's Name after filtering
                var orderedClaims = claimsQuery.OrderBy(c => c.Contractor != null ? c.Contractor.Name : string.Empty);

                // Execute the query and get the list of claims
                var claims = await orderedClaims.ToListAsync();

                // If no claims are available, send a message to the view
                if (claims == null || !claims.Any())
                {
                    ViewBag.Message = "No claims available.";
                }

                return View(claims); // Pass the claims to the Manage view
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logger here)
                Console.WriteLine(ex.Message);
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }


        // GET: Claim/Analytics
        public async Task<IActionResult> Analytics()
        {
            // Count total claims and group by their status (Pending, Approved, Rejected)
            var totalClaims = await _context.Claims.CountAsync();
            var pendingClaims = await _context.Claims.Where(c => c.Status == ClaimStatus.Pending).CountAsync();
            var approvedClaims = await _context.Claims.Where(c => c.Status == ClaimStatus.Approved).CountAsync();
            var rejectedClaims = await _context.Claims.Where(c => c.Status == ClaimStatus.Rejected).CountAsync();

            // Assign claim statistics to ViewBag for displaying in the view
            ViewBag.TotalClaims = totalClaims;
            ViewBag.PendingClaims = pendingClaims;
            ViewBag.ApprovedClaims = approvedClaims;
            ViewBag.RejectedClaims = rejectedClaims;

            return View(); // Render the Analytics view with this data
        }

        // GET: Claim/MyClaims
        // Lecturer view: Show only logged-in Lecturer's claims grouped by status
        [Authorize(Roles = "Contractor")]  // Only Lecturers can access this
        public async Task<IActionResult> MyClaims()
        {
            // Get the current logged-in user's ID (Contractor)
            var lecturerId = User.Identity.Name; // Assumes User.Identity.Name stores unique identifier (e.g., email or username)

            // Retrieve only the logged-in lecturer's claims, ordered by submission date (null safe check for ContractorName)
            var claims = await _context.Claims
                .Where(c => c.ContractorName != null && c.ContractorName == lecturerId)
                .OrderBy(c => c.DateSubmitted)
                .ToListAsync();

            return View(claims);  // Pass claims to the MyClaims view
        }

        // GET: Claim/Index
        public async Task<IActionResult> Index()
        {
            // Load all claims from the database (ensuring you're using Entity Framework)
            var claims = await _context.Claims.ToListAsync(); // Ensure you are loading claims

            // If no claims are available, initialize to an empty list to avoid null references
            if (claims == null)
            {
                claims = new List<Claim>(); // Initialize to an empty list if no claims exist
            }

            return View(claims); // Pass the claims list to the view
        }
    }
}

