using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NerLaiko.Models
{
    public class OrderItem
    {
        public int Quantity { get; set; }

        [Key, ForeignKey(nameof(Order))]
        public Guid OrderId { get; set; }
        public Order Order { get; set; }

        [Key, ForeignKey(nameof(Item))]
        public int ItemId { get; set; }
        public Item Item { get; set; }
    }
}
