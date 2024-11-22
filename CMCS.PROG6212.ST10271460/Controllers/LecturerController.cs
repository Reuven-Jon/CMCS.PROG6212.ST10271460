using CMCS.PROG6212.ST10271460.Data;
using CMCS.PROG6212.ST10271460.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CMCS.PROG6212.ST10271460.Controllers
{
    public class LecturerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LecturerController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult SubmitClaim()
        {
            if (HttpContext.Session.GetString("UserRole") != "Lecturer")
            {
                return RedirectToAction("AccessDenied", "Account");
            }
            return View(new ClaimViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitClaim(ClaimViewModel model, IFormFile document)
        {
            if (HttpContext.Session.GetString("UserRole") != "Lecturer")
            {
                return RedirectToAction("AccessDenied", "Account");
            }

            if (ModelState.IsValid)
            {
                var username = HttpContext.Session.GetString("Username");
                if (string.IsNullOrEmpty(username))
                {
                    return RedirectToAction("Login", "Account");
                }

                // Create new claim
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

                // Handle document upload
                if (document != null && document.Length > 0)
                {
                    var filePath = Path.Combine("wwwroot/uploads", document.FileName);
                    Directory.CreateDirectory(Path.GetDirectoryName(filePath)!); // Ensure the directory exists
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await document.CopyToAsync(stream);
                    }
                    claim.DocumentPath = "/uploads/" + document.FileName;
                }

                _context.Claims.Add(claim);
                await _context.SaveChangesAsync();

                return RedirectToAction("Dashboard", "Lecturer");
            }

            return View(model);
        }

        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetString("UserRole") != "Lecturer")
            {
                return RedirectToAction("AccessDenied", "Account");
            }

            var username = HttpContext.Session.GetString("Username");
            var claims = _context.Claims.Where(c => c.LecturerName == username).ToList();
            return View(claims);
        }
    }
}









