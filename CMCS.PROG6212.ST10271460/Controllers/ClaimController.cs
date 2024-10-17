using CMCS.PROG6212.ST10271460.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CMCS.PROG6212.ST10271460.Controllers

    {
<<<<<<< HEAD
        public class ClaimController : Controller
=======
        private readonly ApplicationDbContext _context;

        public ClaimController(ApplicationDbContext context)
>>>>>>> e67e039fed6ea280849229b3d400860b8a52c9b7
        {
            private readonly ApplicationDbContext _context;
            private readonly IWebHostEnvironment _hostingEnvironment; // Add this field

<<<<<<< HEAD
            public ClaimController(ApplicationDbContext context, IWebHostEnvironment hostingEnvironment) // Modify constructor
            {
                _context = context;
                _hostingEnvironment = hostingEnvironment; // Initialize the field
            }


    // GET: Claim/Submit
    public IActionResult Submit()
=======
        public IActionResult Submit()
>>>>>>> e67e039fed6ea280849229b3d400860b8a52c9b7
        {
            return View(); // Render the Submit Claim page
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Submit(Claim claim, IFormFile Document)
        {
            if (ModelState.IsValid)
            {
                claim.DateSubmitted = DateTime.Now;
<<<<<<< HEAD
                claim.Status = "Pending";

                if (Document != null && Document.Length > 0)
                {
                    var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "uploads", Document.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await Document.CopyToAsync(stream);
                    }
                    claim.DocumentPath = "/uploads/" + Document.FileName;
                }

                _context.Add(claim);
                await _context.SaveChangesAsync();

=======
                claim.Status = ClaimStatus.Pending.ToString();  // Correct status assignment
                _context.Add(claim);
                await _context.SaveChangesAsync();
>>>>>>> e67e039fed6ea280849229b3d400860b8a52c9b7
                return RedirectToAction("Dashboard", "Lecturer");
            }
            return View(claim);
        }

<<<<<<< HEAD
=======

>>>>>>> e67e039fed6ea280849229b3d400860b8a52c9b7
        public async Task<IActionResult> Manage()
        {
            var claims = await _context.Claims.ToListAsync();
            return View(claims);
        }
<<<<<<< HEAD
=======

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

>>>>>>> e67e039fed6ea280849229b3d400860b8a52c9b7
    }
}
