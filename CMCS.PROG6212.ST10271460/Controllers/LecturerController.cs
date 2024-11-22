using Azure.Identity;
using CMCS.PROG6212.ST10271460.Data;
using CMCS.PROG6212.ST10271460.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Roles = "Lecturer")]
public class LecturerController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IWebHostEnvironment _hostingEnvironment;

    public LecturerController(ApplicationDbContext context, IWebHostEnvironment hostingEnvironment)
    {
        _context = context;
        _hostingEnvironment = hostingEnvironment;
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

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SubmitClaim(ClaimViewModel model, IFormFile document)
    {
        if (ModelState.IsValid)
        {
            var username = HttpContext.Session.GetString("Username");
            if (string.IsNullOrEmpty(username))
            {
                // Handle the case where the username is null or empty
                return RedirectToAction("Login", "Account");
            }

            var claim = new Claim
            {
                LecturerName = username,
                ClaimPeriod = model.ClaimPeriod,
                HoursWorked = model.HoursWorked,
                HourlyRate = model.HourlyRate,
                Amount = model.HoursWorked * model.HourlyRate,
                DateSubmitted = DateTime.Now,
                Status = (CMCS.PROG6212.ST10271460.Models.ClaimStatus)ClaimStatus.Pending
            };

            if (document != null && document.Length > 0)
            {
                var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "uploads", document.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await document.CopyToAsync(stream);
                }
                claim.DocumentPath = "/uploads/" + document.FileName;
            }

            _context.Claims.Add(claim);
            await _context.SaveChangesAsync();
            return RedirectToAction("Dashboard");
        }

        return View(model);
    }

}






