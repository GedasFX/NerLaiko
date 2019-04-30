using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace NerLaiko.Models
{
    [Authorize(Roles = "Pisi")]
    public class ApplicationUser : IdentityUser
    {
        public string Address { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
    }
}