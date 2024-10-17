using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace api.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? PhoneNumber { get; set; }

        // Navigation property for optional profile image
        public UserProfileImage? ProfileImage { get; set; }

    }
}