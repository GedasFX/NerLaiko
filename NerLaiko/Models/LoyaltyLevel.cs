using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NerLaiko.Models
{
    public class LoyaltyLevel
    {
        [Key] public int Id { get; set; }
        public int Years { get; set; }
        public decimal Coefficient { get; set; }

        public virtual IEnumerable<ApplicationUser> Users { get; set; }
    }
}
