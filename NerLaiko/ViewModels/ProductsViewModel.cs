using System.Collections.Generic;
using NerLaiko.Models;

namespace NerLaiko.ViewModels
{
    public class ProductsViewModel
    {
        public Refrigerator Fridge { get; set; }
        public IEnumerable<Item> Recommendations { get; set; }
    }
}