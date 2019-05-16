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
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var items = _context.Items.Where(i => i.IsForSale).ToListAsync();
            return View(await items);
        }

        [Authorize]
        public async Task<IActionResult> Order(int? id)
        {
            if (id == null)
                return RedirectToAction(nameof(Index));

            var order = _context.Items.FirstOrDefaultAsync(i => i.Id == id);
            var fridges = (await _context.Users.Include(u => u.Refrigerators).SingleOrDefaultAsync(u => u.Id == User.GetId())).Refrigerators.ToArray();

            return View(new OrderViewModel { Item = await order, Refrigerators = fridges.Select(f => new SelectListItem(f.Location, f.Id.ToString())) });
        }

        [HttpPost]
        [Authorize]
        public async Task<string> Confirm(int? id, OrderViewModel model)
        {
            if (id == null)
                return "Id not found";
            var item = await _context.Items.SingleOrDefaultAsync(i => i.Id == id);
            if (item == null)
                return "Item not found";
            if (!item.IsForSale)
                return "Item is not for sale at the moment";
            if (!ModelState.IsValid)
                return "Invalida data";

            // Item exists. Create an order.
            var order = new Order
            {
                Id = Guid.NewGuid(),
                FridgeId = model.FridgeId,
                IsReccuring = false,
                NextDeliveryDate = DateTime.Today.AddDays(model.NextDeliveryDateOffset),
                StartDate = DateTime.Today
            };
            var orderItem = new OrderItem
            {
                ItemId = (int)id,
                OrderId = order.Id,
                Quantity = model.Quantity
            };

            _context.Orders.Add(order);
            _context.OrderItems.Add(orderItem);

            return await _context.SaveChangesAsync() > 0
                ? "Order successful"
                : "Internal error. Try again";
        }
    }
}