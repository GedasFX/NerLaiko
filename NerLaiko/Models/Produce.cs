using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NerLaiko.Models
{
    public class Produce
    {
        public int Quantity { get; set; }
        public int DeliveredAmount { get; set; }

        [Key, ForeignKey(nameof(Fridge))]
        public Guid FridgeId { get; set; }
        public virtual Refrigerator Fridge { get; set; }

        [Key, ForeignKey(nameof(Item))]
        public int ItemId { get; set; }
        public virtual Item Item { get; set; }
    }
}
