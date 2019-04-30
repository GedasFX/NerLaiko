using System.ComponentModel.DataAnnotations.Schema;

namespace NerLaiko.Models
{
    public class ItemDiscount
    {
        [ForeignKey(nameof(Discount))]
        public int DiscountId { get; set; }
        public Discount Discount { get; set; }

        [ForeignKey(nameof(Item))]
        public int ItemId { get; set; }
        public virtual Item Item { get; set; }
    }
}
