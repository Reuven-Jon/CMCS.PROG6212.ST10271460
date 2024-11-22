using CMCS.PROG6212.ST10271460.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CMCS.PROG6212.ST10271460.Data;

public class LecturerController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IWebHostEnvironment _hostingEnvironment;

    public LecturerController(ApplicationDbContext context, IWebHostEnvironment hostingEnvironment)
    {
        _context = context;
        _hostingEnvironment = hostingEnvironment;
    }

    [HttpGet]
    public IActionResult SubmitClaim()
    {
        // Check if the logged-in user is a Lecturer
        if (HttpContext.Session.GetString("UserRole") != "Lecturer")
        {
            return RedirectToAction("AccessDenied", "Account");
        }
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SubmitClaim(ClaimViewModel model, IFormFile Document)
    {
        if (HttpContext.Session.GetString("UserRole") != "Lecturer")
        {
            return RedirectToAction("AccessDenied", "Account");
        }

        if (ModelState.IsValid)
        {
            var claim = new Claim
            {
                LecturerName = HttpContext.Session.GetString("Username") ?? string.Empty,
                ClaimPeriod = model.ClaimPeriod,
                HoursWorked = model.HoursWorked,
                HourlyRate = model.HourlyRate,
                Amount = model.HoursWorked * model.HourlyRate,
                DateSubmitted = DateTime.Now,
                Status = (CMCS.PROG6212.ST10271460.Models.ClaimStatus)ClaimStatus.Pending

            };

            // File validation for document upload
            if (Document != null && Document.Length > 0)
            {
                var allowedExtensions = new[] { ".pdf", ".docx", ".xlsx" };
                var fileExtension = Path.GetExtension(Document.FileName);

                if (!allowedExtensions.Contains(fileExtension.ToLower()))
                {
                    ModelState.AddModelError("", "Invalid file type. Only PDF, DOCX, and XLSX are allowed.");
                    return View(model);
                }

                var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "uploads", Document.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await Document.CopyToAsync(stream);
                }
                claim.DocumentPath = "/uploads/" + Document.FileName;
            }

            _context.Claims.Add(claim);
            await _context.SaveChangesAsync();
            return RedirectToAction("Dashboard");
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

