using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Booking
{
    public class CreateBookingDto
    {
        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be exactly 10 digits.")]
        public long PhoneNumber { get; set; }

        [Required]
        public int EventId { get; set; } 
        public int NoOfTickets { get; set; } = 0;
        public int PricePaid { get; set; } = 0;
    }
}