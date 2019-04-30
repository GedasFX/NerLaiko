using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NerLaiko.Models
{
    public class Issue
    {
        [Key]
        public Guid Id { get; set; }

        public string Description { get; set; }
        public string OperatorComment { get; set; }

        [ForeignKey(nameof(Fridge))]
        public Guid FridgeId { get; set; }
        public virtual Refrigerator Fridge { get; set; }
    }
}
