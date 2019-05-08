using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using NerLaiko.Models;

namespace NerLaiko.ViewModels
{
    public class OrderViewModel
    {
        public Item Item { get; set; }
        public IEnumerable<SelectListItem> Refrigerators { get; set; }

        // POST data
        [Required]
        public int NextDeliveryDateOffset { get; set; }

        [Required]
        public Guid FridgeId { get; set; }

        [Required, Range(1, 72)]
        public int Quantity { get; set; }
    }
}
