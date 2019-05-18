using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NerLaiko.Data;
using NerLaiko.Models;
using NerLaiko.ViewModels;

namespace NerLaiko.Controllers
{
    [Authorize]
    public class RordersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RordersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Rorders
        public IActionResult List(Guid fridgeId)
        {
            var fridge = _context.Refrigerators
                .Include(o => o.Orders)
                .ThenInclude(o => o.OrderItems)
                .ThenInclude(oi => oi.Item)
                .SingleOrDefault(o => o.Id == fridgeId);
            return View(fridge);
        }

        // GET: Rorders/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Item)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Rorders/Create
        public IActionResult Create(Guid fridgeId)
        {
            ViewData["Items"] = new SelectList(_context.Items, "Id", "Name");
            ViewBag.FridgeId = fridgeId;
            return View();
        }

        // POST: Rorders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FridgeId")] Guid fridgeId, RorderViewModel model)
        {
            if (ModelState.IsValid)
            {
                var order = new Order
                {
                    Id = Guid.NewGuid(),
                    FridgeId = fridgeId,
                    DeliveryInterval = model.DeliveryInterval,
                    IsReccuring = true,
                    StartDate = model.StartDate,
                    NextDeliveryDate = model.StartDate.AddDays(model.DeliveryInterval)
                };
                var orderItem = new OrderItem
                {
                    ItemId = model.ItemId,
                    OrderId = order.Id,
                    Quantity = model.Quantity
                };

                _context.Orders.Add(order);
                _context.OrderItems.Add(orderItem);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(List), new { fridgeId });
            }

            ViewData["Items"] = new SelectList(_context.Items, "Id", "Name");
            ViewBag.FridgeId = fridgeId;
            return View(model);
        }

        // GET: Rorders/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.Include(o => o.OrderItems).FirstAsync(i => i.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            ViewData["Items"] = new SelectList(_context.Items, "Id", "Name");
            return View(new RorderViewModel { Quantity = order.OrderItems.First().Quantity, DeliveryInterval = order.DeliveryInterval });
        }

        // POST: Rorders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, RorderViewModel model)
        {
            if (ModelState.IsValid)
            {
                var order = _context.Orders.Include(o => o.OrderItems).First(o => o.Id == id);
                order.OrderItems.First().Quantity = model.Quantity;
                order.DeliveryInterval = model.DeliveryInterval;
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(List), new { order.FridgeId });
            }

            ViewData["Items"] = new SelectList(_context.Items, "Id", "Name");
            return View(model);
        }

        // GET: Rorders/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Fridge)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Rorders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var order = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(List), new { fridgeId = order.FridgeId });
        }

        private bool OrderExists(Guid id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
