using System;
using System.ComponentModel.DataAnnotations;

namespace NerLaiko.Models
{
    public class Refrigerator
    {
        [Key]
        public Guid Id { get; set; }
        public string Location { get; set; }
        public FridgeState State { get; set; }
    }
}
