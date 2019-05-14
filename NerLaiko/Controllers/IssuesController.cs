using System;
using Microsoft.AspNetCore.Mvc;
using NerLaiko.Data;
using NerLaiko.Models;

namespace NerLaiko.Controllers
{
    public class IssuesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IssuesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [ActionName("New")]
        public IActionResult NewIssue(Guid fridgeId)
        {
            return View(fridgeId);
        }

        [HttpPost, ActionName("New")]
        public IActionResult NewIssue(Guid fridgeId, [Bind("Issue")] string issue)
        {
            _context.Issues.Add(new Issue
            {
                Id = Guid.NewGuid(),
                FridgeId = fridgeId,
                Description = issue
            });
            _context.SaveChanges();

            return RedirectToAction("Details", "Fridge", new { id = fridgeId });
        }
    }
}