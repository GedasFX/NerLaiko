using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NerLaiko.Models
{
    public class Invoice
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string HtmlContents { get; set; }

        [ForeignKey(nameof(Fridge))]
        public Guid FridgeId { get; set; }
        public virtual Refrigerator Fridge { get; set; }
    }
}
