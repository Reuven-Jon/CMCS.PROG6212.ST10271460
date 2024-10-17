using CMCS.PROG6212.ST10271460.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CMCS.PROG6212.ST10271460.Controllers
{
    public class ClaimController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ClaimController(ApplicationDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: Claim/Submit
        public IActionResult Submit()
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
            var pendingClaims = await _context.Claims.Where(c => c.Status == "Pending").CountAsync();
            var approvedClaims = await _context.Claims.Where(c => c.Status == "Approved").CountAsync();
            var rejectedClaims = await _context.Claims.Where(c => c.Status == "Rejected").CountAsync();

            ViewBag.PendingClaims = pendingClaims;
            ViewBag.ApprovedClaims = approvedClaims;
            ViewBag.RejectedClaims = rejectedClaims;

            return View();
        }
    }
}
