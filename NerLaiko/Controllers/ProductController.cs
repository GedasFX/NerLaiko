using System;
using System.Collections.Generic;
using System.Linq;
using FridgeAPI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NerLaiko.Data;
using NerLaiko.Models;

namespace NerLaiko.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IFridge _fridge;

        public ProductController(ApplicationDbContext context, IFridge fridge)
        {
            _context = context;
            _fridge = fridge;
        }

        public IActionResult Index()
        {
            return View();
        }

        private IEnumerable<Item> GenerateRecommendations(Guid fridgeId)
        {
            var fridge = _context.Refrigerators
                .Include(f => f.Products)
                    .ThenInclude(p => p.Item)
                .Include(f => f.Orders)
                .Single(f => f.Id.Equals(fridgeId));

            return GenerateRecommendations(fridge);
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