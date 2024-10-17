using CMCS.PROG6212.ST10271460.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CMCS.PROG6212.ST10271460.Controllers

    {
        public class ClaimController : Controller
        {
            private readonly ApplicationDbContext _context;
            private readonly IWebHostEnvironment _hostingEnvironment; // Add this field

            public ClaimController(ApplicationDbContext context, IWebHostEnvironment hostingEnvironment) // Modify constructor
            {
                _context = context;
                _hostingEnvironment = hostingEnvironment; // Initialize the field
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
    }
}
