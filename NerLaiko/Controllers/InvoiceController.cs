using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NerLaiko.Data;
using NerLaiko.Helper;

namespace NerLaiko.Controllers
{
    [Authorize]
    public class InvoiceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InvoiceController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var userFridges = _context.Users // User table
                .Include(t => t.Refrigerators) // Populates Refrigerators IEnumerable
                .ThenInclude(r => r.Invoices) // Populates the Invoices IEnumerable property of every fridge
                .Single(u => u.Id == User.GetId()) // Actually gets the user
                .Refrigerators;
            return View(userFridges);
        }

        public IActionResult View(Guid? id)
        {
            // User wandered over
            return id == null ? View(null) : View(_context.Invoices.SingleOrDefault(i => i.Id == id));
        }
    }
}