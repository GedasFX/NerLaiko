using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NerLaiko.Models
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; }

        public bool IsReccuring { get; set; }
        [DataType(DataType.Date)] public DateTime StartDate { get; set; }
        [DataType(DataType.Date)] public DateTime NextDeliveryDate { get; set; }
        public int DeliveryInterval { get; set; } // In days

        [ForeignKey(nameof(Fridge))]
        public Guid FridgeId { get; set; }
        public Refrigerator Fridge { get; set; }

        public virtual IEnumerable<OrderItem> OrderItems { get; set; }
    }
}