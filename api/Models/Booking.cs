using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Booking
    {
        //primary key
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be exactly 10 digits.")]
        public long PhoneNumber { get; set; }

        public string ApplicationUserId { get; set; } // foreign key

        public int EventId { get; set; } //foreign key
        public DateTime BookedAt { get; set; }
        public int NoOfTickets { get; set; }
        public int PricePaid { get; set; }

        //navigation properties
        public Event Event { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

    }
}