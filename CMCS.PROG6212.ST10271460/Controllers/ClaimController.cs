using CMCS.PROG6212.ST10271460.Data;
using CMCS.PROG6212.ST10271460.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CMCS.PROG6212.ST10271460.Controllers
{
    public class ClaimController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClaimController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Lecturer")]
        public IActionResult Submit()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Lecturer")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Submit(ClaimViewModel model, IFormFile document)
        {
            if (ModelState.IsValid)
            {
                var username = HttpContext.Session.GetString("Username");
                if (string.IsNullOrEmpty(username))
                {
                    return RedirectToAction("Login", "Account");
                }

                var claim = new Claim
                {
                    LecturerName = username,
                    ClaimPeriod = model.ClaimPeriod,
                    HoursWorked = model.HoursWorked,
                    HourlyRate = model.HourlyRate,
                    Amount = model.HoursWorked * model.HourlyRate,
                    Overtime = model.HoursWorked > 160 ? (model.HoursWorked - 160) * (model.HourlyRate * 1.5m) : 0,
                    SpecialAllowance = model.SpecialAllowance,
                    DateSubmitted = DateTime.Now,
                    Status = (CMCS.PROG6212.ST10271460.Models.ClaimStatus.Pending)
                };

                if (document != null && document.Length > 0)
                {
                    var filePath = Path.Combine("uploads", document.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await document.CopyToAsync(stream);
                    }
                    claim.DocumentPath = "/uploads/" + document.FileName;
                }

                _context.Add(claim);
                await _context.SaveChangesAsync();

                return RedirectToAction("Dashboard", "Lecturer");
            }

            return View(model);
        }

        [Authorize(Roles = "Manager")]
        public IActionResult Manage()
        {
            var claims = _context.Claims.ToList();
            return View(claims);
        }

        [Authorize(Roles = "Manager")]
        public IActionResult ApproveClaim(int id)
        {
            var claim = _context.Claims.Find(id);
            if (claim != null && claim.Status.Equals(ClaimStatus.Pending))
            {
                claim.Status = (CMCS.PROG6212.ST10271460.Models.ClaimStatus.Approved);
                _context.SaveChanges();
            }
            return RedirectToAction("Manage");
        }

        [Authorize(Roles = "Manager")]
        public IActionResult RejectClaim(int id)
        {
            var claim = _context.Claims.Find(id);
            if (claim != null && claim.Status.Equals(ClaimStatus.Pending))
            {
                claim.Status = (CMCS.PROG6212.ST10271460.Models.ClaimStatus.Rejected);
                _context.SaveChanges();
            }
            return RedirectToAction("Manage");
        }

    }
}
