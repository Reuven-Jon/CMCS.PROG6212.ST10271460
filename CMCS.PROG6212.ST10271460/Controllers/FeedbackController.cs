using Microsoft.AspNetCore.Mvc;
using CMCS.PROG6212.ST10271460.Data;
using CMCS.PROG6212.ST10271460.Models;
using Microsoft.AspNetCore.Authorization;

namespace CMCS.PROG6212.ST10271460.Controllers
{
    [Authorize]
    public class FeedbackController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FeedbackController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize(Roles = "Lecturer, Manager, HR")]
        public IActionResult Submit()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Lecturer, Manager, HR")]
        public IActionResult Submit(Feedback model)
        {
            if (ModelState.IsValid)
            {
                _context.Feedbacks.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "HR, Manager")]
        public IActionResult ViewFeedback()
        {
            var feedbackList = _context.Feedbacks.ToList();
            return View(feedbackList);
        }
    }
}


