using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace api.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? PhoneNumber { get; set; }
        
        [Required]
        public string FirstName { get; set; }

        public string? LastName { get; set; }
        
        [Required]
        public string PreferredLanguage { get; set; } = "English";

        [Required]
        public string PreferredCurrency { get; set; } = "INR";

        // Navigation property for optional profile image
        public UserProfileImage? ProfileImage { get; set; }

    }
}