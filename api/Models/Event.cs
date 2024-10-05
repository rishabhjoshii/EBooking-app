using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string EventName { get; set; } = string.Empty;

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public TimeSpan Timing { get; set; }
        public string Venue { get; set; } = "TBD";
        public string Description { get; set; } = string.Empty;
        public int TicketPrice { get; set; }
        public int TotalTickets { get; set; }
        public int BookedTickets { get; set; }
        public DateTime CreatedAt { get; set; }

        // Foreign Key
        [Required]
        public int CategoryId { get; set; }

        [Required]
        public string ApplicationUserId { get; set; } // foreign key  // represents who created the event 

        

        //navigation property
        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

        [ForeignKey("CategoryId")]
        public virtual EventCategory Category { get; set; }

        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser ApplicationUser { get; set; } // navigation property
    }
}