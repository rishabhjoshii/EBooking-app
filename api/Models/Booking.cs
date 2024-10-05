using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be exactly 10 digits.")]
        public long PhoneNumber { get; set; }

        [Required]
        public string ApplicationUserId { get; set; } // foreign key

        [Required]
        public int EventId { get; set; } //foreign key
        public DateTime BookedAt { get; set; }
        public int NoOfTickets { get; set; }
        public int PricePaid { get; set; }

        //navigation properties
        [ForeignKey("EventId")]
        public virtual Event Event { get; set; }

        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}