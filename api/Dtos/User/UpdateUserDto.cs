using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.User
{
    public class UpdateUserDto
    {
        public string? UserName { get; set; }

        [MaxLength(50, ErrorMessage = "FirstName can not exceed 50 characters")]
        public string? FirstName { get; set; }

        [MaxLength(50, ErrorMessage = "LastName can not exceed 50 characters")]
        public string? LastName { get; set; }

        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be exactly 10 digits.")]
        public string? PhoneNumber { get; set; }
        public string? PreferredCurrency { get; set; }
        public string? PreferredLanguage { get; set; }
        public string? OldPassWord { get; set; }

        public string? NewPassWord { get; set; }
        public IFormFile? ProfileImage { get; set; } //for image upload
    }
}