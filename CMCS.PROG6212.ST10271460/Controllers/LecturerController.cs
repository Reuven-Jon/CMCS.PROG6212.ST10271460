using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using CMCS.PROG6212.ST10271460.Models;

namespace CMCS.PROG6212.ST10271460.Controllers
{
    public class LecturerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LecturerController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Dashboard()
        {
            var username = HttpContext.Session.GetString("Username");
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Login", "Account");
            }

            // Get the lecturer's claims
            var claims = GetClaimsForLecturer(username);

            ViewBag.Username = username;  // Pass username to the view
            return View(claims);  // Return the list of claims
        }


        // Fetch claims for the logged-in lecturer
        private List<Claim> GetClaimsForLecturer(string username)
        {
            return _context.Claims
                .Where(c => c.ContractorName == username)
                .ToList();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
       
        public async Task<IActionResult> SubmitClaim(ClaimViewModel model, IFormFile Document)
        {
            if (ModelState.IsValid)
            {
                var claim = new Claim
                {
                    LecturerId = User.Identity.Name,  // Assuming lecturer is logged in
                    LecturerName = Context.Session.GetString("Username"),
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

            return View(model);
        }

    }
}

