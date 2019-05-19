using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NerLaiko.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Unit Unit { get; set; }
        [Range(1, int.MaxValue)] public int DeliveryAmount { get; set; }
        public bool IsForSale { get; set; }

        public virtual IEnumerable<OrderItem> OrderItems { get; set; }
    }
}