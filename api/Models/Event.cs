using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Event
    {
        //primary key
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
        public int CategoryId { get; set; }

         public string ApplicationUserId { get; set; } // foreign key  // represents who created the event 

        public ApplicationUser ApplicationUser { get; set; } // navigation property

        //navigation property
        public List<Booking> Bookings{ get; set; } = new List<Booking>();
        public EventCategory Category { get; set; }
    }
}