using CMCS.PROG6212.ST10271460.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Roles = "AcademicManager")]
public class ManagerController : Controller
{
    private readonly ApplicationDbContext _context;

    public ManagerController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Dashboard()
    {
        var claims = _context.Claims.ToList();
        return View(claims);
    }

    public IActionResult ManageClaims()
    {
        var pendingClaims = _context.Claims.Where(c => c.Status == (CMCS.PROG6212.ST10271460.Models.ClaimStatus)ClaimStatus.Pending).ToList();
        return View(pendingClaims);
    }

    [HttpPost]
    public IActionResult ApproveClaim(int claimId)
    {
        var claim = _context.Claims.FirstOrDefault(c => c.Id == claimId);
        if (claim != null)
        {
            claim.Status = (CMCS.PROG6212.ST10271460.Models.ClaimStatus)ClaimStatus.Approved;
            _context.SaveChanges();
        }
        return RedirectToAction("ManageClaims");
    }

    [HttpPost]
    public IActionResult RejectClaim(int claimId)
    {
        var claim = _context.Claims.FirstOrDefault(c => c.Id == claimId);
        if (claim != null)
        {
            claim.Status = (CMCS.PROG6212.ST10271460.Models.ClaimStatus)ClaimStatus.Rejected;
            _context.SaveChanges();
        }
        return RedirectToAction("ManageClaims");
    }
}

















