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

        // Submit a new claim
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitClaim(Claim claim, IFormFile Document)
        {
            if (ModelState.IsValid)
            {
                claim.ContractorName = HttpContext.Session.GetString("Username") ?? "Unknown";
                claim.DateSubmitted = DateTime.Now;
                claim.Status = ClaimStatus.Pending;

                // Handle document upload
                if (Document != null && Document.Length > 0)
                {
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/documents");
                    var filePath = Path.Combine(uploadsFolder, Document.FileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        Document.CopyTo(stream);
                    }

                    // Store the document path in the claim
                    claim.DocumentPath = "/documents/" + Document.FileName;
                }

                _context.Claims.Add(claim);
                _context.SaveChanges();

                return RedirectToAction("Dashboard");
            }

            return View(claim);
        }

    }
}


