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
        public IActionResult SubmitClaim(Claim claim, IFormFile Document)
        {
            if (ModelState.IsValid)
            {
                if (Document != null && Document.Length > 0)
                {
                    // Save the PDF file to wwwroot/uploads/lecturer_docs
                    var fileName = Path.GetFileName(Document.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/lecturer_docs", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        Document.CopyTo(stream);
                    }

                    // Set the document path to be saved in the database
                    claim.DocumentPath = "/uploads/lecturer_docs/" + fileName;
                }

                claim.ContractorName = HttpContext.Session.GetString("Username") ?? "Unknown";
                claim.Status = ClaimStatus.Pending;
                claim.DateSubmitted = DateTime.Now;
                _context.Claims.Add(claim);
                _context.SaveChanges();

                return RedirectToAction("Dashboard");
            }

            return View(claim);
        }

    }
}

