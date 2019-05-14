using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NerLaiko.Models
{
    public class Refrigerator
    {
        [Key]
        public Guid Id { get; set; }
        
        public string Location { get; set; }
        public FridgeState State { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public virtual IEnumerable<Invoice> Invoices { get; set; }
        public virtual IEnumerable<Issue> Issues { get; set; }
        public virtual IEnumerable<Order> Orders { get; set; }
        public virtual IEnumerable<Produce> Products { get; set; } // Fridge contents
    }
}
