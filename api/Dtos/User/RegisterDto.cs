using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Account
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "FirstName is required")]
        [MaxLength(50, ErrorMessage = "FirstName can not exceed 50 characters")]
        public string FirstName { get; set; }

        [MaxLength(50, ErrorMessage = "LastName can not exceed 50 characters")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be exactly 10 digits.")]
        public string? PhoneNumber { get; set; }

        [Required]
        public string PreferredLanguage { get; set; }

        [Required]
        public string PreferredCurrency { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        
        
    }
}