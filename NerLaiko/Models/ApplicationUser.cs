using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace NerLaiko.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Address { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }

        [ForeignKey(nameof(LoyaltyLevel))]
        public int? LoyaltyLevelId { get; set; }
        public virtual LoyaltyLevel LoyaltyLevel { get; set; }

        public virtual IEnumerable<Refrigerator> Refrigerators { get; set; }
        public virtual IEnumerable<Issue> AssignedIssues { get; set; } // For operators. List of assigned issues
    }
}