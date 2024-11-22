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

    public async Task<IActionResult> ManageClaims(string filter = "")
    {
        if (HttpContext.Session.GetString("UserRole") != "Manager")
        {
            return RedirectToAction("AccessDenied", "Account");
        }

        // Query claims with optional filter
        var claimsQuery = _context.Claims.AsQueryable();

        if (!string.IsNullOrEmpty(filter))
        {
            claimsQuery = claimsQuery.Where(c => c.Status.ToString().Equals(filter, StringComparison.OrdinalIgnoreCase));
        }

        var claims = await claimsQuery.OrderByDescending(c => c.DateSubmitted).ToListAsync();

        return View(claims);
    }


    [HttpPost]
    public async Task<IActionResult> ApproveClaim(int id)
    {
        var claim = _context.Claims.FirstOrDefault(c => c.Id == id);

        if (claim != null)
        {
            claim.Status = (CMCS.PROG6212.ST10271460.Models.ClaimStatus.Approved);
            _context.SaveChanges();

            // Notify clients via SignalR
            await _hubContext.Clients.All.SendAsync("ReceiveClaimUpdate", claim.Id, "Approved", "Claim approved by manager.");
        }

        return RedirectToAction("ManageClaims");
    }

    [HttpPost]
    public async Task<IActionResult> RejectClaim(int id)
    {
        var claim = _context.Claims.FirstOrDefault(c => c.Id == id);

        if (claim != null)
        {
            claim.Status = (CMCS.PROG6212.ST10271460.Models.ClaimStatus.Rejected);
            _context.SaveChanges();

            // Notify clients via SignalR
            await _hubContext.Clients.All.SendAsync("ReceiveClaimUpdate", claim.Id, "Rejected", "Claim rejected by manager.");
        }

        return RedirectToAction("ManageClaims");
    }
}



















