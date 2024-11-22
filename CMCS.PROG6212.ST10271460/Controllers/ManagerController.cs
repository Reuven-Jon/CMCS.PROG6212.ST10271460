using Microsoft.AspNetCore.SignalR;
using CMCS.PROG6212.ST10271460.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class ManagerController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IHubContext<ClaimHub> _hubContext;

    public ManagerController(ApplicationDbContext context, IHubContext<ClaimHub> hubContext)
    {
        _context = context;
        _hubContext = hubContext;
    }
    public IActionResult Dashboard()
    {
        if (HttpContext.Session.GetString("UserRole") != "Manager")
        {
            return RedirectToAction("AccessDenied", "Account");
        }

        var claims = _context.Claims.ToList(); // Fetch claims
        return View(claims);
    }

    public IActionResult ManageClaims()
    {
        if (HttpContext.Session.GetString("UserRole") != "Manager")
        {
            return RedirectToAction("AccessDenied", "Account");
        }

        var claims = _context.Claims.Where(c => c.Status == (CMCS.PROG6212.ST10271460.Models.ClaimStatus.Pending)).ToList();
        return View(claims);
    }




    [HttpPost]
    public IActionResult ApproveClaim(int id)
    {
        var claim = _context.Claims.FirstOrDefault(c => c.Id == id);
        if (claim != null)
        {
            claim.Status = (CMCS.PROG6212.ST10271460.Models.ClaimStatus.Approved);
            _context.SaveChanges();
            TempData["Message"] = "Claim approved successfully.";
        }
        return RedirectToAction("ManageClaims");
    }

    [HttpPost]
    public IActionResult RejectClaim(int id)
    {
        var claim = _context.Claims.FirstOrDefault(c => c.Id == id);
        if (claim != null)
        {
            claim.Status = (CMCS.PROG6212.ST10271460.Models.ClaimStatus.Rejected);
            _context.SaveChanges();
            TempData["Message"] = "Claim rejected successfully.";
        }
        return RedirectToAction("ManageClaims");
    }

}



















