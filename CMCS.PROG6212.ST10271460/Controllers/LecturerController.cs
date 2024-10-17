using CMCS.PROG6212.ST10271460.Models;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

public class LecturerController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IWebHostEnvironment _hostingEnvironment;

    public LecturerController(ApplicationDbContext context, IWebHostEnvironment hostingEnvironment)
    {
        _context = context;
        _hostingEnvironment = hostingEnvironment;
    }

    // Submit a claim view
    public IActionResult SubmitClaim()
    {
        return View();  // Show the form for submitting claims
    }

    // Submit the claim and save it to the database
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SubmitClaim(ClaimViewModel model, IFormFile Document)
    {
        if (ModelState.IsValid)
        {
            var lecturerId = User.Identity?.Name; // Get lecturer's identity
            if (lecturerId == null)
            {
                ModelState.AddModelError(string.Empty, "User is not logged in.");
                return View(model);
            }

            var claim = new Claim
            {
                LecturerId = lecturerId,
                LecturerName = HttpContext.Session.GetString("Username") ?? string.Empty,
                ClaimPeriod = model.ClaimPeriod,
                HoursWorked = model.HoursWorked,
                HourlyRate = model.HourlyRate,
                Amount = model.HoursWorked * model.HourlyRate,
                DateSubmitted = DateTime.Now,
                Status = "Pending"
            };

            // Handle file upload
            if (Document != null && Document.Length > 0)
            {
                var documentPath = Path.Combine(_hostingEnvironment.WebRootPath, "uploads", Document.FileName);
                using (var stream = new FileStream(documentPath, FileMode.Create))
                {
                    await Document.CopyToAsync(stream);
                }
                claim.DocumentPath = "/uploads/" + Document.FileName;
            }

            _context.Claims.Add(claim);
            await _context.SaveChangesAsync();

            return RedirectToAction("YourClaims"); // Redirect to "Your Claims" after successful submission
        }

        return View(model); // Return to the form in case of validation failure
    }

    // Display all claims submitted by the lecturer
    public IActionResult YourClaims()
    {
        var username = HttpContext.Session.GetString("Username");
        if (string.IsNullOrEmpty(username))
        {
            return RedirectToAction("Login", "Account");
        }

        // Get the lecturer's claims
        var claims = _context.Claims.Where(c => c.LecturerName == username).ToList();

        return View(claims);  // Return the list of claims
    }

    public IActionResult Dashboard()
    {
        var username = HttpContext.Session.GetString("Username");
        if (string.IsNullOrEmpty(username))
        {
            return RedirectToAction("Login", "Account");
        }

        // Fetch the lecturer's claims
        var claims = _context.Claims.Where(c => c.LecturerName == username).ToList();

        return View(claims);  // Return the list of claims to the Dashboard view
    }
}
