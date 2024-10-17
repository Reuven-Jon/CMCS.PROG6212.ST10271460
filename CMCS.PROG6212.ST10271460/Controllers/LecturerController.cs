using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using CMCS.PROG6212.ST10271460.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Threading.Tasks;

namespace CMCS.PROG6212.ST10271460.Controllers
{
    public class LecturerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;  // Add this for file upload

        public LecturerController(ApplicationDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;  // Initialize hosting environment
        }

        public IActionResult Dashboard()
        {
            var role = HttpContext.Session.GetString("UserRole");
            if (role != "Lecturer")
            {
                return RedirectToAction("Login", "Account");
            }

            var username = HttpContext.Session.GetString("Username");
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Login", "Account");
            }

            var claims = GetClaimsForLecturer(username);

            return View(claims);
        }


        private List<Claim> GetClaimsForLecturer(string username)
        {
            return _context.Claims.Where(c => c.ContractorName == username).ToList();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitClaim(ClaimViewModel model, IFormFile Document)
        {
            if (ModelState.IsValid)
            {
                var lecturerId = User.Identity?.Name;
                if (lecturerId == null)
                {
                    ModelState.AddModelError(string.Empty, "User identity is not available.");
                    return View(model);
                }

                var claim = new Claim
                {
                    LecturerId = lecturerId,
                    LecturerName = HttpContext.Session.GetString("Username") ?? string.Empty,  // Use HttpContext.Session to get the Username                                                                                               // Use HttpContext.Session to get the Username
                    ClaimPeriod = model.ClaimPeriod,
                    HoursWorked = model.HoursWorked,
                    HourlyRate = model.HourlyRate,
                    Amount = model.HoursWorked * model.HourlyRate,
                    DateSubmitted = DateTime.Now,
                    Status = "Pending"
                };

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

                return RedirectToAction("Index"); // Redirect to Lecturer Dashboard
            }

            return View(model);  // Return the view with the model for error display
        }



        public IActionResult SubmitClaim()
        {
            var model = new ClaimViewModel(); // Initialize a ClaimViewModel instance
            return View(model);  // Return the view with the ViewModel
        }



    }
}

