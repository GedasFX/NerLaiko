using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace NerLaiko.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Address { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }

        public virtual IEnumerable<Refrigerator> Refrigerators { get; set; }
        public virtual IEnumerable<Issue> AssignedIssues { get; set; } // For operators. List of assigned issues
    }
}