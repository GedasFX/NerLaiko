using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NerLaiko.Data;
using NerLaiko.Helper;
using NerLaiko.Models;
using NerLaiko.ViewModels;

namespace NerLaiko.Controllers
{
    [Authorize]
    public class FridgeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FridgeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Fridge
        public IActionResult FridgeList()
        {
            var userFridges = _context.Users // User table
                .Include(t => t.Refrigerators) // Populates Refrigerators IEnumerable
                .ThenInclude(r => r.Invoices) // Populates the Invoices IEnumerable property of every fridge
                .Single(u => u.Id == User.GetId()) // Actually gets the user
                .Refrigerators;
            return View(userFridges);
        }

        // GET: Fridge/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refrigerator = await _context.Refrigerators
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (refrigerator == null)
            {
                return NotFound();
            }

            return View(refrigerator);
        }

        // GET: Fridge/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Fridge/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FridgeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var fridge = new Refrigerator
                {
                    Id = Guid.NewGuid(),
                    Location = model.Location,
                    UserId = User.GetId(),
                    State = FridgeState.Deactivated
                };

                _context.Add(fridge);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(FridgeList));
            }

            return View(model);
        }

        // GET: Fridge/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refrigerator = await _context.Refrigerators.FindAsync(id);
            if (refrigerator == null)
            {
                return NotFound();
            }

            return View(new FridgeViewModel { Location = refrigerator.Location });
        }

        // POST: Fridge/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, FridgeViewModel model)
        {
            var fridge = _context.Refrigerators.Find(id);
            if (fridge == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                fridge.Location = model.Location;
                _context.Update(fridge);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(FridgeList));
            }

            return View(model);
        }

        // GET: Fridge/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refrigerator = await _context.Refrigerators
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (refrigerator == null)
            {
                return NotFound();
            }

            return View(refrigerator);
        }

        // POST: Fridge/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var refrigerator = await _context.Refrigerators.FindAsync(id);
            _context.Refrigerators.Remove(refrigerator);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(FridgeList));
        }

        private bool RefrigeratorExists(Guid id)
        {
            return _context.Refrigerators.Any(e => e.Id == id);
        }
    }
}
