using CMCS.PROG6212.ST10271460.Data;
using CMCS.PROG6212.ST10271460.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;
using System.IO;

namespace CMCS.PROG6212.ST10271460.Controllers
{
    public class LecturerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly IHubContext<ClaimHub> _claimHubContext;

        public LecturerController(ApplicationDbContext context, IWebHostEnvironment environment, IHubContext<ClaimHub> claimHubContext)
        {
            _context = context;
            _environment = environment;
            _claimHubContext = claimHubContext;
        }

        [HttpGet]
        public IActionResult SubmitClaim()
        {
            // Ensure the user has the Lecturer role
            if (HttpContext.Session.GetString("UserRole") != "Lecturer")
            {
                return RedirectToAction("AccessDenied", "Account");
            }

            // Return an empty ClaimViewModel for the form
            return View(new ClaimViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitClaim(ClaimViewModel model, IFormFile document)
        {
            // Ensure the user has the Lecturer role
            if (HttpContext.Session.GetString("UserRole") != "Lecturer")
            {
                return RedirectToAction("AccessDenied", "Account");
            }

            if (ModelState.IsValid)
            {
                var username = HttpContext.Session.GetString("Username");

                if (string.IsNullOrEmpty(username))
                {
                    ModelState.AddModelError(string.Empty, "Username is missing.");
                    return View(model);
                }

                // Create a new Claim object
                var claim = new Claim
                {
                    LecturerName = username,
                    ClaimPeriod = model.ClaimPeriod,
                    HoursWorked = model.HoursWorked,
                    HourlyRate = model.HourlyRate,
                    Amount = model.HoursWorked * model.HourlyRate,
                    DateSubmitted = DateTime.Now,
                    Status = (CMCS.PROG6212.ST10271460.Models.ClaimStatus.Pending)
                };

                // Handle document upload if provided
                if (document != null && document.Length > 0)
                {
                    var fileName = $"{Guid.NewGuid()}_{document.FileName}"; // Unique file name
                    var filePath = Path.Combine(_environment.WebRootPath, "uploads", fileName);

                    Directory.CreateDirectory(Path.GetDirectoryName(filePath)!); // Ensure directory exists

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await document.CopyToAsync(stream);
                    }

                    claim.DocumentPath = "/uploads/" + fileName;
                }

                try
                {
                    _context.Claims.Add(claim);
                    await _context.SaveChangesAsync();

                    // Notify all connected clients about the new claim using SignalR
                    await _claimHubContext.Clients.All.SendAsync("RefreshClaims");

                    // Redirect to the Dashboard after submission
                    return RedirectToAction("Dashboard");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
                }
            }

            return View(model);
        }

        public async Task<IActionResult> Dashboard(string filter = "", int page = 1, int pageSize = 10)
        {
            if (HttpContext.Session.GetString("UserRole") != "Lecturer")
            {
                return RedirectToAction("AccessDenied", "Account");
            }

            var username = HttpContext.Session.GetString("Username");

            // Query with optional filter
            var claimsQuery = _context.Claims.Where(c => c.LecturerName == username);

            if (!string.IsNullOrEmpty(filter))
            {
                claimsQuery = claimsQuery.Where(c => c.Status.ToString().Equals(filter, StringComparison.OrdinalIgnoreCase));
            }

            // Apply pagination
            var totalClaims = await claimsQuery.CountAsync();
            var claims = await claimsQuery
                .OrderByDescending(c => c.DateSubmitted)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            ViewBag.TotalPages = (int)Math.Ceiling(totalClaims / (double)pageSize);
            ViewBag.CurrentPage = page;

            return View(claims);
        }


    }
}



