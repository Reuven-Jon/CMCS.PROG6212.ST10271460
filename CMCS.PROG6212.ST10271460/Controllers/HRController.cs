using Microsoft.AspNetCore.Mvc;
using ClosedXML.Excel;
using System.IO;
using CMCS.PROG6212.ST10271460.Data;
using CMCS.PROG6212.ST10271460.Models;

namespace CMCS.PROG6212.ST10271460.Controllers
{
    public class HRController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HRController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetString("UserRole") != "HR")
            {
                return RedirectToAction("AccessDenied", "Account");
            }

            var claims = _context.Claims.ToList();
            return View(claims);
        }

        [HttpGet]
        public IActionResult ManageLecturers()
        {
            if (HttpContext.Session.GetString("UserRole") != "HR")
            {
                return RedirectToAction("AccessDenied", "Account");
            }

            var lecturers = _context.Lecturers.ToList(); // Assuming you have a Lecturers table
            return View(lecturers);
        }

        [HttpPost]
        public IActionResult UpdateLecturer(Lecturer lecturer)
        {
            if (HttpContext.Session.GetString("UserRole") != "HR")
            {
                return RedirectToAction("AccessDenied", "Account");
            }

            var existingLecturer = _context.Lecturers.FirstOrDefault(l => l.Id == lecturer.Id);
            if (existingLecturer != null)
            {
                existingLecturer.Name = lecturer.Name;
                existingLecturer.Email = lecturer.Email;
                existingLecturer.HourlyRate = lecturer.HourlyRate;
                _context.SaveChanges();
                TempData["Message"] = "Lecturer information updated successfully.";
            }
            return RedirectToAction("ManageLecturers");
        }

        public IActionResult DownloadApprovedClaimsReport()
        {
            if (HttpContext.Session.GetString("UserRole") != "HR")
            {
                return RedirectToAction("AccessDenied", "Account");
            }

            var claims = _context.Claims
                .Where(c => c.Status == (CMCS.PROG6212.ST10271460.Models.ClaimStatus.Approved))
                .ToList();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Approved Claims");
                worksheet.Cell(1, 1).Value = "Lecturer Name";
                worksheet.Cell(1, 2).Value = "Claim Period";
                worksheet.Cell(1, 3).Value = "Hours Worked";
                worksheet.Cell(1, 4).Value = "Amount";
                worksheet.Cell(1, 5).Value = "Date Submitted";

                for (int i = 0; i < claims.Count; i++)
                {
                    worksheet.Cell(i + 2, 1).Value = claims[i].LecturerName;
                    worksheet.Cell(i + 2, 2).Value = claims[i].ClaimPeriod.ToString("MMMM yyyy");
                    worksheet.Cell(i + 2, 3).Value = claims[i].HoursWorked;
                    worksheet.Cell(i + 2, 4).Value = claims[i].Amount;
                    worksheet.Cell(i + 2, 5).Value = claims[i].DateSubmitted.ToString("dd MMM yyyy");
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    stream.Position = 0;
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ApprovedClaimsReport.xlsx");
                }
            }
        }
    }
}



