using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NerLaiko.Models
{
    public class OrderItem
    {
        public int Quantity { get; set; }

        [ForeignKey(nameof(Order))]
        public Guid OrderId { get; set; }
        public virtual Order Order { get; set; }

        [ForeignKey(nameof(Item))]
        public int ItemId { get; set; }
        public virtual Item Item { get; set; }
    }
}
