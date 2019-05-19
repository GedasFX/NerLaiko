using System;
using System.Collections.Generic;
using System.Linq;
using FridgeAPI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NerLaiko.Data;
using NerLaiko.Models;
using NerLaiko.ViewModels;

namespace NerLaiko.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IFridge _fridge;

        public ProductsController(ApplicationDbContext context, IFridge fridge)
        {
            _context = context;
            _fridge = fridge;
        }

        public IActionResult ProductList(Guid fridgeId)
        {
            var fridge = GetFridge(fridgeId);
            var changes = _fridge.GetActivityLog(fridgeId);

            ApplyFridgeChanges(fridgeId, changes);

            return View(new ProductsViewModel { Fridge = fridge, Recommendations = GenerateRecommendations(fridge).ToArray() });
        }

        public IActionResult ProductForm(Guid fridgeId, string name)
        {
            ViewBag.FridgeId = fridgeId.ToString();
            ViewBag.ItemName = name;
            return View();
        }

        [HttpPost]
        public IActionResult ProductForm(Guid fridgeId, [Bind("Name")] string name, [Bind("Quantity")] int quantity)
        {
            // Check if item exists
            var item = _context.Items.SingleOrDefault(i => i.Name == name);
            if (item == null)
            {
                item = new Item
                {
                    Name = name
                };
                _context.Items.Add(item);
                _context.SaveChanges();
            }

            var product = _context.Produces.Find(item.Id, fridgeId);
            if (product == null)
            {
                product = new Produce
                {
                    FridgeId = fridgeId,
                    ItemId = item.Id
                };
                _context.Produces.Add(product);
                _context.SaveChanges();
            }

            product.Quantity = quantity;
            _context.SaveChanges();

            return RedirectToAction("ProductList", new { fridgeId });
        }

        [HttpPost]
        public IActionResult Delete(Guid fridgeId, int itemId)
        {
            var product = _context.Produces.Find(itemId, fridgeId);
            _context.Produces.Remove(product);
            _context.SaveChanges();

            return RedirectToAction(nameof(ProductList), new { fridgeId });
        }

        private void ApplyFridgeChanges(Guid fridgeId, IEnumerable<KeyValuePair<string, int>> changes)
        {
            foreach (var change in changes)
            {
                var item = _context.Items.SingleOrDefault(i => i.Name == change.Key);
                if (item == null)
                {
                    item = new Item
                    {
                        Name = change.Key,
                        Unit = Unit.Unit
                    };
                    _context.Items.Add(item);
                    _context.SaveChanges();
                }

                var product = _context.Produces.Find(item.Id, fridgeId);
                if (product == null)
                {
                    product = new Produce
                    {
                        FridgeId = fridgeId,
                        ItemId = item.Id
                    };
                    _context.Produces.Add(product);
                    _context.SaveChanges();
                }

                product.Quantity += change.Value;
            }

            _context.SaveChanges();
        }

        private Refrigerator GetFridge(Guid fridgeId)
        {
            var fridge = _context.Refrigerators
                .Include(f => f.Products)
                    .ThenInclude(p => p.Item)
                .Include(f => f.Orders)
                .Single(f => f.Id.Equals(fridgeId));

            return fridge;
        }

        private static IEnumerable<Item> GenerateRecommendations(Refrigerator fridge)
        {
            // For all collection elements
            foreach (var product in fridge.Products)
            {
                // Get produce description
                var item = product.Item;
                var itemId = item.Id;

                // Calculate the ratio between devilery unit and remainder
                if (product.Quantity == 0)
                    product.Quantity = 1;
                if (item.DeliveryAmount / product.Quantity < 4) // Difference between the two is 4 times or more
                    continue;

                // Check product's reccurence status
                var recurrentOrders = fridge.Orders
                    .Where(o => o.OrderItems
                        .Any(oi => oi.ItemId == itemId))
                    .ToArray();

                if (recurrentOrders.Length > 0)
                {
                    // Check next delivery date
                    var nextDate = recurrentOrders.OrderBy(o => o.NextDeliveryDate - DateTime.Now).FirstOrDefault(o => o.IsReccuring);
                    if (nextDate != null && (nextDate.NextDeliveryDate - DateTime.Now).TotalDays > 2)
                        continue;
                }

                // if all above fails, recommend
                yield return product.Item;
            }
        }
    }
}